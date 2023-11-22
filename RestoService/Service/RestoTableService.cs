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
    internal class RestoTableService : IRestoTable
    {
        private readonly DataAccess db;
        private bool _IsInitialized = false;

        public byte TableNumber { get; private set; }
        public long OrderNumber { get; private set; }
        public bool IsActive { get; private set; }

        public RestoTableService()
        {
            db = new DataAccess();
        }

        private void CheckInitialized()
        {
            if (!_IsInitialized)
            {
                throw new InvalidOperationException("RestoTableService is not initialized.");
            }
        }

        public void Initialize(RestoTableDTO restoTableDTO)
        {
            TableNumber = restoTableDTO.TableNumber;
            OrderNumber = restoTableDTO.OrderNumber;
            IsActive = restoTableDTO.IsActive;

            _IsInitialized = true;
        }
        public ServiceResponse<byte> Add()
        {
            try
            {
                CheckInitialized();

                db.SetProc("insRestoTable");

                db.SetParam("@tableNumber", TableNumber);

                byte identity = Convert.ToByte(db.ExecuteScalar());

                if (identity <= 0) return ServiceResponse<byte>.Fail("Resto table not added");   

                return ServiceResponse<byte>.Success(identity);
            }
            catch (Exception ex)
            {
                return ServiceResponse<byte>.Fail(ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }
        }
        public ServiceResponse<List<RestoTableDTO>> GetAll()
        {
            List<RestoTableDTO> RestoTableList = new List<RestoTableDTO>();

            try
            {
                db.SetProc("getRestoTable");

                db.ExecuteReader();

                while(db.Reader.Read())
                {
                    RestoTableList.Add(new RestoTableDTO
                    {
                        TableNumber = db.Reader.GetByte(0),
                        OrderNumber = db.Reader.GetInt64(1),
                        IsActive = db.Reader.GetBoolean(2)
                    });
                }

                if (RestoTableList.Count <= 0) return ServiceResponse<List<RestoTableDTO>>.Fail("Resto table list empty");

                return ServiceResponse<List<RestoTableDTO>>.Success(RestoTableList);
            }
            catch (Exception ex)
            {
                return ServiceResponse<List<RestoTableDTO>>.Fail(ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }
        }
        public ServiceResponse<RestoTableDTO> GetByTableNumber(byte tableNumber)
        {
            try
            {
                db.SetProc("getRestoTableByTableNumber");

                db.SetParam("@tableNumber", tableNumber);

                db.ExecuteReader();

                if (!db.Reader.Read()) return ServiceResponse<RestoTableDTO>.Fail("Resto table not found");
                
                return ServiceResponse<RestoTableDTO>.Success(new RestoTableDTO
                {
                    TableNumber = db.Reader.GetByte(0),
                    OrderNumber = db.Reader.GetInt64(1),
                    IsActive = db.Reader.GetBoolean(2)
                });
            }
            catch (Exception ex)
            {
                return ServiceResponse<RestoTableDTO>.Fail(ex.Message);
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

                db.SetProc("updRestoTable");

                db.SetParam("@tableNumber", TableNumber);
                db.SetParam("@orderNumber", OrderNumber);

                int rowsAffected = db.ExecuteNonQuery();

                if (rowsAffected == 0) return ServiceResponse<int>.Fail("Resto table not found");

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

                db.SetProc("delRestoTable");

                db.SetParam("@tableNumber", TableNumber);

                int rowsAffected = db.ExecuteNonQuery();

                if (rowsAffected == 0) return ServiceResponse<int>.Fail("Resto table not found");

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

                db.SetProc("activateRestoTable");

                db.SetParam("@tableNumber", TableNumber);

                int rowsAffected = db.ExecuteNonQuery();

                if (rowsAffected == 0) return ServiceResponse<int>.Fail("Resto table not found");
                if (rowsAffected == -1 ) return ServiceResponse<int>.Fail("Resto table is already active");

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

                db.SetProc("deactivateRestoTable");

                db.SetParam("@tableNumber", TableNumber);

                int rowsAffected = db.ExecuteNonQuery();

                if (rowsAffected == 0) return ServiceResponse<int>.Fail("Resto table not found");
                if (rowsAffected == -1) return ServiceResponse<int>.Fail("Resto table is already inactive");

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
