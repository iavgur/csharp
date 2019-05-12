using System;
using System.Collections.Generic;
using System.Text;

namespace addressbook_web_tests
{
    public class AccountData
{
    private string username;
    private string password;

    public AccountData(string usernam, string password)
    {
        this.username = usernam;
        this.password = password;
    }

    public string Username
    {
        get
        {
            return username;
        }
        set
        {
            username = value;
        }
    }

    public string Password
    {
        get
        {
            return password;
        }
        set
        {
            password = value;
        }
    }
}
}
