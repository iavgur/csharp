using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;

namespace addressbook_web_tests
{
    [TestFixture]
    public class GroupCreationTests : AuthTestBase
    {        
        [Test]
        public void GroupCreationTest()
        {
            GroupData group = new GroupData("aaa");
            group.Header = "sss";
            group.Footer = "ddd";

            List<GroupData> oldGroups = app.Group.GetGroupLIst();
            app.Group.Create(group);

            List<GroupData> newGroups = app.Group.GetGroupLIst();
            Assert.AreEqual(oldGroups.Count + 1, newGroups.Count);
        }

        [Test]
        public void EmptyGroupCreationTest()
        {
            GroupData group = new GroupData("");
            group.Header = "";
            group.Footer = "";

            List<GroupData> oldGroups = app.Group.GetGroupLIst();
            app.Group.Create(group);

            List<GroupData> newGroups = app.Group.GetGroupLIst();
            Assert.AreEqual(oldGroups.Count + 1, newGroups.Count);
        }
    }
}
