using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockLib
{
   class Ethereum : Client
   {
      string _type { get; set; }
      public List<Account> _Accounts { get; set; }
      public Ethereum(string type, string url):base(url)
      {
         _type = type;
         _Accounts = new List<Account>();
      }


      public void GenAccount(string password)
      {
         try
         {
            String rawResponse = JsonCom.InvokeMethod(_Web, "personal_Account", password);
            JObject response = JsonConvert.DeserializeObject<JObject>(rawResponse);
            _Accounts.Add(new Account(response.Path));
         }
         
         
      }
      public JObject GetBalance(Account account)
      {
         String rawResponse = JsonCom.InvokeMethod(_Web, "eth_getBalance");
         JObject response = JsonConvert.DeserializeObject<JObject>(rawResponse);
         return response;
      }

      public JObject SendTransaction(Transaction trans)
      {

         String rawResponse = JsonCom.InvokeMethod(_Web, "eth_sendTransaction");
         JObject response = JsonConvert.DeserializeObject<JObject>(rawResponse);

         return response;

      }
      public SyncResposne Syncing()
      {
         string rawResponse = JsonCom.InvokeMethod(_Web, "eth_syncing");
         SyncResposne response = JsonConvert.DeserializeObject<SyncResposne>(rawResponse);
         return response;
      }
   }
   public class Account
   {
      public string pubKey { get; set; }
      public Account(string pubKey)
      {
         this.pubKey = pubKey;

      }
   }
   public class Transaction
   {
      string From { get; set; }
      string To { get; set; }
      string Gas { get; set; }
      UInt64 GasPrice { get; set; }
      UInt64 Value { get; set; }
      string Data { get; set; }

      public Transaction(string From, string To, string Gas, UInt64 GasPrice, UInt64 Value, string Data)
      {
         this.From = From;
         this.To = To;
         this.Gas = Gas;
         this.GasPrice = GasPrice;
         this.Value = Value;
         this.Data = Data;

      }
   }
   public class BasicResponse
   {
      int id { get; set; }
      string jsonrpc { get; set; }
       
   }
   public class SyncResposne : BasicResponse
   {
      class syncResults
      {
         Int64 startingBlock { get; set; }
         Int64 currentBlock { get; set; }
         Int64 highestBlock { get; set; }
      }
   }
   public class TransactionResponse
   {

   }
   
}
