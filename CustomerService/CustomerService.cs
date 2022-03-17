using CustomerModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CustomerServices
{
    public class CustomerService : ICustomerService
    {
        public List<Customer> Customers { get; set; }
        public string GetCustomerswithAge(int age=56)
        {
            string customers = string.Empty;
            
            try
            {
               
                customers = string.Join("," , Customers.Where(c => c.Age == age).Select(c => c.FirstName));
            }
            catch 
            {
               
             throw new Exception("Error while executing GetCustomerswithAge56");
                
            }

            return customers;


        }
        public List<string> GetCustomerswithPhoneNumber()
        {
            List<string> customers = new List<string>();

            try
            {
              
                customers = Customers.Select(c => $"ID: {c.Id} , Phone Number:  {c.PhoneNumber}").ToList();
            }
            catch 
            {
                throw new Exception("GetCustomerswithPhoneNumber");
            }

            return customers;
        }
        public List<string> GetPhoneNumbersPerState()
        {
            List<string> customers = new List<string>();

            try
            {
          
                customers = Customers.Where(x => x.PhoneNumber != Constants.MISSINGORINVALID)
                                     .GroupBy(x => x.State)
                                     .OrderBy(g => g.Key)
                                     .Select(g => $"{g.Key} : {g.Count()}").ToList();
              
            }

            catch
            {
                throw new Exception("GetCustomerswithPhoneNumber");
            }

            return customers;
        }
    }
}
