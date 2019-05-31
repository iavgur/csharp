using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;

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

            List<ContactData> oldContacts = app.Contact.GetContactList();

            app.Contact.Create(contact);

            List<ContactData> newContacts = app.Contact.GetContactList();
            Assert.AreEqual(oldContacts.Count + 1, newContacts.Count);
        }
    }
}
