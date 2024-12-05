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


| Method | Mean      | Error    | StdDev   | Ratio | RatioSD | Gen0   | Gen1   | Allocated | Alloc Ratio |
|------- |----------:|---------:|---------:|------:|--------:|-------:|-------:|----------:|------------:|
| Auros  |  60.22 us | 0.709 us | 0.592 us |  1.00 |    0.01 | 2.0142 |      - | 101.56 KB |        1.00 |
| Waffle | 495.52 us | 5.675 us | 6.536 us |  8.23 |    0.13 | 2.9297 |      - | 168.09 KB |        1.66 |
| Arimil |  95.39 us | 1.890 us | 2.710 us |  1.58 |    0.05 | 4.0283 | 0.4883 | 201.78 KB |        1.99 |
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


| Method         | Mean      | Error     | Median    | Ratio         | Gen0    | Gen1    | Allocated | Alloc Ratio |
|--------------- |----------:|----------:|----------:|--------------:|--------:|--------:|----------:|------------:|
| Auros          | 518.50 us | 10.355 us | 518.63 us |      baseline | 42.9688 | 34.1797 | 2205057 B |             |
| Waffle         | 286.04 us |  3.626 us | 286.30 us |  1.81x faster | 15.6250 |  6.8359 |  740913 B |  2.98x less |
| AurosZeroAlloc |  29.71 us |  1.527 us |  32.28 us | 17.90x faster |       - |       - |         - |          NA |
| Arimil         | 179.42 us |  1.687 us | 179.10 us |  2.89x faster | 12.2070 |  6.3477 |  621928 B |  3.55x less |
```

## Day 4

```
BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4460/23H2/2023Update/SunValley3)
AMD Ryzen 7 7800X3D, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  DefaultJob : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


| Method    | Mean      | Error     | Median    | Ratio        | Allocated | Alloc Ratio |
|---------- |----------:|----------:|----------:|-------------:|----------:|------------:|
| Auros     | 637.73 us | 12.666 us | 625.66 us | 6.55x slower |       1 B |          NA |
| Waffle    |  88.68 us |  7.925 us |  88.47 us | 1.21x faster |         - |          NA |
| Waffle0PR | 100.76 us |  5.927 us | 100.73 us |     baseline |         - |          NA |
| Arimil    |  75.27 us |  1.378 us |  75.13 us | 1.34x faster |         - |          NA |
```

## Day 5

```
BenchmarkDotNet v0.14.0, Windows 11 (10.0.22631.4460/23H2/2023Update/SunValley3)
AMD Ryzen 7 7800X3D, 1 CPU, 16 logical and 8 physical cores
.NET SDK 9.0.100
  [Host]     : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  DefaultJob : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI


| Method | Mean      | Error     | Ratio         | Gen0      | Gen1    | Allocated   | Alloc Ratio          |
|------- |----------:|----------:|--------------:|----------:|--------:|------------:|---------------------:|
| Auros  |  1.918 ms | 0.0366 ms |      baseline |         - |       - |         1 B |                      |
| Waffle |  1.710 ms | 0.0214 ms |  1.12x faster |   17.5781 |  1.9531 |    895729 B |     895,729.00x more |
| Arimil | 33.384 ms | 0.2650 ms | 17.42x slower | 2600.0000 | 66.6667 | 131478966 B | 131,478,966.00x more |
```

* [Auros's Solution](github.com/Auros/AOC2024/blob/main/src/Day05/Program.cs)
* [Waffle's Solution](https://github.com/GiantWaffleCode/AoC2024/blob/master/Day05/Program.cs)
* [Arimil (Renari)'s Solution](https://github.com/Renari/AoC2024/blob/master/5/Program.cs)