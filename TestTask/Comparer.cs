using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    internal class Comparer
    {
        private Company _companyFromXml;
        private Company _companyFromJson;

        public Comparer(Company companyFromXml, Company companyFromJson)
        {
            _companyFromXml = companyFromXml;
            _companyFromJson = companyFromJson;
        }

        public StringBuilder ComparePhones()
        {
            StringBuilder result = new StringBuilder();

            IEnumerable<string> phonesSortXml = _companyFromXml.Phones.Select(x => x.Replace("+7", "8"));
            IEnumerable<string> phonesSortJson = _companyFromJson.Phones.Select(x => x.Replace("+7", "8"));

            Console.WriteLine("\n");
            if (phonesSortJson.Count() == phonesSortXml.Count())
            {
                bool isEqual = Enumerable.SequenceEqual(phonesSortJson.OrderBy(e => e), phonesSortXml.OrderBy(e => e));
                if (isEqual)
                {
                    result.Append("Номера телефонов совпадают.");
                }
                else
                {
                    result.Append("Номера телефонов не совпадают.");
                    result.Append(GetPhones(phonesSortXml, phonesSortJson));
                }
            }

            else if (phonesSortJson.Count() > phonesSortXml.Count())
            {
                bool containsAll = phonesSortXml.All(s => phonesSortJson.Contains(s));
                if (containsAll)
                {
                    IEnumerable<string> difference = phonesSortJson.Except(phonesSortXml);
                    result.Append("\nФайл json содержит дополнительные номера телефонов: ");
                    result.Append(String.Join(", ", difference));
                }
                else
                {
                    result.Append("Список номеров телефонов разный, файл json содержит больше номеров");
                    result.Append(GetPhones(phonesSortXml, phonesSortJson));
                }
            }
            else
            {
                bool containsAll = phonesSortJson.All(s => phonesSortXml.Contains(s));
                if (containsAll)
                {
                    IEnumerable<string> difference = phonesSortXml.Except(phonesSortJson);
                    result.Append("Файл xml содержит дополнительные номера: ");
                    result.Append(String.Join(", ", difference));
                }
                else
                {
                    result.Append("Список номеров телефонов разный, файл xml содержит больше номеров");
                    result.Append(GetPhones(phonesSortXml, phonesSortJson));
                }
            }

            return result;
        }

        public StringBuilder CompareWorkingTime()
        {
            StringBuilder result = new StringBuilder();

            string workingTimeCompanyXml = _companyFromXml.WorkingTime;
            string workingTimeCompanyJson = _companyFromJson.WorkingTime;

            string[] workingTimeCompanyXmlSplit = workingTimeCompanyXml.Split(" ");
            string[] workingTimeCompanyJsonSplit = workingTimeCompanyJson.Split(" ");

            if (workingTimeCompanyXmlSplit.Length == workingTimeCompanyJsonSplit.Length)
            {
                string timeFromJson = string.Empty, timeFromXml = string.Empty;

                foreach (string item in workingTimeCompanyXmlSplit)
                {
                    if (item.Contains(':'))
                    {
                        if (item[0] == '0')
                        {
                            string itemStandart = item.TrimStart('0').Replace("–", "-");
                            timeFromXml = itemStandart;
                        }
                        else
                        {
                            timeFromXml = item;
                        }
                    }
                }

                foreach (string item in workingTimeCompanyJsonSplit)
                {
                    if (item.Contains(':'))
                    {
                        if (item[0] == '0')
                        {
                            string itemStandart = item.TrimStart('0').Replace("–", "-");
                            timeFromJson = itemStandart;
                        }
                        else
                        {
                            timeFromJson = item;
                        }
                    }
                }

                if (timeFromJson.Trim() != timeFromXml.Trim())
                {
                    result.Append($"Неверные часы работы, в файле json: {timeFromJson}, в файле xml: {timeFromXml}");
                }
                else
                {
                    result.Append("Режимы работы в файле json и в xml соответствуют");
                }
            }
            else
            {
                result.Append($"Режимы работы в файле json и в xml не соответствуют.\nРежим работы в файле json: {workingTimeCompanyJson};\nРежим работы в файле xml: {workingTimeCompanyXml};");
            }

            return result;
        }

        private StringBuilder GetPhones(IEnumerable<string> phonesFromXml, IEnumerable<string> phonesFromJson)
        {
            StringBuilder result = new StringBuilder();

            result.Append("\nНомера телефонов в файле xml: ");
            foreach (string phone in phonesFromXml)
            {
                result.Append($"\n{phone}");
            }
            result.Append("\nНомера телефонов в файле json: ");
            foreach (string phone in phonesFromJson)
            {
                result.Append($"\n{phone}");
            }

            return result;
        }
    }
}