using System.Collections.Generic;
using eZeroOne.Domain;

namespace eZeroOne.Service.Users
{
    public interface IUserService
    {
        User ValidateUser(string userName, string password, int company);
        User GetUser(string userName, int company);
      
        UserRole SaveUserRole(string name);
        IEnumerable<UserRole> GetUserRoles();
        IEnumerable<User>ListUsers(int groupId);
        void DeleteUserRole(int id);
       // void DeleteUser(int id);
        void AssignUserToGroup(int userId, int groupId);
        void AssignPermission(int userId, int groupId,int menuId,bool save,bool view,bool edit,bool delete);
        void GetUserPermission(int userId, int groupId, int menuId, out bool save, out bool view, out bool edit, out bool delete);
        IEnumerable<Menu> GetAllMenus();
        string UserGroupName(int id);
        void SaveUser(int groupId,string fName,string email,string password);
        void DeleteUser(int userId);
        UserMenuPermission GetMenuPermission(int groupId, int menuId);
        void AssignPermission(int userId, int groupId, int menuId, string action, bool permission);
        UsersInRole GetRoleId(int userId);

        //get customer data
        Domain.Customer GetCustomerByUserId(int userId);
        User GetUser(int userId);

        Client GetClient(int userId);
    }
}
