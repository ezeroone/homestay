using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eZeroOne.Domain;
using eZeroOne.Service.Repository;

namespace eZeroOne.Service.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }


        public bool SaveCustomerLogin(Domain.User cust)
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
                                       RoleId = cust.RoleId,
                                       CompanyId = 1,
                                       Created = DateTime.Now

                                   };
                _repository.Add(userData);
                _unitOfWork.Commit();
                return true;

            }
            return false;
        }

        public IEnumerable<Country> ListAllCountries()
        {
            return (from e in _repository.All<Country>()
                    select e).Distinct().AsEnumerable();
        }

        public IEnumerable<Title> ListAllTitles()
        {
            return (from e in _repository.All<Title>()
                    select e).Distinct().AsEnumerable();
        }


        public Domain.Customer GetCustomerById(int custId)
        {
            return (from e in _repository.All<Customer>()
                    where e.CustomerId == custId
                    select e).FirstOrDefault();

        }

        //public bool IsCustomerExist()
        //{

        //}

        public bool AddCustomer(Domain.Customer customer)
        {
            var cust = _repository.Single < Customer>(t => t.UserId == customer.UserId);
            if (cust == null)
            {
                _repository.Add(customer);
                _unitOfWork.Commit();
                return true;
            }
            return false;
        }

        public bool EditCustomer()
        {
            //var repository = new _repository.s<eZeroOne.Domain.Customer>(_dataContext);
            //repository.SaveChanges();
            return true;
        }
        public IEnumerable<BookingCart> GetShoppingCartData(Guid refId)
        {
            return (from e in _repository.All<BookingCart>()
                    where e.BookingCartId == refId
                    select e).AsEnumerable();
        }

        public bool AddToShoppingCart(Guid refId, int? custId, int itemId,int qty)
        {
            //var repository = new Repository<eZeroOne.Domain.ShoppingCart>(_dataContext);
            //var cart = repository.Single(t => t.ShoppingCartRefId == refId && t.ItemId == itemId);
            //if (cart == null)
            //{
            //    var newCart = new ShoppingCart
            //        {
            //            CustomerId = custId,
            //            ItemId = itemId,
            //            Quantity = qty,
            //            Discount = GetDisCount(itemId),// * qty,
            //            ShoppingCartRefId = refId
            //        };
            //    repository.Add(newCart);
            //}
            //else
            //{
            //    var oldQty = cart.Quantity;
            //    cart.Quantity = oldQty + 1;
            //    cart.Discount = GetDisCount(itemId); //*(oldQty + 1);
            //}
          //  repository.SaveChanges();
            return true;
        }

        public bool DeleteShoppingCartItem(int id)
        {
            //var repository = new Repository<eZeroOne.Domain.ShoppingCart>(_dataContext);
            //var cart = repository.Single(t => t.Id == id);
            //if (cart != null)
            //{
            //    repository.Delete(cart);
            //    repository.SaveChanges();
            //    return true;
            //}
            return false;
        }

        public bool UpdateCart(int id, int quantity)
        {
            //var repository = new Repository<eZeroOne.Domain.ShoppingCart>(_dataContext);
            //var cart = repository.Single(t => t.Id == id);
            //if (cart != null)
            //{
            //    if (quantity > 0)
            //        cart.Quantity++;
            //    else
            //    {
            //        if (cart.Quantity >= 2)
            //            cart.Quantity--;
            //    }

            //    cart.Discount = GetDisCount(cart.ItemId);//* (cart.Quantity);

            //    repository.SaveChanges();
            //    return true;
            //}
            return false;
        }

        public void UpdateShoppingCart(Guid refId, int custId)
        {
            //var repository = new Repository<eZeroOne.Domain.ShoppingCart>(_dataContext);
            //var cart = repository.Single(t => t.ShoppingCartRefId == refId);
            //if (cart != null)
            //{
            //    cart.CustomerId = custId;
            //    repository.SaveChanges();
            //}
        }

        public IEnumerable<Invoice> GetInvoiceData()
        {
           return (from e in _repository.All<Invoice>()
                    select e).AsEnumerable();
        }

        public IEnumerable<Transaction> GetTransactionData()
        {
            return (from e in _repository.All<Transaction>()
                    select e).AsEnumerable();
        }

        public IEnumerable<Transaction>GetTransactionData(int invoiceId)
        {
            return (from e in _repository.All<Transaction>()
                    where e.InvoiceId==invoiceId
                    select e).AsEnumerable();
        }

        public bool UpdateDelivaryStatus(string invoiceNo)
        {
            //var repository = new Repository<eZeroOne.Domain.Invoice>(_dataContext);
            //var inv = repository.Single(t => t.InvoiceNo == invoiceNo);
            //if (inv != null)
            //{
            //    inv.IsDelivered = true;
            //    repository.SaveChanges();
            //    return true;
            //}
            return false;
        }

        public bool DelayInvoice(string invoiceNo, string delayText)
        {
            //var repository = new Repository<eZeroOne.Domain.Invoice>(_dataContext);
            //var inv = repository.Single(t => t.InvoiceNo == invoiceNo);
            //if (inv != null)
            //{
            //    inv.IsDelay = true;
            //    inv.DelayNotice = delayText;
            //    repository.SaveChanges();
            //    return true;
            //}
            return false;
        }

        private decimal GetDisCount(int itemId)
        {
            decimal discount = 0;
            //var repository = new Repository<eZeroOne.Domain.Item>(_dataContext);
            //var repositoryCat = new Repository<eZeroOne.Domain.ItemCategory>(_dataContext);
            //var repositorySubCat = new Repository<eZeroOne.Domain.ItemSubCategory>(_dataContext);

            //var item = repository.Single(t =>t.Id == itemId);
            //if (item != null)
            //{
            //    if (item.Discount > 0)
            //        if (item.Discount != null) discount = (decimal)item.Discount;

            //    if (discount == 0)
            //    {
            //        var subCat = (from s in repositorySubCat.GetAll()
            //                      where s.Id == item.SubCategoryId
            //                      select s).FirstOrDefault();
            //        if (subCat != null)
            //        {
            //            discount = subCat.Discount;
            //        }
            //    }

            //    if (discount == 0)
            //    {
            //        var cat = (from s in repositoryCat.GetAll()
            //                      where s.Id == item.CategoryId
            //                      select s).FirstOrDefault();
            //        if (cat != null)
            //        {
            //            discount = cat.Discount;
            //        }
            //    }

            //}
          
            return discount;
        }

        public bool AddClient(Domain.Client client)
        {
            var cust = _repository.Single<Client>(t => t.UserId == client.UserId);
            if (cust == null)
            {
                _repository.Add(client);
                _unitOfWork.Commit();
                return true;
            }
            return false;
        }
    }
}
