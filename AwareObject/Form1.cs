using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AwareObject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //connect to access 
        ADODB.Connection Con;
        ADODB.Recordset Rs;
        String accessTableName = "AwareObjTable";

        //declaring global object variables in SQL DB
        System.Data.OleDb.OleDbConnection cn;
        //int CurrentRow;
        System.Data.OleDb.OleDbDataAdapter da;
        long RecordCount;
        DataSet ds;
        String sqlTableName = "AwarObjSqlTable";

        //define obj from class        
        PartnerClass Obj = new PartnerClass();

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'clientDBDataSet.ClientTable' table. You can move, or remove it, as needed.
            //this.clientTableTableAdapter.Fill(this.clientDBDataSet.ClientTable);
            Con = new ADODB.Connection();
            Rs = new ADODB.Recordset();
            //Connection calling partner object Provider, Path file, access string
            Con.Provider = Obj.MSAccessObj.Provider;
            Con.ConnectionString = Obj.MSAccessObj.PathFile;
            Con.Open();
            //partnerObj.CAccess.AccessString = "select * from " in Class
            String accessStr;
            accessStr = Obj.MSAccessObj.AccessString + accessTableName;
            Rs.Open(accessStr, Con, ADODB.CursorTypeEnum.adOpenDynamic, ADODB.LockTypeEnum.adLockOptimistic);


            // connection in sql DB
            cn = new System.Data.OleDb.OleDbConnection();
            // A dataset for sql table 
            ds = new DataSet();
            //sqlCon.ConnectionString = Obj.MSSQLObj.DataSource + Obj.MSSQLObj.AttachedDB + Obj.MSSQLObj.IntSecurity;
            cn.ConnectionString = "Provider = SQLNCLI11.0;Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\ITD\\Term 3\\C#\\assignment\\assignment7\\AwareObject\\AwareObject\\AwareSQLDB.mdf;Integrated Security = SSPI;database=thingy";
            cn.Open();
            // Attempting to access Admin Table
            String sqlstr;
            sqlstr = Obj.MSSQLObj.SqlStr + sqlTableName;
            da = new System.Data.OleDb.OleDbDataAdapter(sqlstr, cn);
            da.Fill(ds, sqlTableName);
            RecordCount = ds.Tables[sqlTableName].Rows.Count;
        }
        
        public void ShowDataOnForm()
        {
            textBox1.Text = Obj.PartnerID.ToString();
            textBox2.Text = Obj.PartnetType.ToString();
            textBox3.Text = Obj.Name.ToString();
            textBox4.Text = Obj.Phone.ToString();
            textBox5.Text = Obj.AlternativePhone.ToString();
            textBox6.Text = Obj.FaxNumber.ToString();
            textBox7.Text = Obj.Email.ToString();
            textBox8.Text = Obj.AlternativeEmail.ToString();
            textBox9.Text = Obj.SuiteNumber.ToString();
            textBox10.Text = Obj.StreetNumber.ToString();
            textBox11.Text = Obj.StreetName.ToString();
            textBox12.Text = Obj.City.ToString();
            textBox13.Text = Obj.Province.ToString();
            textBox14.Text = Obj.PostalCode.ToString();
            //textBox17.Text = Obj.WhichDB.ToString();

        }
        //transfer access db to obj
        public void MoveObj()
        {
            Obj.PartnerID = Rs.Fields["PartnerID"].Value;
            Obj.PartnetType = Rs.Fields["PartnetType"].Value;
            Obj.Name = Rs.Fields["Name"].Value;
            Obj.Phone = Rs.Fields["Phone"].Value;
            Obj.AlternativePhone = Rs.Fields["AlternativePhone"].Value;
            Obj.FaxNumber = Rs.Fields["FaxNumber"].Value;
            Obj.Email = Rs.Fields["Email"].Value;
            Obj.AlternativeEmail = Rs.Fields["AlternativeEmail"].Value;
            Obj.SuiteNumber = Rs.Fields["SuiteNumber"].Value;
            Obj.StreetNumber = Rs.Fields["StreetNumber"].Value;
            Obj.StreetName = Rs.Fields["StreetName"].Value;
            Obj.City = Rs.Fields["City"].Value;
            Obj.Province = Rs.Fields["Province"].Value = textBox13.Text;
            Obj.PostalCode = Rs.Fields["PostalCode"].Value;
            //Obj.WhichDB = Rs.Fields["WhichDB"].Value;
        }
        public void SaveinTable()
        {
            Obj.PartnerID = Convert.ToInt16(textBox1.Text);
            Obj.PartnetType = textBox2.Text;
            Obj.Name = textBox3.Text;
            Obj.Phone = textBox4.Text;
            Obj.AlternativePhone = textBox5.Text;
            Obj.FaxNumber = textBox6.Text;
            Obj.Email = textBox7.Text;
            Obj.AlternativeEmail = textBox8.Text;
            Obj.SuiteNumber = textBox9.Text;
            Obj.StreetNumber = textBox10.Text;
            Obj.StreetName = textBox11.Text;
            Obj.City = textBox12.Text;
            Obj.Province = textBox13.Text;
            Obj.PostalCode = textBox14.Text;
            //Obj.WhichDB = Convert.ToInt16(textBox17.Text);

        }
        //Adding new record into Access DB
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" ||
                textBox2.Text == "" ||
                textBox3.Text == "" ||
                textBox4.Text == "" ||
                textBox5.Text == "" ||
                textBox6.Text == "" ||
                textBox7.Text == "" ||
                textBox8.Text == "" ||
                textBox9.Text == "" ||
                textBox10.Text == "" ||
                textBox11.Text == "" ||
                textBox12.Text == "" ||
                textBox13.Text == "" ||
                textBox14.Text == "" ||
                textBox17.Text == "")
            {
                MessageBox.Show("Please Fill up all boxes");
                return;
            }
            if (textBox17.Text == "1")
            {
                AccessTableAdd();
            }
            SqlTableAdd();
        }
        public void AccessTableAdd()
        {

            String Criteria;
            Criteria = "PartnerID =" + textBox1.Text;
            Rs.MoveFirst();
            //go to the beginning to start serach 
            Rs.Find(Criteria);
            //Either We find the record(s), which is the first record if there are more than one
            //If record is found the file pointer stays at it
            //if not found, the file pointer has passed eof meaning eof = true
            if (Rs.EOF == true)
            {
                //not found
                Rs.AddNew();
                SaveinTable();
                Rs.Update();
                MessageBox.Show("Record Added succesfully");
                ClearBoxes();
                return;
            }
            else
            {
                //found 
                MessageBox.Show("Duplicate Record, try another PartnerID");
                return;
            }
        }

        //Modifying a existing record into  Access DB
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" ||
                textBox2.Text == "" ||
                textBox3.Text == "" ||
                textBox4.Text == "" ||
                textBox5.Text == "" ||
                textBox6.Text == "" ||
                textBox7.Text == "" ||
                textBox8.Text == "" ||
                textBox9.Text == "" ||
                textBox10.Text == "" ||
                textBox11.Text == "" ||
                textBox12.Text == "" ||
                textBox13.Text == "" ||
                textBox14.Text == "" ||
                textBox17.Text == "")
            {
                MessageBox.Show("Please Fill up all boxes");
                return;
            }
            if (textBox17.Text == "1")
            {
                AccessTableModify();
            }
            SqlTableModify();
        }
        public void AccessTableModify()
        {
            String Criteria;
            Criteria = "PartnerID =" + textBox1.Text;
            Rs.MoveFirst();
            //go to the beginning to start serach 
            Rs.Find(Criteria);
            //Either We find the record(s), which is the first record if there are more than one
            // If record is found the file pointer stays at it
            //if not found, the file pointer has passed eof meaning eof = true
            if (Rs.EOF)
            {
                // it is impossible, if you refrain from changing the ID 
                MessageBox.Show("Record with this PartnerID does not exist");
                return;
            }
            else
            {
                //found 
                SaveinTable();
                Rs.Update();
                MessageBox.Show("Record Modified succesfully");
            }
        }
        //searching one record into DB by Criteria
        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox17.Text == "1")
            {
                AccessTableSearch();
            }
            SqlTableSearch();
        }
        public void AccessTableSearch()
        {
            String Criteria;
            Criteria = "";
            if (textBox1.Text != "")
            {
                Criteria = Criteria + "PartnerID = " + textBox1.Text;
            }           
            if (textBox2.Text != "")
            {
                if (Criteria != "")
                {
                    Criteria = Criteria + " AND PartnetType = '" + textBox2.Text + "'";
                }
                else
                {
                    Criteria = Criteria + "PartnetType = '" + textBox2.Text + "'";
                }
            }
            if (textBox3.Text != "")
            {
                if (Criteria != "")
                {
                    Criteria = Criteria + " AND Name = '" + textBox3.Text + "'";
                }
                else
                {
                    Criteria = Criteria + "Name = '" + textBox3.Text + "'";
                }
            }
            if (textBox4.Text != "")
            {
                if (Criteria != "")
                {
                    Criteria = Criteria + " AND Phone = '" + textBox4.Text + "'";
                }
                else
                {
                    Criteria = Criteria + "Phone = '" + textBox4.Text + "'";
                }
            }
            if (textBox5.Text != "")
            {
                if (Criteria != "")
                {
                    Criteria = Criteria + " AND AlternativePhone = '" + textBox5.Text + "'";
                }
                else
                {
                    Criteria = Criteria + "AlternativePhone = '" + textBox5.Text + "'";
                }
            }
            if (textBox6.Text != "")
            {
                if (Criteria != "")
                {
                    Criteria = Criteria + " AND FaxNumber = '" + textBox6.Text + "'";
                }
                else
                {
                    Criteria = Criteria + "FaxNumber = '" + textBox6.Text + "'";
                }
            }
            if (textBox7.Text != "")
            {
                if (Criteria != "")
                {
                    Criteria = Criteria + " AND Email = '" + textBox7.Text + "'";
                }
                else
                {
                    Criteria = Criteria + "Email = '" + textBox7.Text + "'";
                }
            }
            if (textBox8.Text != "")
            {
                if (Criteria != "")
                {
                    Criteria = Criteria + " AND AlternativeEmail = '" + textBox8.Text + "'";
                }
                else
                {
                    Criteria = Criteria + "AlternativeEmail = '" + textBox8.Text + "'";
                }
            }
            if (textBox9.Text != "")
            {
                if (Criteria != "")
                {
                    Criteria = Criteria + " AND SuiteNumber = '" + textBox9.Text + "'";
                }
                else
                {
                    Criteria = Criteria + "SuiteNumber = '" + textBox9.Text + "'";
                }
            }
            if (textBox10.Text != "")
            {
                if (Criteria != "")
                {
                    Criteria = Criteria + " AND StreetNumber = '" + textBox10.Text + "'";
                }
                else
                {
                    Criteria = Criteria + "StreetNumber = '" + textBox10.Text + "'";
                }
            }
            if (textBox11.Text != "")
            {
                if (Criteria != "")
                {
                    Criteria = Criteria + " AND StreetName = '" + textBox11.Text + "'";
                }
                else
                {
                    Criteria = Criteria + "StreetName = '" + textBox11.Text + "'";
                }
            }
            if (textBox12.Text != "")
            {
                if (Criteria != "")
                {
                    Criteria = Criteria + " AND City = '" + textBox12.Text + "'";
                }
                else
                {
                    Criteria = Criteria + "City = '" + textBox12.Text + "'";
                }
            }
            if (textBox13.Text != "")
            {
                if (Criteria != "")
                {
                    Criteria = Criteria + " AND Province = '" + textBox13.Text + "'";
                }
                else
                {
                    Criteria = Criteria + "Province = '" + textBox13.Text + "'";
                }
            }
            if (textBox14.Text != "")
            {
                if (Criteria != "")
                {
                    Criteria = Criteria + " AND PostalCode = '" + textBox14.Text + "'";
                }
                else
                {
                    Criteria = Criteria + "PostalCode = '" + textBox14.Text + "'";
                }
            }

            //if (textBox17.Text != "")
            //{
            //    if (Criteria != "")
            //    {
            //        Criteria = Criteria + " AND WhichDB = '" + textBox17.Text + "'";
            //    }
            //    else
            //    {
            //        Criteria = Criteria + "WhichDB = '" + textBox17.Text + "'";
            //    }
            //}

            Rs.MoveFirst();
            Rs.Filter = Criteria;
            if (Rs.EOF == true)
            {
                //not found
                MessageBox.Show("Recod with your specific criteria not found");
                return;
            }
            else
            {
                ShowDataOnForm();
                Rs.Filter = "";
            }

        }
        //Deleting ane exsitind record into Access DB
        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" ||
                textBox2.Text == "" ||
                textBox3.Text == "" ||
                textBox4.Text == "" ||
                textBox5.Text == "" ||
                textBox6.Text == "" ||
                textBox7.Text == "" ||
                textBox8.Text == "" ||
                textBox9.Text == "" ||
                textBox10.Text == "" ||
                textBox11.Text == "" ||
                textBox12.Text == "" ||
                textBox13.Text == "" ||
                textBox14.Text == "" ||
                textBox17.Text == "")
            {
                MessageBox.Show("Please Fill up all boxes");
                return;
            }
            if (textBox17.Text == "1")
            {
                AccessTableDelete();
            }
            SqlTableDelete();
        }
        public void AccessTableDelete()
        {
            String Criteria;
            Criteria = "PartnerID =" + textBox1.Text;
            Rs.MoveFirst();
            //go to the beginning to start serach 
            Rs.Find(Criteria);
            //Either We find the record(s), which is the first record if there are more than one
            // If record is found the file pointer stays at it
            //if not found, the file pointer has passed eof meaning eof = true
            if (Rs.EOF)
            {
                // it is impossible, if you refrain from changing the ID 
                MessageBox.Show("Record with this PartnerID does not exist");
                return;
            }
            else
            {
                //found 
                //confirm 
                DialogResult MsgbxResult;
                MsgbxResult = MessageBox.Show("Are you Sure?!", "Confirm Delete", MessageBoxButtons.YesNo);
                if (Convert.ToString(MsgbxResult) == "Yes")
                {
                    Rs.Delete();
                    Rs.Update();
                    MessageBox.Show("Record Deleted Successfully !!!");
                    ClearBoxes();

                }

            }

        }
        //searching one record into Access DB by Criteria
        private void button5_Click_1(object sender, EventArgs e)
        {
            String Criteria;
            Criteria = "";
            if (textBox1.Text != "")
            {
                Criteria = Criteria + "PartnerID = " + textBox1.Text;
            }
            if (textBox2.Text != "")
            {
                if (Criteria != "")
                {
                    Criteria = Criteria + " AND PartnetType = '" + textBox2.Text + "'";
                }
                else
                {
                    Criteria = Criteria + "PartnetType = '" + textBox2.Text + "'";
                }
            }
            if (textBox3.Text != "")
            {
                if (Criteria != "")
                {
                    Criteria = Criteria + " AND Name = '" + textBox3.Text + "'";
                }
                else
                {
                    Criteria = Criteria + "Name = '" + textBox3.Text + "'";
                }
            }
            if (textBox4.Text != "")
            {
                if (Criteria != "")
                {
                    Criteria = Criteria + " AND Phone = '" + textBox4.Text + "'";
                }
                else
                {
                    Criteria = Criteria + "Phone = '" + textBox4.Text + "'";
                }
            }
            if (textBox5.Text != "")
            {
                if (Criteria != "")
                {
                    Criteria = Criteria + " AND AlternativePhone = '" + textBox5.Text + "'";
                }
                else
                {
                    Criteria = Criteria + "AlternativePhone = '" + textBox5.Text + "'";
                }
            }
            if (textBox6.Text != "")
            {
                if (Criteria != "")
                {
                    Criteria = Criteria + " AND FaxNumber = '" + textBox6.Text + "'";
                }
                else
                {
                    Criteria = Criteria + "FaxNumber = '" + textBox6.Text + "'";
                }
            }
            if (textBox7.Text != "")
            {
                if (Criteria != "")
                {
                    Criteria = Criteria + " AND Email = '" + textBox7.Text + "'";
                }
                else
                {
                    Criteria = Criteria + "Email = '" + textBox7.Text + "'";
                }
            }
            if (textBox8.Text != "")
            {
                if (Criteria != "")
                {
                    Criteria = Criteria + " AND AlternativeEmail = '" + textBox8.Text + "'";
                }
                else
                {
                    Criteria = Criteria + "AlternativeEmail = '" + textBox8.Text + "'";
                }
            }
            if (textBox9.Text != "")
            {
                if (Criteria != "")
                {
                    Criteria = Criteria + " AND SuiteNumber = '" + textBox9.Text + "'";
                }
                else
                {
                    Criteria = Criteria + "SuiteNumber = '" + textBox9.Text + "'";
                }
            }
            if (textBox10.Text != "")
            {
                if (Criteria != "")
                {
                    Criteria = Criteria + " AND StreetNumber = '" + textBox10.Text + "'";
                }
                else
                {
                    Criteria = Criteria + "StreetNumber = '" + textBox10.Text + "'";
                }
            }
            if (textBox11.Text != "")
            {
                if (Criteria != "")
                {
                    Criteria = Criteria + " AND StreetName = '" + textBox11.Text + "'";
                }
                else
                {
                    Criteria = Criteria + "StreetName = '" + textBox11.Text + "'";
                }
            }
            if (textBox12.Text != "")
            {
                if (Criteria != "")
                {
                    Criteria = Criteria + " AND City = '" + textBox12.Text + "'";
                }
                else
                {
                    Criteria = Criteria + "City = '" + textBox12.Text + "'";
                }
            }
            if (textBox13.Text != "")
            {
                if (Criteria != "")
                {
                    Criteria = Criteria + " AND Province = '" + textBox13.Text + "'";
                }
                else
                {
                    Criteria = Criteria + "Province = '" + textBox13.Text + "'";
                }
            }
            if (textBox14.Text != "")
            {
                if (Criteria != "")
                {
                    Criteria = Criteria + " AND PostalCode = '" + textBox14.Text + "'";
                }
                else
                {
                    Criteria = Criteria + "PostalCode = '" + textBox14.Text + "'";
                }
            }
            if (textBox17.Text != "")
            {
                if (Criteria != "")
                {
                    Criteria = Criteria + " AND WhichDB = '" + textBox17.Text + "'";
                }
                else
                {
                    Criteria = Criteria + "WhichDB = '" + textBox17.Text + "'";
                }
            }


            Rs.MoveFirst();
            Rs.Filter = Criteria;
            if (Rs.EOF == true)
            {
                //not found
                MessageBox.Show("Recod with your specific criteria not found");
                return;
            }
            else
            {
                ShowDataOnForm();
                Rs.Filter = "";
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClearBoxes();
        }
        //function to clear the form
        public void ClearBoxes()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            textBox11.Clear();
            textBox12.Clear();
            textBox13.Clear();
            textBox14.Clear();
            textBox17.Clear();

        }
        //showing First record in Access DB
        private void button6_Click(object sender, EventArgs e)
        {
            if (Rs.EOF == true && Rs.BOF == true)
            {
                MessageBox.Show("Table is Empty!");
                return;
            }
            Rs.MoveFirst();
            MoveObj();
            ShowDataOnForm();

        }

        //showing last record in Access DB
        private void button9_Click(object sender, EventArgs e)
        {
            if (Rs.EOF == true && Rs.BOF == true)
            {
                MessageBox.Show("Table is Empty!");
                return;
            }
            Rs.MoveLast();
            MoveObj();
            ShowDataOnForm();

        }
        //showing previous record in Access DB
        private void button7_Click(object sender, EventArgs e)
        {
            if (Rs.EOF == true && Rs.BOF == true)
            {
                MessageBox.Show("Table is Empty!");
                return;
            }
            Rs.MovePrevious();
            if (Rs.BOF == true)
            {
                Rs.MoveFirst();
                MessageBox.Show("Passed Beginning of File");
            }
            MoveObj();
            ShowDataOnForm();

        }
        //showing next record in Access DB
        private void button8_Click(object sender, EventArgs e)
        {
            if (Rs.EOF == true && Rs.BOF == true)
            {
                MessageBox.Show("Table is Empty!");
                return;
            }
            Rs.MoveNext();
            if (Rs.EOF == true)
            {
                Rs.MoveLast();
                MessageBox.Show("Passed End of File");
            }
            MoveObj();
            ShowDataOnForm();

        }
        //function adding data to sql table
        public void SqlTableAdd()
        {
            System.Data.DataRow[] foundRows;
            String Strtofind;

            if (textBox1.Text == "" ||
                textBox2.Text == "" ||
                textBox3.Text == "" ||
                textBox4.Text == "" ||
                textBox5.Text == "" ||
                textBox6.Text == "" ||
                textBox7.Text == "" ||
                textBox8.Text == "" ||
                textBox9.Text == "" ||
                textBox10.Text == "" ||
                textBox11.Text == "" ||
                textBox12.Text == "" ||
                textBox13.Text == "" ||
                textBox14.Text == "" ||
                textBox17.Text == "")
            {
                System.Windows.Forms.MessageBox.Show("Incomplete Information, Try Again !!");
                return;
            }

            Strtofind = "PartnerID =" + textBox1.Text;
            foundRows = ds.Tables["AwarObjSqlTable"].Select(Strtofind);
            if (foundRows.Length == 0)
            {
                //its a new record, we should be able to add 
                System.Data.DataRow NewRow = ds.Tables["AwarObjSqlTable"].NewRow();
                //Next line is needed so we can update the database 
                System.Data.OleDb.OleDbCommandBuilder Cb = new System.Data.OleDb.OleDbCommandBuilder(da);
                //NewRow.SetField<int>("PartnerID", Convert.ToInt32(textBox1.Text));
                //NewRow.SetField<String>("PartnetType", textBox2.Text);
                //NewRow.SetField<String>("Name", textBox3.Text);
                //NewRow.SetField<String>("Phone", textBox4.Text);
                //NewRow.SetField<String>("AlternativePhone", textBox5.Text);
                //NewRow.SetField<String>("FaxNumber", textBox6.Text);
                //NewRow.SetField<String>("Email", textBox7.Text);
                //NewRow.SetField<String>("AlternativeEmail", textBox8.Text);
                //NewRow.SetField<String>("SuiteNumber", textBox9.Text);
                //NewRow.SetField<String>("StreetNumber", textBox10.Text);
                //NewRow.SetField<String>("StreetName", textBox11.Text);
                //NewRow.SetField<String>("City", textBox12.Text);
                //NewRow.SetField<String>("Province", textBox13.Text);
                //NewRow.SetField<String>("PostalCode", textBox14.Text);

                SaveinSqlTable();
                NewRow.SetField<Int16>("WhichDB", Convert.ToInt16(textBox17.Text));
                ds.Tables["AwarObjSqlTable"].Rows.Add(NewRow);
                //da.UpdateCommand = Cb.GetUpdateCommand();
                da.Update(ds, "AwarObjSqlTable");
                //da.AcceptChangesDuringUpdate = true;
                //ds.AcceptChanges(); 
                RecordCount = RecordCount + 1;
                //Adding a record for starting Credit Balance 
                //On Transaction

                System.Windows.Forms.MessageBox.Show("Record Added Succesfully");
                //sending an email 

            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Duplicate ID, try Again!!!");
                return;
            }

        }
        public void SqlTableDelete()
        {
            System.Data.DataRow[] foundRows;
            String Strtofind;
            int Rowindex;
            if (textBox1.Text == "")
            {
                System.Windows.Forms.MessageBox.Show("Please Enter a Partner ID to find");
                return;
            }
            Strtofind = "PartnerID =" + textBox1.Text;
            foundRows = ds.Tables["AwarObjSqlTable"].Select(Strtofind);
            if (foundRows.Length == 0)
            {
                System.Windows.Forms.MessageBox.Show("Record Not Found, try again");
            }
            else
            {
                int result;
                Rowindex = ds.Tables["AwarObjSqlTable"].Rows.IndexOf(foundRows[0]);
                result = Convert.ToInt32(System.Windows.Forms.MessageBox.Show("Are you Sure?", "Deleting Record", MessageBoxButtons.YesNo));
                if (result == 6)
                {
                    ds.Tables["AwarObjSqlTable"].Rows[Rowindex].Delete();
                    ClearBoxes();
                    System.Windows.Forms.MessageBox.Show("record deleted Succesfully!!!");
                    RecordCount = RecordCount - 1;
                }
            }

        }
        public void SqlTableModify()
        {
            System.Data.DataRow[] foundRows;
            String Strtofind;
            int Rowindex;
            if (textBox1.Text == "" ||
                textBox2.Text == "" ||
                textBox3.Text == "" ||
                textBox4.Text == "" ||
                textBox5.Text == "" ||
                textBox6.Text == "" ||
                textBox7.Text == "" ||
                textBox8.Text == "" ||
                textBox9.Text == "" ||
                textBox10.Text == "" ||
                textBox11.Text == "" ||
                textBox12.Text == "" ||
                textBox13.Text == "" ||
                textBox14.Text == "" ||
                textBox17.Text == "")
            {
                System.Windows.Forms.MessageBox.Show("Incomplete Information, Try Again !!");
                return;
            }
            Strtofind = "PartnerID =" + textBox1.Text;
            foundRows = ds.Tables["AwarObjSqlTable"].Select(Strtofind);
            System.Data.OleDb.OleDbCommandBuilder Cb = new System.Data.OleDb.OleDbCommandBuilder(da);
            if (foundRows.Length == 0)
            {
                System.Windows.Forms.MessageBox.Show("Record Not Found, try again");
            }
            else
            {
                Rowindex = ds.Tables["AwarObjSqlTable"].Rows.IndexOf(foundRows[0]);
                //ds.Tables["AwarObjSqlTable"].Rows[Rowindex].SetField<Int16>("PartnerID", Convert.ToInt16(textBox1.Text));
                //ds.Tables["AwarObjSqlTable"].Rows[Rowindex].SetField<String>("PartnetType", textBox2.Text);
                //ds.Tables["AwarObjSqlTable"].Rows[Rowindex].SetField<String>("Name", textBox3.Text);
                //ds.Tables["AwarObjSqlTable"].Rows[Rowindex].SetField<String>("Phone", textBox4.Text);
                //ds.Tables["AwarObjSqlTable"].Rows[Rowindex].SetField<String>("AlternativePhone", textBox5.Text);
                //ds.Tables["AwarObjSqlTable"].Rows[Rowindex].SetField<String>("FaxNumber", textBox6.Text);
                //ds.Tables["AwarObjSqlTable"].Rows[Rowindex].SetField<String>("Email", textBox6.Text);
                //ds.Tables["AwarObjSqlTable"].Rows[Rowindex].SetField<String>("AlternativeEmail", textBox6.Text);
                //ds.Tables["AwarObjSqlTable"].Rows[Rowindex].SetField<String>("SuiteNumber", textBox6.Text);
                //ds.Tables["AwarObjSqlTable"].Rows[Rowindex].SetField<String>("StreetNumber", textBox6.Text);
                //ds.Tables["AwarObjSqlTable"].Rows[Rowindex].SetField<String>("StreetName", textBox6.Text);
                //ds.Tables["AwarObjSqlTable"].Rows[Rowindex].SetField<String>("City", textBox6.Text);
                //ds.Tables["AwarObjSqlTable"].Rows[Rowindex].SetField<String>("Province", textBox6.Text);
                //ds.Tables["AwarObjSqlTable"].Rows[Rowindex].SetField<String>("PostalCode", textBox6.Text);
                //ds.Tables["AwarObjSqlTable"].Rows[Rowindex].SetField<Int16>("WhichDB", Convert.ToInt16(textBox6.Text));
                SaveinSqlTable();
                System.Windows.Forms.MessageBox.Show("Modifications saved successfully!");
            }
        }
        public void SqlTableSearch()
        {

            System.Data.DataRow[] foundRows;
            String Strtofind;

            if (textBox1.Text == "")
            {
                System.Windows.Forms.MessageBox.Show("Please Enter a Customer ID to find");
                return;
            }
            Strtofind = "CustomerID =" + textBox1.Text;
            foundRows = ds.Tables["Customers"].Select(Strtofind);
            int Rowindex;

            if (foundRows.Length == 0)
            {
                MessageBox.Show("Customer Not Found");
            }
            else
            {
                Rowindex = ds.Tables["Customers"].Rows.IndexOf(foundRows[0]);
                //CurrentRow = Rowindex;
                //ShowDataFromSql(CurrentRow);
            }

        }


        public void ShowDataFromSql()
        {
            textBox1.Text = Obj.PartnerID.ToString();
            textBox2.Text = Obj.PartnetType.ToString();
            textBox3.Text = Obj.Name.ToString();
            textBox4.Text = Obj.Phone.ToString();
            textBox5.Text = Obj.AlternativePhone.ToString();
            textBox6.Text = Obj.FaxNumber.ToString();
            textBox7.Text = Obj.Email.ToString();
            textBox8.Text = Obj.AlternativeEmail.ToString();
            textBox9.Text = Obj.SuiteNumber.ToString();
            textBox10.Text = Obj.StreetNumber.ToString();
            textBox11.Text = Obj.StreetName.ToString();
            textBox12.Text = Obj.City.ToString();
            textBox13.Text = Obj.Province.ToString();
            textBox14.Text = Obj.PostalCode.ToString();
            textBox17.Text = Obj.WhichDB.ToString();

        }
        ////transfer access db to obj
        public void MoveSqlObj(int ThisRow)
        {
            Obj.PartnerID = ds.Tables["AwarObjSqlTable"].Rows[ThisRow].Field<Int16>("PartnetID");
            Obj.PartnetType = ds.Tables["AwarObjSqlTable"].Rows[ThisRow].Field<String>("PartnetType");
            Obj.Name = ds.Tables["AwarObjSqlTable"].Rows[ThisRow].Field<String>("Name");
            Obj.Phone = ds.Tables["AwarObjSqlTable"].Rows[ThisRow].Field<String>("Phone");
            Obj.AlternativePhone = ds.Tables["AwarObjSqlTable"].Rows[ThisRow].Field<String>("AlternativePhone");
            Obj.FaxNumber = ds.Tables["AwarObjSqlTable"].Rows[ThisRow].Field<String>("FaxNumber");
            Obj.Email = ds.Tables["AwarObjSqlTable"].Rows[ThisRow].Field<String>("Email");
            Obj.AlternativeEmail = ds.Tables["AwarObjSqlTable"].Rows[ThisRow].Field<String>("AlternativeEmail");
            Obj.SuiteNumber = ds.Tables["AwarObjSqlTable"].Rows[ThisRow].Field<String>("SuiteNumber");
            Obj.StreetNumber = ds.Tables["AwarObjSqlTable"].Rows[ThisRow].Field<String>("StreetNumber");
            Obj.StreetName = ds.Tables["AwarObjSqlTable"].Rows[ThisRow].Field<String>("StreetName");
            Obj.City = ds.Tables["AwarObjSqlTable"].Rows[ThisRow].Field<String>("City");
            Obj.Province = ds.Tables["AwarObjSqlTable"].Rows[ThisRow].Field<String>("Province");
            Obj.PostalCode = ds.Tables["AwarObjSqlTable"].Rows[ThisRow].Field<String>("PostalCode");
            //Obj.WhichDB = ds.Tables["AwarObjSqlTable"].Rows[ThisRow].Field<Int16>("WhichDB");
        }
        public void SaveinSqlTable()
        {
            Obj.PartnerID = Convert.ToInt16(textBox1.Text);
            Obj.PartnetType = textBox2.Text;
            Obj.Name = textBox3.Text;
            Obj.Phone = textBox4.Text;
            Obj.AlternativePhone = textBox5.Text;
            Obj.FaxNumber = textBox6.Text;
            Obj.Email = textBox7.Text;
            Obj.AlternativeEmail = textBox8.Text;
            Obj.SuiteNumber = textBox9.Text;
            Obj.StreetNumber = textBox10.Text;
            Obj.StreetName = textBox11.Text;
            Obj.City = textBox12.Text;
            Obj.Province = textBox13.Text;
            Obj.PostalCode = textBox14.Text;
            //Obj.WhichDB = Convert.ToInt16(textBox17.Text);

        }
    } 
}

