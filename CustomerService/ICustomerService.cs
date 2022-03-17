using CustomerModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerServices
{
    public interface ICustomerService
    {
        List<Customer> Customers
        {
            get;
            set;
        }
        string GetCustomerswithAge(int age);
        List<string> GetCustomerswithPhoneNumber();
        List<string> GetPhoneNumbersPerState();
    }
}
