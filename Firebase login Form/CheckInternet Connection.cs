using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firebase_login_Form
{
    class CheckInternet_Connection
    {
        public static bool CheckInternetConnection()
        {
            try
            {
                using (var client = new System.Net.WebClient())
                using (var stream = client.OpenRead("https://google.com"))
                {
                    return true;
                }
            }catch
            {
                return false;
            }
        }
    }
}
