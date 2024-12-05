# Benchmarks
For people that I know who are doing AOC2024 and using C#

## Disclaimer
The input is loaded **before** the benchmarks start, so the initial data input's allocation is not represented in these benchmarks.

Each solution may have had slight adjustments to be compatible with being run multiple times (primarily instances of StreamReader).

The benchmarks is to see how long it takes to run *both* Part 1 and Part 2. If the solution happens to calculate both part 1 and part 2 in a single pass, then the result from the single pass is used.

# Results

## Day 1

```
BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4460/23H2/2023Update/SunValley3)
AMD Ryzen 7 7800X3D, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  DefaultJob : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


| Method | Mean      | Error    | StdDev   | Ratio | RatioSD | Gen0   | Allocated | Alloc Ratio |
|------- |----------:|---------:|---------:|------:|--------:|-------:|----------:|------------:|
| Auros  |  58.89 us | 0.534 us | 0.446 us |  1.00 |    0.01 | 2.0142 | 101.56 KB |        1.00 |
| Waffle | 490.36 us | 6.345 us | 5.935 us |  8.33 |    0.11 | 3.4180 | 168.09 KB |        1.66 |
```

## Day 2

```
BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4460/23H2/2023Update/SunValley3)
AMD Ryzen 7 7800X3D, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  DefaultJob : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


| Method | Mean     | Error    | StdDev   | Ratio | RatioSD | Gen0    | Gen1   | Allocated | Alloc Ratio |
|------- |---------:|---------:|---------:|------:|--------:|--------:|-------:|----------:|------------:|
| Auros  | 176.9 us |  3.05 us |  2.70 us |  1.00 |    0.02 |  7.5684 |      - | 375.55 KB |        1.00 |
| Waffle | 626.1 us | 11.35 us | 10.06 us |  3.54 |    0.08 | 41.9922 | 7.8125 | 2078.8 KB |        5.54 |
```

## Day 3

```
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

## Day 4

```
BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4460/23H2/2023Update/SunValley3)
AMD Ryzen 7 7800X3D, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  DefaultJob : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


| Method    | Mean     | Error    | StdDev   | Median   | Ratio | RatioSD | Allocated | Alloc Ratio |
|---------- |---------:|---------:|---------:|---------:|------:|--------:|----------:|------------:|
| Auros     | 637.0 us | 12.67 us | 25.30 us | 624.3 us |  5.56 |    0.72 |         - |          NA |
| Waffle    | 132.0 us |  8.78 us | 25.89 us | 123.5 us |  1.15 |    0.27 |         - |          NA |
| Waffle0PR | 116.6 us |  5.34 us | 15.76 us | 111.6 us |  1.02 |    0.19 |         - |          NA |
```

## Day 5

```
BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4460/23H2/2023Update/SunValley3)
AMD Ryzen 7 7800X3D, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  DefaultJob : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


| Method | Mean      | Error     | StdDev    | Ratio | RatioSD | Gen0      | Gen1    | Allocated   | Alloc Ratio   |
|------- |----------:|----------:|----------:|------:|--------:|----------:|--------:|------------:|--------------:|
| Auros  |  1.916 ms | 0.0383 ms | 0.0728 ms |  1.00 |    0.05 |         - |       - |         2 B |          1.00 |
| Waffle |  1.740 ms | 0.0339 ms | 0.0558 ms |  0.91 |    0.04 |   17.5781 |  1.9531 |    895729 B |    447,864.50 |
| Arimil | 34.639 ms | 0.6428 ms | 0.9011 ms | 18.11 |    0.81 | 2571.4286 | 71.4286 | 131478989 B | 65,739,494.50 |
```

* [Auros's Solution](github.com/Auros/AOC2024/blob/main/src/Day05/Program.cs)
* [Waffle's Solution](https://github.com/GiantWaffleCode/AoC2024/blob/master/Day05/Program.cs)
* [Arimil (Renari)'s Solution](https://github.com/Renari/AoC2024/blob/master/5/Program.cs)