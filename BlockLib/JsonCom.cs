using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BlockLib
{
    class JsonCom
    {
      public static string InvokeMethod(HttpWebRequest web, string a_sMethod, params object[] a_params)
      {
         HttpWebRequest _Web = web;

         JObject joe = new JObject();
         joe["jsonrpc"] = "2.0";
         joe["method"] = a_sMethod;

         if (a_params != null)
         {
            //if (a_params.Length > 0)
            //{
            //   JArray props = new JArray();
            //   foreach (var p in a_params)
            //   {
            //      props.Add(p);
            //   }
            //   joe.Add(new JProperty("params", props));
            //}
            JArray props = new JArray();
            foreach (var p in a_params)
            {
               props.Add(p);
            }
            joe.Add(new JProperty("params", props));

         }
         joe["id"] = "1";


         string s = JsonConvert.SerializeObject(joe);
         // serialize json for the request
         byte[] byteArray = Encoding.UTF8.GetBytes(s);
         _Web.ContentLength = byteArray.Length;

         try
         {
            using (Stream dataStream = _Web.GetRequestStream())
            {
               dataStream.Write(byteArray, 0, byteArray.Length);
            }
         }
         catch (WebException we)
         {
            //inner exception is socket
            //{"A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 23.23.246.5:8332"}
            throw;
         }
         WebResponse webResponse = null;
         try
         {
            using (webResponse = _Web.GetResponse())
            {
               using (Stream str = webResponse.GetResponseStream())
               {
                  using (StreamReader sr = new StreamReader(str))
                  {
                     return sr.ReadToEndAsync().Result;
                     //return JsonConvert.DeserializeObject<JObject>(sr.ReadToEnd());
                  }
               }
            }
         }
         catch (WebException webex)
         {

            using (Stream str = webex.Response.GetResponseStream())
            {
               using (StreamReader sr = new StreamReader(str))
               {
                  //JObject tmp = (JObject)sr.ReadToEnd();

                  return sr.ReadToEndAsync().Result;
                  //return JsonConvert.DeserializeObject<JObject>(sr.ReadToEnd());

               }
            }

         }
         catch (Exception)
         {

            throw;
         }
      }
   }
}
