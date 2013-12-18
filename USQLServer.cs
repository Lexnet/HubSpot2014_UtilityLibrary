using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace com.lexnetcrm.common
{
    public class USQLServer : ADB
    {
        public static void openConnection(SqlConnection connection)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public static void closeConnection(SqlConnection connection)
        {
            if (connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
        }

        public static void executeSQL(string query, SqlConnection connection)
        {
            /**
             * Used to run insert, update and delete type 
             * SQL statements against the database. Use other 
             * methods for retrieving datasets and datareaders, etc             
             **/

            //Log sql queries to a log file
            //SLX_Utils.sendToFile(query, debugLogPath);

            openConnection(connection);

            SqlCommand queryCommand = new SqlCommand();

            queryCommand.Connection = connection;
            queryCommand.CommandType = CommandType.Text;
            queryCommand.CommandText = query;

            try
            {
                queryCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                //SLX_Utils.sendToFile("ERROR CODE: " + e.Number + " MSG: " + e.Message, debugLogPath);
            }
            finally
            {
                closeConnection(connection);
            }
        }

        public static void executeCommand(SqlCommand command, SqlConnection connection)
        {
            openConnection(connection);

            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                closeConnection(connection);
            }
        }
    }
}
