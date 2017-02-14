using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using System.Drawing;
using Nodes.Utils;

namespace Nodes.UI
{
    public class TreeLookup : PopupContainerEdit
    {
        private TreeList _treeList = new TreeList();
        private LabelControl _labelMsg = new LabelControl();
        private TreeListNode _node = null;//选中节点
        private ButtonEdit txtFilter = new ButtonEdit();
        private PanelControl panelControl1 = new PanelControl();

        public TreeLookup() { }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            if (IsDesignMode) return;

            AppendTreelist();
        }

        protected override void OnResize(EventArgs e)
        {
            if (this.Properties.PopupControl == null) return;
            PopupContainerControl _container = this.Properties.PopupControl as PopupContainerControl;
            _container.Size = new System.Drawing.Size(this.Width - 2, 200);

            base.OnResize(e);
        }

        private void AppendTreelist()
        {
            //初始化树
            _treeList.OptionsBehavior.Editable = false;
            _treeList.OptionsSelection.EnableAppearanceFocusedCell = false;
            _treeList.OptionsView.ShowFocusedFrame = false;
            _treeList.OptionsView.ShowIndicator = false;
            _treeList.OptionsView.ShowColumns = false;
            _treeList.RowHeight = 23;
            _treeList.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            _treeList.Dock = System.Windows.Forms.DockStyle.Fill;
            _treeList.MouseUp += new MouseEventHandler(OnSelectedNode);

            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(5, 5);
            this.txtFilter.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Properties.Buttons.Clear();
            this.txtFilter.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.txtFilter.Properties.MaxLength = 50;
            this.txtFilter.Properties.NullValuePrompt = "输入查找内容，按下回车键模糊查找";
            this.txtFilter.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtFilter.Size = new System.Drawing.Size(190, 20);
            this.txtFilter.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.txtFilter_ButtonClick);
            this.txtFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFilter_KeyDown);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.txtFilter);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(190, 34);

            _labelMsg.ForeColor = Color.Green;
            _labelMsg.Dock = DockStyle.Bottom;
            _labelMsg.Text = "请选择节点";

            PopupContainerControl _container = new PopupContainerControl();
            _container.SuspendLayout();
            _container.Controls.Add(_treeList);
            _container.Controls.Add(panelControl1);
            _container.Controls.Add(_labelMsg);

            this.Properties.PopupSizeable = false;
            this.Properties.ShowPopupCloseButton = false;
            this.Properties.ShowPopupShadow = false;
            this.Properties.PopupControl = _container;

            _container.ResumeLayout();
            this.OnResize(null);
        }
        /// <summary>
        /// 节点是否任意选择
        /// </summary>
        public bool AnySelect { get; set; }

        void OnSelectedNode(object sender, MouseEventArgs e)
        {
            TreeListHitInfo _hitinfo = _treeList.CalcHitInfo(e.Location);
            if (_hitinfo.Column != null && _hitinfo.Node != null)
            {
                //TreeListNode node = _hitinfo.Node;
                this._node = _hitinfo.Node;
                if (this._node != null)
                {

                    if (this._node.HasChildren && AnySelect == false)
                    {
                        _labelMsg.Text = "请选中叶子节点";
                        _labelMsg.ForeColor = Color.Red;
                        return;
                    }

                    _labelMsg.ForeColor = Color.Green;

                    InnerValue = ConvertUtil.ToString(this._node.GetValue(ValueFieldName));
                    this.EditValue = this._node.GetValue(DisplayFieldName);
                    //selectNodeDesc = string.Empty;
                    //this.EditValue = GetAllDisplayField(node);
                }
                this.ClosePopup();
            }
        }

        string selectNodeDesc = string.Empty;
        public string GetAllDisplayField(TreeListNode node)
        {
            if (node == null || node.ParentNode == null) return selectNodeDesc;
            if (string.IsNullOrEmpty(selectNodeDesc))
            {
                selectNodeDesc = node.GetValue(DisplayFieldName).ToString();
            }
            else
            {
                selectNodeDesc = node.GetValue(DisplayFieldName).ToString() + "/" + selectNodeDesc;
            }
            if (node.ParentNode != null && node.ParentNode.Level > 0)
            {
                GetAllDisplayField(node.ParentNode);
            }
            return selectNodeDesc;
        }

        /// <summary>
        /// 获取指定字段的值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetFieldValue(string name)
        {
            if (_node == null)
                return null;

            return ConvertUtil.ToString(this._node.GetValue(name));
        }

        public object DataSource
        {
            set
            {
                if (value == null)
                    return;
                IntPtr handle = _treeList.Handle;
                _treeList.BindingContext = new System.Windows.Forms.BindingContext();
                _treeList.DataSource = value;
            }
            get
            {
                return _treeList.DataSource;
            }
        }

        /// <summary>
        /// 主ID
        /// </summary>
        public string KeyFieldName
        {
            get
            {
                return _treeList.KeyFieldName;
            }
            set
            {
                _treeList.KeyFieldName = value;
            }
        }

        public string DisplayFieldName
        {
            get;
            set;
        }

        public string ValueFieldName
        {
            get;
            set;
        }
        /// <summary>
        /// ParentID
        /// </summary>
        public string ParentFieldName
        {
            set
            {
                _treeList.ParentFieldName = value;
            }
        }

        public void ExpandTree()
        {
            _treeList.ExpandAll();
        }

        public string InnerValue
        {
            get;
            set;
        }

        public void SetFocusedByValue(object val)
        {
            if (val == null)
            {
                _node = null;
                EditValue = null;
                InnerValue = null;
            }
            else
            {
                TreeListNode node = _treeList.FindNodeByFieldValue(ValueFieldName, val);
                if (node != null)
                {
                    this._node = _treeList.FocusedNode = node;
                    this.InnerValue = ConvertUtil.ObjectToNull(this._node.GetValue(ValueFieldName));
                    this.EditValue = this._node.GetValue(DisplayFieldName);
                }
            }
        }

        public void SetFocusedByID(int id)
        {
            TreeListNode node = _treeList.FindNodeByKeyID(id);
            if (node != null)
            {
                this._node = _treeList.FocusedNode = node;
                this.InnerValue = ConvertUtil.ObjectToNull(this._node.GetValue(ValueFieldName));
                this.EditValue = this._node.GetValue(DisplayFieldName);
            }
        }

        public string BindFields
        {
            set
            {
                string[] _fields = value.Split(new Char[] { ';' });
                int _index = 0;
                foreach (string _field in _fields)
                {
                    string[] _s = _field.Split(new Char[] { ',' });
                    TreeListColumn _treeColumn = new TreeListColumn();
                    _treeColumn.FieldName = _s[0];
                    _treeColumn.Caption = _s[1];
                    _treeColumn.Visible = true;
                    _treeColumn.VisibleIndex = _index;

                    _index++;
                    _treeList.Columns.AddRange(new TreeListColumn[] { _treeColumn });
                }
            }
        }

        private void txtFilter_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            txtFilter.Text = string.Empty;
        }

        private void txtFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DoSearch();
            }
        }

        private void DoSearch()
        {
            if (string.IsNullOrEmpty(txtFilter.Text.Trim()))
                return;

            foreach (TreeListNode n in _treeList.Nodes)
            {
                TreeListNode node = TreeListUtil.SearchNode(n, txtFilter.Text.Trim(), DisplayFieldName);
                if (node != null)
                    _treeList.FocusedNode = node;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DoSearch();
        }
    }
}
