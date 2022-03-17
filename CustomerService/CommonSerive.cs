using CustomerModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CustomerServices
{
    public class CommonSerive : ICommonService
    {
        public string BaseAddress { get; set; }

        public List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();
           
            try
            {
                var data = executeAPI();

                var options = new JsonSerializerOptions();
                options.Converters.Add(new JsonStringEnumConverter());

                customers = JsonSerializer.Deserialize<List<Customer>>(data, options);
            }
            catch(Exception ex)
            {
                throw new Exception (ex.Message);
            }
            

            return customers;
        }

        public List<Customer> GetAllCustomersWithPhoneNumber(List<Customer> data)
        {
          
          foreach(var customer in data)
            {
                customer.PhoneNumber = getPhoneNumber(customer.PhoneNumber,customer.State);
                if (customer.State==null && customer.PhoneNumber != Constants.MISSINGORINVALID)
                {
                    customer.State = States.NSW;
                }
            }

            return data;
        }

        private string executeAPI()
        {
            using (var client = new HttpClient())
            {
                using (var response = client.GetAsync(BaseAddress).Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var customerJsonString = response.Content.ReadAsStringAsync();
                        return customerJsonString.Result;
                    }

                }
            }

            return string.Empty;

        }
        private string getAreCode(States state)
        {
            int areCode =0;
            switch (state)
            {
                case States.NSW:
                case States.ACT:
                    areCode = (int)AreCodes.AREACODE02;

                    break;

                case States.VIC:
                case States.TAS:
                    areCode = (int)AreCodes.AREACODE03;
                    break;


                case States.QLD:
                    areCode = (int)AreCodes.AREACODE07;
                    break;

                case States.WA:
                case States.SA:
                case States.NT:
                    areCode = (int)AreCodes.AREACODE08;
                    break;
                case States.AUSWIDE:
                    areCode = (int)AreCodes.AREACODE04;
                    break;
            }

            return  string.Concat("0" ,areCode);
        }

        private  bool isAreaCodeValid(int areCode)
        {
          
            return Enum.IsDefined(typeof(AreCodes), areCode);
        }

        private string getPhoneNumber(string phoneNumber, States ? state)
        {
            if (string.IsNullOrEmpty(phoneNumber))
            {
                return Constants.MISSINGORINVALID;
            }
            else
            {
                var temp = string.Concat(phoneNumber.Where(x => char.IsDigit(x)).Select(x => x));

                if (temp.Length > 10 && temp.StartsWith("61")) // if start with country code its still valid
                {
                    temp = temp.Substring(2);
                }


                if (temp.Length < 8 || temp.Length > 10)
                {
                    return Constants.MISSINGORINVALID;
                }
                else if (temp.Length == 8) // area code is missing
                {
                    string areCode = string.Empty;

                    if (state == null)
                    {
                      state = States.NSW;
                    }

                    areCode = getAreCode(state.Value);

                    string tempPhoneNumber = string.Concat(areCode, temp);


                    return tempPhoneNumber.getFormatedPhoneNumber();
                }

                else if (temp.Length == 9) // 
                {
                    if (isAreaCodeValid(Convert.ToInt16(temp.Substring(0, 1))))
                    {

                        string tempPhoneNumber = string.Concat("0", temp);

                        //to check code number is as per state or not

                        if (state != null && tempPhoneNumber.Substring(0, 2) != getAreCode(States.AUSWIDE))
                        {
                            if (getAreCode(state.Value) == tempPhoneNumber.Substring(0, 2))
                            {
                                return tempPhoneNumber.getFormatedPhoneNumber();
                            }
                            else
                            {
                                return Constants.MISSINGORINVALID;
                            }
                        }
                        else
                        {
                            return tempPhoneNumber.getFormatedPhoneNumber();
                        }
                    }
                    else
                    {
                      return Constants.MISSINGORINVALID;
                    }
                }
                else if (temp.Length == 10) // need formating
                {
                    //to check code number is as per state or not
                    if (state != null && temp.Substring(0, 2) != getAreCode(States.AUSWIDE))
                    {
                        if (getAreCode(state.Value) == temp.Substring(0, 2))
                        {
                            return temp.getFormatedPhoneNumber();
                        }
                        else
                        {
                            return Constants.MISSINGORINVALID;
                        }
                    }
                    else
                    {
                        return temp.getFormatedPhoneNumber();
                    }

                   
                }
            }

            return Constants.MISSINGORINVALID;
        }

    }
}
