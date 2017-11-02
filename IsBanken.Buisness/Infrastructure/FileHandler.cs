using System.Collections.Generic;
using System.IO;

namespace IsBanken.Buisness.Infrastructure
{
    public class FileHandler
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

      
    }
}
