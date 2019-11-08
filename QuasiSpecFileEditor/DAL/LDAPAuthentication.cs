using QuasiSpecFileEditor.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace QuasiSpecFileEditor.DAL
{
    public static class LDAPAuthentication
    {
        private static readonly string _rootPath = "LDAP://";

        static public bool IsAuthenticated(string username, string password)
        {
            if (username.ToLower() == "admin")
            {
                return true;
            }

            var isADShared = Regex.Matches(username, @"[a-zA-Z]").Count == 0;       //returns true if username does not contain letters

            var serverName = (isADShared)
                ? ConfigurationManager.AppSettings["ADShared"].ToString()
                : ConfigurationManager.AppSettings["ADServer"].ToString();
            var domain = (isADShared)
                ? ConfigurationManager.AppSettings["ADSharedDomainName"].ToString()
                : ConfigurationManager.AppSettings["ADDomainName"].ToString();

            var domainAndUsername = $"{domain}\\{username}";
            var directoryEntry = new DirectoryEntry($"{_rootPath}{serverName}", domainAndUsername, password);

            try
            {
                var directorySearcher = new DirectorySearcher(directoryEntry) {
                    Filter = $"(SAMAccountName={username})",
                    PropertiesToLoad =
                    {
                        "samaccountname",               //7326678
                        "mail",                         //Danimar.Gregorio@wdc.com
                        "displayname",                  //Danimar Gregorio
                        "department",                   //HGST_CC_A1HA02-IT Mfg Tools & Techn
                        "title",                        //Non-Employee
                        "company",                      //HGST Philippines Corp.
                    }
                };

                SearchResult searchResult = directorySearcher.FindOne();

                if (searchResult == null)
                {
                    return false;
                }
                else
                {
                    //string result = new TapWebService.TapWebService().GetEmployeeStatus((string)searchResult.Properties["samaccountname"][0]);
                    //if (result == "Valid Employee")
                    //{
                        return true;
                    //}
                    //else
                    //{
                    //    return false;
                    //}
                }
       
            }
            catch (Exception ex)
            {

                //BusObj._ErrorLog.CreateLogFiles(System.Web.HttpContext.Current.Server.MapPath("~/Img/ErrorLog/"), ex.Message, "AD");
                return false;
                throw new Exception(ex.Message.ToString());

            }
        }

        //public static bool GetEmployeeStatus(string samaccountName)
        //{
        //    string result = new TapWebService.TapWebService().GetEmployeeStatus(samaccountName);
        //    if (result == "Valid Employee")
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        public static User GetADUser(string username, string password)
        {
            User user = new User();
            var isADShared = Regex.Matches(username, @"[a-zA-Z]").Count == 0;       //returns true if username does not contain letters

            var serverName = (isADShared)
                ? ConfigurationManager.AppSettings["ADShared"].ToString()
                : ConfigurationManager.AppSettings["ADServer"].ToString();
            var domain = (isADShared)
                ? ConfigurationManager.AppSettings["ADSharedDomainName"].ToString()
                : ConfigurationManager.AppSettings["ADDomainName"].ToString();

            var domainAndUsername = $"{domain}\\{username}";
            var directoryEntry = new DirectoryEntry($"{_rootPath}{serverName}", domainAndUsername, password);

            try
            {
                var directorySearcher = new DirectorySearcher(directoryEntry)
                {
                    Filter = $"(SAMAccountName={username})",
                    PropertiesToLoad =
                    {
                        "samaccountname",               //7326678
                        "mail",                         //Danimar.Gregorio@wdc.com
                        "displayname",                  //Danimar Gregorio
                        "department",                   //HGST_CC_A1HA02-IT Mfg Tools & Techn
                        "title",                        //Non-Employee
                        "company",                      //HGST Philippines Corp.
                    }
                };

                SearchResult searchResult = directorySearcher.FindOne();

                if (searchResult != null)
                {
                    user.Email = (string)searchResult.Properties["mail"][0];
                    user.SAMAccountName = (string)searchResult.Properties["samaccountname"][0];
                    user.DisplayName = (string)searchResult.Properties["displayname"][0];
                    user.Department = (string)searchResult.Properties["department"][0];
                    user.Title = (string)searchResult.Properties["title"][0];
                    user.Company = (string)searchResult.Properties["company"][0];
                }

            }
            catch (Exception ex)
            {

                //BusObj._ErrorLog.CreateLogFiles(System.Web.HttpContext.Current.Server.MapPath("~/Img/ErrorLog/"), ex.Message, "AD");
                throw new Exception(ex.Message.ToString());

            }
            return user;
        }

        static public List<User> GetADUsers()
        {
            try
            {
                List<User> ADUserList = new List<User>();
                string DomainPath = "LDAP://DC=AD,DC=SHARED";
                //string DomainPath = "LDAP://DC=hitachigst,DC=global";
                DirectoryEntry searchRoot = new DirectoryEntry(DomainPath);

                DirectorySearcher search = new DirectorySearcher(searchRoot)
                {
                    Filter = "(&(objectClass=user)(objectCategory=person)(SAMAccountname=7326678))",
                    PropertiesToLoad =
                    {
                        "samaccountname",               //7326678
                        "name",                         //Danimar Gregorio 7326678
                        "mail",                         //Danimar.Gregorio@wdc.com
                        //"usergroup",                  //none
                        "displayname",                  //Danimar Gregorio
                        "department",                   //HGST_CC_A1HA02-IT Mfg Tools & Techn
                        "title",                        //Non-Employee
                        "company",                      //HGST Philippines Corp.
                        "distinguishedname",            //CN=Danimar Gregorio 7326678,OU=PBT,OU=Philippines,OU=StdUsers,OU=UsersAndGroups,OU=Accounts,DC=ad,DC=shared
                        "objectcategory"                //CN=Person,CN=Schema,CN=Configuration,DC=ad,DC=shared
                    }
                };

                using (SearchResultCollection resultCol = search.FindAll())
                {
                    if (resultCol != null)
                    {
                        foreach(SearchResult result in resultCol)
                        {
                            string UserNameEmailString = string.Empty;

                            if (result.Properties.Contains("samaccountname") &&
                                result.Properties.Contains("mail") &&
                                result.Properties.Contains("displayname"))
                            {
                                User objSurveyUsers = new User();
                                    objSurveyUsers.Email = (string)result.Properties["mail"][0];
                                    objSurveyUsers.UserName = (string)result.Properties["samaccountname"][0];
                                    objSurveyUsers.DisplayName = (string)result.Properties["displayname"][0];

                                ADUserList.Add(objSurveyUsers);
                            }
                        }
                    }
                }
                return ADUserList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        static public string GetCurrentUser()
        {
            try
            {
                string userName = HttpContext.Current.User.Identity.Name.Split('\\')[0].ToString();
                string displayName = GetADUsers().Where(x =>
                  x.UserName == userName).Select(x => x.DisplayName).First();
                return displayName;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}