using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace Nodes.WMS.Shares
{
    public partial class LookupEditInboundStatus : DevExpress.XtraEditors.PopupContainerEdit
    {
        public LookupEditInboundStatus()
        {
            InitializeComponent();
        }

        public LookupEditInboundStatus(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
