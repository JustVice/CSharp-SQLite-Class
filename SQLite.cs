using System;
using System.Data.SQLite;
using System.Data;

namespace namespaceapp
{
    public class SQLite
    {
        #region DOCUMENTATION
        /////////////////////////////////////////////////////////////
        //-----------------------------------------------------------------
        //SQLITE C# CLASS TEMPLATE BY VICE. Version 1.1 - Date: 2020/02/06
        //-----------------------------------------------------------------
        //System.Data.SQLite library must be get from NuGet.
        /////////////////////////////////////////////////////////////
        #endregion

        private SQLiteConnection sqlite_conn;
        public string sqlite_database_path;

        public SQLite(string SQLite_database_path)
        {
            if (SQLite_database_path_contains_dot_db(SQLite_database_path))
            {
                this.sqlite_database_path = SQLite_database_path;
            }
            else
            {
                this.sqlite_database_path = SQLite_database_path + ".db";
            }
            this.sqlite_conn = new SQLiteConnection(
                "Data Source= " + this.sqlite_database_path + "; " +
                "Version = 3; " +
                "New = True; " +
                "Compress = True; ");
        }

        public SQLite()
        {

        }

        #region QUERY METHODS

        public void Query(string query)
        {
            SQLiteCommand sqlite_cmd;
            try
            {
                this.sqlite_conn.Open();
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = query;
                sqlite_cmd.ExecuteNonQuery();
                Console.WriteLine("Query successful.");
            }
            catch (Exception ex)
            {
                Console.Write("Error performing Query!\n\r" + ex);
                this.sqlite_conn.Close();
            }
            finally
            {
                this.sqlite_conn.Close();
            }
        }

        public void Query(string query, string console_output_text)
        {
            SQLiteCommand sqlite_cmd;
            try
            {
                this.sqlite_conn.Open();
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = query;
                sqlite_cmd.ExecuteNonQuery();
                Console.WriteLine("Query successful. " + console_output_text);
            }
            catch (Exception ex)
            {
                Console.Write("Error performing Query! " + console_output_text + "\n\r" + ex);
                this.sqlite_conn.Close();
            }
            finally
            {
                this.sqlite_conn.Close();
            }
        }

        #endregion

        #region FETCH METHODS

        public DataSet Fetch(string query)
        {
            SQLiteCommand sql_cmd;
            SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            try
            {
                this.sqlite_conn.Open();
                sql_cmd = this.sqlite_conn.CreateCommand();
                DB = new SQLiteDataAdapter(query, this.sqlite_conn);
                DS.Reset();
                DB.Fill(DS);
                Console.WriteLine("Fetch query completed.");
            }
            catch (Exception ex)
            {
                this.sqlite_conn.Close();
                Console.WriteLine("Read data failed! \n\r" + ex);
            }
            finally
            {
                this.sqlite_conn.Close();
            }
            return DS;
        }

        public DataSet Fetch(string query, string console_output_text)
        {
            SQLiteCommand sql_cmd;
            SQLiteDataAdapter DB;
            DataSet DS = new DataSet();
            try
            {
                this.sqlite_conn.Open();
                sql_cmd = this.sqlite_conn.CreateCommand();
                DB = new SQLiteDataAdapter(query, this.sqlite_conn);
                DS.Reset();
                DB.Fill(DS);
                Console.WriteLine("Fetch query completed. " + console_output_text);
            }
            catch (Exception ex)
            {
                this.sqlite_conn.Close();
                Console.WriteLine("Read data failed! console output text: " + console_output_text + "\n\r" + ex);
            }
            finally
            {
                this.sqlite_conn.Close();
            }
            return DS;
        }

        #endregion

        private bool SQLite_database_path_contains_dot_db(string SQLite_database_path)
        {
            return SQLite_database_path.Contains(".db");
        }

    }
}
