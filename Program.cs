using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;

namespace WebServiceConsumer
{
    class Program
    {
        static void Main(string[] args)
        {

            OleDbConnection objConn = null;
            System.Data.DataTable dt = null;
            string clientNumber = "";

            try
            {
               
                string[] arrayColumnNames = null;
                string path = @"C:\Users\UlysesRico.Rea\Documents\NotestoDocument\";
                string excelFile = path + "SampleBookforWKClark.xlsx";
                //Excel connection strings reference: https://www.connectionstrings.com/excel-2013/
                //Extenden Properties: HDR Yes, the first rows contain column names
                string connString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + excelFile + ";Extended Properties='Excel 12.0 Xml;HDR=YES'";
                // Create connection object by using the preceding connection string.
                objConn = new OleDbConnection(connString);
                // Open connection with the database.
                objConn.Open();
                // Get the data table containg the schema guid.
                dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                foreach (DataRow row in dt.Rows)
                {
                    string sheet = row["TABLE_NAME"].ToString();
                    //I take out the $ sign from sheet 
                    string strSheet = sheet.Replace("$", String.Empty);
                    OleDbDataAdapter objDA = new OleDbDataAdapter("select * from [" + sheet + "]", objConn);
                    DataTable excelDataTable = new DataTable();
                    objDA.Fill(excelDataTable);

                    //Get the columns of the sheets, as they are the same for every tab then it can be fetch once. 
                    //This works just having the condition every tab has the same columns, if not, this array has to be filled every time it steps in the sheet loop.
                    if (arrayColumnNames == null)
                    {

                        arrayColumnNames = new string[excelDataTable.Columns.Count];
                        int i = 0;
                        foreach (DataColumn col in excelDataTable.Columns)
                        {
                            arrayColumnNames[i] = col.ColumnName;
                            i++;

                        }

                    }


                    //Read each row from the current Sheet
                    foreach (DataRow rowSheet in excelDataTable.Rows)
                    {

                        

                        clientNumber = rowSheet["Client Number"].ToString();
                        string clientFilePath = path + clientNumber+".xlsx";
                        string strClientFileConnection = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + clientFilePath + ";Extended Properties='Excel 12.0 Xml;HDR=YES'";
                        OleDbConnection clientFileConnection = new OleDbConnection(strClientFileConnection);
                        string strCreateTable = "create table [" + strSheet + "] ( [" + arrayColumnNames[0] + "] VarChar (100),[" + arrayColumnNames[1] + "] VarChar (100),[" + arrayColumnNames[2] + "] VarChar (100),[" + arrayColumnNames[3] + "] VarChar (100),[" + arrayColumnNames[4] + "] VarChar (100),[" + arrayColumnNames[5] + "] VarChar (200) )";
                        string strinsertQuery = "insert into  [" + sheet + "] ([" + arrayColumnNames[0] + "],[" + arrayColumnNames[1] + "],[" + arrayColumnNames[2] + "],[" + arrayColumnNames[3] + "],[" + arrayColumnNames[4] + "],[" + arrayColumnNames[5] + "])" + " values (@clientnumber,@clientname,@engType,@workflow,@noteType,@note)";




                        OleDbCommand cmdCreateTable = new OleDbCommand(strCreateTable,clientFileConnection);
                        OleDbCommand cmdInsertStm = new OleDbCommand();
                        cmdInsertStm.Connection = clientFileConnection;
                        cmdInsertStm.CommandText = strinsertQuery;
                        cmdInsertStm.Parameters.AddRange(
                            
                            new OleDbParameter[] { 
                            
                               new OleDbParameter("@clientnumber",rowSheet[arrayColumnNames[0]]),
                               new OleDbParameter("@clientname",rowSheet[arrayColumnNames[1]]),
                               new OleDbParameter("@engType",rowSheet[arrayColumnNames[2]]),
                               new OleDbParameter("@workflow",rowSheet[arrayColumnNames[3]]),
                               new OleDbParameter("@noteType",rowSheet[arrayColumnNames[4]]),
                               new OleDbParameter("@note",rowSheet[arrayColumnNames[5]])
                            }
                            
                            );


                        DataTable dtSheets = null;

                        /*
                        Check of the excel file is created
                        */
                        if (File.Exists(clientFilePath))
                        {
                            //Check if the sheet exists

                            if (clientFileConnection.State != ConnectionState.Open)
                                clientFileConnection.Open();

                            dtSheets = clientFileConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            Boolean sheetFound = false;
                            foreach (DataRow osheet in dtSheets.Rows) {

                                if (osheet["TABLE_NAME"].ToString() == sheet)
                                {
                                    sheetFound = true;
                                } 
                            
                            }

                            if (sheetFound)
                                executeCommand(clientFileConnection, cmdInsertStm);
                            else {

                                executeCommand(clientFileConnection, cmdCreateTable);
                                executeCommand(clientFileConnection, cmdInsertStm);
                            }




                        }
                        else
                        {
                            executeCommand(clientFileConnection,cmdCreateTable);
                            executeCommand(clientFileConnection, cmdInsertStm);
                        }


                    }


                }
                //EnForEach of sheets

               
             

            }
            catch (Exception ex)
            {
                Console.WriteLine("Client : "+clientNumber+"-->Exception:" + ex.ToString());
                
                Console.ReadKey();
            }
            finally
            {
                // Clean up.
                if (objConn != null)
                {
                    objConn.Close();
                    objConn.Dispose();
                  
                }
                if (dt != null)
                {
                    dt.Dispose();
                    
                }

               

               

            }



        }

        static void executeCommand(OleDbConnection con,OleDbCommand cmd) {


            if (con.State != ConnectionState.Open)
                con.Open();
                
            cmd.ExecuteNonQuery();
            con.Close();


        }





        
    }
}
