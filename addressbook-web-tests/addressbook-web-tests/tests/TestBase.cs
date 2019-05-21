using System;
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
            app = ApplicationManager.GetInstance();                       
        }
    }
}
