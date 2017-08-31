using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ANummerGenerator.Controllers
{
    [Produces("application/json")]
    [Route("api/Anummer/{amount:int}")]
    public class AnummerController : Controller
    {
        public AnummerController()
        {
        }

        public IEnumerable<string> Generate(int amount)
        {
            if (amount > 10)
            {
                throw new ArgumentException("amount shoeld not exceed 10");
            }
            return new AnummerGenerator(true).GenerateAnummer(amount);
        }

        
    }
}