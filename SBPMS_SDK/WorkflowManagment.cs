using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICAN.FarzinSDK.WebServices.Proxy;

namespace SBPMS_SDK
{
    public class WorkflowManagment
    {
        public WorkflowManagment()
        {
            ConfigurationService _ConfigurationService = new ConfigurationService("Information");
        }
        public List<string> StartFlow()
        {
            try
            {
                List<string> strtFlowResult = new List<string>();
                if (PublicMethods.Login())
                {
                    FarzinDB db = new FarzinDB();
                    DataTable dt=db.Bulk_StartFlowXml_All(Farzininfo.batchStrtFlow);
                    ICAN.FarzinSDK.WebServices.Proxy.WorkflowManagment workflowManagment = new ICAN.FarzinSDK.WebServices.Proxy.WorkflowManagment(ConfigurationService.wsUrl);
                    foreach (DataRow dr in dt.Rows)
                    {
                        strtFlowResult.Add(workflowManagment.StartFlow(Convert.ToInt32(dr[0]), Convert.ToInt32(dr[1]), Convert.ToInt32(dr[2])));
                    } 
                }
                return strtFlowResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public string StartFlow(int etc,int ec,int senderRoleId)
        {
            try
            {
                string result = "";
                if (PublicMethods.Login())
                {
                    ICAN.FarzinSDK.WebServices.Proxy.WorkflowManagment workflowManagment = new ICAN.FarzinSDK.WebServices.Proxy.WorkflowManagment(ConfigurationService.wsUrl);
                    result=workflowManagment.StartFlow(etc,ec,senderRoleId);
                }
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        
    }
}
