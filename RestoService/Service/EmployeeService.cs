using RestoService.Database;
using RestoShared;
using RestoShared.DTO;
using RestoShared.ITable;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoService.Service
{
    

    internal class EmployeeService : IEmployee
    {
        private readonly DataAccess db;
        private bool _IsInitialized = false;

        public int EmployeeId { get; private set; }
        public string EmployeeNumber { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public int RoleId { get; private set; }

        public bool IsActive { get; private set; }

        public EmployeeService()
        {
            db = new DataAccess();
        }

        private void CheckInitialized()
        {
            if (!_IsInitialized)
            {
                throw new InvalidOperationException("Employee is not initialize");
            }
        }
        public void Initialize(EmployeeDTO employeeDTO)
        {
            EmployeeId = employeeDTO.EmployeeId;

            EmployeeNumber = employeeDTO.EmployeeNumber;

            FirstName = employeeDTO.FirstName;

            LastName = employeeDTO.LastName;

            RoleId = employeeDTO.RoleId;

            IsActive = employeeDTO.IsActive;

            _IsInitialized = true;
        }
        public ServiceResponse<int> Add()
        {
            try
            {
                CheckInitialized();

                db.SetProc("insEmployee");

                db.SetParam("@employeeNumber", EmployeeNumber);
                db.SetParam("@firstName", FirstName);
                db.SetParam("@lastName", LastName);
                db.SetParam("@roleId", RoleId);

                int identity = Convert.ToInt32(db.ExecuteScalar());
                
                Console.WriteLine(identity);
                if (identity <= 0) return ServiceResponse<int>.Fail("Employee not added");

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
        public ServiceResponse<List<EmployeeDTO>> GetAll()
        {
            List<EmployeeDTO> EmployeeList = new List<EmployeeDTO>();

            try
            {
                db.SetProc("getEmployee");
                db.ExecuteReader();

                while (db.Reader.Read())
                {
                    EmployeeList.Add(new EmployeeDTO
                    {
                        EmployeeId = db.Reader.GetInt32(0),
                        EmployeeNumber = db.Reader.GetString(1),
                        FirstName = db.Reader.GetString(2),
                        LastName = db.Reader.GetString(3),
                        RoleId = db.Reader.GetInt32(4),
                        IsActive = db.Reader.GetBoolean(5)
                    });
                }
                
                if (EmployeeList.Count == 0) return ServiceResponse<List<EmployeeDTO>>.Fail("Employee list empty");

                return ServiceResponse<List<EmployeeDTO>>.Success(EmployeeList);
            }
            catch (Exception ex)
            {
                return ServiceResponse<List<EmployeeDTO>>.Fail(ex.Message);
            } 
            finally
            {
                db.CloseConnection();
            }
        }
        public ServiceResponse<EmployeeDTO> GetById(int employeeId)
        {
            EmployeeDTO employeeDTO = new EmployeeDTO();

            try
            {
                db.SetProc("getEmployeeById");
                db.SetParam("@employeeId", employeeId);
                db.ExecuteReader();

                if (!db.Reader.Read()) return ServiceResponse<EmployeeDTO>.Fail("Employee not found");

                employeeDTO.EmployeeId = db.Reader.GetInt32(0);
                employeeDTO.EmployeeNumber = db.Reader.GetString(1);
                employeeDTO.FirstName = db.Reader.GetString(2);
                employeeDTO.LastName = db.Reader.GetString(3);
                employeeDTO.RoleId = db.Reader.GetInt32(4);
                employeeDTO.IsActive = db.Reader.GetBoolean(5);

                return ServiceResponse<EmployeeDTO>.Success(employeeDTO);
            }
            catch (Exception ex)
            {               
                return ServiceResponse<EmployeeDTO>.Fail(ex.Message);
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

                db.SetProc("updEmployee");

                db.SetParam("@employeeId", EmployeeId);
                db.SetParam("@firstName", FirstName);
                db.SetParam("@lastName", LastName);
                db.SetParam("@roleId", RoleId);

                int rowsAffected = db.ExecuteNonQuery();

                if (rowsAffected == 0) return ServiceResponse<int>.Fail("Employee not found");

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

                db.SetProc("delEmployee");

                db.SetParam("@employeeId", EmployeeId);

                int rowsAffected = db.ExecuteNonQuery();

                if (rowsAffected == 0) return ServiceResponse<int>.Fail("Employee not found");

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

                db.SetProc("activateEmployee");

                db.SetParam("@employeeId", EmployeeId);

                int rowsAffected = db.ExecuteNonQuery();

                if (rowsAffected == 0) return ServiceResponse<int>.Fail("Employee not found");

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

                db.SetProc("deactivateEmployee");

                db.SetParam("@employeeId", EmployeeId);

                int rowsAffected = db.ExecuteNonQuery();

                if (rowsAffected == 0) return ServiceResponse<int>.Fail("Employee not found");

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
