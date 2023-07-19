
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1
{
    public class FileOperations
    {
        public async Task<string> ReadFile()
        {

            string line;
            string fileName = "cache.txt";

            string output = "";

            using Stream fileStream = await FileSystem.Current.OpenAppPackageFileAsync(fileName);
            using StreamReader reader = new StreamReader(fileStream);

            while((line = reader.ReadLine()) != null)
            {
                output += line;
            }

            return output;
        }
    }
}
