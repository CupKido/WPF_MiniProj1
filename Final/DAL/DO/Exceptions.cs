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

    public class BadUserNameException : Exception
    {
        public string ID;
        public BadUserNameException(string id) : base() => ID = id;
        public BadUserNameException(string id, string message) :
            base(message) => ID = id;
        public BadUserNameException(string id, string message, Exception innerException) :
            base(message, innerException) => ID = id;
        public override string ToString() => base.ToString() + $", bad User Name: {ID}";
    }

    public class BadStationIdException : Exception
    {
        public int ID;
        public BadStationIdException(int id) : base() => ID = id;
        public BadStationIdException(int id, string message) :
            base(message) => ID = id;
        public BadStationIdException(int id, string message, Exception innerException) :
            base(message, innerException) => ID = id;
        public override string ToString() => base.ToString() + $", bad station id: {ID}";
    }

    public class BadLineIdException : Exception
    {
        public int ID;
        public BadLineIdException(int id) : base() => ID = id;
        public BadLineIdException(int id, string message) :
            base(message) => ID = id;
        public BadLineIdException(int id, string message, Exception innerException) :
            base(message, innerException) => ID = id;
        public override string ToString() => base.ToString() + $", bad Line id: {ID}";
    }
    public class BadTripIdException : Exception
    {
        public int ID;
        public BadTripIdException(int id) : base() => ID = id;
        public BadTripIdException(int id, string message) :
            base(message) => ID = id;
        public BadTripIdException(int id, string message, Exception innerException) :
            base(message, innerException) => ID = id;
        public override string ToString() => base.ToString() + $", bad Trip id: {ID}";
    }

    public class BadBOTIdException : Exception
    {
        public int ID;
        public BadBOTIdException(int id) : base() => ID = id;
        public BadBOTIdException(int id, string message) :
            base(message) => ID = id;
        public BadBOTIdException(int id, string message, Exception innerException) :
            base(message, innerException) => ID = id;
        public override string ToString() => base.ToString() + $", bad Trip id: {ID}";
    }

    public class XMLFileLoadCreateException : Exception
    {
        string Path;
        public XMLFileLoadCreateException(string path, string message, Exception innerException) :
            base(message, innerException) => Path = path;
        public override string ToString() => base.ToString() + $", Path: {Path}";
    }
}
