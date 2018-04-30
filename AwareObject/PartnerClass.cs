using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareObject
{
    class PartnerClass
    {
        //Create Class Variables
        public Int16 CPartnerID;
        public String CPartnetType;
        public String CName;
        public String CPhone;
        public String CAlternativePhone;
        public String CFaxNumber;
        public String CEmail;
        public String CAlternativeEmail;
        public String CSuiteNumber;
        public String CStreetNumber;
        public String CStreetName;
        public String CCity;
        public String CProvince;
        public String CPostalCode ;
        public MSSqlClass CMSSQLObj = new MSSqlClass();
        public MSAccessClass CMSAccessObj = new MSAccessClass();
        public Int16 CWhichDB;

        //Implementing default constructor

        public PartnerClass()
        {

        }
        
        //Copy Constructor
        public PartnerClass(PartnerClass right)
        {
            this.CPartnerID = right.CPartnerID;
            this.CPartnetType = right.CPartnetType;
            this.CName = right.CName;
            this.CPhone = right.CPhone;
            this.CAlternativePhone = right.CAlternativePhone;
            this.CFaxNumber = right.CFaxNumber;
            this.CEmail = right.CEmail;
            this.CAlternativeEmail = right.CAlternativeEmail;
            this.CSuiteNumber = right.CSuiteNumber;
            this.CStreetNumber = right.CStreetNumber;
            this.CStreetName = right.CStreetName;
            this.CCity = right.CCity;
            this.CProvince = right.CProvince;
            this.CPostalCode = right.CPostalCode;
            this.CMSSQLObj = right.CMSSQLObj;
            this.CMSAccessObj = right.CMSAccessObj;
            this.CWhichDB = right.CWhichDB;
        }
        
        public Int16 PartnerID
        {
            get
            {
                return CPartnerID;
            }
            set
            {
                CPartnerID = value;
            }
        }
        public String PartnetType
        {
            get
            {
                return CPartnetType;
            }
            set
            {
                CPartnetType = value;
            }
        }
        public String Name
        {
            get
            {
                return CName;
            }
            set
            {
                CName = value;
            }
        }
        public String Phone
        {
            get
            {
                return CPhone;
            }
            set
            {
                CPhone = value;
            }
        }
        public String AlternativePhone
        {
            get
            {
                return CAlternativePhone;
            }
            set
            {
                CAlternativePhone = value;
            }
        }
        public String FaxNumber
        {
            get
            {
                return CFaxNumber;
            }
            set
            {
                CFaxNumber = value;
            }
        }
        public String Email
        {
            get
            {
                return CEmail;
            }
            set
            {
                CEmail = value;
            }
        }
        public String AlternativeEmail
        {
            get
            {
                return CAlternativeEmail;
            }
            set
            {
                CAlternativeEmail = value;
            }
        }
        public String SuiteNumber
        {
            get
            {
                return CSuiteNumber;
            }
            set
            {
                CSuiteNumber = value;
            }
        }
        public String StreetNumber
        {
            get
            {
                return CStreetNumber;
            }
            set
            {
                CStreetNumber = value;
            }
        }
        public String StreetName
        {
            get
            {
                return CStreetName;
            }
            set
            {
                CStreetName = value;
            }
        }
        public String City
        {
            get
            {
                return CCity;
            }
            set
            {
                CCity = value;
            }
        }
        public String Province
        {
            get
            {
                return CProvince;
            }
            set
            {
                CProvince = value;
            }
        }
        public String PostalCode
        {
            get
            {
                return CPostalCode;
            }
            set
            {
                CPostalCode = value;
            }
        }
        public MSSqlClass MSSQLObj
        {
            get
            {
                return CMSSQLObj;
            }
            set
            {
                CMSSQLObj = value;
            }
        }
        public MSAccessClass MSAccessObj
        {
            get
            {
                return CMSAccessObj;
            }
            set
            {
                CMSAccessObj = value;
            }
        }
        public Int16 WhichDB
        {
            get
            {
                return CWhichDB;
            }
            set
            {
                CWhichDB = value;
            }
        }

        public string MakeString()
        {

            string temp;
            temp = "";
            temp = temp + "PartnerID=" + CPartnerID;
            temp = temp + "PartnetType" + CPartnetType;
            temp = temp + "Name" + CName;
            temp = temp + "Phone" + CPhone;
            temp = temp + "AlternativePhone" + CAlternativePhone;
            temp = temp + "FaxNumber" + CFaxNumber;
            temp = temp + "Email" + CEmail;
            temp = temp + "AlternativeEmail" + CAlternativeEmail;
            temp = temp + "SuiteNumber" + CSuiteNumber;
            temp = temp + "StreetNumber" + CStreetNumber;
            temp = temp + "StreetName" + CStreetName;
            temp = temp + "City" + CCity;
            temp = temp + "Province" + CProvince;
            temp = temp + "PostalCode" + CPostalCode;
            temp = temp + "MSSQLObj" + CMSSQLObj;
            temp = temp + "MSAccessObj" + CMSAccessObj;
            if(WhichDB == 2)
            {
                //SQL Class
                temp = temp + "SQL Connection String: \n" + CMSSQLObj.DataSource + CMSSQLObj.AttachedDB + CMSSQLObj.IntSecurity + "\n";
            }
            else
            {
                //ACCESS Class
            }


            return temp;
        }
           
    }
}

