﻿using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;


namespace addressbook_web_tests
{
    public class TestBase
{
        protected ApplicationManager app;
              
        
        [SetUp]
        public void SetupTest()
        {
            app = new ApplicationManager();
            app.Navigator.OpenHomePage();
            app.Auth.Login(new AccountData("admin", "secret"));            
        }

        [TearDown]
        public void TeardownTest()
        {
            app.Auth.Logout();
            app.Stop();
        }
    }
}
