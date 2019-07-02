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

            List<ContactData> oldContacts = ContactData.GetAll();
            ContactData toBeRemoved = oldContacts[0];

            app.Contact.Remove(toBeRemoved);
            Assert.AreEqual(oldContacts.Count - 1, app.Contact.GetContactCount());

            List<ContactData> newContacts = ContactData.GetAll();

            oldContacts.RemoveAt(0);
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                Assert.AreNotEqual(contact.Id, toBeRemoved.Id);
            }
        }
    }
}
