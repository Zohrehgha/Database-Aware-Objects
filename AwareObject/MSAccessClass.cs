using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwareObject
{
    class MSAccessClass
    {
        //Create Class Variables
        private String CProvider;
        private String CPathFile;
        private String CAccessStr;
        
        //Implementing default constructor
        
        public MSAccessClass()
        {
            CProvider = "Microsoft.jet.oledb.4.0";
            CPathFile = "C:\\ITD\\Term 3\\C#\\assignment\\assignment7\\AwareObjDB.mdb";
            CAccessStr = "Select * from ";
        }

        //Copy Constructor
        public MSAccessClass(MSAccessClass right)
        {
            this.CProvider = right.CProvider;
            this.CPathFile = right.CPathFile;
            this.CAccessStr = right.CAccessStr;
        }
        //Destructor
        ~MSAccessClass()
        {

        }

        //Creating access functions
        public String Provider
        {
            get
            {
                return CProvider;
            }
            set
            {
                CProvider = value;
            }
        }
        public String PathFile
        {
            get
            {
                return CPathFile;
            }
            set
            {
                CPathFile = value;
            }
        }
        public String AccessString
        {
            get
            {
                return CAccessStr;
            }
            set
            {
                CAccessStr = value;
            }
        }       
    }
}
