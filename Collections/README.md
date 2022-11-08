# Benchmarking collections

PC specs:
* AMD Ryzen 5 1600
* 16 GB RAM
* SATA3 SSD

## Lists benchmark

```
BenchmarkDotNet=v0.13.2, OS=Windows 10 (10.0.19045.2130)
AMD Ryzen 5 1600, 1 CPU, 12 logical and 6 physical cores
.NET SDK=6.0.305
  [Host]     : .NET 6.0.10 (6.0.1022.47605), X64 RyuJIT AVX2
  DefaultJob : .NET 6.0.10 (6.0.1022.47605), X64 RyuJIT AVX2


|             Method |    Count |           Mean |          Error |         StdDev |       Gen0 |       Gen1 |   Allocated |
|------------------- |--------- |---------------:|---------------:|---------------:|-----------:|-----------:|------------:|
|  GenericListAdding |     1000 |      13.976 us |      0.2787 us |      0.5818 us |     1.9989 |          - |      8424 B |
| GenericListReading |     1000 |       4.449 us |      0.0372 us |      0.0348 us |          - |          - |           - |
|  AntiqueListAdding |     1000 |      22.892 us |      0.4226 us |      0.9879 us |     9.7046 |          - |     40600 B |
| AntiqueListReading |     1000 |      12.115 us |      0.1759 us |      0.1469 us |          - |          - |        48 B |
|  GenericListAdding | 10000000 | 161,937.579 us |  2,393.8088 us |  2,122.0491 us |          - |          - | 134219194 B |
| GenericListReading | 10000000 |  50,674.883 us |    196.7192 us |    174.3864 us |          - |          - |        48 B |
|  AntiqueListAdding | 10000000 | 829,578.581 us | 16,437.8813 us | 30,468.6669 us | 38000.0000 | 13000.0000 | 508436488 B |
| AntiqueListReading | 10000000 | 123,708.757 us |  2,445.1524 us |  2,616.2857 us |          - |          - |       270 B |

// * Hints *
Outliers
  BenchmarkLists.GenericListAdding: Default  -> 3 outliers were removed (16.38 us..17.72 us)
  BenchmarkLists.GenericListReading: Default -> 1 outlier  was  detected (4.36 us)
  BenchmarkLists.AntiqueListAdding: Default  -> 10 outliers were removed (26.56 us..39.29 us)
  BenchmarkLists.AntiqueListReading: Default -> 2 outliers were removed (12.71 us, 13.06 us)
  BenchmarkLists.GenericListAdding: Default  -> 1 outlier  was  removed (171.85 ms)
  BenchmarkLists.GenericListReading: Default -> 1 outlier  was  removed (51.57 ms)
  BenchmarkLists.AntiqueListAdding: Default  -> 1 outlier  was  removed (966.43 ms)

// * Legends *
  Count     : Value of the 'Count' parameter
  Mean      : Arithmetic mean of all measurements
  Error     : Half of 99.9% confidence interval
  StdDev    : Standard deviation of all measurements
  Gen0      : GC Generation 0 collects per 1000 operations
  Gen1      : GC Generation 1 collects per 1000 operations
  Allocated : Allocated memory per single operation (managed only, inclusive, 1KB = 1024B)

```