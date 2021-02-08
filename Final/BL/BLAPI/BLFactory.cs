using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public static class BLFactory 
    {
        public static IBL GetBL(int type)
        {
            switch(type)
            {
                case 1:
                    return new MyBL();
                    break;
                default:
                    return new MyBL();
            }
        }
    }
}
