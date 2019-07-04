using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace addressbook_web_tests
{
    [TestFixture]
    public class GroupModificationTests : GroupTestBase
    {
        [Test]
        public void GroupModificationTest()
        {
            GroupData newData = new GroupData("zzz");
            newData.Header = null;
            newData.Footer = null;
            int i = 0;

            app.Group.GroupExists();

            List<GroupData> oldGroups = GroupData.GetAll();
            GroupData oldData = oldGroups[1];

            app.Group.Modify(oldData.Id, newData);

            Assert.AreEqual(oldGroups.Count, app.Group.GetGropCount());

            List<GroupData> newGroups = GroupData.GetAll();
            oldGroups[i].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                if (group.Id == oldData.Id)
                {
                    Assert.AreEqual(newData.Name, group.Name);
                }
            }
        }
    }
} 
