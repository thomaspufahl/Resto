using RestoService;
using RestoShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestoWebClient.InstanceManager
{
    public class RestoDataManager
    {
        private static IRestoManager _Instance;
        public static IRestoManager Data
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new RestoManager();
                }
                return _Instance;
            }
        }
    }
}