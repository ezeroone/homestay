using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eZeroOne.MailService
{
    public class EmailInvoiceDetails
    {
        public string InvoiceId { get; set; }
        public DateTime InvoicedDate { get; set; }

        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }


        public string CompanyName { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyTelephone { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyStreet { get; set; }
        public string CompanyCity { get; set; }
        public string CompanyCountry { get; set; }
        public string CompanyRegNo { get; set; }
        public string CompanyFax { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal BillAmount { get; set; }

        public List<ItemDetails> ItemDetails { get; set; }
    }
    public class ItemDetails
    {
        public string ItemName { get; set; }
        public int Qty { get; set; }
        public decimal Tax { get; set; }
        public decimal Discount { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }

    }
}
