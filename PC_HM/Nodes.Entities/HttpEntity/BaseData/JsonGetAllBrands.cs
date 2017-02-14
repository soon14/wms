using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.BaseData
{
    public class JsonGetAllBrands:BaseResult
    {
        public JsonGetAllBrandsResult[] result { get; set; }
    }
}
