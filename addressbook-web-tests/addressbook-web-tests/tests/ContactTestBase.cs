using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests
{
    public class ContactTestBase : AuthTestBase
    {
        [TearDown]
        public void CompareContactUI_DB()
        {
            if (PERFORM_LONG_UI_CHECKS)
            {
                List<ContactData> fromDB = app.Contact.GetContactList();
                List<ContactData> fromUI = ContactData.GetAll();
                fromUI.Sort();
                fromDB.Sort();
                Assert.AreEqual(fromUI, fromDB);
            }

        }
    }
}