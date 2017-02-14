using System;
using System.Xml.Serialization;

namespace Nodes.Entities
{
    [Serializable]
    public class VersionInfo
    {
        public string ID { get; set; }
        public string VER { get; set; }
        public string UPDATE { get; set; }

        [XmlIgnore]
        public string URL { get; set; }

        [XmlIgnore]
        public string REMARK { get; set; }

        public int UPDATE_FLAG { get; set; }

        public string WH_CODE { get; set; }
    }
}
