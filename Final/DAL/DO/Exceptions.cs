using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
 
        public class BadBusIdException : Exception
        {
            public int ID;
            public BadBusIdException(int id) : base() => ID = id;
            public BadBusIdException(int id, string message) :
                base(message) => ID = id;
            public BadBusIdException(int id, string message, Exception innerException) :
                base(message, innerException) => ID = id;
            public override string ToString() => base.ToString() + $", bad BUS id: {ID}";
        }
    
}
