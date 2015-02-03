using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eZeroOne.Domain
{
    public class EmailActivationToken
    {
        public DateTime ExpirationDate
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public string EncryptedForm
        {
            get;
            set;
        }
    }
}
