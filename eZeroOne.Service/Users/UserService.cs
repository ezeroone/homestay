using System;
using System.Collections.Generic;
using System.Linq;
using eZeroOne.Domain;
using eZeroOne.Service.Repository;

namespace eZeroOne.Service.Users
{
    public class UserService : IUserService
    {
       
        #region "Constructors"

        private readonly IRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        #endregion

        public User ValidateUser(string userName, string password, int company)
        {
            var encPassword = SecurityService.HashPassword(password);

            var user = _repository.Single < User>(a => a.Email == userName && a.Password == encPassword && a.CompanyId == company);
            if (user != null)
                return user;
            else
            {
                return null;
            }
        }
        
        public User GetUser(string userName,int company)
        {
            var user = _repository.Single <User>(a => a.Email == userName && a.CompanyId == company);
            if (user!=null)
            return user;
            else
            {
                return null;
            }
        }
        public UserRole SaveUserRole(string name)
        {
            var com = _repository.Single < UserRole>(t => t.RoleName == name.Trim());
            if (com != null)
                return com;

            var newCom = new UserRole { RoleName = name.Trim(), IsActive = true };
            _repository.Add(newCom);
            _unitOfWork.Commit();
            return newCom;
        }

        public IEnumerable<UserRole> GetUserRoles()
       {
            var groups = (from grp in _repository.All<UserRole>()
                         where grp.IsActive == true
                         select grp).ToList();
           
            return groups;
        }

        public string UserGroupName(int id)
        {
            var result = _repository.Single <UserRole>(t => t.Id == id);
            return result.RoleName ?? "";
        }

        public IEnumerable<User>ListUsers(int groupId)
       {
           var users = (from usr in _repository.All<User>()
                        join role in _repository.All<UsersInRole>()
                        on usr.UserId equals role.UserId
                        where usr.Active == true && role.RoleId == groupId
                        select usr).ToList();
           if (groupId==0)
               users = (from usr in _repository.All<User>()
                        join role in _repository.All<UsersInRole>()
                        on usr.UserId equals role.UserId
                        where usr.Active == true
                        select usr).ToList();

           return users;
       }
        public void DeleteUserRole(int id)
        {
            var result = _repository.Single < UsersInRole>(t => t.UserId == id);
            if (result != null)
                _repository.Delete(result);
            
               _unitOfWork.Commit();
        }
        public void DeleteUser(int userId)
        {
            var result = _repository.Single < User>(t => t.UserId == userId);
            if (result != null)
            {
                _repository.Delete(result);
                var result1 = _repository.Single < UsersInRole>(t => t.UserId == userId);
                if (result1!=null)
                {
                    _repository.Delete(result1);
                    
                }
                _unitOfWork.Commit();
            }
        }
        public void AssignUserToGroup(int userId, int groupId)
        {
            var result = _repository.Single<UsersInRole>(t => t.UserId == userId);
            if (result == null)
            {
                var newgrp = new UsersInRole
                                 {
                                     RoleId = groupId,
                                     UserId = userId
                                 };
                _repository.Add(newgrp);
                _unitOfWork.Commit();
            }
        }

