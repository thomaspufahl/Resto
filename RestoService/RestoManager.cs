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
        private static IRestoDataManager _DataInstance;
        private static IRestoSecurityManager _SecurityInstance;
        public static IRestoDataManager Data
        {
            get
            {
                if (_DataInstance == null)
                {
                    _DataInstance = new RestoDataManager();
                }
                return _DataInstance;
            }
        }
        public static IRestoSecurityManager Security
        {
            get
            {
                if (_SecurityInstance == null)
                {
                    _SecurityInstance = new RestoSecurityManager();
                }
                return _SecurityInstance;
            }
        }
    }
}