using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;

namespace com.lexnetcrm.common
{
    public class UOleDB : ADB
    {
        public static void openConnection(OleDbConnection connection)
        {
            if (connection.State == ConnectionState.Open)
            {
                return;
            }

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public static void closeConnection(OleDbConnection connection)
        {
            if (connection.State == ConnectionState.Closed)
            {
                return;
            }

            if (connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
        }

        public static void executeSQL(string query, OleDbConnection connection)
        {
            openConnection(connection);

            OleDbCommand command = new OleDbCommand();

            command.Connection = connection;
            command.CommandType = CommandType.Text;
            command.CommandText = query;

            try
            {
                command.ExecuteNonQuery();
            }
            catch (OleDbException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                closeConnection(connection);
            }

            closeConnection(connection);
        }

        public static void executeCommand(OleDbCommand command, OleDbConnection connection)
        {            
            openConnection(connection);            

            try
            {
                command.ExecuteNonQuery();
            }
            catch (OleDbException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                closeConnection(connection);
            }
        }

        public static List<string> getDataValues(string table, string returnField, string lookupField, string lookupValue, string connectionString)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);

            UOleDB.openConnection(connection);

            List<string> values = getDataValues(table, returnField, lookupField, lookupValue, connection);

            UOleDB.closeConnection(connection);

            return values;
        }
        
        public static List<string> getDataValues(string table, string returnField, string lookupField, string lookupValue, OleDbConnection connection)
        {            
            string query = "SELECT " +
                            returnField + " as field " +
                        "FROM " +
                            table + " " +
                        "WHERE " +
                            lookupField + " = " + UString.addQuotes(lookupValue);

            List<string> values = getDataValuesList(query, connection);

            return values;
        }

        public static string getDataValue(string table, string returnField, string lookupField, string lookupValue, string connectionString)
        {
            OleDbConnection connection = new OleDbConnection(connectionString);

            UOleDB.openConnection(connection);

            string returnValue = getDataValue(table, returnField, lookupField, lookupValue, connection);

            UOleDB.closeConnection(connection);

            return returnValue;
        }

        public static string getDataValue(string table, string returnField, string lookupField, string lookupValue, OleDbConnection connection)
        {
            string query = "SELECT " +
                            returnField + " as field " +
                        "FROM " +
                            table + " " +
                        "WHERE " +
                            lookupField + " = " + UString.addQuotes(lookupValue);

            return getDataValue(query, connection);
        }

        public static string getDataValue(string query, string connectionString)
        {            
            OleDbConnection connection = new OleDbConnection(connectionString);

            UOleDB.openConnection(connection);

            string returnValue = getDataValue(query, connection);

            UOleDB.closeConnection(connection);

            return returnValue;
        }
    
        public static string getDataValue(string query, OleDbConnection connection)
        {
            List<string> values = getDataValuesList(query, connection);

            if (values.Count > 0)
            {
                return "" + values[0];                
            }
            else
            {
                return "";
            }
        }

        public static List<string> getDataValuesList(string query, string connectionString)
        {
            List<string> returnValues = new List<string>();

            OleDbConnection connection = new OleDbConnection(connectionString);

            UOleDB.openConnection(connection);

            returnValues = getDataValuesList(query, connection);

            UOleDB.closeConnection(connection);

            return returnValues;
        }

        public static List<string> getDataValuesList(string query, OleDbConnection connection)
        {
            List<string> returnValues = new List<string>();
            //string returnValue = "";

            OleDbCommand command = null;
            OleDbDataReader reader = null;

            try
            {
                openConnection(connection);

                try
                {
                    command = new OleDbCommand(query, connection);
                    command.CommandType = CommandType.Text;

                    reader = command.ExecuteReader();
                    
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            //returnValue = reader.GetValue(0).ToString();
                            returnValues.Add(reader.GetValue(0).ToString());
                        }
                    }
                    else
                    {
                        //returnValue = "";
                    }                    
                }
                catch (Exception e)
                {

                }
                finally
                {
                    //This may be the cause of closed connections issues
                    //closeConnection(connection);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            //return returnValue;            
            return returnValues;            
        }

        public static int countRecords(string query, string connectionString)
        {
            int returnValue = 0;

            OleDbConnection connection = new OleDbConnection(connectionString);

            UOleDB.openConnection(connection);

            returnValue = countRecords(query, connection);

            UOleDB.closeConnection(connection);

            return returnValue;
        }

        public static int countRecords(string query, OleDbConnection connection)
        {
            int returnValue = 0;

            string upperCaseQuery = query.ToUpper();

            if (upperCaseQuery.Contains("SELECT COUNT(*) FROM") == false)
            {
                throw new ArgumentException(UException.getExceptionMessage("UOleDb", "countRecords", "You must supply a valid select query such as SELECT COUNT(*) FROM <MyTable>"));
            }

            string value = getDataValue(query, connection);

            if (String.IsNullOrEmpty(value) == true)
            {
                returnValue = 0;
            }
            else
            {
                returnValue = int.Parse(value);
            }

            return returnValue;
        }

        public static void updateDataValue(string table, string field, string value, string pKeyField, string pKeyValue, OleDbConnection connection)
        {
            string updateQuery;
            
            updateQuery = "UPDATE " + table + " SET " +
                                field + " = " + UString.blankStringToNull(UString.addQuotes(value)) + " " +
                            "WHERE " +
                                pKeyField + " = " + UString.addQuotes(pKeyValue) + "";

            updateDataValue(updateQuery, connection);
        }

        public static void updateDataValue(string query, OleDbConnection connection)
        {
            executeSQL(query, connection);
        }

        public static void executeSQLFiles(OleDbConnection connection, List<FileInfo> files)
        {
            if (connection == null)
            {
                throw new ArgumentException(UException.getExceptionMessage("UOleDb", "executeSQLFiles", "You must supply a valid connection object"));
            }

            if (files == null)
            {
                throw new ArgumentException(UException.getExceptionMessage("UOleDb", "executeSQLFiles", "You must supply a valid list of files"));
            }

            foreach (FileInfo file in files)
            {
                string sql = "";
            
                sql = "" + file.OpenText().ReadToEnd();
                                       
                string[] cmds = splitSqlFilesIntoSeperateStatements(sql);

                foreach (string cmd in cmds)
                {
                    executeSQL(cmd, connection);
                }
            }
        }

        public static string[] splitSqlFilesIntoSeperateStatements(string sql)
        {
            //null forces the default of "GO" + System.Environment.NewLine
            return splitSqlFilesIntoSeperateStatements(sql, null);
        }

        private static string[] splitSqlFilesIntoSeperateStatements(string sql, string splitString)
        {
            if (String.IsNullOrEmpty(sql) == true)
            {
                return new string[] { };
            }

            if (String.IsNullOrEmpty(splitString) == true)
            {
                splitString = "GO" + System.Environment.NewLine;
            }

            string[] cmds = sql.Trim().Split(new string[]{ splitString }, System.StringSplitOptions.RemoveEmptyEntries);

            return cmds;
        }

    }
}
