using System;
using System.Diagnostics;

namespace WpfApp1
{
    static class BstBenchmark
    {
        public static long ContainsBenchmark(int N)
        {
            var tree = new Bst();
            var stopWatch = new Stopwatch();
            var rand = new Random();
            var randInt = new int[N];

            for (int i = 0; i < N; i++)
            {
                randInt[i] = rand.Next() % N;
                tree.Insert(randInt[i]);
            }

            for (int i = 0; i < N; i++)
            {
                stopWatch.Start();
                tree.Contains(randInt[i]);
                stopWatch.Stop();
            }

            return stopWatch.ElapsedMilliseconds;
        }
    }
}