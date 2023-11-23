using RestoShared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestoWebClient
{
    public class ProductIdConverter
    {
        public static string Convert(int productId)
        {
            List<ProductDTO> list = SessionManager.ProductList;

            foreach (ProductDTO product in list)
            {
                if (product.ProductId == productId)
                {
                    return product.ProductName;
                }
            }

            return "Unknown";
        }

        public static int Convert(string productName)
        {
            List<ProductDTO> list = SessionManager.ProductList;

            foreach (ProductDTO product in list)
            {
                if (product.ProductName == productName)
                {
                    return product.ProductId;
                }
            }

            return -1;
        }
    }
}