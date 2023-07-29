using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.XPath;

namespace SBPMS_SDK
{
    public class ConfigurationService
    {
        #region [Fields]

        private string _siteConfiPath;

        public static string ConnectionString { set; get; }
        //public static Farzininfo EntityInfo { get; set; }
        public static string wsUrl { get; set; }

        #endregion

        #region [Ctor]

        public ConfigurationService(string prograName)
        {
            try
            {
                if (string.IsNullOrEmpty(prograName))
                    throw new ArgumentNullException("prograName");
                this.ProgramName = prograName;


                var path = GetConfigPath();
                _siteConfiPath = Path.Combine(path, "Config.xml");

                Refresh();

                LoadConfigFile();
            }
            catch (Exception eX)
            {
                
                throw eX;
            }

        }

        #endregion

        #region [Utilities]

        public void LoadConfigFile()
        {
            try
            {
                var dbserver = this.Settings["DB_Server"];
                var dbname = this.Settings["DB_DataBaseName"];
                var dbusername = this.Settings["DB_Username"];
                var dbpassword = this.Settings["DB_Password"];
                var farzinpassword = this.Settings["FarzinPassword"];
                var farzinusername = this.Settings["FarzinUserName"];
                var FarzinWebServiceUrl = this.Settings["FarzinWebServiceUrl"];
                var batchStrtFlow = this.Settings["BatchStartFlowSP"];
                var batchInsertDocument = this.Settings["BatchInsertDocumentSP"];
                
                string _connectionString = "Server=" + dbserver.ToString() + ";Database=" + dbname.ToString() + ";User Id=" + dbusername.ToString() + ";Password=" + dbpassword.ToString() + ";";
                Farzininfo.UserName = farzinusername;
                Farzininfo.Password = farzinpassword;
                Farzininfo.batchStrtFlow= batchStrtFlow;
                Farzininfo.batchInsertDocument= batchInsertDocument;
                ConnectionString = _connectionString;
                wsUrl = FarzinWebServiceUrl;
            }
            catch (Exception eX)
            {
                
                throw eX;
            }

        }

        private void Refresh()
        {
            if (Settings != null)
            {
                Settings.Clear();
                Settings = null;
            }

            try
            {
                var xDoc = XDocument.Load(_siteConfiPath);
                var configuration = xDoc.XPathSelectElement("Configuration/"+ProgramName);

                //Load Settings
                var settingsElement = configuration.XPathSelectElement("Settings");
                if (settingsElement == null) return;

                var settingElements = settingsElement.Elements().Select(se => new
                {
                    Key = se.Name.LocalName,
                    se.Attribute("Value").Value
                });

                Settings = settingElements.ToDictionary(s => s.Key, s => s.Value);
            }
            catch (Exception eX)
            {
                throw eX;
            }
        }


        private string GetConfigPath()
        {
            //From App Data
            //var path = (AppDomain.CurrentDomain.GetData("DataDirectory") ?? Path.Combine(Application.StartupPath, "App_Data")).ToString();
            //return path;
            //return Path.Combine(path, "Config.xml");


            //From ParentDirectory of  warpper

            try
            {
                var aep = Assembly.GetCallingAssembly().Location;
                var p1 = Directory.GetParent(aep);
                var p2 = Directory.GetParent(p1.FullName);
                return p2 == null ? p1.FullName : p2.FullName;
                //if (p1 == null)
                //    return aep;
                //else
                //    return p1.FullName;
            }
            catch (Exception eX)
            {
                
                throw eX;
            }
           

            //var p2 = Directory.GetParent(p1.FullName);
            //return p2 == null ? p1.FullName : p2.FullName;
        }

        #endregion

        #region [Methods]

        public string GetSettingByKey(string programName, string key, string defaultValue = "")
        {
            string value;
            Settings.TryGetValue(key, out value);
            if (string.IsNullOrEmpty((value)))
                return defaultValue;

            return value;
        }

        #endregion

        #region [Properties]

        public string ProgramName { get; private set; }
        public Dictionary<string, string> Settings { get; private set; }

        #endregion
    }
}