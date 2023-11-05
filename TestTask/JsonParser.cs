using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask
{
    internal class JsonParser
    {
        private readonly string _nameFeaturesNode = "features";
        private readonly string _namePropertiesNode = "properties";
        private readonly string _nameCompanyMetaDataNode = "CompanyMetaData";
        private readonly string _nameAddressNode = "address";
        private readonly string _nameHoursNode = "Hours";
        private readonly string _namePhonesNode = "Phones";
        private readonly string _nameFormattedNode = "formatted";
        private readonly string _nameTextNode = "text";

        private List<Company> companies = new List<Company>();

        public List<Company> Parse(string json)
        {
            JObject jsonObject = JObject.Parse(json);

            var features = jsonObject[_nameFeaturesNode];

            foreach (var feature in features)
            {
                JToken properties = feature[_namePropertiesNode];
                JToken companyMetaData = properties[_nameCompanyMetaDataNode];
                JToken address = companyMetaData[_nameAddressNode];
                JToken workingTime = companyMetaData[_nameHoursNode];

                List<string> phonesList = new List<string>();

                JToken phones = companyMetaData[_namePhonesNode];
                foreach (var phone in phones)
                {
                    phonesList.Add(phone[_nameFormattedNode].ToString());
                }

                companies.Add(new Company(address.ToString(), workingTime[_nameTextNode].ToString(), phonesList));
            }

            return companies;
        }
    }
}
