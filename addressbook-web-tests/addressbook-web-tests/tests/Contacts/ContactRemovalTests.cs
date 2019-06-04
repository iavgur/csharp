using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace addressbook_web_tests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            app.Contact.ContactExists();

            List<ContactData> oldContacts = app.Contact.GetContactList();

            app.Contact.Remove(0);

            List<ContactData> newContacts = app.Contact.GetContactList();

            oldContacts.RemoveAt(0);
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
