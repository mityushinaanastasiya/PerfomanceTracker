using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorService
{
    class LogCollector
    {
        int numberOfLastRow;
        string path;
        public LogCollector( string name, string path)
        {
            numberOfLastRow = 0;
            this.path = path;
        }
        public List<KeyValuePair<DateTime, string>> GetLastRows()
        {
            List<KeyValuePair<DateTime, string>> lastRows = new List<KeyValuePair<DateTime, string>>();
            try
            {
                string row = File.ReadLines(path).Skip(numberOfLastRow++).First();
                DateTime rowDate = Convert.ToDateTime(Regex.Match(row, @"\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2},\d{3}").ToString().Replace(',', '.'));
                string rowLog = Regex.Match(row, @"\|(\s\S*)*").ToString();
                try
                {
                    while (true)
                    {
                        row = File.ReadLines(path).Skip(numberOfLastRow).First();
                        numberOfLastRow++;
                        if (Regex.IsMatch(row, @"\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2},\d{3}"))
                        {
                            lastRows.Add(new KeyValuePair<DateTime, string>(rowDate, rowLog));
                            rowDate = Convert.ToDateTime(Regex.Match(row, @"\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2},\d{3}").ToString().Replace(',', '.'));
                            rowLog = Regex.Match(row, @"\|(\s\S*)*").ToString();
                        }
                        else
                        {
                            rowLog += '\n' + row;
                        }
                    }
                }
                catch
                {
                }
                numberOfLastRow++;
                lastRows.Add(new KeyValuePair<DateTime, string>(rowDate, rowLog));    
            }
            catch
            {
            }
            return lastRows;
        }
    }
}