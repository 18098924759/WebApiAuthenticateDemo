using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string test = Convert.ToBase64String(Encoding.Default.GetBytes(string.Format("{0}:{1}", "Foo", "Password")));

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync("http://localhost:5000/api/demo").Result;
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Console.WriteLine("认证失败！");
                AuthenticationHeaderValue headerValue = response.Headers.WwwAuthenticate.FirstOrDefault();
                if (headerValue != null && headerValue.Scheme == "Basic")
                {
                    Console.WriteLine("用户名：");
                    string userName = Console.ReadLine().Trim();
                    Console.WriteLine("密码：");
                    string password = Console.ReadLine().Trim();
                    byte[] credential = Encoding.Default.GetBytes(string.Format("{0}:{1}", userName, password));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(credential));
                    response = client.GetAsync("http://localhost:5000/api/demo").Result;
                    string result = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(result);

                    response = client.GetAsync("http://localhost:5000/api/demo").Result;
                    result = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(result);
                }
            }
            Console.ReadLine();
        }
    }
}
