using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestRowUpdate.Data;

namespace TestRowUpdate
{
    public partial class Form1 : Form
    {
        DbContext _context = new DbContext("test.db");
        DataTable _table = new DataTable();

        public Form1()
        {
            InitializeComponent();
            dataGridView1.DataSource = _table;
        }

        private void InitDataButton_Click(object sender, EventArgs e)
        {
            _context.InitializeTable();
        }

        private void FillButton_Click(object sender, EventArgs e)
        {
            _context.FillDataTable(_table);
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            _context.UpdateDatabase(_table);
        }

        private void ChangeButton_Click(object sender, EventArgs e)
        {
            var random = new Random();
            var choices = new[] { "bernd", "jens", "chris", "hans" };
            var maxId = _table.AsEnumerable().Max(row => row.Field<long>("id")) + 1;

            _table.Rows[0]["id"] = maxId;
            _table.Rows[0]["bilder"] = choices[random.Next(choices.Length)];
        }
    }
}
