﻿using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace addressbook_web_tests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            ContactData newData = new ContactData("Sergey");
            newData.Lastname = "Andreev";

            app.Contact.ContactExists();
            app.Contact.Modify(newData);
        }
    }
}
