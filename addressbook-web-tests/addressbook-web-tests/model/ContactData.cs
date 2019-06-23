using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace addressbook_web_tests
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>
{
        private string allPhones;
        private string allEmails;
        private string detailsInfo;

        public ContactData(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }

        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            if (Lastname == other.Lastname)
            {
                return Firstname == other.Firstname;
            }
            return Lastname == other.Lastname;
        }

        public override int GetHashCode()
        {
            return Lastname.GetHashCode();
        }

        public override string ToString()
        {
            return "name=" + Firstname + " " + Lastname;
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            if (Lastname == other.Lastname)
            {
                return Firstname.CompareTo(other.Firstname);
            }
            return Lastname.CompareTo(other.Lastname);
        }

        public ContactData()
        {
            
        }

        public string Firstname { get; set; }
        
        public string Lastname { get; set; }

        public string Middlename { get; set; }

        public string Nickname { get; set; }

        public string Company { get; set; }

        public string Title { get; set; }

        public string Address { get; set; }

        public string HomePhone { get; set; }

        public string MobilePhone { get; set; }

        public string WorkPhone { get; set; }

        public string Fax { get; set; }

        public string Email { get; set; }

        public string Email2 { get; set; }

        public string Email3 { get; set; }

        public string Homepage { get; set; }

        public string Bday { get; set; }

        public string Bmonth { get; set; }

        public string Byear { get; set; }

        public string Address2 { get; set; }

        public string HomePhone2 { get; set; }

        public string Notes { get; set; }

        public string AllPhones
        {
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone) + CleanUp(Fax)).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }

        public string AllEmails
        {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                } 
                else
                {
                    return (CleanUp2(Email) + CleanUp2(Email2) + CleanUp2(Email3)).Trim();
                }
            }
            set
            {
                allEmails = value;
            }
        }

        public string DetailsInfo
        {
            get
            {
                if (detailsInfo != "")
                {
                    return detailsInfo;
                }
                else
                {
                    if (Firstname != "")
                    {
                        detailsInfo += Firstname + " ";
                    }

                    if (Middlename != "")
                    {
                        detailsInfo += Middlename + " ";
                    }

                    if (Lastname != "")
                    {
                        detailsInfo += Lastname + "\r\n";
                    }

                    if (Nickname != "")
                    {
                        detailsInfo += Nickname + "\r\n";
                    }

                    if (Title != "")
                    {
                        detailsInfo += Title + "\r\n";
                    }

                    if (Company != "")
                    {
                        detailsInfo += Company + "\r\n";
                    }

                    if (Address != "")
                    {
                        detailsInfo += Address + "\r\n\r\n";
                    }

                    if (HomePhone != "")
                    {
                        detailsInfo += "H: " + CleanUp(HomePhone) + "\r\n";
                    }

                    if (MobilePhone != "")
                    {
                        detailsInfo += "M: " + CleanUp(MobilePhone) + "\r\n";
                    }

                    if (WorkPhone != "")
                    {
                        detailsInfo += "M: " + CleanUp(WorkPhone) + "\r\n";
                    }

                    if (Fax != "")
                    {
                        detailsInfo += "F: " + CleanUp(Fax) + "\r\n\r\n";
                    }

                    if (Email != "")
                    {
                        detailsInfo += Email + "\r\n";
                    }

                    if (Email2 != "")
                    {
                        detailsInfo += Email2 + "\r\n";
                    }

                    if (Email3 != "")
                    {
                        detailsInfo += Email3 + "\r\n";
                    }

                    return detailsInfo.Trim();
                }
            }
            set
            {
                detailsInfo = value;
            }
        }

        public ContactData(string detailsinfo)
        {
            DetailsInfo = detailsInfo;
        }

        public string CleanUp(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";
            }
            return Regex.Replace(phone, "[- ()]", "") + "\r\n";
        }

        public string CleanUp2(string email)
        {
            if (email == null || email == "")
            {
                return "";
            }
            return Regex.Replace(email, "[ ]", "") + "\r\n";

        }

        public string Id { get; set; }
    }
}
