using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRowUpdate.Data
{
    class DbContext
    {
        private const string TableName = "testtable";
        private readonly string _dbPath;

        public DbContext(string dbPath)
        {
            this._dbPath = dbPath;
        }

        public SQLiteConnection GetConnection()
        {
            var conneciton = new SQLiteConnection("Data Source=" + _dbPath);
            conneciton.Open();
            return conneciton;
        }

        public void InitializeTable()
        {
            var query = $"CREATE TABLE IF NOT EXISTS {TableName} (id integer primary key, bilder TEXT)";
            ExecuteQuery(query);

            var insertQuery = $"INSERT INTO {TableName}(bilder) VALUES ('bild1'), ('bild2')";
            ExecuteQuery(insertQuery);
        }

        public void FillDataTable(DataTable table)
        {
            var selectQuery = $"SELECT * FROM {TableName}";

            using (var conneciton = GetConnection())
            using (var command = new SQLiteCommand(selectQuery, conneciton))
            using (var adapter = new SQLiteDataAdapter(command))
            {
                adapter.Fill(table);
            }

            if (table.PrimaryKey.Count() == 0)
            {
                table.PrimaryKey = new DataColumn[] { table.Columns["id"] };
            }
        }

        public void UpdateDatabase(DataTable table)
        {
            var selectQuery = $"SELECT * FROM {TableName}";

            using (var conneciton = GetConnection())
            using (var command = new SQLiteCommand(selectQuery, conneciton))
            using (var adapter = new SQLiteDataAdapter(command))
            {
                var builder = new SQLiteCommandBuilder(adapter);
                adapter.InsertCommand = builder.GetInsertCommand();
                adapter.DeleteCommand = builder.GetDeleteCommand();
                adapter.UpdateCommand = builder.GetUpdateCommand();
                adapter.Update(table);
            }
        }

        public void ExecuteQuery(string query)
        {
            using (var conneciton = GetConnection())
            using (var command = new SQLiteCommand(query, conneciton))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}