        public void AssignPermission(int userId, int groupId,int menuId,bool save,bool view,bool edit,bool delete)
        {
            var result = _repository.Single<UserMenuPermission>(t => t.UserId == userId && t.RoleId == groupId && t.MenuId == menuId);
            if (result == null)
            {
                var data = new UserMenuPermission
                               {
                                   UserId=userId,
                                   RoleId=groupId,
                                   MenuId=menuId,
                                   IsSave=save,
                                   IsUpdate=edit,
                                   IsDelete=delete,
                                   IsViewed=true
                               };
                _repository.Add(data);
                _unitOfWork.Commit();
            }


        }
        public void AssignPermission(int userId, int groupId, int menuId, string action, bool permission)
        {
            bool isSave = false;
            bool isUpdate = false;
            bool isDelete = false;
            bool isView = true;

            if(action=="save")
                isSave = permission;
            else if (action == "update")
                isUpdate = permission;
            else if (action == "delete")
                isDelete = permission;
            else if (action == "view")
                isView = permission;

            var result = _repository.Single < UserMenuPermission>(t => t.RoleId == groupId && t.MenuId == menuId);
            if (result == null)
            {
                var data = new UserMenuPermission
                {
                    UserId = 0,
                    RoleId = groupId,
                    MenuId = menuId,
                    IsSave = isSave,
                    IsUpdate = isUpdate,
                    IsDelete = isDelete,
                    IsViewed = true
                };
                _repository.Add(data);
                _unitOfWork.Commit();
            }
            else
            {
                if (action == "save")
                    result.IsSave = permission;
                else if (action == "update")
                    result.IsUpdate = permission;
                else if (action == "delete")
                    result.IsDelete = permission;
                else if (action == "view")
                    result.IsViewed = true;
            }
            _unitOfWork.Commit();


        }
        public void GetUserPermission(int userId, int groupId, int menuId, out bool save, out bool view, out bool edit, out bool delete)
        {
            save = false;
            delete = false;
            view = false;
            edit = false;

            var result = (from usr in _repository.All<UserMenuPermission>()
                          join role in _repository.All<UsersInRole>()
                         on usr.RoleId equals role.RoleId
                         where role.RoleId == groupId && usr.MenuId == menuId && role.UserId==userId
                         select usr).FirstOrDefault();

           // var result = repositoryPer.Single(t => t.UserId == userId && t.RoleId == groupId && t.MenuId == menuId);
            if (result != null)
            {
                save = result.IsSave;
                delete = result.IsDelete;
                view = true;
                edit = result.IsUpdate;
            }

        }
        public IEnumerable<Menu> GetAllMenus()
        {
            var menus = (from menu in _repository.All<Menu>()
                         where menu.IsActive == true
                         select menu).ToList();

            return menus;
        }
        public void SaveUser(int groupId,string fName,string email,string password)
        {
            password=SecurityService.HashPassword(password);
            var user = (from e in _repository.All<User>()
                        where e.Email == email
                        select e).FirstOrDefault();
            if(user==null)
            {
                var userData = new User
                                   {
                                       FirstName=fName,
                                       LastName="",
                                       Password=password,
                                       Email=email,
                                       IsEmailActivated=true,
                                       Active=true,
                                       RoleId=groupId,//==1?1:2,
                                       CompanyId=1,
                                       Created=DateTime.Now

                                   };
                _repository.Add(userData);
                _unitOfWork.Commit();

                var right = new UsersInRole
                                {
                                    RoleId = groupId,
                                    UserId = userData.UserId
                                };

                _repository.Add(right);
                _unitOfWork.Commit();

            }
        }
        public UserMenuPermission GetMenuPermission(int groupId, int menuId)
        {
            var permission = new UserMenuPermission();
            var result = _repository.Single <UserMenuPermission>(t => t.RoleId == groupId && t.MenuId == menuId);
            if (result != null)
            {
                return result;
            }
            return permission;
        }
        public UsersInRole GetRoleId(int userId)
        {
            var result = _repository.Single <UsersInRole>(t => t.UserId == userId);
            if(result!=null)
                return result;
            else
            {
                return null;
            }
        }
        public Domain.Customer GetCustomerByUserId(int userId)
        {
           return (from u in _repository.All<User>()
                    join c in _repository.All<Customer>()
                    on u.UserId equals c.UserId
                    where u.IsEmailActivated & u.Active
                    select c).FirstOrDefault();

        }
        public User GetUser(int userId)
        {
            var user = _repository.Single<User>(a => a.UserId==userId);
            if (user != null)
                return user;
            else
            {
                return null;
            }
        }
        public Client GetClient(int userId)
        {
            var user = _repository.Single<Client>(a => a.ClientId == userId);
            if (user != null)
                return user;
            else
            {
                return null;
            }
        }
    }
}
