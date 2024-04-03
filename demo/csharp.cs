using System.Globalization;
using System.Xml;

namespace MySuperApp.Demo123;

static void Main(string[] args)
{
    using var client = new HttpClient();
    client.BaseAddress = new Uri(url);
    // Add an Accept header for JSON format.
    client.DefaultRequestHeaders.Accept.Add(
       new MediaTypeWithQualityHeaderValue("application/json"));
    // Get data response
    var response = client.GetAsync(urlParameters).Result;
    if (response.IsSuccessStatusCode)
    {
        // Parse the response body
        var dataObjects = response.Content
                       .ReadAsAsync<IEnumerable<DataObject>>().Result;
        foreach (var d in dataObjects)
        {
            Console.WriteLine("{0}", d.Name);
        }
    }
    else
    {
        Console.WriteLine("{0} ({1})", (int)response.StatusCode,
                      response.ReasonPhrase);
    }
}
