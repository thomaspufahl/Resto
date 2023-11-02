using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoService.Database
{
    internal class Context
    {
        private static readonly Context _Instance = new Context();
        public DataAccess Db { get; private set; }

        private Context()
        {
            Db = new DataAccess();
        }

        public static Context Instance
        {
            get
            {
                return _Instance;
            }
        }
    }
}
