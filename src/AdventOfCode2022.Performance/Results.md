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