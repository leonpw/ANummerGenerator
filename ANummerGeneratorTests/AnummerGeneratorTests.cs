using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ANummerGenerator;
using Xunit;

namespace ANummerGeneratorTests
{
    public class AnummerGeneratorTests
    {
        [Fact]
        public void GenerateAnummer_ShouldGenerate()
        {
            var gen = new AnummerGenerator();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var num = gen.GenerateAnummer(1).ToArray();

            sw.Stop();

            Trace.WriteLine($"Generated 1 A-nummer in: {sw.Elapsed}. We tried {gen.Tries} times");
        }

        [Fact]
        public void IsValidANummerRangeParallel_ShouldGenerate10Numbers()
        {
            // smallest Anummer
            var range = LongRange(1010101010, 10);

            var result = new AnummerGenerator(allowMod5: false).
                IsValidANummerRangeParallel(range);

            var res = CheckDiff(result.ToArray()).ToArray();
        }

        public static IEnumerable<long> CheckDiff(long[] collection)
        {
            for (int i = 1; i < collection.Length; i++)
            {
                if (collection[i].ToString()[7] == collection[i - 1].ToString()[7])
                    yield return collection[i];
            }
        }

        public static IEnumerable<long> LongRange(long start, long count)
        {
            long max = start + count - 1;
            if (count < 0)
                throw new ArgumentOutOfRangeException("count");

            return RangeIterator(start, count);
        }

        private static IEnumerable<long> RangeIterator(long start, long count)
        {
            for (int i = 0; i < count; i++)
                yield return start + i;
        }
    }
}