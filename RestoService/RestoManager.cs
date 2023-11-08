using RestoService;
using RestoShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestoService
{
    public class RestoManager
    {
        private static IRestoDataManager _Instance;
        public static IRestoDataManager Data
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new RestoDataManager();
                }
                return _Instance;
            }
        }
    }
}