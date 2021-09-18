using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using System.Security.Cryptography;
using System.Linq;
using System.Collections.Generic;
using System;

namespace BenchmarkingTests
{
    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.Net48)]
    [SimpleJob(RuntimeMoniker.Net60)]
    [SimpleJob(RuntimeMoniker.Net50)]
    public class LongListBenchMark
    {
        [Params(1000000)]
        public int IterationCount;
        private IList<int> LongList { get; set; }


        [GlobalSetup]
        public void Setup()
        {
            var random = new Random();

            LongList = new List<int>();
            for (int index = 0; index < IterationCount; index++)
            {
                LongList.Add(random.Next());
            }
        }

        [Benchmark]
        public int GetBigNumberCountLinq()
        {
            return LongList.Where(l => int.MaxValue / 2 < l).Count();
        }

        [Benchmark]
        public int GetBigNumberCount()
        {
            int counter = 0;
            for(int index = 0; index < IterationCount; index++)
            {
                if (int.MaxValue / 2 < LongList[index])
                {
                    counter++;
                }
            }
            return counter;
        }
    }
}
