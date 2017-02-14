using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Entities;

namespace Nodes.SystemManage
{
    public interface IvUserEdit
    {
        void BindRoleToGrid(List<RoleEntity> roles);
        UserEntity PrepareSave();
        bool IsCreateNew();
    }
}
