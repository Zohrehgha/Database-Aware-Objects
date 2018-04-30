using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareObject
{
    class MSSqlClass
    {
        //Create Class Variables
        public String CSqlStr; 
        public String CDataSource;
        public String CAttachedDB;
        public String CIntSecurity;
        public String[] CTables = new String[14];
        public String[][] CArrayTables = new String[1][];

        //Implementing default constructor
        public MSSqlClass()
        {
            CSqlStr = "select * from ";
            CDataSource = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=";
            CAttachedDB = "C:\\ITD\\Term 3\\C#\\assignment\\assignment7\\AwareObject\\AwareObject\\AwareSQLDB.mdf";
            CIntSecurity = "Integrated Security=SSPI";
        
            for (int i = 0; i < CTables.Length; i++)
            {
                CTables[i] = "";              
            }
            CArrayTables[0] = new String[14] {"PartnerID", "PartnerType", "Name", "Phone", "AlternativePhone", "FaxNumber", "Email",
                              "AlternativeEmail", "SuiteNumber", "StreetNumber", "StreetName", "City", "Province", "PostalCode"};

        }
        //Copy Constructor
        public MSSqlClass(MSSqlClass right)
        {
            this.CSqlStr = right.CSqlStr;
            this.CDataSource = right.CDataSource;
            this.CAttachedDB = right.CAttachedDB;
            this.CIntSecurity = right.CIntSecurity;          
        }
        //Destructor
        ~MSSqlClass()
        {

        }
        //Creating access functions
        public String SqlStr
        {
            get
            {
                return CSqlStr;
            }
            set
            {
                SqlStr = value;
            }
        }
        public String DataSource
        {
            get
            {
                return CDataSource;
            }
            set
            {
                CDataSource = value;
            }
        }
        public String AttachedDB
        {
            get
            {
                return CAttachedDB;
            }
            set
            {
                CAttachedDB = value;
            }
        }
        public String IntSecurity
        {
            get
            {
                return CIntSecurity;
            }
            set
            {
                CIntSecurity = value;
            }
        }
        public String[] getSetTable
        {
            get
            {
                return CTables;
            }
            set
            {
                CTables = value;
            }
        }
        public String[][] getSetArrayArray
        {
            get
            {
                return CArrayTables;
            }
            set
            {
                CArrayTables = value;
            }
        }

        public String PrintSQL()
        {
            string temp;
            temp = "SQL String: " + SqlStr + "\n";
            temp = temp + "SQL Connection String: " + DataSource + AttachedDB + IntSecurity + "\n";

            return temp;
        }

        //Global variable
        protected string tempArray;

        public String PrintArraySQL()
        {
            for (int i = 0; i < CArrayTables.Length; i++)
            {
                tempArray = "SQL Table Fields:\n";

                for (int j = 0; j < CArrayTables[i].Length; j++)
                {

                    tempArray = tempArray + "[" + j + "]: " + CArrayTables[i][j] + "\n";
                }
            }
            return tempArray;
        }

    }
}
