using System.Globalization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;
using ProtoBuf;

Person data = new ("Bob", "Bobbit", DateTime.Now);


WithBinary();
await WithJson();
WithXml();
WithProto();


void WithBinary()
{
    using var fsw = new FileStream("withbinary.dat", FileMode.OpenOrCreate);
    var formatter = new BinaryFormatter();

    // Serialize
    try
    {
#pragma warning disable SYSLIB0011
        formatter.Serialize(fsw, data);
#pragma warning restore SYSLIB0011
    }
    catch (Exception e)
    {
        Console.WriteLine("Can't serialize");
        Console.WriteLine(e.Message);
    }
    finally
    {
        fsw.Close();
    }
    
    // Deserialize
    using var fso = new FileStream("withbinary.dat", FileMode.Open);
    try
    {
#pragma warning disable SYSLIB0011
        var person = (Person)formatter.Deserialize(fso);
        Console.WriteLine(person);
#pragma warning restore SYSLIB0011
    }
    catch (Exception e)
    {
        Console.WriteLine("Can't deserialize");
        Console.WriteLine(e.Message);
    }
    finally
    {
        fso.Close();
    }
}


void WithXml()
{
    // Serialize
    using var fsw = new FileStream("withxml.xml", FileMode.OpenOrCreate);
    var serializer = new XmlSerializer(typeof(PersonXml));
    
    serializer.Serialize(fsw, data.ToXml());
    
    fsw.Close();
    
    // Deserialize
    using var fso = new FileStream("withxml.xml", FileMode.Open);

    var person = (PersonXml?) serializer.Deserialize(fso);
    
    Console.WriteLine(person);
    fso.Close();
}


async Task WithJson()
{
    // Serialize
    await using var fsw = new FileStream("withjson.json", FileMode.OpenOrCreate);
    
    var json = JsonSerializer.Serialize(data);
    
    await fsw.WriteAsync(Encoding.UTF8.GetBytes(json));
    fsw.Close();
    
    // Deserialize
    await using var fso = new FileStream("withjson.json", FileMode.Open);
    using var reader = new StreamReader(fso, Encoding.UTF8);
    var content = await reader.ReadToEndAsync();
    
    var person = JsonSerializer.Deserialize<Person>(content);
    
    Console.WriteLine(person);
    fso.Close();
}


void WithProto()
{
    // Serialize
    using (var filew = File.Create("withproto.bin"))
    {
        Serializer.Serialize(filew, data.ToProto());
    }

    // Deserialize
    using (var fileo = File.OpenRead("withproto.bin"))
    {
        var person = Serializer.Deserialize<PersonProto>(fileo);

        Console.WriteLine(person);
    }
}



[Serializable] // only for binary formatter
public record Person(string Name, string Surname, DateTime Birthday)
{
    public override string ToString() => $"{Name} {Surname} was born on {Birthday.ToString("dd MMMM yyyy", CultureInfo.InvariantCulture)}";
}

public class PersonXml
{
    public required string Name { get; init; }
    public required string Surname { get; init; }
    public required DateTime Birthday { get; init; }
    
    public override string ToString() => $"{Name} {Surname} was born on {Birthday.ToString("dd MMMM yyyy", CultureInfo.InvariantCulture)}";
}

[ProtoContract]
public class PersonProto
{
    [ProtoMember(1)]
    public required string Name { get; init; }
    [ProtoMember(2)]
    public required string Surname { get; init; }
    [ProtoMember(3)]
    public required DateTime Birthday { get; init; }
    
    public override string ToString() => $"{Name} {Surname} was born on {Birthday.ToString("dd MMMM yyyy", CultureInfo.InvariantCulture)}";
}

public static class PersonConverters
{
    public static PersonXml ToXml(this Person p) => new()
    {
        Name = p.Name,
        Surname = p.Surname,
        Birthday = p.Birthday
    };
    
    public static PersonProto ToProto(this Person p) => new()
    {
        Name = p.Name,
        Surname = p.Surname,
        Birthday = p.Birthday
    };
}