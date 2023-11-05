using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    internal class FileWriter
    {
        private string _path;

        private readonly string _headerPhoneResult = "1) Результат сравнения номеров телефонов";
        private readonly string _headerWorkingTimeResult = "2) Результат сравнения режимов работы";

        public FileWriter(string path)
        {
            _path = path;
        }

        public void WritePhonesResult(StringBuilder result)
        {
            WriteResult(_headerPhoneResult, result);
        }

        public void WriteWorkingTimeResult(StringBuilder result)
        {
            WriteResult(_headerWorkingTimeResult, result);
        }

        private void WriteResult(string header, StringBuilder result)
        {
            using (FileStream file = new FileStream(_path, FileMode.Append))
            {
                using (StreamWriter writer = new StreamWriter(file))
                {
                    writer.WriteLine($"{header}: {result}");
                }
            }
        }
    }
}
