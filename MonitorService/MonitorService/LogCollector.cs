using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace MonitorService
{
    class LogCollector
    {
        int line;
        string path;
        DateTime creationDate;
        int logsInterval;
        string nameOfExtension;


        public LogCollector( string nameOfExtension, string path, int logsInterval)
        {
            this.nameOfExtension = nameOfExtension;
            this.path = path;
            this.logsInterval = logsInterval;
            line = 0;
            creationDate = File.GetCreationTime(path);
        }

        /// <summary>
        /// Возвращает полный список логов с момента последнего чтения
        /// </summary>
        /// <returns></returns>
        List<KeyValuePair<DateTime, string>> GetLastRows()
        {
            List<KeyValuePair<DateTime, string>> lastRows = new List<KeyValuePair<DateTime, string>>();
            lastRows = CheckingTheCreationOfANewLog();
            if (lastRows != null) line = 0;
            else lastRows = new List<KeyValuePair<DateTime, string>>();
            //берет строки из текущего файла логов
            List<KeyValuePair<DateTime, string>> rows = returnRows(path);
            //добавляем логи из текущего файла к логам из прошлого файла
            if (rows != null ) lastRows.AddRange(rows);
            return lastRows;
        }

        /// <summary>
        /// Возвращает список логов из файла extensions.log.1 в случае, если файл extensions.log был пересоздан
        /// </summary>
        /// <returns>Возвращает:
        /// пустой список логов, если на момент обновления файла новых логов с момента последнего чтения не появилось
        /// непустой список
        /// null, если файл не был обновлен</returns>
        List<KeyValuePair<DateTime, string>> CheckingTheCreationOfANewLog() => (creationDate != File.GetCreationTime(path)) ? returnRows(path + ".1") : null;

        /// <summary>
        /// Возвращает список логов из указанного файла с момента последнего чтения
        /// </summary>
        /// <param name="currentPath">Путь к файлу</param>
        /// <returns></returns>
        List<KeyValuePair<DateTime, string>> returnRows (string currentPath)
        {
            List<KeyValuePair<DateTime, string>> lastRows = new List<KeyValuePair<DateTime, string>>();
            try
            {
                string row = File.ReadLines(currentPath).Skip(line++).First();
                DateTime rowDate = Convert.ToDateTime(Regex.Match(row, @"\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2},\d{3}").ToString().Replace(',', '.'));
                string rowLog = Regex.Match(row, @"\|(\s\S*)*").ToString();
                try
                {
                    while (true)
                    {
                        row = File.ReadLines(currentPath).Skip(line).First();
                        line++;
                        if (Regex.IsMatch(row, @"\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2},\d{3}"))
                        {
                            Console.WriteLine(rowLog);
                            lastRows.Add(new KeyValuePair<DateTime, string>(rowDate, rowLog));
                            rowDate = Convert.ToDateTime(Regex.Match(row, @"\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2},\d{3}").ToString().Replace(',', '.'));
                            rowLog = Regex.Match(row, @"\|(\s\S*)*").ToString();
                        }
                        else
                        {
                            if (row != "") rowLog += '\n' + row;
                        }
                    }
                }
                catch { }
                line++;
                Console.WriteLine(rowLog);
                lastRows.Add(new KeyValuePair<DateTime, string>(rowDate, rowLog));
            }
            catch { }
            return lastRows;
        }

        /// <summary>
        /// Считывает логи через заданный logInterval
        /// </summary>
        /// в дальнейшем будет доработан, чтобы отправлять логи на сервер
        public void GetLastRowOnSceduller ()
        {
            int i = 0;
            while (i<5)
            {
                GetLastRows();
                Thread.Sleep(logsInterval);
                i++;
            }
        }
    }
}