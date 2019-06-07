﻿using System;
using System.Collections.Generic;
using System.Text;
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

        public ContactHelper Modify(ContactData newData)
        {
            InitContactModification(0);
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
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + (index+1) + "]")).Click();
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
            driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + (index+1) + "]")).Click();
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
            string lastname = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");

            return new ContactData(firstname, lastname)
            {
                Address = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone,
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
            string allPhones = cells[5].Text;

            return new ContactData(firstname, lastname)
            {
                Address = address,
                AllPhones = allPhones,
            };
        }

    }
}
