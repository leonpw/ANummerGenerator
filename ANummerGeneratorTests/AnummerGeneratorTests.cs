using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
        [Category("Slow")]
        public void GenerateAllANumbers()
        {
            string filename = $"{Directory.GetCurrentDirectory()}\\{DateTime.UtcNow.ToString("yyyy-MM-dd_hh-mm-ss-tt")}.txt";

            Stopwatch sw = new Stopwatch();
            sw.Start();
            var generator = new AnummerGenerator(allowMod5: true);

            var range = IEnumerableHelpers.LongRange(1010101010, 100000000);

            using (FileStream stream = new FileStream(filename, FileMode.Append, FileAccess.Write, FileShare.None, 4096, true))
            using (StreamWriter streamWriter = new StreamWriter(stream))
            {
                var result = generator.
                    GetValidANummerRangeParallel(range, async (validAnumber) =>
                        await streamWriter.WriteLineAsync(validAnumber.ToString()));

                sw.Stop();

                string log = $"Generated {result.Count()} A-nummer in: {sw.Elapsed}. We checked {generator.Tries} numbers";
                streamWriter.WriteLine(log);
                Trace.WriteLine(log);
            }
        }

        private async Task FileWriteAsync(string filePath, string message, bool append = true)
        {
            using (FileStream stream = new FileStream(filePath, append ? FileMode.Append : FileMode.Create, FileAccess.Write, FileShare.None, 4096, true))
            using (StreamWriter sw = new StreamWriter(stream))
            {
                await sw.WriteLineAsync(message);
                await sw.FlushAsync();
            }
        }

        [Fact]
        public void GetValidANummerRangeParallel_1010101010_ShouldBeValid()
        {
            long[] range = new long[] { 1010101010 };

            var result = new AnummerGenerator(allowMod5: true).
                GetValidANummerRangeParallel(range);

            Assert.True(result.Count() == 1);
        }
    }
}