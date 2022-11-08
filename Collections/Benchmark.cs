using System.Collections;
using BenchmarkDotNet.Attributes;

namespace Collections;

public class BenchmarkLists
{
    private ArrayList _antiqueList = new();
    private List<int> _genericList = new();
    
    [Benchmark]
    public void GenericListAdding()
    {
        for (var i = 0; i < 10_000_000; i++)
        {
            _genericList.Add(Random.Shared.Next(0, 100));
        }
    }

    [Benchmark]
    public int GenericListReading()
    {
        var temp = 0;
        foreach (var i in _genericList) {
            var num = i;
            temp += num % 2 == 0 
                ? num 
                : num * -1;
        }
        return temp;
    }

    [Benchmark]
    public void AntiqueListAdding()
    {
        for (var i = 0; i < 10_000_000; i++)
        {
            _antiqueList.Add(Random.Shared.Next(0, 100));
        }
    }

    [Benchmark]
    public int AntiqueListReading()
    {
        var temp = 0;
        foreach (var i in _antiqueList) {
            var num = (int)i;
            temp += num % 2 == 0 
                ? num 
                : num * -1;
        }
        return temp;
    }

    [IterationCleanup]
    public void IterationCleanup()
    {
        _genericList = new List<int>();
        _antiqueList = new ArrayList();
    }
}