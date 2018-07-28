using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_DTO.CMSContact
{
    public class CMS_ContactModels
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public CMS_ContactModels()
        {
        }

        public string GetContactInfo()
        {
            var ret = "Name: " + Name
                + "\n"
                + "Email: " + Email
                + "\n"
                + "Phone: " + Phone
                + "\n"
                + "Subject: " + Subject
                + "\n"
                + "Message: " + Message;
            return ret;
        }
    }
}
