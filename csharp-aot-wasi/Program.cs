﻿using System;
using CSharpPayload;
var watch = System.Diagnostics.Stopwatch.StartNew();
int n = 1000000000;
int a = 8888;
for (int b = 9000; b < 10000; ++b)
{
    var calculationResult = CSharpDirectSolution.NthMagicalNumber(n, a, b);
}

watch.Stop();
var elapsedMs = watch.ElapsedMilliseconds;
Console.WriteLine($"Elapsed time: {elapsedMs}");