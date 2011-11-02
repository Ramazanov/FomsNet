using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Foms.ExceptionsHandler;
using Foms.GUI.UserControl;
using Foms.Services;
using Foms.CoreDomain;
 
namespace Foms.GUI.Configuration
{
    public partial class FrmRoles : SweetBaseForm
    {
        private readonly Form _mdiParent;
        private readonly List<MenuObject> _menuItems;
        private Role _role;
        
        public FrmRoles(Form pMdiParent)
        {
            InitializeComponent();
            _mdiParent = pMdiParent;
            _menuItems = new List<MenuObject>();
            _menuItems = ServicesProvider.GetInstance().GetRoleServices().GetMenuList();
            InitListView();
            InitActionTree((Role)listViewRoles.Items[0].Tag);
            _InitActionsTreeView((Role)listViewRoles.Items[0].Tag);
            UpdateControlStatus(false);
            _role = new Role();
            btnNew.Text = GetString("new");
        }

        public void EraseRole()
        {
            _role = new Role();
            txtTitle.Text = _role.RoleName;
            txtDescription.Text = _role.Description;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (_role.Id == 0) SaveRole();
            else UpdateRole();

            InitListView();
        }

        private void UpdateRole()
        {
            _role.RoleName = txtTitle.Text;
            _role.Description = txtDescription.Text;

            try
            {
                RoleServices.RoleErrors roleErrors = ServicesProvider.GetInstance().GetRoleServices().UpdateRole(_role);
                if (roleErrors.FindError)
                {
                    MessageBox.Show(GetString(roleErrors.ErrorCode), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                UpdateControlStatus(true);
                User.CurrentUser = ServicesProvider.GetInstance().GetUserServices().Find(User.CurrentUser.Id, true);
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();  
            }
            EraseRole();
        }

        private void SaveRole()
        {
            _role.RoleName = txtTitle.Text;
            _role.Description = txtDescription.Text;

            try
            {
                RoleServices.RoleErrors roleErrors = ServicesProvider.GetInstance().GetRoleServices().SaveRole(_role);
                if (roleErrors.FindError)
                {
                    MessageBox.Show(GetString(roleErrors.ErrorCode), "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                UpdateControlStatus(false);
                User.CurrentUser = ServicesProvider.GetInstance().GetUserServices().Find(User.CurrentUser.Id, true);
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
            
            EraseRole();
        }

        private void InitListView()
        {
            listViewRoles.Items.Clear();
            List<Role> roles = new List<Role>();
            roles = ServicesProvider.GetInstance().GetRoleServices().FindAllRoles(false);
            foreach (Role role in roles)
            {
                ListViewItem lv = new ListViewItem(new []{role.RoleName,role.Description});
                lv.Tag = role;
                listViewRoles.Items.Add(lv);
            }
        }

        private void InitActionTree(Role pRole)
        {
            trvMenuItems.Nodes.Clear();

            TreeNode root = new TreeNode("Пункты меню");
            trvMenuItems.Nodes.Add(root);


            foreach (ToolStripMenuItem mi in (_mdiParent).MainMenuStrip.Items)
            {
                if (mi is ToolStripSeparator)
                    continue;

                if (!string.IsNullOrEmpty(mi.Text) && mi.Tag!=null)
                {
                    TreeNode mainNode = new TreeNode(mi.Text);
                    mainNode.Tag = mi.Tag;
                    mainNode.Checked = pRole.IsMenuAllowed(mainNode.Tag as MenuObject);
                    mainNode.Expand();
                    root.Nodes.Add(_FillChildNodes(mainNode, mi, pRole));
                }
            }
            root.Expand();
            Invalidate();
        }

        private void UpdateControlStatus(bool isUpdate)
        {
            UpdateControlStatus(false,"");
        }

        private void UpdateControlStatus(bool isUpdate,string roleName)
        {
            if (isUpdate)
            {
                stateLabel.Text = string.Format(GetString("updateLabel"),roleName);
                btnAdd.Text = GetString("update");
            }
            else
            {
                stateLabel.Text = GetString("createLabel");
                btnAdd.Text = GetString("create");
            }

        }

        private TreeNode _FillChildNodes(TreeNode pParent, ToolStripMenuItem pMenuItem, Role pRole)
        {
            if (!pMenuItem.HasDropDownItems)
            {
                TreeNode lastChildNode = new TreeNode(pMenuItem.Text);
                lastChildNode.Tag = GetMenuObject(pMenuItem.Text); 
                lastChildNode.Checked = pRole.IsMenuAllowed(lastChildNode.Tag as MenuObject);
                return lastChildNode;
            }

            foreach (Object tsmi in pMenuItem.DropDownItems)
            {
                if (!(tsmi is ToolStripMenuItem))
                    continue;
                ToolStripMenuItem tsmiMenu = (ToolStripMenuItem)tsmi;
                TreeNode childNode = new TreeNode(tsmiMenu.Text);
                childNode.Tag = GetMenuObject(tsmiMenu.Text); 
                childNode.Checked = pRole.IsMenuAllowed(childNode.Tag as MenuObject);
                childNode.Collapse(true);
                _FillChildNodes(childNode, tsmiMenu, pRole);
                pParent.Nodes.Add(childNode);                   
                pParent.Collapse(true);
            }
            return pParent;

        }

        /*Init ActionsTreeView*/
        private void _InitActionsTreeView(Role pRole)
        {
            trwActionItems.Nodes.Clear();

            TreeNode root = new TreeNode("Операции");
            trwActionItems.Nodes.Add(root);

            string prev = "";
            TreeNode mainNode = null;
            foreach (KeyValuePair<ActionItemObject, bool> _action in pRole.GetSortedActionItems())
            {
                if (!string.Equals(prev, _action.Key.ClassName))
                {
                    mainNode = new TreeNode(_action.Key.ClassName);
                    mainNode.Tag = _action.Key;
                    mainNode.Collapse();
                    root.Nodes.Add(mainNode);
                    prev = _action.Key.ClassName;
                }
                
                    TreeNode childNode = new TreeNode(_action.Key.MethodName);
                    childNode.Tag = _action.Key;
                    childNode.Checked = _action.Value;
                    mainNode.Nodes.Add(childNode);
            }
            root.Expand();
            Invalidate();
        }

        private MenuObject GetMenuObject(string pText)
        {
            if (!string.IsNullOrEmpty(pText))
            {
                MenuObject foundObject = _menuItems.Find(item => item == pText.Trim());
                if (foundObject == null)
                    foundObject = ServicesProvider.GetInstance().GetRoleServices().AddNewMenu(pText.Trim());

                return foundObject;
            }
            return null;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_role.Id == 0)
            {
                MessageBox.Show(GetString("RoleSelect"));
                return;
            }
            try
            {

                if (listViewRoles.SelectedItems != null)
                {
                    ServicesProvider.GetInstance().GetRoleServices().DeleteRole(listViewRoles.SelectedItems[0].Text);
                }
            }
            catch (Exception ex)
            {
                new frmShowError(CustomExceptionHandler.ShowExceptionText(ex)).ShowDialog();
            }
            InitListView();
            EraseRole();
            UpdateControlStatus(false);
        }
 
        private void buttonUpdateMenuList_Click(object sender, EventArgs e)
        {
            ServicesProvider.GetInstance().GetRoleServices().UpdateMenuList(_menuItems);
        }

        private void listViewRoles_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            _role = (Role) listViewRoles.SelectedItems[0].Tag;
            if (listViewRoles.SelectedItems != null)
            {
                InitActionTree(_role);
                _InitActionsTreeView(_role);
            }
            UpdateControlStatus(true,_role.RoleName);
        }

        private void treeViewMenuItems_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TreeNode selectedNode = e.Node;
            if (selectedNode != null)
            {
                    if (listViewRoles.SelectedItems != null)
                    {
                        MenuObject triggered = (MenuObject)selectedNode.Tag;

                        //Role selectedRole = listViewRoles.SelectedItems[0].Tag as Role;
                        Role selectedRole = _role;
                        if (selectedNode.Tag != null)
                            selectedRole.SetMenuAllowed((MenuObject)selectedNode.Tag, selectedNode.Checked);
                        _ChangeMenuAccess(selectedNode, selectedNode.Checked, selectedRole);

                    }
            }
        }

        private void _ChangeMenuAccess(TreeNode pNode, bool pChecked, Role pRole)
        {
            if (pNode.Nodes.Count < 1)
            {
                return;
            }

            foreach (TreeNode child in pNode.Nodes)
            {
                child.Checked = pChecked;
            }  
            
            return;
        }

        private void _ChangeActionAccess(TreeNode pNode, bool pChecked, Role pRole)
        {
            if (pNode.Nodes.Count == 0)
            {
                if (pNode.Tag != null)
                    pRole.SetActionAllowed((ActionItemObject)pNode.Tag, pNode.Checked);
                return;
            }
            foreach (TreeNode child in pNode.Nodes)
            {
                child.Checked = pChecked;
                if (pNode.Tag != null)
                    pRole.SetActionAllowed((ActionItemObject)child.Tag, child.Checked);
            }
            return;
        }

        private void listViewRoles_ItemSelectionChanged_1(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (listViewRoles.SelectedItems.Count>0)
            {
                InitActionTree((Role)listViewRoles.SelectedItems[0].Tag);
                _InitActionsTreeView((Role)listViewRoles.SelectedItems[0].Tag);
            }
        }

        private void treeViewActionItems_AfterCheck(object sender, TreeViewEventArgs e)
        {
            TreeNode selectedNode = e.Node;
            if (selectedNode != null)
            {
                if (listViewRoles.SelectedItems != null)
                {
                    ActionItemObject triggered = (ActionItemObject)selectedNode.Tag;

                    Role selectedRole = _role;
                    _ChangeActionAccess(selectedNode, selectedNode.Checked, selectedRole);

                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listViewRoles_Click(object sender, EventArgs e)
        {
            if (listViewRoles.SelectedItems.Count>0)
            {
                _role = (Role)listViewRoles.SelectedItems[0].Tag;
                LoadRoleProperties(_role);
                UpdateControlStatus(true, _role.RoleName);
            }
            else
            {
                EraseRole();
                UpdateControlStatus(false);
            }
        }
        
        private void LoadRoleProperties(Role role)
        {
            txtTitle.Text = role.RoleName;
            txtDescription.Text = role.Description;

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            EraseRole();
            UpdateControlStatus(false);
        }
    }
}
