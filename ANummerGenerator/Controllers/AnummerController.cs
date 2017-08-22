using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

//using System.Net;
//using System.Net.Http;

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

            isValid = _anummerArray.Length == 10;

            if (_anummerArray.Length < 10)
            {
                return false;
            }
            else if (_anummerArray.Length > 10)
            {
                return false;
            }

            // Voorwaarde 1: a0 != 0
            isValid = isValid && (_anummerArray[0] != '0');

            // Voorwaarde 2: 2 opeenvolgende cijfers zijn ongelijk
            if (isValid)
            {
                for (_index = 1; _index < 10; _index++)
                {
                    isValid = isValid && (_anummerArray[_index] != _anummerArray[_index - 1]);
                }
            }

            // Voorwaarde 3: a0+a1+..+a9 is deelbaar door 11, met rest 0
            // Release 6.0 RNI: a0+a1+..+a9 is deelbaar door 11, met rest 0 of 5
            if (isValid)
            {
                _totaal = 0;
                for (_index = 0; _index < 10; _index++)
                {
                    _totaal += (int)_anummerArray[_index] - (int)'0';
                }
                isValid = isValid && (((_totaal % 11) == 0) || ((_totaal % 11) == 5));
            }

            // Voorwaarde 4: (1*a0)+(2*a2)+(4*a2)+..+(512*a9) is deelbaar door 11
            if (isValid)
            {
                _totaal = 0;
                for (_index = 0; _index < 10; _index++)
                {
                    _totaal += ((int)System.Math.Pow(2, _index)) * ((int)_anummerArray[_index] - (int)'0');
                }
                isValid = isValid && ((_totaal % 11) == 0);
            }
            return isValid;
        }
    }
}