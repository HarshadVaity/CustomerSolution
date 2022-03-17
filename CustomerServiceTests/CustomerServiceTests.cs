using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CustomerModel;

namespace CustomerServices.Tests
{
    [TestClass()]
    public class CustomerServiceTests
    {

        ICommonService commonService = null;
        ICustomerService customerService = null;

        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.test.json")
               .Build();
            return config;
        }
        
        [TestInitialize]
        public void Setup()
        {
            var services = new ServiceCollection();
            services.AddScoped<ICommonService, CommonSerive>();
            services.AddScoped<ICustomerService, CustomerService>();

            var serviceProvider = services.BuildServiceProvider();

            commonService = serviceProvider.GetService<ICommonService>();
            customerService = serviceProvider.GetService<ICustomerService>();

            var config = InitConfiguration();
            commonService.BaseAddress = config.GetSection("APIConfig:Value").Value;
        }
       

        
        [TestMethod()]
        public void GetCustomerswithAge56Test()
        {
            customerService.Customers = commonService.GetAllCustomers();

            Assert.IsNotNull(customerService.Customers);
            Assert.IsTrue(customerService.Customers.Count==9);


            var data=  customerService.GetCustomerswithAge(56);

            Assert.IsNotNull(data);
            Assert.IsTrue(data== "Robert,Jenny,Jo");


            customerService.Customers = new List<Customer>();
            data = customerService.GetCustomerswithAge(56);

          
            Assert.IsTrue(string.IsNullOrEmpty(data));


            
            //user defined customers without fistname
            Customer customer1 = new Customer();
            Customer customer2 = new Customer();
            customerService.Customers = new List<Customer>();


            customer1.Age = 56;

            customerService.Customers.Add(customer1);


            data = customerService.GetCustomerswithAge(56);


            Assert.IsTrue(string.IsNullOrEmpty(data));


            //user defined customers with fistname
            customer1 = new Customer();
           
            customerService.Customers = new List<Customer>();


            customer1.Age = 56;
            customer1.FirstName = "Harshad";

            customerService.Customers.Add(customer1);


            data = customerService.GetCustomerswithAge(56);


            Assert.IsTrue(data == "Harshad");


            //user defined customers with fistname and age
            customer1 = new Customer();
            customer2 = new Customer();

            customerService.Customers = new List<Customer>();


            customer1.Age = 56;
            customer1.FirstName = "Harshad";

            customer2.Age = 56;
            customer2.FirstName = "vaity";

            customerService.Customers.Add(customer1);
            customerService.Customers.Add(customer2);


            data = customerService.GetCustomerswithAge(56);


            Assert.IsTrue(data == "Harshad,vaity");

            //user defined customers with one customer age 56 and other customer with age 100
            customer1 = new Customer();
            customer2 = new Customer();

            customerService.Customers = new List<Customer>();


            customer1.Age = 100;
            customer1.FirstName = "100Harshad";

            customer2.Age = 56;
            customer2.FirstName = "56Harshad";

            customerService.Customers.Add(customer1);
            customerService.Customers.Add(customer2);


            data = customerService.GetCustomerswithAge(56);


            Assert.IsTrue(data == "56Harshad");

        }

