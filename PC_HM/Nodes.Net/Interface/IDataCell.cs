using System;
using System.Collections.Generic;
using System.Text;

namespace Nodes.Net
{
    public interface IDataCell
    {
        byte[] ToBuffer();

        IDataCell FromBuffer(byte[] data);
    }
}
