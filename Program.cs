using System.Globalization;
using System.Text.Json;
HttpClient client = new HttpClient();

HttpResponseMessage ?responseFirst = null;
HttpResponseMessage ?responseSecond = null;

int currencyAmmount = 0;
string endPointFirst = "";
string endPointSecond = "";

while (true)
{
    Console.WriteLine("Enter the 3-4 letter code for the currency you want to use:");
    string ?readResult = Console.ReadLine();
    
    endPointFirst = readResult;

    responseFirst = await client.GetAsync($"https://cdn.jsdelivr.net/npm/@fawazahmed0/currency-api@2025.5.6/v1/currencies/{endPointFirst}.json");

    if (responseFirst != null && responseFirst.StatusCode.ToString() == "OK")
    {
        break;
    }
    else
    {
        Console.WriteLine(responseFirst.StatusCode.ToString());
    }
}

while(true)
{
    Console.WriteLine("enter the ammount of currency you want to exchange:");
    string? readResult = Console.ReadLine();
    if(int.TryParse(readResult, out currencyAmmount))
    {
        break;
    }
    else
    {
        Console.WriteLine("must be a whole number.          (fuck att göra detta med decimal)");
    }
}

while(true)
{
    Console.WriteLine("Finally enter the currency you want to exchange with:    (must be different than your orignial currency)");
    string? readResult = Console.ReadLine();
    endPointSecond = readResult;

    responseSecond = await client.GetAsync($"https://cdn.jsdelivr.net/npm/@fawazahmed0/currency-api@2025.5.6/v1/currencies/{endPointSecond}.json");

    if(endPointFirst != endPointSecond && responseSecond != null && responseSecond.StatusCode.ToString() == "OK")
    {
        break;
    }
    else
    {
        Console.WriteLine(responseSecond.StatusCode.ToString());
    }

}

string responseBodyFirst = await responseFirst.Content.ReadAsStringAsync();

using JsonDocument docFirst = JsonDocument.Parse(responseBodyFirst);

JsonElement rootFirst = docFirst.RootElement;

string ?data = rootFirst.GetProperty($"{endPointFirst}").GetProperty($"{endPointSecond}").ToString();

decimal converionRate = decimal.Parse(data, CultureInfo.InvariantCulture);

decimal finalNumber = currencyAmmount * converionRate;

Console.WriteLine($"With a conversion rate of: {converionRate}\t your final ammount becomes:\n");
Console.WriteLine(finalNumber);

Console.ReadLine();



