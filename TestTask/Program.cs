using Newtonsoft.Json.Linq;
using System.Text;
using TestTask;

internal class Program
{
    private static void Main(string[] args)
    {
        string nameXmlDocument = "CenterInvestList.xml";
        string url = @"https://search-maps.yandex.ru/v1/?text=ПАО%20КБ%20“Центр-инвест”&type=biz&lang=ru_RU&apikey=694d9a1c-f507-4f37-95e2-7675448e9ab2";
        string filePath = "result.txt";

        File.Delete(filePath);

        XMLParser xmlParser = new XMLParser();
        List<Company> companiesFromXml = xmlParser.Parse(nameXmlDocument);

        Request request = new Request(url);
        string response = request.GetResponse();

        if(response == string.Empty)
        {
            return;
        }

        JsonParser jsonParser = new JsonParser();
        List<Company> companiesFromJson = jsonParser.Parse(response);

        int indexCompanyXml, indexCompanyJson;
        Searcher searcher = new Searcher();

        searcher.SearchMatchesByAdress(companiesFromXml, companiesFromJson, out indexCompanyXml, out indexCompanyJson);

        if(!(indexCompanyXml >= 0 && indexCompanyJson >= 0))
        {
            return;
        }

        Comparer comparer = new Comparer(companiesFromXml[indexCompanyXml], companiesFromJson[indexCompanyJson]);
        StringBuilder resultComparePhones = comparer.ComparePhones();
        StringBuilder resultCompareWorkingTime = comparer.CompareWorkingTime();

        FileWriter fileWriter = new FileWriter(filePath);
        fileWriter.WritePhonesResult(resultComparePhones);
        fileWriter.WriteWorkingTimeResult(resultCompareWorkingTime);

        EmailSender emailSender = new EmailSender();
        string textMessage = File.ReadAllText(filePath);
        emailSender.Send(textMessage);
        Console.WriteLine(textMessage);
    }
}


