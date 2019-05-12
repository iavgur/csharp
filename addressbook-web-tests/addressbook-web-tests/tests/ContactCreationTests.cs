using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace addressbook_web_tests
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {        
        [Test]
        public void ContactCreationTest()
        {
            app.Navigator.OpenHomePage();
            app.Auth.Login(new AccountData("admin", "secret"));
            app.Contact.InitContactCreation();
            ContactData contact = new ContactData("Vladimir");
            contact.Lastname = "Geyer";
            app.Contact.FillContactForm(contact);
            app.Contact.SubmitContactCreation();
            app.Contact.ReturnOnHomePage();
            app.Auth.Logout();
        }
    }
}
