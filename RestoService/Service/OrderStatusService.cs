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
    internal class OrderStatusService : IOrderStatus
    {
        private readonly DataAccess db;
        private bool _IsInitialized = false;

        public int OrderStatusId { get; private set; }
        public string StatusCode { get; private set; }
        public string OrderStatusName { get; private set; }
        public string OrderStatusDescription { get; private set; }
        public bool IsActive { get; private set; }

        public OrderStatusService()
        {
            db = new DataAccess();
        }

        private void CheckInitialized()
        {
            if (!_IsInitialized)
            {
                throw new InvalidOperationException("OrderStatus is not initialize");
            }
        }

        public void Initialize(OrderStatusDTO orderStatusDTO)
        {
            OrderStatusId = orderStatusDTO.OrderStatusId;
            StatusCode = orderStatusDTO.StatusCode;
            OrderStatusName = orderStatusDTO.OrderStatusName;
            OrderStatusDescription = orderStatusDTO.OrderStatusDescription;
            IsActive = orderStatusDTO.IsActive;

            _IsInitialized = true;
        }
        public ServiceResponse<int> Add()
        {
            try
            {
                CheckInitialized();

                db.SetProc("insOrderStatus");

                db.SetParam("@statusCode", StatusCode);
                db.SetParam("@orderStatusName", OrderStatusName);
                db.SetParam("@orderStatusDescription", OrderStatusDescription);

                int identity = Convert.ToInt32(db.ExecuteScalar());

                if (identity <= 0) return ServiceResponse<int>.Fail("OrderStatus not added");

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
        public ServiceResponse<List<OrderStatusDTO>> GetAll()
        {
            List<OrderStatusDTO> OrderStatusList = new List<OrderStatusDTO>();

            try
            {
                db.SetProc("getOrderStatus");

                db.ExecuteReader();

                while (db.Reader.Read())
                {
                    OrderStatusList.Add(new OrderStatusDTO
                    {
                        OrderStatusId = db.Reader.GetInt32(0),
                        StatusCode = db.Reader.GetString(1),
                        OrderStatusName = db.Reader.GetString(2),
                        OrderStatusDescription = db.Reader.GetString(3),
                        IsActive = db.Reader.GetBoolean(4)
                    }); 
                }

                if (OrderStatusList.Count == 0) return ServiceResponse<List<OrderStatusDTO>>.Fail("OrderStatus list empty");

                return ServiceResponse<List<OrderStatusDTO>>.Success(OrderStatusList);
            }
            catch (Exception ex)
            {
                return ServiceResponse<List<OrderStatusDTO>>.Fail(ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }
            
        }
        public ServiceResponse<OrderStatusDTO> GetById(int orderStatusId)
        {
            try
            {
                db.SetProc("getOrderStatusById");

                db.SetParam("@orderStatusId", orderStatusId);

                db.ExecuteReader();

                if (!db.Reader.Read()) return ServiceResponse<OrderStatusDTO>.Fail("OrderStatus not found");

                return ServiceResponse<OrderStatusDTO>.Success
                (
                    new OrderStatusDTO
                    {
                        OrderStatusId = db.Reader.GetInt32(0),
                        StatusCode = db.Reader.GetString(1),
                        OrderStatusName = db.Reader.GetString(2),
                        OrderStatusDescription = db.Reader.GetString(3),
                        IsActive = db.Reader.GetBoolean(4)
                    }
                );
            }
            catch (Exception ex)
            {
                return ServiceResponse<OrderStatusDTO>.Fail(ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }
        }
        public ServiceResponse<OrderStatusDTO> GetByCode(string statusCode)
        {
            try
            {
                db.SetProc("getOrderStatusByCode");

                db.SetParam("@statusCode", statusCode);

                db.ExecuteReader();

                if (!db.Reader.Read()) return ServiceResponse<OrderStatusDTO>.Fail("OrderStatus not found");

                return ServiceResponse<OrderStatusDTO>.Success
                (
                    new OrderStatusDTO
                    {
                        OrderStatusId = db.Reader.GetInt32(0),
                        StatusCode = db.Reader.GetString(1),
                        OrderStatusName = db.Reader.GetString(2),
                        OrderStatusDescription = db.Reader.GetString(3),
                        IsActive = db.Reader.GetBoolean(4)
                    }
                );
            }
            catch (Exception ex)
            {
                return ServiceResponse<OrderStatusDTO>.Fail(ex.Message);
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

                db.SetProc("updOrderStatus");

                db.SetParam("@orderStatusId", OrderStatusId);
                db.SetParam("@statusCode", StatusCode);
                db.SetParam("@orderStatusName", OrderStatusName);
                db.SetParam("@orderStatusDescription", OrderStatusDescription);

                int rowsAffected = db.ExecuteNonQuery();

                if (rowsAffected == 0) return ServiceResponse<int>.Fail("OrderStatus not found");

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

                db.SetProc("delOrderStatus");

                db.SetParam("@orderStatusId", OrderStatusId);

                int rowsAffected = db.ExecuteNonQuery();

                if (rowsAffected == 0) return ServiceResponse<int>.Fail("OrderStatus not found");

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

                db.SetProc("activateOrderStatus");

                db.SetParam("@orderStatusId", OrderStatusId);

                int rowsAffected = db.ExecuteNonQuery();

                if (rowsAffected == 0) return ServiceResponse<int>.Fail("OrderStatus not found");
                if (rowsAffected == -1) return ServiceResponse<int>.Fail("OrderStatus is already active");

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

                db.SetProc("deactivateOrderStatus");

                db.SetParam("@orderStatusId", OrderStatusId);

                int rowsAffected = db.ExecuteNonQuery();

                if (rowsAffected == 0) return ServiceResponse<int>.Fail("OrderStatus not found");
                if (rowsAffected == -1) return ServiceResponse<int>.Fail("OrderStatus is already inactive");

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
