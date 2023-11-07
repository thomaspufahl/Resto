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
    internal class RoleService : IRole
    {
        private readonly DataAccess db;
        private bool _IsInitialized = false;

        public int RoleId { get; private set; }
        public string RoleName { get; private set; }
        public string RoleDescription { get; private set; }
        public bool IsActive { get; private set; }

        public RoleService()
        {
            db = new DataAccess();
        }

        private void CheckInitialized()
        {
            if (!_IsInitialized)
            {
                throw new InvalidOperationException("Role is not initialize");
            }
        }
        public void Initialize(RoleDTO roleDTO)
        {
            RoleId = roleDTO.RoleId;
            RoleName = roleDTO.RoleName;
            RoleDescription = roleDTO.RoleDescription;
            IsActive = roleDTO.IsActive;

            _IsInitialized = true;
        }
        public ServiceResponse<List<RoleDTO>> GetAll()
        {
            List<RoleDTO> roleList = new List<RoleDTO>();

            try
            {
                db.SetProc("getRole");
                db.ExecuteReader();

                while(db.Reader.Read())
                {
                    roleList.Add(new RoleDTO
                    {
                        RoleId = db.Reader.GetInt32(0),
                        RoleName = db.Reader.GetString(1),
                        RoleDescription = db.Reader.GetString(2),
                        IsActive = db.Reader.GetBoolean(3)
                    });
                }

                if (roleList.Count == 0) throw new Exception("No roles found");   

                return ServiceResponse<List<RoleDTO>>.Success(roleList);

            } catch (Exception ex)
            {
                return ServiceResponse<List<RoleDTO>>.Fail(ex.Message);
            } 
            finally
            {
                db.CloseConnection();
            }
        }

        /// <summary>
        /// Returns a RoleDTO object obtained from the database.
        /// Initializes the Role fields with the values obtained from the database.
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns>
        /// A RoleDTO obtained from the database.
        /// </returns>
        /// <exception cref="Exception"></exception>
        public ServiceResponse<RoleDTO> GetById(int roleId)
        {
            RoleDTO roleDTO = new RoleDTO();

            try
            {
                db.SetProc("getRoleById");
                db.SetParam("@roleId", roleId);
                db.ExecuteReader();
                
                if (!db.Reader.Read()) throw new Exception("Role not found");

                
                roleDTO.RoleId = db.Reader.GetInt32(0);
                roleDTO.RoleName = db.Reader.GetString(1);
                roleDTO.RoleDescription = db.Reader.GetString(2);
                roleDTO.IsActive = db.Reader.GetBoolean(3);

                Initialize(roleDTO);
                return ServiceResponse<RoleDTO>.Success(roleDTO);
            } 
            catch (Exception ex)
            {
                return ServiceResponse<RoleDTO>.Fail(ex.Message);
            } 
            finally
            {
                db.CloseConnection();
            }
        }
    }
}
