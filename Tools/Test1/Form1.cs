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
            DB dB = new DB();
            DataTable dt = dB.Get();
            dataGridView1.DataSource = dt;
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
