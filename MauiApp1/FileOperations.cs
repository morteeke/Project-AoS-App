
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1
{
    public static class FileOperations
    {
        public static async Task<string> ReadFile()
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

        public static async void WriteFile(string data)
        {
            string fileName = "cache.txt";

            //string mainDir = FileSystem.Current.AppDataDirectory;
            //string filePath = System.IO.Path.Combine(mainDir, fileName);

            //using FileStream fileStream = System.IO.File.OpenWrite(filePath);
            //using StreamWriter streamWriter = new StreamWriter(fileStream);

            string targetFile = System.IO.Path.Combine(FileSystem.Current.AppDataDirectory, fileName);

            using FileStream fileStream = System.IO.File.OpenWrite(targetFile);
            using StreamWriter writer = new StreamWriter(fileStream);

            await writer.WriteAsync(data);


            //using FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate);
            //using StreamWriter writer = new StreamWriter(fileStream);

            //writer.Write(data);
        }
    }
}
