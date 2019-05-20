using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace addressbook_web_tests
{
    public class LoginHelper : HelperBase
{

        public LoginHelper(ApplicationManager manager) : base(manager)
        { }
        
        public void Login(AccountData account)
        {
            Type(By.Name("user"), account.Username);
            Type(By.Name("password"), account.Password);
            driver.FindElement(By.XPath("//input[@value='Login']")).Click();
        }

        public void Logout()
        {
            driver.FindElement(By.LinkText("Logout")).Click();
        }
    }
}
