
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Threading;
using PetaPoco;

namespace WebApiGetData
{
    class Program
    {
        static void Main(string[] args)
        {
            User user = new User();
            user.UserSex = "男";
            user.UserName = "chali";
            user.UserPhone = "18482111111";
            user.UserAge = 12;

            GetDataByPage(2, 2);
           
            
            //Console.WriteLine(GetDataAsync().Result);
            // Console.WriteLine("-----------------------------------------");
            //var a = UpdateDataAsync(3, user).Result;
            //AddDataAsync(user);
            //Console.WriteLine(a);
            //DeleteDataAsync(5);

            //GetDataAsyncById(2);
            Console.ReadLine();
        }

        //查询数据显示
        public static async Task<string> GetDataAsync()
        {
            string url = "https://localhost:5001/Api/User";
            HttpClient client = new HttpClient();
            //GetAsync Get方法
            HttpResponseMessage response = await client.GetAsync(url);
            //ReadAsStringAsync
            string userStr = await response.Content.ReadAsStringAsync();
            return userStr;
        }

        //根据ID查询
        public static async Task<string> GetDataAsyncById(int id)
        {
            string url = "https://localhost:5001/Api/User/item/"+id;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            string userStr = await response.Content.ReadAsStringAsync();
            return userStr;
        }

        //修改数据
        public static async Task<string> UpdateDataAsync(int id,User user)
        {
            string url = "https://localhost:5001/Api/User/update/" + id;


            //创建一个处理序列化的DataContractJsonSerializer
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(User));
            MemoryStream ms = new MemoryStream();
            serializer.WriteObject(ms, user);
            //一定要在这设定Position
            ms.Position = 0;
            //将MemoryStream转成HttpContent
            HttpContent content = new StreamContent(ms);
            //一定要设定Header
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpClient client = new HttpClient();
            //由HttpClient发出PUT Method
            HttpResponseMessage response = await client.PutAsync(url, content);
            string requst =  await response.Content.ReadAsStringAsync();
            Console.WriteLine();
            if (response.IsSuccessStatusCode)
            {
                
                return "成功连接"+ requst;
            }
            else
            {
                return "连接失败"+ requst;
            }
        }

        //增加数据
        public static async Task<string> AddDataAsync(User user)
        {
            //访问Urls
            string url = "https://localhost:5001/Api/User/Add";
            
            //创建一个处理序列化的DataContractJsonSerializer 用来加数据
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(User));
            MemoryStream ms = new MemoryStream();
            serializer.WriteObject(ms, user);
            //一定要在这设定Position
            ms.Position = 0;
            //将MemoryStream转成HttpContent
            HttpContent content = new StreamContent(ms);
            //一定要设定Header
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpClient client = new HttpClient();
            //由HttpClient发出Post Method
            HttpResponseMessage response = await client.PostAsync(url, content);
            string requst = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {

                return "成功连接  " + requst;
            }
            else
            {
                return "连接失败  " + requst;
            }

        }

        //删除数据
        public static async Task DeleteDataAsync(int id)
        {
            string url = "https://localhost:5001/Api/User/delete/"+id;
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.DeleteAsync(url);
            string a = await response.Content.ReadAsStringAsync();
            Console.WriteLine(a);
        }

        //分页查询
        public static async Task<List<User>> GetDataByPage(int page,int pageSize)
        {
            string url = "https://localhost:5001/Api/User"+ "/item/page="+page+ "&pageSize=" + pageSize;
            HttpClient client = new HttpClient();
            //GetAsync Get方法
            HttpResponseMessage response = await client.GetAsync(url);
            //ReadAsStringAsync
            string userStr = await response.Content.ReadAsStringAsync();
            Page<User> p = JsonConvert.DeserializeObject<Page<User>>(userStr);
            List<User> userList  = p.Items;
            foreach (var user in userList)
            {
                Console.WriteLine(user.Id);
            }
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine(userStr);
            return userList;

        }
    }
}
