using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace addressbook_web_tests
{
    [TestFixture]
    public class ContactRemovalTests : TestBase
{
        [Test]
        public void ContactRemovalTest()
        {
            app.Contact.Remove(1);
        }
    }
}
