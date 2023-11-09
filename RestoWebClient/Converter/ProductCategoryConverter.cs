using RestoShared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestoWebClient
{
    public class ProductCategoryConverter
    {
        public static string Convert(int productCategoryId)
        {
            List<ProductCategoryDTO> list = SessionManager.ProductCategoryList;

            foreach (ProductCategoryDTO category in list)
            {
                if (category.ProductCategoryId == productCategoryId)
                {
                    return category.ProductCategoryName;
                }
            }

            return "Unknown";
        }

        public static int Convert(string productCategoryName)
        {
            List<ProductCategoryDTO> list = SessionManager.ProductCategoryList;

            foreach (ProductCategoryDTO category in list)
            {
                if (category.ProductCategoryName == productCategoryName)
                {
                    return category.ProductCategoryId;
                }
            }

            return -1;
        }
    }
}