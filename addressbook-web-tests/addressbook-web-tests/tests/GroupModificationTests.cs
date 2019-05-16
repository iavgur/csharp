﻿using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace addressbook_web_tests
{
    [TestFixture]
    public class GroupModificationTests : TestBase
{
        [Test]
        public void GroupModificationTest()
        {
            GroupData newData = new GroupData("zzz");
            newData.Header = "xxx";
            newData.Footer = "ccc";

            app.Group.Modify(1, newData);
        }
    }
} 
