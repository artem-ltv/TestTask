using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TestTask
{
    internal class XMLParser
    {
        private readonly string _nameCompaniesNode = "companies";
        private readonly string _nameCompanyNode = "company";
        private readonly string _nameAddressNode = "address";
        private readonly string _nameWorkingTimeNode = "working-time";
        private readonly string _namePhoneNode = "phone";
        private readonly string _nameNumberNode = "number";

        private List<Company> companies = new List<Company>();

        public List<Company> Parse(string document)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(document);

            foreach (XmlNode companiesNode in xmlDocument.ChildNodes)
            {
                if (companiesNode.Name == _nameCompaniesNode)
                {
                    foreach (XmlNode companyNode in companiesNode.ChildNodes)
                    {
                        if (companyNode.Name == _nameCompanyNode)
                        {
                            string address = companyNode[_nameAddressNode].InnerText;
                            string workingTime = companyNode[_nameWorkingTimeNode].InnerText;
                            List<string> phones = new List<string>();

                            foreach (XmlNode phoneNode in companyNode.ChildNodes)
                            {
                                if (phoneNode.Name == _namePhoneNode)
                                {
                                    phones.Add(phoneNode[_nameNumberNode].InnerText);
                                }
                            }

                            companies.Add(new Company(address, workingTime, phones));
                        }
                    }
                }
            }

            return companies;
        }
    }
}
