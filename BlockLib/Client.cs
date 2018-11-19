using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BlockLib
{
   public class Client
   {
      public string _url { get; set; }
      public HttpWebRequest _Web { get; set; }

      public Client(string url)
      {
         _url = url;
         _Web = (HttpWebRequest)WebRequest.Create(_url);
         _Web.ContentType = "application/json";
         _Web.Method = "POST";
      }
      
   }
  
}
