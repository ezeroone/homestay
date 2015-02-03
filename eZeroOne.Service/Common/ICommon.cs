using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eZeroOne.Domain;

namespace eZeroOne.Service.Common
{
    public interface  ICommon
    {
        IEnumerable<Group> LoadGroup();
        IEnumerable<Company> LoadCompany();
        IEnumerable<Company> LoadCompany(int groupId);
    
        IEnumerable<Title> LoadTitles();
        IEnumerable<Country> LoadCountries();
        IEnumerable<Group> LoadGroupList();
        IEnumerable<Company> LoadCompanyList(int groupId);

        int SaveTitle(string title);
        int SaveCountry(string country);
        int SaveCompany(string companyName, int groupId);
        
        int SaveDesignation(string name);
        int SaveGroup(string name);
        Group SaveGroupName(string name);
        Title SaveNewTitle(string title);
        bool ActivateEmail(string email);

        IEnumerable<PaymentType>PaymentTypes();
        CompanyProfile GetCompanyDetails(int id);

        IEnumerable<Tax> LoadTaxDetails();
        IEnumerable<CompanyProfile> LoadCompanyDetails();
        void UpdateTaxDetails(int id, decimal taxValue, bool isincluded);
        void EditCompanyInfo();

    }
}
