using RestoShared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestoWebClient
{
    public class RoleIdToRoleNameConverter 
    {
        public static string Convert(int roleId)
        {
            List<RoleDTO> list = SessionManager.RoleList;

            foreach (RoleDTO role in list)
            {
                if (role.RoleId == roleId)
                {
                    return role.RoleName;
                }
            }

            return "Unknown";
        }

        public static int Convert(string roleName)
        {
            List<RoleDTO> list = SessionManager.RoleList;

            foreach (RoleDTO role in list)
            {
                if (role.RoleName == roleName)
                {
                    return role.RoleId;
                }
            }

            return -1;
        }
    }
}