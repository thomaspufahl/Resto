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
    internal class OrderItemService : IOrderItem
    {
        private readonly DataAccess db;
        private bool _IsInitialized = false;

        public long OrderItemId { get; private set; }
        public long OrderNumber { get; private set; }
        public int ProductId { get; private set; }
        public byte Quantity { get; private set; }
        public bool IsActive { get; private set; }

        public OrderItemService()
        {
            db = new DataAccess();
        }

        private void CheckInitialized()
        {
            if (!_IsInitialized)
            {
                throw new InvalidOperationException("OrderItem is not initialize");
            }
        }

        public void Initialize(OrderItemDTO orderItemDTO)
        {
            OrderItemId = orderItemDTO.OrderItemId;
            OrderNumber = orderItemDTO.OrderNumber;
            ProductId = orderItemDTO.ProductId;
            Quantity = orderItemDTO.Quantity;
            IsActive = orderItemDTO.IsActive;

            _IsInitialized = true;
        }
        public ServiceResponse<long> Add()
        {
            try
            {
                CheckInitialized();

                db.SetProc("insOrderItem");

                db.SetParam("@orderNumber", OrderNumber);
                db.SetParam("@productId", ProductId);
                db.SetParam("@quantity", Quantity);

                long identity = Convert.ToInt64(db.ExecuteScalar());

                if (identity <= 0) return ServiceResponse<long>.Fail("OrderItem not added");

                return ServiceResponse<long>.Success(identity);
            }
            catch (Exception ex)
            {
                return ServiceResponse<long>.Fail(ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }
        }
        public ServiceResponse<List<OrderItemDTO>> GetAll()
        {
            List<OrderItemDTO> orderItemList = new List<OrderItemDTO>();

            try
            {
                db.SetProc("getOrderItem");

                db.ExecuteReader();

                while (db.Reader.Read())
                {
                    orderItemList.Add(new OrderItemDTO
                    {
                        OrderItemId = db.Reader.GetInt64(0),
                        OrderNumber = db.Reader.GetInt64(1),
                        ProductId = db.Reader.GetInt32(2),
                        Quantity = db.Reader.GetByte(3),
                        IsActive = db.Reader.GetBoolean(4)
                    });
                }

                if (orderItemList.Count == 0) return ServiceResponse<List<OrderItemDTO>>.Fail("OrderItem list empty");

                return ServiceResponse<List<OrderItemDTO>>.Success(orderItemList);
            }
            catch (Exception ex)
            {
                return ServiceResponse<List<OrderItemDTO>>.Fail(ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }
        }
        public ServiceResponse<OrderItemDTO> GetById(long orderItemId)
        {
            try
            {
                db.SetProc("getOrderItemById");

                db.SetParam("@orderItemId", orderItemId);

                db.ExecuteReader();

                if (!db.Reader.Read()) return ServiceResponse<OrderItemDTO>.Fail("OrderItem not found");

                return ServiceResponse<OrderItemDTO>.Success(new OrderItemDTO
                {
                    OrderItemId = db.Reader.GetInt64(0),
                    OrderNumber = db.Reader.GetInt64(1),
                    ProductId = db.Reader.GetInt32(2),
                    Quantity = db.Reader.GetByte(3),
                    IsActive = db.Reader.GetBoolean(4)
                });
            }
            catch (Exception ex)
            {
                return ServiceResponse<OrderItemDTO>.Fail(ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }
        }
        public ServiceResponse<OrderItemDTO> GetByOrderNumber(long orderNumber)
        {
            try
            {
                db.SetProc("getOrderItemByOrderNumber");

                db.SetParam("@orderNumber", orderNumber);

                db.ExecuteReader();

                if (!db.Reader.Read()) return ServiceResponse<OrderItemDTO>.Fail("OrderItem not found");

                return ServiceResponse<OrderItemDTO>.Success(new OrderItemDTO
                {
                    OrderItemId = db.Reader.GetInt64(0),
                    OrderNumber = db.Reader.GetInt64(1),
                    ProductId = db.Reader.GetInt32(2),
                    Quantity = db.Reader.GetByte(3),
                    IsActive = db.Reader.GetBoolean(4)
                });
            }
            catch (Exception ex)
            {
                return ServiceResponse<OrderItemDTO>.Fail(ex.Message);
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

                db.SetProc("updOrderItem");

                db.SetParam("@orderItemId", OrderItemId);
                db.SetParam("productId", ProductId);
                db.SetParam("@quantity", Quantity);

                int rowsAffected = db.ExecuteNonQuery();

                if (rowsAffected == 0) return ServiceResponse<int>.Fail("OrderItem not found");

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

                db.SetProc("delOrderItem");

                db.SetParam("@orderItemId", OrderItemId);

                int rowsAffected = db.ExecuteNonQuery();

                if (rowsAffected == 0) return ServiceResponse<int>.Fail("OrderItem not found");

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

                db.SetProc("activateOrderItem");

                db.SetParam("@orderItemId", OrderItemId);

                int rowsAffected = db.ExecuteNonQuery();

                if (rowsAffected == 0) return ServiceResponse<int>.Fail("OrderItem not found");
                if (rowsAffected == -1) return ServiceResponse<int>.Fail("OrderItem is already active");

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

                db.SetProc("deactivateOrderItem");

                db.SetParam("@orderItemId", OrderItemId);

                int rowsAffected = db.ExecuteNonQuery();

                if (rowsAffected == 0) return ServiceResponse<int>.Fail("OrderItem not found");
                if (rowsAffected == -1) return ServiceResponse<int>.Fail("OrderItem is already inactive");

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
