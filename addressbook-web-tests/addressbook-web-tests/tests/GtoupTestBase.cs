using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tests
{
    public class GtoupTestBase : AuthTestBase
{
        [TearDown]
        public void CompareGroupUI_BD()
        {
            List<GroupData> fromUI = app.Group.GetGroupList();
            List<GroupData> fromDB = GroupData.GetAll();
            fromUI.Sort();
            fromDB.Sort();
            Assert.AreEqual(fromUI, fromDB);
        }
}
}
