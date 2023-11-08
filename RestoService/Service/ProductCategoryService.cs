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
    internal class ProductCategoryService : IProductCategory
    {
        private readonly DataAccess db;
        private bool _IsInitialized = false;

        public int ProductCategoryId { get; private set; }
        public string ProductCategoryName { get; private set; }
        public bool IsActive { get; private set; }

        public ProductCategoryService()
        {
            db = new DataAccess();
        }

        private void CheckInitialized()
        {
            if (!_IsInitialized)
            {
                throw new InvalidOperationException("Product Category is not initialize");
            }
        }

        public void Initialize(ProductCategoryDTO productCategoryDTO)
        {
            ProductCategoryId = productCategoryDTO.ProductCategoryId;

            ProductCategoryName = productCategoryDTO.ProductCategoryName;

            IsActive = productCategoryDTO.IsActive;

            _IsInitialized = true;
        }
        public ServiceResponse<int> Add()
        {
            try
            {
                CheckInitialized();

                db.SetProc("insProductCategory");

                db.SetParam("@productCategoryName", ProductCategoryName);

                int identity = Convert.ToInt32(db.ExecuteScalar());

                if (identity <= 0) return ServiceResponse<int>.Fail("Product Category not added");

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
        public ServiceResponse<List<ProductCategoryDTO>> GetAll()
        {
            List<ProductCategoryDTO> ProductCategoryList = new List<ProductCategoryDTO>();

            try
            {
                db.SetProc("getProductCategory");

                db.ExecuteReader();

                while (db.Reader.Read())
                {
                    ProductCategoryList.Add
                    (
                        new ProductCategoryDTO
                        {
                            ProductCategoryId = db.Reader.GetInt32(0),
                            ProductCategoryName = db.Reader.GetString(1),
                            IsActive = db.Reader.GetBoolean(2)
                        }
                    );
                }

                if (ProductCategoryList.Count == 0) return ServiceResponse<List<ProductCategoryDTO>>.Fail("Product category list empty");

                return ServiceResponse<List<ProductCategoryDTO>>.Success(ProductCategoryList);         
            }
            catch (Exception ex)
            {
                return ServiceResponse<List<ProductCategoryDTO>>.Fail(ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }
        }
        public ServiceResponse<ProductCategoryDTO> GetById(int productCategoryId)
        {
            try
            {
                db.SetProc("getProductCategoryById");

                db.SetParam("@productCategoryId", productCategoryId);

                db.ExecuteReader();

                if (!db.Reader.Read()) return ServiceResponse<ProductCategoryDTO>.Fail("Product Category not found");

                return ServiceResponse<ProductCategoryDTO>.Success
                (
                    new ProductCategoryDTO
                    {
                        ProductCategoryId = db.Reader.GetInt32(0),
                        ProductCategoryName = db.Reader.GetString(1),
                        IsActive = db.Reader.GetBoolean(2)
                    }
                );
            }
            catch (Exception ex)
            {
                return ServiceResponse<ProductCategoryDTO>.Fail(ex.Message);
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

                db.SetProc("updProductCategory");

                db.SetParam("@productCategoryId", ProductCategoryId);
                db.SetParam("@productCategoryName", ProductCategoryName);

                int rowsAffected = db.ExecuteNonQuery();

                if (rowsAffected == 0) return ServiceResponse<int>.Fail("Product Category not found");

                return ServiceResponse<int>.Success(rowsAffected);
            }
            catch
            (Exception ex)
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

                db.SetProc("delProductCategory");

                db.SetParam("@productCategoryId", ProductCategoryId);

                int rowsAffected = db.ExecuteNonQuery();

                if (rowsAffected == 0) return ServiceResponse<int>.Fail("Product Category not found");

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

                db.SetProc("activateProductCategory");

                db.SetParam("@productCategoryId", ProductCategoryId);

                int rowsAffected = db.ExecuteNonQuery();

                if (rowsAffected == 0) return ServiceResponse<int>.Fail("Product Category not found");
                if (rowsAffected == -1) return ServiceResponse<int>.Fail("Product Category is already active");

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

                db.SetProc("deactivateProductCategory");

                db.SetParam("@productCategoryId", ProductCategoryId);

                int rowsAffected = db.ExecuteNonQuery();

                if (rowsAffected == 0) return ServiceResponse<int>.Fail("Product Category not found");
                if (rowsAffected == -1) return ServiceResponse<int>.Fail("Product Category is already inactive");

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
