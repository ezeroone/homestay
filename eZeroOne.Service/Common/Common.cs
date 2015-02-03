using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eZeroOne.Domain;
using eZeroOne.Service.Repository;

namespace eZeroOne.Service.Common
{
    public class Common:ICommon
    {
        private readonly IRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public Common(IRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Group> LoadGroup()
        {
            var selectOne = new Group
                                {
                                    Id=0,
                                    Name="---- Select One ----"
                                };
            var group = _repository.All <Group>().OrderBy(o => o.Name).ToList();
            group.Insert(0, selectOne);
            
            return group;
           
        }
        public IEnumerable<Company> LoadCompany()
        {
            var selectOne = new Company
            {
                Id = 0,
                Name = "---- Select One ----"
            };

            var company = _repository.All <Company>().OrderBy(o => o.Name).ToList();
            company.Insert(0,selectOne);
            return company;
            
        }
        public IEnumerable<Company> LoadCompany(int groupId)
        {
            var selectOne = new Company
            {
                Id = 0,
                Name = "---- Select One ----"
            };

            var company = _repository.All <Company>().ToList();

            if(groupId>0)
                company = (from c in company
                          where c.GroupId == groupId
                          select c).OrderBy(o=>o.Name).ToList();

            company.Insert(0, selectOne);
           
            return company;
          
        }

        public IEnumerable<Group> LoadGroupList()
        {
            return  _repository.All <Group>().OrderBy(o => o.Name).ToList();
            

        }
        public IEnumerable<Company> LoadCompanyList(int groupId)
        {
           
            var company = _repository.All<Company>().ToList();

            if (groupId > 0)
                company = (from c in company
                           where c.GroupId == groupId
                           select c).OrderBy(o => o.Name).ToList();

           return company;

        }

       
        public IEnumerable<Title> LoadTitles()
        {
            var title = _repository.All<Title>();
            if (title != null)
                return title;
            else
            {
                return null;
            }
        }
        public IEnumerable<Country> LoadCountries()
        {
            var country = _repository.All<Country>();
            if (country != null)
                return country;
            else
            {
                return null;
            }
        }

        public IEnumerable<PaymentType> PaymentTypes()
        {
            var paymentType = _repository.All<PaymentType>();
            if (paymentType != null)
                return paymentType;
            else
            {
                return null;
            }
        }

        public int SaveTitle(string title)
        {
            
            var tit = _repository.Single<Title>(t => t.Name == title.Trim());
            if(tit!=null)
                return tit.Id;
            var newTitle = new Title{Name=title.Trim()};
            
            _repository.Add(newTitle);
            _unitOfWork.Commit();
            return newTitle.Id;
        }


        public int SaveCountry(string country)
        {
            var con = _repository.Single <Country>(t => t.Name == country.Trim());
            if (con != null)
                return con.Id;

            var newCon = new Country{ Name=country.Trim()};
            _repository.Add(newCon);
            _unitOfWork.Commit();return newCon.Id;
        }
        public int SaveDesignation(string name)
        {
            var con = _repository.Single < Designation>(t => t.Name == name.Trim());
            if (con != null)
                return con.Id;

            var newCon = new Designation { Name = name.Trim() };
            _repository.Add(newCon);
            _unitOfWork.Commit();
            return newCon.Id;
        }

        public int SaveCompany(string name, int groupId)
        {
            var com = _repository.Single<Company>(t => t.Name == name.Trim() && t.GroupId == groupId);
            if (com != null)
                return com.Id;

            var newCom = new Company { Name = name.Trim(), GroupId = groupId, Created = DateTime.UtcNow };
            _repository.Add(newCom);
            _unitOfWork.Commit();
            return newCom.Id;
            
        }
        public int SaveGroup(string name)
        {
            var com = _repository.Single < Group>(t => t.Name == name.Trim());
            if (com != null)
                return com.Id;

            var newCom = new Group{ Name = name.Trim(), Created = DateTime.UtcNow };
            _repository.Add(newCom);
            _unitOfWork.Commit();
            return newCom.Id;

        }
      
        public Group SaveGroupName(string name)
        {
            var com = _repository.Single < Group>(t => t.Name == name.Trim());
            if (com != null)
                return com;

            var newCom = new Group { Name = name.Trim(), Created = DateTime.UtcNow };
            _repository.Add(newCom);
            _unitOfWork.Commit();
            return newCom;
        }

        public Title SaveNewTitle(string title)
        {
            var tit = _repository.Single <Title>(t => t.Name == title.Trim());
            if (tit != null)
                return tit;
            var newTitle = new Title { Name = title.Trim() };

            _repository.Add(newTitle);
            _unitOfWork.Commit();
            return newTitle;
        }

        public bool CreateLogin(Domain.User cust)
        {
            cust.Password = SecurityService.HashPassword(cust.Password);
            var user = (from e in _repository.All<User>()
                        where e.Email == cust.Email
                        select e).FirstOrDefault();
            if (user == null)
            {
                var userData = new User
                {
                    FirstName = cust.FirstName,
                    LastName = cust.LastName,
                    Password = cust.Password,
                    Email = cust.Email,
                    IsEmailActivated = false,
                    Active = false,
                    RoleId = 2,
                    CompanyId = 1,
                    Created = DateTime.Now

                };
                _repository.Add(userData);
                _unitOfWork.Commit();
                return true;

            }
            return false;
        }
        public bool ActivateEmail(string email)
        {
            var user = (from e in _repository.All<User>()
                        where e.Email == email
                        select e).FirstOrDefault();
            if (user != null)
            {
                user.Active = true;
                user.IsEmailActivated = true;
                _unitOfWork.Commit();
                return true;

            }
            return false;
        }
        public IEnumerable<Tax> LoadTaxDetails()
        {
            var tax = _repository.All<Tax>();
            if (tax != null)
                return tax;
            else
            {
                return null;
            }
        }

        public IEnumerable<CompanyProfile> LoadCompanyDetails()
        {
            var com = _repository.All<CompanyProfile>();
            if (com != null)
                return com;
            else
            {
                return null;
            }
        }

        public void UpdateTaxDetails(int id, decimal taxValue, bool isincluded)
        {
            var tax = _repository.Single <Tax>(t => t.Id == id);
            if (tax != null)
            {
                tax.Value = taxValue;
                tax.IsIncluded = isincluded;
            }
            _unitOfWork.Commit();

        }

        public void EditCompanyInfo()
        {
            //var repository = new _repository.<eZeroOne.Domain.CompanyProfile>(_dataContext);
            //repository.SaveChanges();
        }

        public CompanyProfile GetCompanyDetails(int id)
        {
             return (from e in _repository.All<CompanyProfile>()
                    where e.Id == id
                    select e).FirstOrDefault();
        }

    }
}
