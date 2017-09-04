using System;
using System.Collections.Generic;

namespace ANummerGeneratorTests
{
    public static class IEnumerableHelpers
    {
        public static IEnumerable<long> GetDiff(long[] collection)
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