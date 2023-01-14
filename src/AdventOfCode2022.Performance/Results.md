# Performance Results

## Day 1

```
|    Method |        input |        Mean |     Error |    StdDev |   Gen0 | Allocated | Alloc Ratio |
|---------- |------------- |------------:|----------:|----------:|-------:|----------:|------------:|
|       Run |   String[14] |    253.1 ns |   5.08 ns |  10.27 ns | 0.0315 |     264 B |        1.00 |
| RunFaster |   String[14] |    122.9 ns |   2.48 ns |   3.48 ns |      - |         - |        0.00 |

|       Run | String[2238] | 31,188.8 ns | 591.94 ns | 848.94 ns | 0.2441 |    2368 B |        8.97 |
| RunFaster | String[2238] | 24,927.5 ns | 263.79 ns | 233.84 ns |      - |         - |        0.00 |
```

## Day 23

```
|                          Method |     Mean |     Error |    StdDev | Ratio |     Gen0 | Allocated | Alloc Ratio |
|-------------------------------- |---------:|----------:|----------:|------:|---------:|----------:|------------:|
|               GetNoMovementTurn | 4.239 ms | 0.0550 ms | 0.0459 ms |  1.00 | 132.8125 | 567.39 KB |        1.00 |
| GetNoMovementTurnOptimizedAsync | 2.665 ms | 0.0223 ms | 0.0197 ms |  0.63 | 140.6250 | 574.88 KB |        1.01 |
```

## Day 24

```
|                     Method |     Mean |    Error |   StdDev | Ratio |       Gen0 |       Gen1 | Allocated | Alloc Ratio |
|--------------------------- |---------:|---------:|---------:|------:|-----------:|-----------:|----------:|------------:|
|          GetMinimumMinutes | 65.08 ms | 0.656 ms | 0.547 ms |  1.00 | 13750.0000 | 11125.0000 |  70.78 MB |        1.00 |
| GetMinimumMinutesOptimized | 31.96 ms | 0.585 ms | 0.801 ms |  0.49 |  6062.5000 |   125.0000 |  24.23 MB |        0.34 |
```