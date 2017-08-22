using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ANummerGenerator.Controllers
{
    [Produces("application/json")]
    [Route("api/Anummer/{amount:int}")]
    public class AnummerController : Controller
    {
        private Random random;

        public AnummerController()
        {
            random = new Random();
        }

        public IEnumerable<string> Generate(int amount)
        {
            if (amount > 10)
            {
                throw new ArgumentException("amount shoeld not exceed 10");
            }
            return GenerateAnummer(amount);
        }

        private IEnumerable<string> GenerateAnummer(int amount)
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
            return Math.Round(random.NextDouble() * 10000000000, 0);
        }

        private bool IsValidANummer(string aNummer)
        {
            Trace.WriteLine($"Check Number: {aNummer}");
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
                isValid = isValid && (((_totaal % 11) == 0) || ((_totaal % 11) == 5));
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