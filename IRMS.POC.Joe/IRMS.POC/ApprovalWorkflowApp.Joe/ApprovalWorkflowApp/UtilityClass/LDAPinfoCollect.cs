using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.Linq;
using System.Web;


namespace ApprovalWorkflowApp.UtilityClass
{
    public class LDAPinfoCollect
    {

        public static UserDetails GetUserDetails(string loginID)
        {
            UserDetails user = new UserDetails();
            string userName = string.Empty;
            if (loginID.Contains("\\"))
            {
                // strip out domain
                userName = loginID.Split('\\')[1];
            }
            else
            {
                userName = loginID;
            }

            /*Code commented on 02/12/2015 by Shekhar. Reason: Now it is out of scope
            if (userName.ToUpper().Contains("ADMIN_"))
                userName = userName.Substring(6);
            */
            try
            {
                DirectoryEntry root;
                DirectorySearcher searcher;
                string m = ""; // sk05
                string sEmail = string.Empty;
                string bindDN = "";
                string bindPass = "";
                string ldapHost = ConfigurationManager.AppSettings["LDAPHOST"];//CommonUtility.LDAPHOST;
                string ldapPort = ConfigurationManager.AppSettings["LDAPPORT"]; //CommonUtility.LDAPPORT;
                string baseDN = ConfigurationManager.AppSettings["BASEDN"]; //CommonUtility.BASEDN;
                string ldapPath = ConfigurationManager.AppSettings["LDAPPATH"]; //CommonUtility.LDAPPATH;

                DirectoryEntry dEntry = new DirectoryEntry("LDAP://" + ldapHost + ":" + ldapPort + "/" + baseDN);
                dEntry.Username = bindDN;
                dEntry.Password = bindPass;
                dEntry.AuthenticationType = AuthenticationTypes.FastBind;

                DirectorySearcher dSearch = new DirectorySearcher(dEntry);
                root = new DirectoryEntry(ldapPath);
                searcher = new DirectorySearcher(root);


                dSearch.Filter = "(&(jnjMSUsername=" + userName + "))";
                dSearch.PropertiesToLoad.Add("mail");

                dSearch.PropertiesToLoad.Add("givenname");
                dSearch.PropertiesToLoad.Add("sn");

                foreach (System.DirectoryServices.SearchResult dResult in dSearch.FindAll())
                {
                    System.DirectoryServices.PropertyCollection pColl = dResult.GetDirectoryEntry().Properties;

                    if (pColl != null && pColl["mail"].Value != null)
                    {

                        user.UserName = pColl["givenname"].Value.ToString() + " " + pColl["sn"].Value.ToString();
                        user.UserEmailId = pColl["mail"].Value.ToString();
                        user.UserWWID = pColl["uid"].Value.ToString();
                        user.UserFirstName= pColl["givenName"].Value.ToString();
                        user.UserLastName = pColl["sn"].Value.ToString();
                        user.UserNetworkId = userName;
                        string ManagerStr = pColl["manager"].Value.ToString();
                        string ManagerNamewithOU = ManagerStr.Split(',')[1];
                        string ManagerWWID = ManagerNamewithOU.Remove(ManagerNamewithOU.IndexOf("OU="), ("OU=").Length);
                        user.ManagerName = (ManagerStr.Split(',')[0]).Remove((ManagerStr.Split(',')[0]).IndexOf("CN="), ("CN=").Length);
                        user.ManagerWWID = ManagerWWID;

                        dSearch.Filter = "(&(uid=" + ManagerWWID + "))";
                        foreach (System.DirectoryServices.SearchResult dResult1 in dSearch.FindAll())
                        {
                            System.DirectoryServices.PropertyCollection pCol2 = dResult1.GetDirectoryEntry().Properties;

                            user.ManagerEmailId = pCol2["mail"].Value.ToString();
                            user.ManagerFirstName = pCol2["givenName"].Value.ToString();
                            user.ManagerLastName = pCol2["sn"].Value.ToString();
                            user.ManagerNetworkId = pCol2["jnjMSUsername"].Value.ToString();
                        }
                    }
                }

                dEntry.Close();
                if (String.IsNullOrEmpty(m))
                    m = userName;
                return user;
            }
            catch (Exception Ex) { throw Ex; }
        }

        public static string GetUserName(string loginID)
        {
            string RealUserName = string.Empty;
            string userName = string.Empty;
            if (loginID.Contains("\\"))
            {
                userName = loginID.Split('\\')[1];
            }
            else
            {
                userName = loginID;
            }

            try
            {
                DirectoryEntry root;
                DirectorySearcher searcher;
                string m = ""; // sk05
                string sEmail = string.Empty;
                string bindDN = "";
                string bindPass = "";
                string ldapHost = ConfigurationManager.AppSettings["LDAPHOST"];
                string ldapPort = ConfigurationManager.AppSettings["LDAPPORT"]; 
                string baseDN = ConfigurationManager.AppSettings["BASEDN"]; 
                string ldapPath = ConfigurationManager.AppSettings["LDAPPATH"]; 

                DirectoryEntry dEntry = new DirectoryEntry("LDAP://" + ldapHost + ":" + ldapPort + "/" + baseDN);
                dEntry.Username = bindDN;
                dEntry.Password = bindPass;
                dEntry.AuthenticationType = AuthenticationTypes.FastBind;

                DirectorySearcher dSearch = new DirectorySearcher(dEntry);
                root = new DirectoryEntry(ldapPath);
                searcher = new DirectorySearcher(root);


                dSearch.Filter = "(&(jnjMSUsername=" + userName + "))";

                dSearch.PropertiesToLoad.Add("givenname");
                dSearch.PropertiesToLoad.Add("sn");

                foreach (System.DirectoryServices.SearchResult dResult in dSearch.FindAll())
                {
                    System.DirectoryServices.PropertyCollection pColl = dResult.GetDirectoryEntry().Properties;

                    if (pColl != null && pColl["givenname"].Value != null)
                    {

                        RealUserName = pColl["givenname"].Value.ToString() + " " + pColl["sn"].Value.ToString();
                        
                    }
                }

                dEntry.Close();
                
                return RealUserName;
            }
            catch (Exception Ex) { throw Ex; }
        }
    }
}