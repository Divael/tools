using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 异步并行async_await_patalled用法
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //泛型写法
            BeginInvoke(new Action(async ()=> {
                int Count = 100;
                await Task.Run(()=> {
                    Parallel.For(0,Count,new ParallelOptions() {MaxDegreeOfParallelism = 8 },(i,state)=> { 
                        
                    });
                });
            }));
        }
    }
}
