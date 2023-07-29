using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBPMS_SDK
{
    public class CartableManagment
    {
        public int Response(string responseFormat)
        {
            try
            {
                string errMessage = "";
                int counter = 0;
                ConfigurationService _ConfigurationService = new ConfigurationService("Information");
                if (PublicMethods.Login())
                {
                    List<ResponseData> responses= PublicMethods.GetResponses(responseFormat);
                    ICAN.FarzinSDK.WebServices.Proxy.CartableManagment cartable = new ICAN.FarzinSDK.WebServices.Proxy.CartableManagment(ConfigurationService.wsUrl);
                    foreach (var response in responses)
                    {
                        cartable.Response(response.ReceiverCode, response.Description, response.Type,out errMessage);
                        counter++;
                    }
                    PublicMethods.AddLog(".NET Runtime", "Count Of Entites: "+counter, EventLogEntryType.Information,1000);
                }
                return counter;
            }
            catch (Exception ex)
            {
                PublicMethods.AddLog(".NET Runtime", ex.Message+" - " + ex.StackTrace +" - " + ConfigurationService.wsUrl, EventLogEntryType.Error,1000);
                return -1;
            }
        }
    }
}
