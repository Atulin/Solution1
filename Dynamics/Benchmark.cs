using System.Text.Json;
using BenchmarkDotNet.Attributes;

namespace Dynamics;

[MemoryDiagnoser]
public class Benchmark
{
    private const string Json = """
            {
                "Name": "Bob",
                "Age": 43,
                "ForkliftCertified": true,
                "Address": {
                    "Street": "Daisy Boulevard",
                    "Number": 23
                }
            }
            """;

    [Benchmark]
    public string BenchDynamic()
    {
        var person = JsonSerializer.Deserialize<dynamic>(Json);
        
        if (person is null) return "null";

        var p = (JsonElement)person;

        var gotName = p.TryGetProperty("Name", out var name);
        var gotForkliftCertification = p.TryGetProperty("ForkliftCertified", out var forkliftCertified);

        var gotAddress = p.TryGetProperty("Address", out var address);

        var gotStreet = address.TryGetProperty("Street", out var street);
        var gotNumber = address.TryGetProperty("Number", out var number);

        if (!gotName || !gotForkliftCertification || !gotAddress || !gotStreet || !gotNumber) return "That's why you don't do that";

        return $"{name.GetString()} {(forkliftCertified.GetBoolean() ? "is" : "is not")} forklift certified and lives at {street.GetString()} {number.GetInt32()}";
        
    }

    [Benchmark]
    public string BenchDictionary()
    {
        var person = JsonSerializer.Deserialize<Dictionary<string, object>>(Json);
        
        if (person is null) return "null";

        var address = (JsonElement)person["Address"];

        var gotStreet = address.TryGetProperty("Street", out var street);
        var gotNumber = address.TryGetProperty("Number", out var number);

        if (!gotStreet || !gotNumber) return "Couldn't get street or number";

        return $"{person["Name"]} {(((JsonElement)person["ForkliftCertified"]).GetBoolean() ? "is" : "is not")} forklift certified and lives at {street.GetString()} {number.GetInt32()}";
        
    }

    [Benchmark]
    public string BenchClass()
    {
        var person = JsonSerializer.Deserialize<Person>(Json);
        
        if (person is null) return "null";
        
        return $"{person.Name} {(person.ForkliftCertified ? "is" : "is not")} forklift certified and lives at {person.Address.Street} {person.Address.Number}";

    }

    public class Person
    {
        public required string Name { get; init; }
        public required int Age { get; init; }
        public required bool ForkliftCertified { get; init; }
        public required Address Address { get; init; }
    }

    public class Address
    {
        public required string Street { get; init; }
        public required int Number { get; init; }
    }
}