using ICAN.FarzinSDK.WebServices.Proxy;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SBPMS_SDK
{
    public class PublicMethods
    {
        public static bool Login()
        {
            try
            {
                string errMessage = "";
                CSHA1 cSHA1 = new CSHA1();
                string password = cSHA1.hex_sha1(Farzininfo.Password);
                Authentication authentication = new Authentication(ConfigurationService.wsUrl);
                bool authRes = authentication.Login(Farzininfo.UserName, password, out errMessage);
                if (!authRes)
                    throw new Exception(errMessage);
                return authRes;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public static List<ResponseData> GetResponses(string responseFormat)
        {
            try
            {
                List<ResponseData> responses = new List<ResponseData>();
                XmlDocument xmldoc = new XmlDocument();
                XmlNodeList xmlnode;
                int i = 0;
                xmldoc.LoadXml(responseFormat);
                xmlnode = xmldoc.GetElementsByTagName("Response");
                for (i = 0; i <= xmlnode.Count - 1; i++)
                {
                    responses.Add(new ResponseData()
                    {
                        ReceiverCode= Convert.ToInt32(xmlnode[i].ChildNodes.Item(0).InnerText.Trim()),
                        Description= xmlnode[i].ChildNodes.Item(1).InnerText.Trim(),
                        Type= xmlnode[i].ChildNodes.Item(2).InnerText.Trim()
                    });         
                }
                return responses;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        internal static void AddLog(string source,string message, EventLogEntryType type,int eventID)
        {
            EventLog.WriteEntry(source, message, type, eventID);
        }
    }
}
