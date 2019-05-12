using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace addressbook_web_tests
{
    [TestFixture]
    public class GroupCreationTests : TestBase
    {        
        [Test]
        public void GroupCreationTest()
        {
            app.Navigator.OpenHomePage();
            app.Auth.Login(new AccountData("admin", "secret"));
            app.Navigator.GoToGroupsPage();
            app.Group.InitGroupCreation();
            GroupData group = new GroupData("aaa");
            group.Header = "sss";
            group.Footer = "ddd";
            app.Group.FillGroupForm(group);
            app.Group.SubmitGroupCreation();
            app.Group.ReturnToGroupsPage();
            app.Auth.Logout();
        }          
    }
}
