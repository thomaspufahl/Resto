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

        public int RoleId { get; private set; }
        public string RoleName { get; private set; }
        public string RoleDescription { get; private set; }
        public bool IsActive { get; private set; }

        public RoleService()
        {
            db = new DataAccess();
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

                if (roleList.Count == 0) throw new Exception("Role list empty");   

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
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns>
        /// A RoleDTO obtained from the database.
        /// </returns>
        /// <exception cref="Exception"></exception>
        public ServiceResponse<RoleDTO> GetById(int roleId)
        {
            try
            {
                db.SetProc("getRoleById");
                db.SetParam("@roleId", roleId);
                db.ExecuteReader();
                
                if (!db.Reader.Read()) throw new Exception("Role not found");

                return ServiceResponse<RoleDTO>.Success
                (
                    new RoleDTO
                    {
                        RoleId = db.Reader.GetInt32(0),
                        RoleName = db.Reader.GetString(1),
                        RoleDescription = db.Reader.GetString(2),
                        IsActive = db.Reader.GetBoolean(3)
                    }
                );
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
