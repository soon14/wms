using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Entities;

namespace Nodes.SystemManage
{
    public interface IvRoleManager
    {
        void BindGrid(List<RoleEntity> roles);
    }
}
