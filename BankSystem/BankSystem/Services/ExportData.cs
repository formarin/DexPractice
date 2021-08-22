using Newtonsoft.Json;
using System;
using System.IO;

namespace Bank_Sistem.Services
{
    public class ExportData
    {
        public static string path = Path.Combine("C:", "Users", "Irina", "source",
          "repos", "DexPractice", "FileFolder");

        DirectoryInfo directoryInfo = new DirectoryInfo(path);

        public void AddDataToFile<T>(T obj)
        {
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }

            var myType = obj.GetType();
            var properties = myType.GetProperties();

            var str = "";
            foreach(var property in properties)
            {
                str += $"{property.Name}: {property.GetValue(obj)},  ";
            }

            using (var streamWriter = new FileStream($"{path}\\Report.txt", FileMode.Append))
            {
                var data = JsonConvert.SerializeObject(str);
                byte[] dataArray = System.Text.Encoding.Default.GetBytes(data);
                streamWriter.Write(dataArray);
            }
        }
    }
}
