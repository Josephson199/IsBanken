using IsBanken.Buisness.Interfaces;
using System.Collections.Generic;
using System.IO;
using IsBanken.Buisness.Models;

namespace IsBanken.Buisness.Infrastructure
{
    public class FileHandler : IFileHandler
    {
        public List<string> ReadFile()
        {
            var fileLines = new List<string>();
            using (var sr = new StreamReader(@"C:\Users\josep\source\repos\IsBanken\IsBanken.Buisness\Files\bankdata.txt"))
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
