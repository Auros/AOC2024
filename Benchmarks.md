```
=== Day01 ===

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4460/23H2/2023Update/SunValley3)
AMD Ryzen 7 7800X3D, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  DefaultJob : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


| Method | Mean      | Error    | StdDev   | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|------- |----------:|---------:|---------:|------:|--------:|-------:|----------:|------------:|
| Auros  |  58.89 us | 0.534 us | 0.446 us |  1.00 |    0.01 | 2.0142 | 101.56 KB |        1.00 |
| Waffle | 490.36 us | 6.345 us | 5.935 us |  8.33 |    0.11 | 3.4180 | 168.09 KB |        1.66 |



=== Day02 ===

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4460/23H2/2023Update/SunValley3)
AMD Ryzen 7 7800X3D, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  DefaultJob : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


| Method | Mean     | Error    | StdDev   | Ratio | RatioSD | Gen0    | Gen1   | Allocated | Alloc Ratio |
|------- |---------:|---------:|---------:|------:|--------:|--------:|-------:|----------:|------------:|
| Auros  | 176.9 us |  3.05 us |  2.70 us |  1.00 |    0.02 |  7.5684 |      - | 375.55 KB |        1.00 |
| Waffle | 626.1 us | 11.35 us | 10.06 us |  3.54 |    0.08 | 41.9922 | 7.8125 | 2078.8 KB |        5.54 |



=== Day03 ===

BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4460/23H2/2023Update/SunValley3)
AMD Ryzen 7 7800X3D, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  DefaultJob : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


| Method         | Mean      | Error    | StdDev   | Median    | Ratio | Gen0    | Gen1    | Allocated | Alloc Ratio |
|--------------- |----------:|---------:|---------:|----------:|------:|--------:|--------:|----------:|------------:|
| Auros          | 484.82 us | 9.136 us | 7.133 us | 485.86 us | 15.82 | 42.9688 | 34.1797 | 2205057 B |          NA |
| Waffle         | 273.83 us | 5.131 us | 4.799 us | 272.77 us |  8.94 | 14.6484 |  6.8359 |  740912 B |          NA |
| AurosZeroAlloc |  31.15 us | 1.309 us | 3.861 us |  32.96 us |  1.02 |       - |       - |         - |          NA |
```