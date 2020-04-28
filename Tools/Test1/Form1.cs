using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tools.mvvm;
namespace Test1
{
    public partial class Form1 : Form
    {
        private Team team;
        private bool isUpdate = false;//是否需要更新
        private DataTable dt;

        DB dB = new DB();

        public Form1()
        {
            InitializeComponent();
            team = new Team() { Leader = "杨海"};
            Binding binding = new Binding("Text",team,"Leader",false,DataSourceUpdateMode.OnPropertyChanged);
            button1.DataBindings.Add(binding);

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (team.Leader == "杨海")
            {
                team.Leader = "杨秋香";
            }
            else
            {
                team.Leader = "杨海";
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            dt = dB.Get();
            dataGridView1.DataSource = dt;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (isUpdate)
            {
                bool isOk = dB.Update(dt);
                isUpdate = false;
                MessageBox.Show("更新成功","保存");
            }
            else
            {
                MessageBox.Show("没有更新内容! ");
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    dataGridView1[j, i].Style.BackColor = Color.White;
                }
            }
        }

        private void DataGridView1_CellValueChanged_1(object sender, DataGridViewCellEventArgs e)
        {
            if (MessageBox.Show("是否需要单个单元格保存？","保存",MessageBoxButtons.OKCancel)==DialogResult.OK)
            {
                dB.Update(e.RowIndex + 1, dt.Columns[e.ColumnIndex].Caption, dataGridView1.SelectedCells[0].FormattedValue.ToString());
            }
            else
            {
                isUpdate = true;
                dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor = Color.Blue;
            }

        }
    }

    public class Team:PropertyChangedBase {
        private string _leader;
        public string Leader {
            get { return _leader; }
            set {
                if (value != _leader)
                {
                    _leader = value;
                    this.NotifyPropertyChanged(()=>Leader);
                }
            }
        }
    }
}
