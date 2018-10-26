using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BlockLib
{
   class Client
   {
      public string _url { get; set; }
      public List<Account> _Accounts { get; set; }
      private HttpWebRequest _Web { get; set; }
      public Client(string url)
      {
         _url = url;
         _Web = (HttpWebRequest)WebRequest.Create(_url);
         _Web.ContentType = "application/json";
         _Web.Method = "POST";
         _Accounts = new List<Account>();
      }
      public JObject GenAccount(string password)
      {
         JObject response = JsonCom.InvokeMethod(_Web, "personal_Account", password);
         _Accounts.Add(new Account(_url, response.Path));
         return response;
      }
      public JObject GetBalance(Account account)
      {
         JObject response = JsonCom.InvokeMethod(_Web, "eth_getBalance");
         return response;
      }

      public JObject SendTransaction()
      {
         JObject response = jsonMethod.InvokeMethod(_Web, "eth_sendTransaction");
         return response

      }
   }
   public class Account : Client
   {
      public string pubKey { get; set; }
      public string url { get; set; }
      public Account(string url, string pubKey) : base(url)
      {
         this.pubKey = pubKey;

      }
   }
}
