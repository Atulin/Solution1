using System.Collections;
using BenchmarkDotNet.Attributes;

namespace Collections;

[MemoryDiagnoser]
public class BenchmarkAssocs
{
    [Params(1_000, 10_000_000)] 
    public int Count { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        for (var i = 0; i < Count; i++)
        {
            var rnd = Random.Shared.Next(0, 1_000);
            _hashtable.Add(i, rnd);
            _dictionary.Add(i, rnd);
        }
    }

    private readonly Hashtable _hashtable = new();
    private readonly Dictionary<int, int> _dictionary = new();

    [Benchmark]
    public Dictionary<int, int> DictionaryAdding()
    {
        var dictionary = new Dictionary<int, int>();
        for (var i = 0; i < Count; i++)
        {
            dictionary.Add(i, Random.Shared.Next(0, 1_000));
        }

        return dictionary;
    }

    [Benchmark]
    public int DictionaryReading()
    {
        var temp = 0;
        for (var i = 0; i < _dictionary.Count; i++)
        {
            var num = _dictionary[i];
            temp += num % 2 == 0
                ? num
                : num * -1;
        }

        return temp;
    }

    [Benchmark]
    public Hashtable HashtableAdding()
    {
        var hashtable = new Hashtable();
        for (var i = 0; i < Count; i++)
        {
            hashtable.Add(i, Random.Shared.Next(0, 1_000));
        }

        return hashtable;
    }

    [Benchmark]
    public int HashtableReading()
    {
        var temp = 0;
        for (var i = 0; i < _hashtable.Count; i++)
        {
            var num = (int)_hashtable[i];
            temp += num % 2 == 0
                ? num
                : num * -1;
        }

        return temp;
    }
}