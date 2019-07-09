using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace addressbook_web_tests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base(manager)
        { }

        public ContactHelper Create(ContactData contact)
        {
            InitContactCreation();
            FillContactForm(contact);
            SubmitContactCreation();
            manager.Navigator.OpenHomePage();
            return this;
        }

        internal void VerifyContactExistInGroup(ContactData contact, List<ContactData> oldList, GroupData group)
        {
            if (oldList.Count == 0)
            {
                AddContactToGroup(contact, group);
            }
        }

        internal void VerifyContactBeforeAdding(List<GroupData> groupContactList)
        {
            List<GroupData> allGroups = GroupData.GetAll();

            if (allGroups.Count.Equals(groupContactList.Count))
            {
                ContactData contact = new ContactData("lName", "fName");
                Create(contact);
            }
        }

        public void AddContactToGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.OpenHomePage();
            ClearGroupFilter();
            SelectContactToAdd(contact.Id);
            SelectGroupToAdd(group.Name);
            CommitAddingContactToGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        internal void RemoveContactFromGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.OpenHomePage();
            ClearGroupFilter();
            SelectGroupToDelete(group.Name);
            SelectContactInGroup(contact.Id);
            CommitDelettingContactFromGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        private void CommitDelettingContactFromGroup()
        {
            driver.FindElement(By.Name("remove")).Click();
        }

        private void SelectContactInGroup(string contactId)
        {
            driver.FindElement(By.Id(contactId)).Click();
        }

        private void SelectGroupToDelete(string name)
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText(name);
        }

        public void CommitAddingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }

        public void SelectGroupToAdd(string name)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(name);
        }

        public void SelectContactToAdd(string contactId)
        {
            driver.FindElement(By.Id(contactId)).Click();
        }

        public void ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
        }

        public ContactHelper Modify(ContactData newData)
        {
            InitContactModification(0);
            FillContactForm(newData);
            SubmitContactModification();
            manager.Navigator.OpenHomePage();

            return this;
        }

        public ContactHelper Modify(int index, ContactData newData)
        {
            InitContactModification(index);
            FillContactForm(newData);
            SubmitContactModification();
            manager.Navigator.OpenHomePage();

            return this;
        }

        public ContactHelper Remove(int v)
        {
            SelectContact(v);
            RemoveContact();
            manager.Navigator.OpenHomePage();
            return this;
        }

        public ContactHelper Remove(ContactData contact)
        {
            SelectContact(contact.Id);
            RemoveContact();
            manager.Navigator.OpenHomePage();
            return this;
        }

        public ContactHelper InitContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("lastname"), contact.Lastname);
            return this;
        }

        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.XPath("(//input[@name='submit'])[2]")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper ReturnOnHomePage()
        {
            driver.FindElement(By.LinkText("home")).Click();
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index + 1) + "]")).Click();
            return this;
        }

        public ContactHelper SelectContact(string id)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]' and @value ='"+id+"'])")).Click();
            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper InitContactModification(int index)
        {
            driver.FindElements(By.Name("entry"))[index]
               .FindElements(By.TagName("td"))[7]
               .FindElement(By.TagName("a")).Click();
            return this;
        }

        public ContactHelper OpenDitails(int index)
        {
            driver.FindElement(By.XPath("(//img[@alt='Details'])[" + (index + 1) + "]")).Click();
            return this;
        }

        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            driver.SwitchTo().Alert().Accept();
            contactCache = null;
            return this;
        }

        public ContactHelper ContactExists()
        {
            if (!IsElementPresent(By.XPath("(//img[@alt='Edit'])")))
            {
                ContactData contact = new ContactData("Vladimir", "Geyer");
                Create(contact);
            }
            return this;
        }

        private List<ContactData> contactCache = null;

        public List<ContactData> GetContactList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactData>();
                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("tr[name = entry]"));
                foreach (IWebElement element in elements)
                {
                    contactCache.Add(new ContactData(element.FindElement(By.XPath(".//td[3]")).Text,
                          element.FindElement(By.XPath(".//td[2]")).Text)
                    {
                        Id = element.FindElement(By.TagName("input")).GetAttribute("value")
                    });
                }
            }
            return new List<ContactData>(contactCache);
        }
        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.OpenHomePage();
            InitContactModification(0);

            string firstname = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string middlename = driver.FindElement(By.Name("middlename")).GetAttribute("value");
            string lastname = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string nickname = driver.FindElement(By.Name("nickname")).GetAttribute("value");
            string company = driver.FindElement(By.Name("company")).GetAttribute("value");
            string title = driver.FindElement(By.Name("title")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");
            string fax = driver.FindElement(By.Name("fax")).GetAttribute("value");

            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            return new ContactData(firstname, lastname)
            {
                Middlename = middlename,
                Nickname = nickname,
                Company = company,
                Title = title,
                Address = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,
                Fax = fax,
                Email = email,
                Email2 = email2,
                Email3 = email3,
            };
        }

        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.OpenHomePage();

            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"));
            string lastname = cells[1].Text;
            string firstname = cells[2].Text;
            string address = cells[3].Text;
            string allEmails = cells[4].Text;
            string allPhones = cells[5].Text;

            return new ContactData(firstname, lastname)
            {
                Address = address,
                AllPhones = allPhones,
                AllEmails = allEmails,
            };
        }

        public ContactData GetContactInformationFromDitails(int index)
        {
            manager.Navigator.OpenHomePage();
            OpenDitails(0);

            string detailsInfo = driver.FindElement(By.XPath("//div[@id='content']")).Text;
         
            return new ContactData(detailsInfo);            
        }

        public int GetContactCount()
        {
            manager.Navigator.OpenHomePage();
            return driver.FindElements(By.XPath(".//tr[@name='entry']")).Count;
        }
    }
}
