using IsBanken.Buisness.Interfaces;
using System.Collections.Generic;
using System.IO;
using IsBanken.Buisness.Models;

namespace IsBanken.Buisness.Infrastructure
{
    public class FileHandler : IFileHandler
    {
        private readonly string path =
            @"C:\Users\patricstromberg\source\repos\IsBanken\IsBanken.Buisness\Files\bankdata.txt";
        public List<string> ReadFile()
        {
            var fileLines = new List<string>();
            using (var sr = new StreamReader(path))
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
            throw new System.NotImplementedException();
        }
    }
}
