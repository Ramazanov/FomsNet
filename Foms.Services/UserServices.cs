//Octopus MFS is an integrated suite for managing a Micro Finance Institution: clients, contracts, accounting, reporting and risk
//Copyright © 2006,2007 OCTO Technology & OXUS Development Network
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
using System.Collections.Generic;
using System.Diagnostics;
using Foms.CoreDomain;
using Foms.ExceptionsHandler.Exceptions.UserExceptions;
using Foms.Manager;

namespace Foms.Services
{
    public class UserServices : MarshalByRefObject
    {
        private readonly UserManager _userManager;
        private User _user;

        private static List<User> _users;
        private static Dictionary<int, List<int>> _subordinateRel;
        private static Dictionary<int, List<int>> _branchRel;

        public UserServices(User pUser)
        {
            _user = pUser;
            _userManager = new UserManager(pUser);
        }

        public UserServices(User pUser, string pTestDb)
        {
            _user = pUser;
            _userManager = new UserManager(pTestDb, pUser);
        }

        public UserServices(UserManager pUserManager)
        {
            _user = new User();
            _userManager = pUserManager;
        }

        public bool Delete(User user)
        {
            if (user.Id == 0)
                throw new OctopusUserDeleteException(OctopusUserDeleteExceptionEnum.UserIsNull);

            if (user.Id == 1)
                throw new OctopusUserDeleteException(OctopusUserDeleteExceptionEnum.AdministratorUser);

            user.IsDeleted = true;
            _userManager.DeleteUser(user);
            return true;
        }

        /// <summary>
        /// Contains all user's errors
        /// </summary>
        [Serializable]
        public struct UserErrors
        {
            public bool FindError;
            public bool LoginError;
            public bool PasswordError;
            public bool RoleError;
            public bool FirstNameError;
            public bool LastNameError;
            public bool MailError;
            public string ResultMessage;

        }

        /// <summary>
        /// Save selected user
        /// </summary>
        /// <param name="pUser"></param>
        /// <returns>A struct contains, if necessary, errors occurs</returns>
        public UserErrors SaveUser(User pUser)
        {
            UserErrors userErrors = new UserErrors();

            if (pUser.UserName == null)
            {
                userErrors.FindError = true;
                userErrors.LoginError = true;
               // userErrors.ResultMessage += "\n - " + MultiLanguageStrings.GetString(Ressource.StringRes, "User_Login_Empty.Text");
            }

            if (pUser.Password == null)
            {
                userErrors.FindError = true;
                userErrors.PasswordError = true;
               // userErrors.ResultMessage += "\n - " + MultiLanguageStrings.GetString(Ressource.StringRes, "User_Password_Empty.Text");
            }

            if (pUser.UserRole == null)
            {
                userErrors.FindError = true;
                userErrors.RoleError = true;
               // userErrors.ResultMessage += "\n - " + MultiLanguageStrings.GetString(Ressource.StringRes, "User_Role_Empty.Text");
            }
            if (pUser.UserRole.Id < 1)
            {
                userErrors.FindError = true;
                userErrors.RoleError = true;
               // userErrors.ResultMessage += "\n - " + MultiLanguageStrings.GetString(Ressource.StringRes, "User_Role_Empty.Text");
            }
            if (pUser.FirstName == null)
            {
                userErrors.FindError = true;
                userErrors.FirstNameError = true;
              //  userErrors.ResultMessage += "\n - " + MultiLanguageStrings.GetString(Ressource.StringRes, "User_LastName_Empty.Text");
            }

            if (pUser.LastName == null)
            {
                userErrors.FindError = true;
                userErrors.LastNameError = true;
               // userErrors.ResultMessage += "\n - " + MultiLanguageStrings.GetString(Ressource.StringRes, "User_FirstName_Empty.Text");
            }

            if (pUser.Mail == null)
            {
                userErrors.FindError = true;
                userErrors.MailError = true;
               // userErrors.ResultMessage += "\n - " + MultiLanguageStrings.GetString(Ressource.StringRes, "User_Mail_Empty.Text");
            }

            if (userErrors.FindError) return userErrors;

            if (pUser.Id == 0)
            {
                if (Find(pUser.UserName, pUser.Password) != null)
                {
                    userErrors.FindError = true;
               //     userErrors.ResultMessage += "\n - " + MultiLanguageStrings.GetString(Ressource.StringRes, "User_Save_AlreadyExist.Text");
                }
                else
                {
                    _userManager.AddUser(pUser);
                    if (_users != null) _users.Add(pUser);
                 //   userErrors.ResultMessage = MultiLanguageStrings.GetString(Ressource.StringRes, "User_Save_OK.Text");
                }
            }
            else
            {
                _userManager.UpdateUser(pUser);
                //userErrors.ResultMessage = MultiLanguageStrings.GetString(Ressource.StringRes, "User_Update_OK.Text");
            }

            return userErrors;
        }

        public User Find(int id)
        {
            LoadUsers();
            Debug.Assert(_users != null, "User list is null");
            return _users.Find(item => item.Id == id);
        }

        public User Find(int id, bool toUpdateUsers)
        {
            if (toUpdateUsers)
                _users = null;

            LoadUsers();
            Debug.Assert(_users != null, "User list is null");
            return _users.Find(item => item.Id == id);
        }

        public User Find(string username, string password)
        {
            LoadUsers();
            Debug.Assert(_users != null, "User list is null");
            User u = _users.Find(item => item.UserName == username
                                         && item.Password == password 
                                         && !item.IsDeleted);
            return u;
        }

        public User Find(string username, string password, string account)
        {
            return Find(username, password);
        }

        public List<User> FindAll(bool includeDeleted)
        {
            LoadUsers();
            Debug.Assert(_users != null, "User list is null");
            return _users.FindAll(item => !item.IsDeleted 
                || item.IsDeleted == includeDeleted);
        }

        public List<User> FindAllExcept(User user, bool includeDeleted)
        {
            LoadUsers();
            Debug.Assert(_user != null, "User list is null");
            return _users.FindAll(item => (!item.IsDeleted 
                || item.IsDeleted == includeDeleted) 
                && !item.Equals(user));
        }

        private void LoadSubordinateRel()
        {
            if (_subordinateRel != null) return;
            _subordinateRel = _userManager.SelectSubordinateRel();
        }

        private void LoadBranchRel()
        {
            if (_branchRel != null) return;
            _branchRel = _userManager.SelectBranchRel();
        }

        private void LoadUsers()
        {
            if (_users != null) return;
            _users = _userManager.SelectAll();
            RoleServices rs = ServicesProvider.GetInstance().GetRoleServices();
       
            rs = rs ?? new RoleServices(_user);
            foreach (User user in _users)
            {
                user.UserRole = rs.Find(user);
                 
            }
        }

        public void Save(User user)
        {
            if (_subordinateRel != null)
            {
                if (!_subordinateRel.ContainsKey(user.Id))
                {
                    _subordinateRel.Add(user.Id, new List<int>());
                }
                foreach (User sub in user.Subordinates)
                {
                    _subordinateRel[user.Id].Add(sub.Id);
                }
            }
            if (_branchRel != null)
            {
                if (!_branchRel.ContainsKey(user.Id))
                {
                    _branchRel.Add(user.Id, new List<int>());
                }
                _branchRel[user.Id].Clear();
                foreach (Branch b in user.Branches)
                {
                    _branchRel[user.Id].Add(b.Id);
                }
            }
            _userManager.Save(user);
        }
    }
}
