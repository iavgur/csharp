﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests
{
    public class AddingContactToGroupTests : AuthTestBase
{
        [Test]

        public void TestAddingContactToGroup()
        {
            app.Contact.ContactExists();
            app.Group.GroupExists();

            GroupData group = GroupData.GetAll()[0];
            ContactData contact = ContactData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();
            List<GroupData> allGroups = contact.GetGroups();
            app.Contact.VerifyContactBeforeAdding(allGroups);
          
            contact = ContactData.GetAll().Except(group.GetContacts()).First();

            app.Contact.AddContactToGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }
    }
}
