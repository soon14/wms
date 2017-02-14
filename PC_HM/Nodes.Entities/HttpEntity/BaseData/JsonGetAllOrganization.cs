using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Entities.HttpEntity.BaseData
{
    public class JsonGetAllOrganization:BaseResult
    {
        public JsonGetAllOrganizationResult[] result { get; set; }
    }
}
