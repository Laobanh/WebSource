using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace HTT
{
    public class PersonalInfo
    {
        #region Fields       
        private Person person;
        private ContactMethod contact;
        private AddressInfo address;
        //private AccountInfo account;

        #endregion End Fields

        #region Constructors
        public PersonalInfo()
        {

        }

        public PersonalInfo(String[] csv)
        {        

            this.person = new Person(csv);

            this.contact = new ContactMethod(csv);

            this.address = new AddressInfo(csv);

            //this.account = new AccountInfo(context);

        }


        public PersonalInfo(HttpContext context)
        {
            HttpRequest request = context.Request;
           
            this.person = new Person(context);

            this.contact = new ContactMethod(context);

            this.address = new AddressInfo(context);

            //this.account = new AccountInfo(context);
            
        }
        

        public Person Person
        {
            get
            {
                return person;
            }

            set
            {
                person = value;
            }
        }

        

        public ContactMethod Contact
        {
            get
            {
                return contact;
            }

            set
            {
                contact = value;
            }
        }

        public AddressInfo Address
        {
            get
            {
                return address;
            }

            set
            {
                address = value;
            }
        }

       

        #endregion End Constructors

        #region Methods
        #endregion End Methods

    }
}