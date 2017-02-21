using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TutorMaster
{
    //class TutorMasterClasses
    //{
    //}

    public partial class User
    {
        public User()
        {
        }
        //private int ID;
        //private string FirstName;
        //private string LastName;
        //private string Email;
        //private string PhoneNumber;
        //private string AccountType;
        //private string Password;
        //private string Username;

        public User(int id, string firstname, string lastname, string email, string phonenumber, string accounttype, string password, string username)
        {
            this.ID = id;
            this.FirstName = firstname;
            this.LastName = lastname;
            this.Email = email;
            this.PhoneNumber = phonenumber;
            this.AccountType = accounttype;
            this.Password = password;
            this.Username = username;
        }
    }
}
