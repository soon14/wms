using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Entities;

namespace Nodes.SystemManage
{
    public interface IvUserManager
    {
        void BindGrid(List<UserEntity> users);
        void RemoveRowFromGrid(UserEntity user);
    }
}
