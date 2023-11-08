using RestoService.Database;
using RestoShared;
using RestoShared.DTO;
using RestoShared.ITable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoService.Service
{
    internal class ProductService : IProduct
    {
        private readonly DataAccess db;
        private bool _IsInitialized = false;

        public int ProductId { get; private set; }
        public string ProductName { get; private set; }
        public string ProductDescription { get; private set; }
        public int ProductCategoryId { get; private set; }
        public int Stock { get; private set; }
        public int MinStockLevel { get; private set; }
        public decimal UnitPrice { get; private set; }
        public bool IsActive { get; private set; }

        public ProductService()
        {
            db = new DataAccess();
        }

        private void CheckInitialized()
        {
            if (!_IsInitialized)
            {
                throw new InvalidOperationException("ProductService not initialized.");
            }
        }

        public void Initialize(ProductDTO productDTO)
        {
            ProductId = productDTO.ProductId;
            ProductName = productDTO.ProductName;
            ProductDescription = productDTO.ProductDescription;
            ProductCategoryId = productDTO.ProductCategoryId;
            Stock = productDTO.Stock;
            MinStockLevel = productDTO.MinStockLevel;
            UnitPrice = productDTO.UnitPrice;
            IsActive = productDTO.IsActive;

            _IsInitialized = true;
        }
        public ServiceResponse<int> Add()
        {
            try
            {
                CheckInitialized();

                db.SetProc("insProduct");

                db.SetParam("@productName", ProductName);
                db.SetParam("@productCategoryId", ProductCategoryId);
                db.SetParam("@stock", Stock);
                db.SetParam("@minStockLevel", MinStockLevel);
                db.SetParam("@unitPrice", UnitPrice);
                if (ProductDescription != null) db.SetParam("@productDescription", ProductDescription);

                int identity = Convert.ToInt32(db.ExecuteScalar());

                if (identity <= 0) return ServiceResponse<int>.Fail("Product not added.");

                return ServiceResponse<int>.Success(identity);
            }
            catch (Exception ex)
            {
                return ServiceResponse<int>.Fail(ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }
        }
        public ServiceResponse<List<ProductDTO>> GetAll()
        {
            List<ProductDTO> ProductList = new List<ProductDTO>();

            try
            {
                db.SetProc("getProduct");

                db.ExecuteReader();

                while (db.Reader.Read())
                {
                    ProductList.Add(new ProductDTO
                    {
                        ProductId = db.Reader.GetInt32(0),
                        ProductName = db.Reader.GetString(1),
                        ProductDescription = db.Reader.GetString(2),
                        ProductCategoryId = db.Reader.GetInt32(3),
                        Stock = db.Reader.GetInt32(4),
                        MinStockLevel = db.Reader.GetInt32(5),
                        UnitPrice = db.Reader.GetDecimal(6),
                        IsActive = db.Reader.GetBoolean(7)
                    });
                }

                if (ProductList.Count == 0) return ServiceResponse<List<ProductDTO>>.Fail("Product list empty.");

                return ServiceResponse<List<ProductDTO>>.Success(ProductList);
            }
            catch (Exception ex)
            {
                return ServiceResponse<List<ProductDTO>>.Fail(ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }
        }
        public ServiceResponse<ProductDTO> GetById(int productId)
        {
            try
            {
                db.SetProc("getProductById");

                db.SetParam("@productId", productId);

                db.ExecuteReader();

                if (!db.Reader.Read()) return ServiceResponse<ProductDTO>.Fail("Product not found.");

                return ServiceResponse<ProductDTO>.Success
                (
                    new ProductDTO
                    {
                        ProductId = db.Reader.GetInt32(0),
                        ProductName = db.Reader.GetString(1),
                        ProductDescription = db.Reader.GetString(2),
                        ProductCategoryId = db.Reader.GetInt32(3),
                        Stock = db.Reader.GetInt32(4),
                        MinStockLevel = db.Reader.GetInt32(5),
                        UnitPrice = db.Reader.GetDecimal(6),
                        IsActive = db.Reader.GetBoolean(7)
                    }
                );
            }
            catch (Exception ex)
            {
                return ServiceResponse<ProductDTO>.Fail(ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }
        }
        public ServiceResponse<int> Update()
        {
            try
            {
                CheckInitialized();

                db.SetProc("updProduct");

                db.SetParam("@productId", ProductId);
                db.SetParam("@productName", ProductName);
                db.SetParam("@productCategoryId", ProductCategoryId);
                db.SetParam("@stock", Stock);
                db.SetParam("@minStockLevel", MinStockLevel);
                db.SetParam("@unitPrice", UnitPrice);
                if (ProductDescription != null) db.SetParam("@productDescription", ProductDescription);

                int rowsAffected = db.ExecuteNonQuery();

                if (rowsAffected == 0) return ServiceResponse<int>.Fail("Product not found.");

                return ServiceResponse<int>.Success(rowsAffected);
            }
            catch (Exception ex)
            {
                return ServiceResponse<int>.Fail(ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }
        }
        public ServiceResponse<int> Delete()
        {
            try
            {
                CheckInitialized();

                db.SetProc("delProduct");

                db.SetParam("@productId", ProductId);

                int rowsAffected = db.ExecuteNonQuery();

                if (rowsAffected == 0) return ServiceResponse<int>.Fail("Product not found");

                return ServiceResponse<int>.Success(rowsAffected);
            }
            catch (Exception ex)
            {
                return ServiceResponse<int>.Fail(ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }
        }
        public ServiceResponse<int> Activate()
        {
            try
            {
                CheckInitialized();

                db.SetProc("activateProduct");

                db.SetParam("@productId", ProductId);

                int rowsAffected = db.ExecuteNonQuery();

                if (rowsAffected == 0) return ServiceResponse<int>.Fail("Product not found");
                if (rowsAffected == -1) return ServiceResponse<int>.Fail("Product is already active");

                return ServiceResponse<int>.Success(rowsAffected);
            }
            catch (Exception ex)
            {
                return ServiceResponse<int>.Fail(ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }
        }
        public ServiceResponse<int> Deactivate()
        {
            try
            {
                CheckInitialized();

                db.SetProc("deactivateProduct");

                db.SetParam("@productId", ProductId);

                int rowsAffected = db.ExecuteNonQuery();

                if (rowsAffected == 0) return ServiceResponse<int>.Fail("Product not found");
                if (rowsAffected == -1) return ServiceResponse<int>.Fail("Product is already inactive");

                return ServiceResponse<int>.Success(rowsAffected);
            }
            catch (Exception ex)
            {
                return ServiceResponse<int>.Fail(ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }
        }
    }
}
