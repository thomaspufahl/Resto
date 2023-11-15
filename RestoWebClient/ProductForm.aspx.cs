using RestoShared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RestoWebClient
{
    public partial class ProductForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int id = Convert.ToInt32(Request.QueryString["id"]);
                    ProductDTO pro = SessionManager.ProductById(id);

                    if (pro != null)
                    {
                        name.Value = pro.ProductName;
                        description.Value = pro.ProductDescription;
                        category.Value = ProductCategoryConverter.Convert(pro.ProductCategoryId);
                        price.Value = UnitPriceConverter.Convert(pro.UnitPrice);
                        stock.Value = pro.Stock.ToString();
                        stockMin.Value = pro.MinStockLevel.ToString();
                    }
                }
            }
        }
    }
}