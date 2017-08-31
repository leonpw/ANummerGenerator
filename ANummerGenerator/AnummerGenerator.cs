using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ANummerGenerator
{
    public class AnummerGenerator
    {
        private bool allowMod5;
        private Random random;

        public long Tries { get; set; }

        /// <summary>
        /// Creates an instance of the "AnummerGenerator"
        /// </summary>
        /// <param name="allowMod5">Allow Mod11 = 5 validation</param>
        public AnummerGenerator(bool allowMod5 = true)
        {
            this.allowMod5 = allowMod5;
            this.random = new Random();
            Tries = 0;
        }

        public IEnumerable<string> GenerateAnummer(int amount)
        {
            while (amount != 0)
            {
                double startNumber = GetRand();
                if (IsValidANummer(startNumber.ToString()))
                {
                    amount--;
                    yield return startNumber.ToString();
                }
            }
        }

        private double GetRand()
        {
            Tries++;
            return Math.Round(random.NextDouble() * 10000000000, 0);
        }

        public IEnumerable<long> IsValidANummerRange(IEnumerable<long> range)
        {
            foreach (var aNummer in range)
            {
                if (IsValidANummer(aNummer.ToString()))
                {
                    yield return aNummer;
                }
            }
        }

        public IEnumerable<long> IsValidANummerRangeParallel(IEnumerable<long> range)
        {
            List<long> list = new List<long>();
            var x = Parallel.ForEach(range, (aNummer) =>
            {
                if (IsValidANummer(aNummer.ToString()))
                {
                    Trace.WriteLine($"Number: {aNummer} is valid!");
                    list.Add(aNummer);
                }
            });
            return list;
        }

        private bool IsValidANummer(string aNummer)
        {
            bool isValid = true;
            int _index;
            long _totaal;

            if (aNummer == null)
            {
                return false;
            }

            char[] _anummerArray = aNummer.ToCharArray();

            if (_anummerArray.Length != 10)
            {
                return false;
            }

            isValid = isValid && (_anummerArray[0] != '0');

            if (isValid)
            {
                for (_index = 1; _index < 10; _index++)
                {
                    isValid = isValid && (_anummerArray[_index] != _anummerArray[_index - 1]);
                }
            }

            if (isValid)
            {
                _totaal = 0;
                for (_index = 0; _index < 10; _index++)
                {
                    _totaal += _anummerArray[_index] - '0';
                }
                isValid = isValid && ((_totaal % 11) == 0);

                if (allowMod5)
                {
                    isValid = isValid && ((_totaal % 11) == 5);
                }
            }

            if (isValid)
            {
                _totaal = 0;
                for (_index = 0; _index < 10; _index++)
                {
                    _totaal += ((int)Math.Pow(2, _index)) * (_anummerArray[_index] - '0');
                }
                isValid = isValid && ((_totaal % 11) == 0);
            }
            return isValid;
        }
    }
}