using IsBanken.Buisness.Interfaces;
using System.Collections.Generic;
using System.IO;
using IsBanken.Buisness.Models;
using System;
using System.Linq;
using System.Globalization;

namespace IsBanken.Buisness.Infrastructure
{
    public class FileHandler : IFileHandler
    {
        private readonly string _path;
        private readonly string _bankDataStorageFolder = @"..\IsBanken.Buisness\Files\BankDataStorage\";
        private readonly string _dateFormat = "yyyy-MM-dd_hh-mm-ss.fff";

        public FileHandler()
        {
            _path = Directory.GetFiles(_bankDataStorageFolder, "*.txt")
                                    .Select(Path.GetFileNameWithoutExtension)
                                    .Select(f => DateTime.ParseExact(f, _dateFormat, CultureInfo.GetCultureInfo("sv-SE")))
                                    .OrderByDescending(d => d)
                                    .FirstOrDefault()
                                    .ToString(_dateFormat) + ".txt";
        }

        public List<string> ReadFile()
        {
            var fileLines = new List<string>();
            using (var sr = new StreamReader(_bankDataStorageFolder + _path))
            {
                while (sr.ReadLine() != null)
                {
                    fileLines.Add(sr.ReadLine());
                }
            }

            return fileLines;
        }

        public void SaveFile(List<Account> accounts, List<Customer> customers)
        {
            var savePath = $"{_bankDataStorageFolder}{DateTime.Now.ToString(_dateFormat, CultureInfo.GetCultureInfo("sv-SE"))}.txt";
        }
    }
}
