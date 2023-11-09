using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestoWebClient
{
    public class UnitPriceConverter
    {
        public static string Convert(decimal unitPrice)
        {
            return unitPrice.ToString("#.###");
        }
    }
}