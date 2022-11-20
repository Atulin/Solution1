# Dynamics

```
BenchmarkDotNet=v0.13.2, OS=Windows 10 (10.0.19045.2251)
AMD Ryzen 5 1600, 1 CPU, 12 logical and 6 physical cores
.NET SDK=7.0.100
  [Host]     : .NET 7.0.0 (7.0.22.51805), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.0 (7.0.22.51805), X64 RyuJIT AVX2


|          Method |     Mean |     Error |    StdDev |   Gen0 | Allocated |
|---------------- |---------:|----------:|----------:|-------:|----------:|
|    BenchDynamic | 2.274 us | 0.0335 us | 0.0344 us | 0.1755 |     736 B |
| BenchDictionary | 3.000 us | 0.0461 us | 0.0431 us | 0.3777 |    1584 B |
|      BenchClass | 1.538 us | 0.0232 us | 0.0205 us | 0.2060 |     864 B |

// * Hints *
Outliers
  Benchmark.BenchDynamic: Default -> 3 outliers were removed (2.42 us..2.49 us)
  Benchmark.BenchClass: Default   -> 1 outlier  was  removed (1.61 us)

// * Legends *
  Mean      : Arithmetic mean of all measurements
  Error     : Half of 99.9% confidence interval
  StdDev    : Standard deviation of all measurements
  Gen0      : GC Generation 0 collects per 1000 operations
  Allocated : Allocated memory per single operation (managed only, inclusive, 1KB = 1024B)
  1 us      : 1 Microsecond (0.000001 sec)

```
