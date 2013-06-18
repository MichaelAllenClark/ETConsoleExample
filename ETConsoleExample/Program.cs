using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ETConsoleExample.ExactTargetSOAPAPI;


namespace ETConsoleExample
{
    class Program
    {
        static void Main(string[] args)
        {
            BasicHttpBinding binding = new BasicHttpBinding();
            binding.Name = "UserNameSoapBinding";
            binding.Security.Mode = BasicHttpSecurityMode.TransportWithMessageCredential;
            binding.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.UserName;
            binding.ReceiveTimeout = new TimeSpan(0, 5, 0);
            binding.OpenTimeout = new TimeSpan(0, 5, 0);
            binding.CloseTimeout = new TimeSpan(0, 5, 0);
            binding.SendTimeout = new TimeSpan(0, 5, 0);

            EndpointAddress endpoint = new EndpointAddress("https://webservice.exacttarget.com/Service.asmx");
            SoapClient ETSOAPAPI = new SoapClient(binding, endpoint);

            ETSOAPAPI.ClientCredentials.UserName.UserName = "ccc";
            ETSOAPAPI.ClientCredentials.UserName.Password = "ccc";

            RetrieveRequest getLists = new RetrieveRequest();
            getLists.ObjectType = "List";
            getLists.Properties = new string[] { "ID", "ListName" };

            String RequestID = String.Empty;
            APIObject[] responseObjects = new APIObject[] { };
            String OverallStatus = String.Empty;

            OverallStatus = ETSOAPAPI.Retrieve(getLists, out RequestID, out responseObjects);

            Console.WriteLine("OverallStatus: " + OverallStatus + "  RequestID: " + RequestID);

            if (responseObjects.Length > 0)
            {
                foreach (List l in responseObjects)
                {
                    Console.WriteLine("ListID : " + l.ID.ToString() + " Name: " + l.ListName);
                }
            }
            Console.ReadLine();
        }
    }
}
