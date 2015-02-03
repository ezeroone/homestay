using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eZeroOne.Domain;

namespace eZeroOne.Service.Customers
{
    public interface ICustomerService
    {
        bool SaveCustomerLogin(User user);
        IEnumerable<Country> ListAllCountries();
        IEnumerable<Title> ListAllTitles();

        Domain.Customer GetCustomerById(int custId);
        bool AddCustomer(Domain.Customer cust);
        bool EditCustomer();

        IEnumerable<BookingCart> GetShoppingCartData(Guid refId);
        bool AddToShoppingCart(Guid refId, int? custId, int itemId, int qty);

        bool DeleteShoppingCartItem(int id);
        bool UpdateCart(int id, int quantity);

        IEnumerable<Invoice> GetInvoiceData();
        IEnumerable<Transaction> GetTransactionData();

        IEnumerable<Transaction> GetTransactionData(int invoiceId);

        bool UpdateDelivaryStatus(string invoiceNo);
        bool DelayInvoice(string invoiceNo, string delayText);

        void UpdateShoppingCart(Guid refId, int custId);
        bool AddClient(Client client);
    }
}
