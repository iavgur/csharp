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
        public void SetupApplicationManager()
        {
            app = ApplicationManager.GetInstance();
        }

        public static Random rnd = new Random();

        public static string GenerateRandomString(int max)
        {
            int l = Convert.ToInt32(rnd.NextDouble() * max);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < l; i++)
            {
                //builder.Append(Convert.ToChar(32 + Convert.ToInt32(rnd.NextDouble() * 65)));
                builder.Append((char)rnd.Next(28, 127) + (char)rnd.Next(21, 27));
            }
            return builder.ToString();
        }
    }
}
