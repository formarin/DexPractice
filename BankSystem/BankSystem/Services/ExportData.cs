using Newtonsoft.Json;
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
            var myType = obj.GetType();
            var properties = myType.GetProperties();

            var text = "";
            foreach (var property in properties)
            {
                text += $"{property.Name}: {property.GetValue(obj)},  ";
            }

            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
            using (var fileStream = new FileStream($"{path}\\Report.txt", FileMode.Append))
            {
                var serText = JsonConvert.SerializeObject(text);
                byte[] dataArray = System.Text.Encoding.Default.GetBytes(serText);
                fileStream.Write(dataArray);
            }
        }
    }
}
