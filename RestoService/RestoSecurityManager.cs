using RestoService.Database;
using RestoShared;
using RestoShared.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoService
{
    internal class RestoSecurityManager : IRestoSecurityManager
    {
        private readonly DataAccess db;
        private AccessLevel _LoggedAccessLevel { get; set; }

        public EmployeeDTO LoggedUser { get; private set; }
        public bool IsLogged
        {
            get
            {
                return LoggedUser != null;
            }
        }
        public bool IsLoggedAsManager { get; private set; }

        public RestoSecurityManager()
        {
            db = new DataAccess();
            _LoggedAccessLevel = AccessLevel.MANAGER;
        }

        private void CheckLogged()
        {
            if (!IsLogged) throw new Exception("You are not logged in");
        }
        private ServiceResponse<bool> IsManager()
        {
            try
            {
                db.SetQuery($"SELECT dbo.IsManager({LoggedUser.EmployeeId})");

                bool isManager = Convert.ToBoolean(db.ExecuteScalar());

                return ServiceResponse<bool>.Success(isManager);
            }
            catch (Exception ex)
            {
                return ServiceResponse<bool>.Fail(ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }
        }

        public ServiceResponse<bool> Login(string employeeNumber)
        {
            ServiceResponse<EmployeeDTO> Response = RestoManager.Data.Employee.GeyByEmployeeNumber(employeeNumber);

            if (!Response.IsSuccess) return ServiceResponse<bool>.Fail(Response.Message);

            LoggedUser = Response.Data;
            _LoggedAccessLevel = AccessLevel.LOGGED;

            var IsManagerResponse = IsManager();
            if (!IsManagerResponse.IsSuccess) return ServiceResponse<bool>.Fail(IsManagerResponse.Message);
            IsLoggedAsManager = IsManager().Data;

            if (IsLoggedAsManager) _LoggedAccessLevel = AccessLevel.MANAGER;

            return ServiceResponse<bool>.Success(true, "Login successful. Welcome!");
        }
        public ServiceResponse<bool> Logout()
        {
            try
            {
                CheckLogged();
                LoggedUser = null;
                IsLoggedAsManager = false;
                _LoggedAccessLevel = AccessLevel.NOT_LOGGED;

                return ServiceResponse<bool>.Success(true, "Logged out successfully.");
            }
            catch (Exception ex)
            {
                return ServiceResponse<bool>.Fail(ex.Message);
            }
        }
        public bool IsAuthorized(AccessLevel accessLevel)
        {
            /*
            Debug.WriteLine($"IsAuthorized: {accessLevel} > {_LoggedAccessLevel}");
            Debug.WriteLine(accessLevel > _LoggedAccessLevel);
            */
            return accessLevel > _LoggedAccessLevel;
        }
    }
}
