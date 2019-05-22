using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace addressbook_web_tests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {        
        [Test]
        public void ContactCreationTest()
        {
            ContactData contact = new ContactData("Vladimir");
            contact.Lastname = "Geyer";

            app.Contact.Create(contact);                            
        }
    }
}
