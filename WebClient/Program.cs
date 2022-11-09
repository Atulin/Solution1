using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

const string url = "https://api.isevenapi.xyz/api/iseven/";
const int num = 87;
var options = new JsonSerializerOptions {
    PropertyNameCaseInsensitive = true
};


Console.WriteLine(GetStuffWebClient());
Console.WriteLine(await GetStuffWebRequest());
Console.WriteLine(await GetStuffHttpClient());
Console.WriteLine(await GetJsonHttpClient());


Response? GetStuffWebClient()
{
    using var client = new WebClient();
    var res = client.DownloadString($"{url}/{num}/");
    return JsonSerializer.Deserialize<Response>(res, options);
}


async Task<Response?> GetStuffWebRequest()
{
    var client = WebRequest.Create($"{url}/{num}/");
    
    using var res = await client.GetResponseAsync();
    await using var stream = res.GetResponseStream();

    using var sr = new StreamReader(stream, Encoding.UTF8);
    var json = await sr.ReadToEndAsync();
    
    return JsonSerializer.Deserialize<Response>(json, options);
}


async Task<Response?> GetStuffHttpClient()
{
    using var client = new HttpClient();
    var res = await client.GetStringAsync($"{url}/{num}/");
    return JsonSerializer.Deserialize<Response>(res, options);
}


async Task<Response?> GetJsonHttpClient()
{
    using var client = new HttpClient();
    return await client.GetFromJsonAsync<Response>($"{url}/{num}/");
}




internal record Response(bool IsEven, string Ad)
{
    public override string ToString() => $"{(IsEven ? "Even" : "Odd")} (Ad: {Ad})";
}