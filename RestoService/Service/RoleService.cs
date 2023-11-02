using RestoService.Database;
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

        public RoleService()
        {
            db = Context.Instance.Db;
        }

        public List<RoleDTO> GetAll()
        {
            List<RoleDTO> roleList = new List<RoleDTO>();

            try
            {
                db.SetProc("getRole");
                db.Execute();

                while(db.Reader.Read())
                {
                    roleList.Add(new RoleDTO
                    {
                        RoleId = db.Reader.GetInt32(0),
                        Name = db.Reader.GetString(1),
                        Description = db.Reader.GetString(2)
                    });
                }

                return roleList;

            } catch (Exception ex)
            {
                throw new Exception("Error at getAll method", ex);
            }

        }
    }
}
