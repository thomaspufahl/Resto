using RestoService.Database;
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

        public List<EmployeeDTO> GetAll()
        {
            List<EmployeeDTO> EmployeeList = new List<EmployeeDTO>();

            try
            {
                db.SetProc("getEmployee");
                db.Execute();

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

                return EmployeeList;

            }
            catch (Exception ex)
            {
                throw new Exception("Error at getAll method", ex);
            } finally
            {
                db.CloseConnection();
            }
        }


        public int Activate()
        {
            throw new NotImplementedException();
        }

        public int Add()
        {
            CheckInitialized();
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public int Deactivate()
        {
            throw new NotImplementedException();
        }

        public EmployeeDTO GetById()
        {
            throw new NotImplementedException();
        }


        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