        [TestMethod()]
        public void GetCustomerswithPhoneNumberTest()
        {
            var customers = commonService.GetAllCustomers();
            customerService.Customers = commonService.GetAllCustomersWithPhoneNumber(customers);
            var data = customerService.GetCustomerswithPhoneNumber();

            Assert.IsNotNull(data);
            Assert.IsTrue(data.Count==9);

            Assert.IsTrue(data.Count(x => x.Contains("Missing or invalid")) == 3);
            Assert.IsTrue(data.Count(x => x.Contains("Missing or invalid")==false) == 6);

            List<string> phoneNumbers = new List<string>();
            phoneNumbers.Add("(02) 9001 7760");
            phoneNumbers.Add("(04) 0012 3999");
            phoneNumbers.Add("(03) 9822 1100");
            phoneNumbers.Add("(02) 9321 0988");
            phoneNumbers.Add("(07) 9411 0000");

            var valid = data.Where(x => x.Contains("Missing or invalid") == false).ToList();

            Assert.IsTrue(valid.Count(x => phoneNumbers.Contains(x.Substring(x.IndexOf("("))))==5);

            var first= data.FirstOrDefault();

            Assert.IsNotNull(first);
            Assert.IsTrue(first.Contains("(02) 9001 7760"));


            //user defined customers
            Customer customer1 = new Customer();
            customer1.State = States.NSW;
            customer1.PhoneNumber = "12345678";
            List<Customer> userDefinedCustomers = new List<Customer>();
            userDefinedCustomers.Add(customer1);
            customerService.Customers = commonService.GetAllCustomersWithPhoneNumber(userDefinedCustomers);
            data = customerService.GetCustomerswithPhoneNumber();

            Assert.IsTrue(data.Count() == 1);
            Assert.IsTrue(data.Any(x => x.Contains("(02) 1234 5678"))==true);
            Assert.IsTrue(data.FirstOrDefault()== "ID: 0 , Phone Number:  (02) 1234 5678");



            //user defined customers
            customer1 = new Customer();
            customer1.State = States.NSW;
            
            userDefinedCustomers = new List<Customer>();
            userDefinedCustomers.Add(customer1);
            customerService.Customers = commonService.GetAllCustomersWithPhoneNumber(userDefinedCustomers);
            data = customerService.GetCustomerswithPhoneNumber();

            Assert.IsTrue(data.Count() == 1);
            Assert.IsTrue(data.Any(x => x.Contains("Missing or invalid")) == true);
            Assert.IsTrue(data.FirstOrDefault() == "ID: 0 , Phone Number:  Missing or invalid");



        }

        [TestMethod()]
        public void GetPhoneNumbersPerStateTest()
        {
            var customers = commonService.GetAllCustomers();
            customerService.Customers = commonService.GetAllCustomersWithPhoneNumber(customers);
            var data = customerService.GetPhoneNumbersPerState();

            Assert.IsNotNull(data);
            Assert.IsTrue(data.Count==3);

            var first = data.FirstOrDefault();

            Assert.IsNotNull(first);
            Assert.IsTrue(first.Contains("3") ==true);
            Assert.IsTrue(first.Contains("NSW") == true);


            var last = data.LastOrDefault();

            Assert.IsNotNull(last);
            Assert.IsTrue(last.Contains("2") == true);
            Assert.IsTrue(last.Contains("QLD") == true);


            //customer with default to state NSW

            Customer customer1 = new Customer();
            customer1.PhoneNumber = "12345678";
            List<Customer> userDefinedCustomers = new List<Customer>();
            userDefinedCustomers.Add(customer1);

            customerService.Customers = commonService.GetAllCustomersWithPhoneNumber(userDefinedCustomers);
             data = customerService.GetPhoneNumbersPerState();

            Assert.IsNotNull(data);
            Assert.IsTrue(data.Count == 1);

             first = data.FirstOrDefault();

            Assert.IsNotNull(first);
            Assert.IsTrue(first.Contains("1") == true);
            Assert.IsTrue(first.Contains("NSW") == true);


             last = data.LastOrDefault();

            Assert.IsNotNull(last);
            Assert.IsTrue(last.Contains("1") == true);
            Assert.IsTrue(last.Contains("NSW") == true);



            customer1 = new Customer();
            Customer customer2 = new Customer();
            customer1.PhoneNumber = "12345678";


            customer2.PhoneNumber = "12345678";
            customer2.State = States.NSW;

            userDefinedCustomers = new List<Customer>();
            userDefinedCustomers.Add(customer1);
            userDefinedCustomers.Add(customer2);

            customerService.Customers = commonService.GetAllCustomersWithPhoneNumber(userDefinedCustomers);
            data = customerService.GetPhoneNumbersPerState();

            Assert.IsNotNull(data);
            Assert.IsTrue(data.Count == 1);

            first = data.FirstOrDefault();

            Assert.IsNotNull(first);
            Assert.IsTrue(first.Contains("2") == true);
            Assert.IsTrue(first.Contains("NSW") == true);


            last = data.LastOrDefault();

            Assert.IsNotNull(last);
            Assert.IsTrue(last.Contains("2") == true);
            Assert.IsTrue(last.Contains("NSW") == true);

        }
    }
}