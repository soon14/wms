using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.BaseData
{
    public class JsonGetAllLocationByZoneResult
    {
        #region 0-10
        public string floorCode
        {
            get;
            set;
        }
        public string lcName
        {
            get;
            set;
        }
        public string cellCode
        {
            get;
            set;
        }
        public string znName
        {
            get;
            set;
        }
        public string sortOrder
        {
            get;
            set;
        }
        public string lcCode
        {
            get;
            set;
        }
        public string upperSize
        {
            get;
            set;
        }
        public string lowerSize
        {
            get;
            set;
        }
        public string passageCode
        {
            get;
            set;
        }
        public string whCode
        {
            get;
            set;
        }
        #endregion

        #region 11-14
        public string whName
        {
            get;
            set;
        }
        public string isActive
        {
            get;
            set;
        }
        public string znCode
        {
            get;
            set;
        }
        public string shelfCode
        {
            get;
            set;
        }
        #endregion
    }
}
