﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Security.AccessControl;
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
        public LogCollector(string path)
        {
            this.path = path;
            line = 0;
            creationDate = File.GetCreationTime(path);
        }

        /// <summary>
        /// Возвращает полный список логов с момента последнего чтения
        /// </summary>
        /// <returns></returns>
        public List<KeyValuePair<DateTime, string>> GetLastRows()
        {
            List<KeyValuePair<DateTime, string>> lastRows;
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
        List<KeyValuePair<DateTime, string>> ReturnRows (string currentPath)
        {
            Regex reg = new  Regex(@"\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2},\d{3}");
            DateTime rowDate = DateTime.Now;
            string rowLog ="";
            List<KeyValuePair<DateTime, string>> lastRows = new List<KeyValuePair<DateTime, string>>();
            var rows = this.Read(line);
            foreach (var row in rows)
                {
                    if (reg.IsMatch(row))
                    {
                    lastRows.Add(new KeyValuePair<DateTime, string>(rowDate, rowLog));
                    rowDate = Convert.ToDateTime(reg.Match(row).ToString().Replace(',', '.'));
                        rowLog = row;
                    }
                    else if (row != "") rowLog += '\n' + row;
                }
            if (lastRows.Any()) lastRows.Remove(lastRows[0]);
            if (rowLog != "")
            {
                lastRows.Add(new KeyValuePair<DateTime, string>(rowDate, rowLog));
                line += rows.Count();
            }
            return lastRows;
        }

        private IEnumerable<string> Read(int lineNumber)
        {
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var sr = new StreamReader(fs, Encoding.Default))
            {
                int index = 0;
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (index < lineNumber)
                    {
                        index++;
                        continue;
                    }

                    yield return line;
                }
            }
        }
    }
}