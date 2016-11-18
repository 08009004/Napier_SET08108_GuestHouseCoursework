using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Custom imports:
using System.Xml.Serialization;

namespace Program
{
    // [Serializable(DoPrivate = true, DoProtected = true)]
    // http://stackoverflow.com/questions/802711/serializing-private-member-data
    [Serializable]
    public abstract class XmlWritable
    {
        public XmlWritable() { }

//        public abstract String ToCsv();

    }
}
