using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALAPI
{
    class DALconfig
    {
        static class DALConfig
        {
            public class DALPac
            {
                public string Name;
                public string PkgName;
                public string NameSpace;
                public string ClassName;
            }
            internal static string DLName;
            internal static Dictionary<string, DALPac> DLPackages;

            static DALConfig()
            {
                XElement dlcon = XElement.Load(@"config.xml");
                DLName = dlcon.Element("dl").Value;
                DLPackages = (from pkg in dlcon.Element("dl-packages").Elements()
                              let tmp1 = pkg.Attribute("namespace")
                              let nameSpace = tmp1 == null ? "DL" : tmp1.Value
                              let tmp2 = pkg.Attribute("class")
                              let className = tmp2 == null ? pkg.Value : tmp2.Value
                              select new DALPac()
                              {
                                  Name = "" + pkg.Name,
                                  PkgName = pkg.Value,
                                  NameSpace = nameSpace,
                                  ClassName = className
                              })
                               .ToDictionary(p => "" + p.Name, p => p);
            }
        }

        [Serializable]
        public class DALConEx : Exception
        {
            public DALConEx(string message) : base(message) { }
            public DALConEx(string message, Exception inner) : base(message, inner) { }
        }
    }
}
