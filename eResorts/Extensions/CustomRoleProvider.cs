using System;
using System.Linq;
using System.Web.Security;
using System.Linq.Expressions;
using System.Collections.Generic;
using eZeroOne.Domain;
using eZeroOne.Service.Repository;

namespace eResorts.Extensions
{
    public class CustomRoleProvider : RoleProvider
    {
        private readonly IRepository _repository;
        public CustomRoleProvider()
        {
            var ctx = new eResortsEntities();
            _repository = new Repository(ctx);
            
        }

        public override void AddUsersToRoles(string[] emails, string[] roleNames)
        {
           
        }

        private string _ApplicationName;
        public override string ApplicationName
        {
            get
            {
                return _ApplicationName;
            }
            set
            {
                _ApplicationName = value;
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string email)
        {
            
            var user = _repository.Single<User>(a => a.Email == email && a.CompanyId == 1);
            if (user != null)
            {
                var roles = _repository.Single<Role>(a => a.RoleId == user.RoleId);
                if (roles != null)
                   // return roles.Name == roleName;
                    return roles==null ? new string[] { } : new[] { roles.Name };
            }
         
            string[] array = new string[] {};
            return array;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string email, string roleName)
        {
            var user = _repository.Single<User>(a => a.Email == email && a.CompanyId == 1);
            if (user != null)
            {
                var roles = _repository.Single<Role>(a => a.RoleId == user.RoleId);
                if (roles!=null)
                    return roles.Name == roleName;
            }
          
            return true;
        }

        public override void RemoveUsersFromRoles(string[] emails, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
           // var rolesRepository = WindsorContainerWrapper.Container.Resolve<IRepository>();
           // return rolesRepository.Single<Role>(r => r.Name == roleName) != null;
            return true;
        }
    }

    public class Extensions
    {
        public static Expression<Func<TElement, bool>> BuildOrExpression<TElement, TValue>(Expression<Func<TElement, TValue>> valueSelector, IEnumerable<TValue> values)
        {
            if (null == valueSelector)
                throw new ArgumentNullException("valueSelector");
            if (null == values)
                throw new ArgumentNullException("values");
            ParameterExpression p = valueSelector.Parameters.Single();

            if (!values.Any())
                return e => false;

            var equals = values.Select(value => (Expression)Expression.Equal(valueSelector.Body, Expression.Constant(value, typeof(TValue))));
            var body = equals.Aggregate<Expression>((accumulate, equal) => Expression.Or(accumulate, equal));

            return Expression.Lambda<Func<TElement, bool>>(body, p);
        }
    }
}