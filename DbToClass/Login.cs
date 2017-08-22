using Common;
using Common.models;
using Common.Extension;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DbToClass
{
    public partial class Login : Form
    {
        DBOpertion opertion = new DBOpertion();
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            var table_Names = opertion.GetTableName();
            DataTable table = new DataTable();
            DataColumn column = new DataColumn();
            column.ColumnName = "TableName";

            table.Columns.Add(column);
            foreach (var name in table_Names)
            {
                DataRow row = table.NewRow();
                row["TableName"] = name;
                table.Rows.Add(row);
            }
            dataGridView_TableName.DataSource = table;
            dataGridView_TableName.AllowUserToDeleteRows = false;
            dataGridView_TableName.AllowUserToAddRows = false;
            dataGridView_TableName.AllowUserToOrderColumns = false;
            dataGridView_TableName.AllowUserToResizeColumns = false;
            dataGridView_TableName.AllowUserToResizeRows = false;
            dataGridView_TableName.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView_TableName.ReadOnly = true;
            dataGridView_FieIdInfo.AllowUserToDeleteRows = false;
            dataGridView_FieIdInfo.AllowUserToAddRows = false;
            dataGridView_FieIdInfo.AllowUserToOrderColumns = false;
            dataGridView_FieIdInfo.AllowUserToResizeColumns = false;
            dataGridView_FieIdInfo.AllowUserToResizeRows = false;
            dataGridView_FieIdInfo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView_FieIdInfo.ReadOnly = true;
            panel1.Dock = DockStyle.Fill;
            panel1.Visible = false;
        }

        private void dataGridView_TableName_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var tableName = dataGridView_TableName.SelectedCells[0].Value.ToString();
            panel1.Visible = true;
            var list = opertion.GetFieIdInfo(tableName);
            var table = list.ToDataTable<Fieid>();
            dataGridView_FieIdInfo.DataSource = table;

            panel1.Visible = false;
        }

        private void DbOpertionBtn_Click(object sender, EventArgs e)
        {
            var tableName = dataGridView_TableName.SelectedCells[0].Value.ToString();
            var list = opertion.GetFieIdInfo(tableName);
            var text = Common.DbToText.DBToSqlOption.Instance.GetText(list, tableName);
            Clipboard.SetDataObject(text);
            MessageBox.Show("数据库操作类已复制");
        }

        private void ModelBtn_Click(object sender, EventArgs e)
        {
            var tableName = dataGridView_TableName.SelectedCells[0].Value.ToString();
            var list = opertion.GetFieIdInfo(tableName);
            var text = Common.DbToText.DbToModel.Instance.GetText(list, tableName);
            Clipboard.SetDataObject(text);
            MessageBox.Show("数据库模型类已复制");
        }

        private void ModelCreateBtn_Click(object sender, EventArgs e)
        {
            var table_Names = opertion.GetTableName();
            var ModelPath = "E:\\DB\\" + opertion.GetDbName() + "\\Model";
            if (!Directory.Exists(ModelPath))
            {
                Directory.CreateDirectory(ModelPath);
            }
            foreach (var item in table_Names)
            {
                var csModelPath = ModelPath + "\\" + item + ".cs";
                var list = opertion.GetFieIdInfo(item);
                var text = Common.DbToText.DbToModel.Instance.GetText(list, item);
                File.WriteAllLines(csModelPath, text.Split('\n'));
            }
            MessageBox.Show("数据库模型类已生成");
        }

        private void OperCreateBtn_Click(object sender, EventArgs e)
        {
            var table_Names = opertion.GetTableName();
            var OperPath = "E:\\DB\\" + opertion.GetDbName() + "\\Oper";
            if (!Directory.Exists(OperPath))
            {
                Directory.CreateDirectory(OperPath);
            }
            foreach (var item in table_Names)
            {
                var csOperPath = OperPath + "\\" + item + "Oper.cs";
                var list = opertion.GetFieIdInfo(item);
                var text = Common.DbToText.DBToSqlOption.Instance.GetText(list, item);
                File.WriteAllLines(csOperPath, text.Split('\n'));
            }
            MessageBox.Show("数据库操作类已生成");
        }
    }
}
