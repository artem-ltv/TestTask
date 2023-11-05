using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    internal class Searcher
    {
        public void SearchMatchesByAdress(List<Company> companiesFromXml, List<Company> companiesFromJson, out int indexCompanyFromXml, out int indexCompanyFromJson)
        {
            for(int i = 0; i < companiesFromXml.Count; i++)
            {
                for(int j = 0; j < companiesFromJson.Count; j++)
                {
                    string addressXml = companiesFromXml[i].Address.Replace("д.", "").ToLower();
                    string addressJson = companiesFromJson[j].Address.Replace("улица ", "").ToLower();

                    if (addressXml.Contains(addressJson) || addressJson.Contains(addressXml))
                    {
                        Console.WriteLine("Есть совпадения");

                        indexCompanyFromXml = i;
                        indexCompanyFromJson = j;

                        return;
                    }
                }
            }

            indexCompanyFromXml = int.MinValue;
            indexCompanyFromJson = int.MinValue;
        }
    }
}
