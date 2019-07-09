using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests
{
    public class RemovingContactFromGroupTest : AuthTestBase
    {
        [Test]
        public void TestRemovingContactFromGroup()
        {
            app.Contact.ContactExists();
            app.Group.GroupExists();

            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();
            ContactData contact = ContactData.GetAll().First();
            app.Contact.VerifyContactExistInGroup(contact, oldList, group);

            app.Contact.RemoveContactFromGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Remove(contact);
            newList.Sort();
            oldList.Sort();
                        
            Assert.AreEqual(oldList, newList);
        }
    }
}