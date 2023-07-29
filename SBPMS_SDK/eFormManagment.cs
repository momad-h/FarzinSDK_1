using ICAN.FarzinSDK.WebServices.Proxy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBPMS_SDK
{
    public class eFormManagment
    {
        public eFormManagment()
        {
            ConfigurationService _ConfigurationService = new ConfigurationService("Information");
        }
        public List<string> InsertDocument()
        {
            try
            {
                List<string> insertDocResult = new List<string>();
                if (PublicMethods.Login())
                {
                    FarzinDB db = new FarzinDB();
                    DataTable dt = db.Bulk_InsertDocumentXml_All(Farzininfo.batchInsertDocument);
                    ICAN.FarzinSDK.WebServices.Proxy.eFormManagment eFormManagment = new ICAN.FarzinSDK.WebServices.Proxy.eFormManagment(ConfigurationService.wsUrl);
                    foreach (DataRow dr in dt.Rows)
                    {
                        insertDocResult.Add(eFormManagment.InsertDocument(dr[0].ToString()));
                    }
                }
                return insertDocResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public string InsertDocument(string xmlData)
        {
            try
            {
                string insertDocResult = "";
                if (PublicMethods.Login())
                {
                    ICAN.FarzinSDK.WebServices.Proxy.eFormManagment eFormManagment = new ICAN.FarzinSDK.WebServices.Proxy.eFormManagment(ConfigurationService.wsUrl);
                    insertDocResult=eFormManagment.InsertDocument(xmlData);
                }
                return insertDocResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        
    }
}
