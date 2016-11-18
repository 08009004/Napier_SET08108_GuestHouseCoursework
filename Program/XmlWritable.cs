using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Custom imports:
using System.Xml.Serialization;

namespace Program
{
    [Serializable]
    abstract class XmlWritable
    {
        public XmlWritable() { }

        public abstract String ToCsv();

    }
}
