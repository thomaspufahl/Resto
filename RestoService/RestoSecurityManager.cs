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
        public AccessLevel LoggedAccessLevel { get; set; }

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
            LoggedAccessLevel = AccessLevel.NOT_LOGGED;
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
            LoggedAccessLevel = AccessLevel.LOGGED;

            var IsManagerResponse = IsManager();
            if (!IsManagerResponse.IsSuccess) return ServiceResponse<bool>.Fail(IsManagerResponse.Message);
            IsLoggedAsManager = IsManager().Data;

            if (IsLoggedAsManager) LoggedAccessLevel = AccessLevel.MANAGER;

            return ServiceResponse<bool>.Success(true, "Login successful. Welcome!");
        }
        public ServiceResponse<bool> Logout()
        {
            try
            {
                CheckLogged();
                LoggedUser = null;
                IsLoggedAsManager = false;
                LoggedAccessLevel = AccessLevel.NOT_LOGGED;

                return ServiceResponse<bool>.Success(true, "Logged out successfully.");
            }
            catch (Exception ex)
            {
                return ServiceResponse<bool>.Fail(ex.Message);
            }
        }
        public bool IsAuth(AccessLevel accessLevelToValidate)
        {
            /*
            Debug.WriteLine($"IsAuthorized: {accessLevelToValidate} > {LoggedAccessLevel}");
            Debug.WriteLine(accessLevelToValidate > LoggedAccessLevel);
            */
            return accessLevelToValidate > LoggedAccessLevel;
        }
        public bool IsAuth(AccessLevel loggedAccessLevel, AccessLevel accessLevelToValidate)
        {
            return accessLevelToValidate > loggedAccessLevel;
        }
    }
}
