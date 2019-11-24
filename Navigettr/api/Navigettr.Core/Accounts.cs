using Navigettr.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Navigettr.Core
{
    public class Accounts: NavigettrEntities
    {

        //Varifying user credentials
        NavigettrEntities Objdb = new NavigettrEntities();
        public bool Login(string userName, string password)
        {
            try
            {

                var userInfo = Objdb.UserDetails.Where(x => x.UserName == userName && x.Password==password && x.Status=="ACTIVE").FirstOrDefault();
                if (userInfo != null)
                {
                    //string stringPwd = Encoding.ASCII.GetString(userInfo.Password);
                    //return stringPwd == password;
                    string stringPwd = userInfo.Password;
                    return stringPwd == password;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        ////regitering new 
        //public bool Register(UserDetails userData)
        //{
        //    try
        //    {
        //        //register new user
        //        Objdb.UserDetails.Add(userData);
        //        Objdb.SaveChanges();

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}

        //To get user role provided with username
        public string GetUserRole(string userName)
        {
            //return Objdb.UserDetails.Where(x => x.UserName == userName).Select(y => y.UserRole.RoleName).FirstOrDefault();
            return Objdb.UserDetails.Where(x => x.UserName == userName).FirstOrDefault().ToString();
        }

        public List<string> GetUserRoles()
        {
            return Objdb.Roles.Select(x => x.RoleName).ToList();
        }

    }
}