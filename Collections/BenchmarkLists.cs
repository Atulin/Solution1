using System.Collections;
using BenchmarkDotNet.Attributes;

namespace Collections;

[MemoryDiagnoser]
public class BenchmarkLists
{
    [Params(1_000, 10_000_000)]
    public int Count { get; set;  }

    [GlobalSetup]
    public void GlobalSetup()
    {
        for (var i = 0; i < Count; i++)
        {
            var rnd = Random.Shared.Next(0, 1_000);
            _antiqueListRead.Add(rnd);
            _genericListRead.Add(rnd);
        }
    }

    private readonly ArrayList _antiqueListRead = new();
    private readonly List<int> _genericListRead = new();
    
    [Benchmark]
    public List<int> GenericListAdding()
    {
        var list = new List<int>();
        for (var i = 0; i < Count; i++)
        {
            list.Add(Random.Shared.Next(0, 1_000));
        }
        return list;
    }

    [Benchmark]
    public int GenericListReading()
    {
        var temp = 0;
        foreach (var i in _genericListRead) {
            var num = i;
            temp += num % 2 == 0 
                ? num 
                : num * -1;
        }
        return temp;
    }

    [Benchmark]
    public ArrayList AntiqueListAdding()
    {
        var list = new ArrayList();
        for (var i = 0; i < Count; i++)
        {
            list.Add(Random.Shared.Next(0, 1_000));
        }
        return list;
    }

    [Benchmark]
    public int AntiqueListReading()
    {
        var temp = 0;
        foreach (var i in _antiqueListRead) {
            var num = (int)i;
            temp += num % 2 == 0 
                ? num 
                : num * -1;
        }
        return temp;
    }
}