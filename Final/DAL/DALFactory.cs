using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    static public class DALFactory
    {
        static readonly private IDAL instance = new MyDAL();
        static  public IDAL GetDAL()
        {
            return instance;
        }
    }
}
