using CustomerModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerServices
{
    public interface ICommonService
    {
        string BaseAddress
        {
            get;
            set;
        }
        public List<Customer> GetAllCustomers();

        public List<Customer> GetAllCustomersWithPhoneNumber(List<Customer> data);
    }
}
