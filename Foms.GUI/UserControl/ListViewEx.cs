﻿//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright ?2006,2007 OCTO Technology & OXUS Development Network
//
//This program is free software; you can redistribute it and/or modify
//it under the terms of the GNU Lesser General Public License as published by
//the Free Software Foundation; either version 2 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Lesser General Public License for more details.
//
//You should have received a copy of the GNU Lesser General Public License along
//with this program; if not, write to the Free Software Foundation, Inc.,
//51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
//
//
// Licence : http://www.octopusnetwork.org/OverviewLicence.aspx
//
// Website : http://www.octopusnetwork.org
// Business contact: business(at)octopusnetwork.org
// Technical contact email : tech(at)octopusnetwork.org 

using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Foms.GUI.UserControl
{
    /// <summary>
    /// Event Handler for SubItem events
    /// </summary>
    public delegate void SubItemEventHandler(object sender, SubItemEventArgs e);
    /// <summary>
    /// Event Handler for SubItemEndEditing events
    /// </summary>
    public delegate void SubItemEndEditingEventHandler(object sender, SubItemEndEditingEventArgs e);

    ///	<summary>
    ///	Inherited ListView to allow in-place editing of subitems
    ///	</summary>
    public class ListViewEx : System.Windows.Forms.ListView
    {
        #region Interop structs, imports and constants
        /// <summary>
        /// MessageHeader for WM_NOTIFY
        /// </summary>
        private struct NMHDR
        {
            public IntPtr hwndFrom;
            public Int32 idFrom;
            public Int32 code;
        }


        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wPar, IntPtr lPar);
        [DllImport("user32.dll", CharSet = CharSet.Ansi)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int len, ref	int[] order);

        // ListView messages
        private const int LVM_FIRST = 0x1000;
        private const int LVM_GETCOLUMNORDERARRAY = (LVM_FIRST + 59);

        // Windows Messages that will abort editing
        private const int WM_HSCROLL = 0x114;
        private const int WM_VSCROLL = 0x115;
        private const int WM_SIZE = 0x05;
        private const int WM_NOTIFY = 0x4E;

        private const int HDN_FIRST = -300;
        private const int HDN_BEGINDRAG = (HDN_FIRST - 10);
        private const int HDN_ITEMCHANGINGA = (HDN_FIRST - 0);
        private const int HDN_ITEMCHANGINGW = (HDN_FIRST - 20);
        #endregion
        private IContainer components;
        private ToolStripMenuItem menuItemCopy;
        private ContextMenuStrip contextMenuStripCopyToClipBoard;

        public event SubItemEventHandler SubItemClicked;
        public event SubItemEventHandler SubItemBeginEditing;
        public event SubItemEndEditingEventHandler SubItemEndEditing;


        public ListViewEx()
        {
            // This	call is	required by	the	Windows.Forms Form Designer.
            InitializeComponent();
            menuItemCopy.Click += new System.EventHandler(this.contextMenuStripCopyToClipBoard_Click);

            base.View = View.Details;
            base.ContextMenuStrip = contextMenuStripCopyToClipBoard;

        }

        ///	<summary>
        ///	Clean up any resources being used.
        ///	</summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component	Designer generated code
        ///	<summary>
        ///	Required method	for	Designer support - do not modify 
        ///	the	contents of	this method	with the code editor.
        ///	</summary>
        private void InitializeComponent()
        {
           this.components = new System.ComponentModel.Container();
           System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListViewEx));
           this.contextMenuStripCopyToClipBoard = new System.Windows.Forms.ContextMenuStrip(this.components);
           this.menuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
           this.contextMenuStripCopyToClipBoard.SuspendLayout();
           this.SuspendLayout();
           // 
           // contextMenuStripCopyToClipBoard
           // 
           this.contextMenuStripCopyToClipBoard.AccessibleDescription = null;
           this.contextMenuStripCopyToClipBoard.AccessibleName = null;
           resources.ApplyResources(this.contextMenuStripCopyToClipBoard, "contextMenuStripCopyToClipBoard");
           this.contextMenuStripCopyToClipBoard.BackgroundImage = null;
           this.contextMenuStripCopyToClipBoard.Font = null;
           this.contextMenuStripCopyToClipBoard.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemCopy});
           this.contextMenuStripCopyToClipBoard.Name = "contextMenuStripCopyToClipBoard";
           // 
           // menuItemCopy
           // 
           this.menuItemCopy.AccessibleDescription = null;
           this.menuItemCopy.AccessibleName = null;
           resources.ApplyResources(this.menuItemCopy, "menuItemCopy");
           this.menuItemCopy.BackgroundImage = null;
           this.menuItemCopy.Name = "menuItemCopy";
           this.menuItemCopy.ShortcutKeyDisplayString = null;
           // 
           // ListViewEx
           // 
           this.AccessibleDescription = null;
           this.AccessibleName = null;
           resources.ApplyResources(this, "$this");
           this.BackgroundImage = null;
           this.Font = null;
           this.contextMenuStripCopyToClipBoard.ResumeLayout(false);
           this.ResumeLayout(false);

        }
        #endregion

        private bool _doubleClickActivation = false;
        /// <summary>
        /// Is a double click required to start editing a cell?
        /// </summary>
        public bool DoubleClickActivation
        {
            get { return _doubleClickActivation; }
            set { _doubleClickActivation = value; }
        }


        /// <summary>
        /// Retrieve the order in which columns appear
        /// </summary>
        /// <returns>Current display order of column indices</returns>
        public int[] GetColumnOrder()
        {
            IntPtr lPar = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(int)) * Columns.Count);

            IntPtr res = SendMessage(Handle, LVM_GETCOLUMNORDERARRAY, new IntPtr(Columns.Count), lPar);
            if (res.ToInt32() == 0)	// Something went wrong
            {
                Marshal.FreeHGlobal(lPar);
                return null;
            }

            int[] order = new int[Columns.Count];
            Marshal.Copy(lPar, order, 0, Columns.Count);

            Marshal.FreeHGlobal(lPar);

            return order;
        }


        /// <summary>
        /// Find ListViewItem and SubItem Index at position (x,y)
        /// </summary>
        /// <param name="x">relative to ListView</param>
        /// <param name="y">relative to ListView</param>
        /// <param name="item">Item at position (x,y)</param>
        /// <returns>SubItem index</returns>
        public int GetSubItemAt(int x, int y, out ListViewItem item)
        {
            item = this.GetItemAt(x, y);

            if (item != null)
            {
                int[] order = GetColumnOrder();
                Rectangle lviBounds;
                int subItemX;

                lviBounds = item.GetBounds(ItemBoundsPortion.Entire);
                subItemX = lviBounds.Left;
                for (int i = 0; i < order.Length; i++)
                {
                    ColumnHeader h = this.Columns[order[i]];
                    if (x < subItemX + h.Width)
                    {
                        return h.Index;
                    }
                    subItemX += h.Width;
                }
            }

            return -1;
        }


        /// <summary>
        /// Get bounds for a SubItem
        /// </summary>
        /// <param name="Item">Target ListViewItem</param>
        /// <param name="SubItem">Target SubItem index</param>
        /// <returns>Bounds of SubItem (relative to ListView)</returns>
        public Rectangle GetSubItemBounds(ListViewItem Item, int SubItem)
        {
            int[] order = GetColumnOrder();

            Rectangle subItemRect = Rectangle.Empty;
            if (SubItem >= order.Length)
                throw new IndexOutOfRangeException("SubItem " + SubItem + " out of range");

            if (Item == null)
                throw new ArgumentNullException("Item");

            Rectangle lviBounds = Item.GetBounds(ItemBoundsPortion.Entire);
            int subItemX = lviBounds.Left;

            ColumnHeader col;
            int i;
            for (i = 0; i < order.Length; i++)
            {
                col = this.Columns[order[i]];
                if (col.Index == SubItem)
                    break;
                subItemX += col.Width;
            }
            subItemRect = new Rectangle(subItemX, lviBounds.Top, this.Columns[order[i]].Width, lviBounds.Height);
            return subItemRect;
        }
        protected override void WndProc(ref	Message msg)
        {
            switch (msg.Msg)
            {
                // Look	for	WM_VSCROLL,WM_HSCROLL or WM_SIZE messages.
                case WM_VSCROLL:
                case WM_HSCROLL:
                case WM_SIZE:
                    EndEditing(false);
                    break;
                case WM_NOTIFY:
                    // Look for WM_NOTIFY of events that might also change the
                    // editor's position/size: Column reordering or resizing
                    NMHDR h = (NMHDR)Marshal.PtrToStructure(msg.LParam, typeof(NMHDR));
                    if (h.code == HDN_BEGINDRAG ||
                        h.code == HDN_ITEMCHANGINGA ||
                        h.code == HDN_ITEMCHANGINGW)
                        EndEditing(false);
                    break;
            }

            base.WndProc(ref msg);
        }
        /*protected override void OnMouseDown(MouseEventArgs e)
        {
           if (e.Button.Equals(MouseButtons.Right))
           {
              contextMenuStripCopyToClipBoard.Show(this, e.Location);
           }

        }*/
        protected void contextMenuStripCopyToClipBoard_Click(object sender, EventArgs e)
        {
           _CopyData();
           
        }
       private void _CopyData()
       {
          ListView lv = this;
          StringBuilder buffer = new StringBuilder();
          for (int i = 0; i < lv.Columns.Count; i++)
          {
             buffer.Append(lv.Columns[i].Text);
             buffer.Append("\t");
          }
          buffer.Append("\n");

          for (int i = 0; i < lv.Items.Count; i++)
          {
             for (int j = 0; j < lv.Items[i].SubItems.Count; j++)
             {
                buffer.Append(lv.Items[i].SubItems[j].Text);
                buffer.Append("\t");
             }
             buffer.Append("\n");
          }

          Clipboard.SetText(buffer.ToString());
          contextMenuStripCopyToClipBoard.Visible = false;
       }
        #region Initialize editing depending of DoubleClickActivation property
        protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (DoubleClickActivation)
            {
                return;
            }

            EditSubitemAt(new Point(e.X, e.Y));
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);

            if (!DoubleClickActivation)
            {
                return;
            }

            Point pt = this.PointToClient(Cursor.Position);

            EditSubitemAt(pt);
        }

        ///<summary>
        /// Fire SubItemClicked
        ///</summary>
        ///<param name="p">Point of click/doubleclick</param>
        private void EditSubitemAt(Point p)
        {
            ListViewItem item;
            int idx = GetSubItemAt(p.X, p.Y, out item);
            if (idx >= 0)
            {
                OnSubItemClicked(new SubItemEventArgs(item, idx));
            }
        }

        #endregion

        #region In-place editing functions
        // The control performing the actual editing
        private Control _editingControl;
        // The LVI being edited
        private ListViewItem _editItem;
        // The SubItem being edited
        private int _editSubItem;

        protected void OnSubItemBeginEditing(SubItemEventArgs e)
        {
            if (SubItemBeginEditing != null)
                SubItemBeginEditing(this, e);
        }
        protected void OnSubItemEndEditing(SubItemEndEditingEventArgs e)
        {
            if (SubItemEndEditing != null)
                SubItemEndEditing(this, e);
        }
        protected void OnSubItemClicked(SubItemEventArgs e)
        {
            if (SubItemClicked != null)
                SubItemClicked(this, e);
        }


        /// <summary>
        /// Begin in-place editing of given cell
        /// </summary>
        /// <param name="c">Control used as cell editor</param>
        /// <param name="Item">ListViewItem to edit</param>
        /// <param name="SubItem">SubItem index to edit</param>
        public void StartEditing(Control c, ListViewItem Item, int SubItem)
        {
            OnSubItemBeginEditing(new SubItemEventArgs(Item, SubItem));

            Rectangle rcSubItem = GetSubItemBounds(Item, SubItem);

            if (rcSubItem.X < 0)
            {
                // Left edge of SubItem not visible - adjust rectangle position and width
                rcSubItem.Width += rcSubItem.X;
                rcSubItem.X = 0;
            }
            if (rcSubItem.X + rcSubItem.Width > this.Width)
            {
                // Right edge of SubItem not visible - adjust rectangle width
                rcSubItem.Width = this.Width - rcSubItem.Left;
            }

            // Subitem bounds are relative to the location of the ListView!
            rcSubItem.Offset(Left, Top);

            // In case the editing control and the listview are on different parents,
            // account for different origins
            Point origin = new Point(0, 0);
            Point lvOrigin = this.Parent.PointToScreen(origin);
            Point ctlOrigin = c.Parent.PointToScreen(origin);

            rcSubItem.Offset(lvOrigin.X - ctlOrigin.X, lvOrigin.Y - ctlOrigin.Y);

            // Position and show editor
            c.Bounds = rcSubItem;
            c.Text = Item.SubItems[SubItem].Text;
            c.Visible = true;
            c.BringToFront();
            c.Focus();

            _editingControl = c;
            _editingControl.Leave += new EventHandler(_editControl_Leave);
            _editingControl.KeyPress += new KeyPressEventHandler(_editControl_KeyPress);

            _editItem = Item;
            _editSubItem = SubItem;
        }


        private void _editControl_Leave(object sender, EventArgs e)
        {
            // cell editor losing focus
            EndEditing(true);
        }

        private void _editControl_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)(int)Keys.Escape:
                    {
                        EndEditing(false);
                        break;
                    }

                case (char)(int)Keys.Enter:
                    {
                        EndEditing(true);
                        break;
                    }
            }
        }

        /// <summary>
        /// Accept or discard current value of cell editor control
        /// </summary>
        /// <param name="AcceptChanges">Use the _editingControl's Text as new SubItem text or discard changes?</param>
        public void EndEditing(bool AcceptChanges)
        {
            if (_editingControl == null)
                return;

            SubItemEndEditingEventArgs e = new SubItemEndEditingEventArgs(
                _editItem,		// The item being edited
                _editSubItem,	// The subitem index being edited
                AcceptChanges ?
                    _editingControl.Text :	// Use editControl text if changes are accepted
                    _editItem.SubItems[_editSubItem].Text,	// or the original subitem's text, if changes are discarded
                !AcceptChanges	// Cancel?
            );

            OnSubItemEndEditing(e);

            _editItem.SubItems[_editSubItem].Text = e.DisplayText;
            _editingControl.Leave -= new EventHandler(_editControl_Leave);
            _editingControl.KeyPress -= new KeyPressEventHandler(_editControl_KeyPress);

            _editingControl.Visible = false;

            _editingControl = null;
            _editItem = null;
            _editSubItem = -1;
        }
        #endregion
    }
    /// <summary>
    /// Event Args for SubItemClicked event
    /// </summary>
    public class SubItemEventArgs : EventArgs
    {
        public SubItemEventArgs(ListViewItem item, int subItem)
        {
            _subItemIndex = subItem;
            _item = item;
        }
        private int _subItemIndex = -1;
        private ListViewItem _item = null;
        public int SubItem
        {
            get { return _subItemIndex; }
        }
        public ListViewItem Item
        {
            get { return _item; }
        }

    }


    /// <summary>
    /// Event Args for SubItemEndEditingClicked event
    /// </summary>
    public class SubItemEndEditingEventArgs : SubItemEventArgs
    {
        private string _text = string.Empty;
        private bool _cancel = true;

        public SubItemEndEditingEventArgs(ListViewItem item, int subItem, string display, bool cancel) :
            base(item, subItem)
        {
            _text = display;
            _cancel = cancel;
        }
        public string DisplayText
        {
            get { return _text; }
            set { _text = value; }
        }
        public bool Cancel
        {
            get { return _cancel; }
            set { _cancel = value; }
        }
    }
}
