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
    internal class OrderService : IOrder
    {
        private readonly DataAccess db;
        private bool _IsInitialized = false;

        public long OrderNumber { get; private set; }
        public int EmployeeId { get; private set; }
        public byte TableNumber { get; private set; }
        public DateTime OrderDate { get; private set; }
        public int OrderStatusId { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public bool IsActive { get; private set; }

        public OrderService()
        {
            db = new DataAccess();
        }

        private void CheckInitialized()
        {
            if (!_IsInitialized)
            {
                throw new InvalidOperationException("Order is not initialize");
            }
        }

        public void Initialize(OrderDTO orderDTO)
        {
            OrderNumber = orderDTO.OrderNumber;
            EmployeeId = orderDTO.EmployeeId;
            TableNumber = orderDTO.TableNumber;
            OrderDate = orderDTO.OrderDate;
            OrderStatusId = orderDTO.OrderStatusId;
            UpdatedAt = orderDTO.UpdatedAt;
            IsActive = orderDTO.IsActive;

            _IsInitialized = true;
        }
        public ServiceResponse<int> Add()
        {
            try
            {
                CheckInitialized();

                db.SetProc("insOrder");

                db.SetParam("@employeeId", EmployeeId);
                db.SetParam("@tableNumber", TableNumber);
                db.SetParam("@orderStatusId", OrderStatusId);

                int identity = Convert.ToInt32(db.ExecuteScalar());

                if (identity <= 0) return ServiceResponse<int>.Fail("Order not added");

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
        public ServiceResponse<List<OrderDTO>> GetAll()
        {
            List<OrderDTO> OrderList = new List<OrderDTO>();

            try
            {
                db.SetProc("getOrder");

                db.ExecuteReader();

                while (db.Reader.Read())
                {
                    OrderList.Add(new OrderDTO
                    {
                        OrderNumber = Convert.ToInt64(0),
                        EmployeeId = Convert.ToInt32(1),
                        TableNumber = Convert.ToByte(2),
                        OrderDate = Convert.ToDateTime(3),
                        OrderStatusId = Convert.ToInt32(4),
                        UpdatedAt = Convert.ToDateTime(5),
                        IsActive = Convert.ToBoolean(6)
                    });
                }

                if (OrderList.Count <= 0) return ServiceResponse<List<OrderDTO>>.Fail("Order list empty");

                return ServiceResponse<List<OrderDTO>>.Success(OrderList);
            }
            catch (Exception ex)
            {
                return ServiceResponse<List<OrderDTO>>.Fail(ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }
        }
        public ServiceResponse<OrderDTO> GetByOrderNumber(long orderNumber)
        {
            try
            {
                db.SetProc("getOrderByOrderNumber");

                db.SetParam("@orderNumber", orderNumber);

                db.ExecuteReader();

                if (!db.Reader.Read()) return ServiceResponse<OrderDTO>.Fail("Order not found");

                return ServiceResponse<OrderDTO>.Success
                (
                    new OrderDTO
                    {
                        OrderNumber = Convert.ToInt64(0),
                        EmployeeId = Convert.ToInt32(1),
                        TableNumber = Convert.ToByte(2),
                        OrderDate = Convert.ToDateTime(3),
                        OrderStatusId = Convert.ToInt32(4),
                        UpdatedAt = Convert.ToDateTime(5),
                        IsActive = Convert.ToBoolean(6)
                    }
                );
            }
            catch (Exception ex)
            {
                return ServiceResponse<OrderDTO>.Fail(ex.Message);
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

                db.SetProc("updOrder");

                db.SetParam("@orderNumber", OrderNumber);
                db.SetParam("@orderStatusId", OrderStatusId);

                int rowsAffected = db.ExecuteNonQuery();

                if (rowsAffected == 0) return ServiceResponse<int>.Fail("Order not found");

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

                db.SetProc("delOrder");

                db.SetParam("@orderNumber", OrderNumber);

                int rowsAffected = db.ExecuteNonQuery();

                if (rowsAffected == 0) return ServiceResponse<int>.Fail("Order not found");

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

                db.SetProc("activateOrder");

                db.SetParam("@orderNumber", OrderNumber);

                int rowsAffected = db.ExecuteNonQuery();

                if (rowsAffected == 0) return ServiceResponse<int>.Fail("Order not found");
                if (rowsAffected == -1) return ServiceResponse<int>.Fail("Order is already active");

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

                db.SetProc("deactivateOrder");

                db.SetParam("@orderNumber", OrderNumber);

                int rowsAffected = db.ExecuteNonQuery();

                if (rowsAffected == 0) return ServiceResponse<int>.Fail("Order not found");
                if (rowsAffected == -1) return ServiceResponse<int>.Fail("Order is already inactive");

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
