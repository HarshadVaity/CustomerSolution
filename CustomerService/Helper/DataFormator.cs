using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerServices
{
   

    public static class DataFormator
    {
        public static string getFormatedPhoneNumber(this string phoneNumber)
        {
            return string.Format("{0}{1}{2} {3} {4}", "(", phoneNumber.Substring(0,2), ")", phoneNumber.Substring(2,4), phoneNumber.Substring(6,4));
        }
    }
}
