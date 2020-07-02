using Sunisoft.IrisSkin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace 皮肤
{
    public partial class Form1 : Form
    {
        SkinEngine skinEngine = new SkinEngine();
        List<String> Skins = new List<string>();

        public Form1()
        {
            InitializeComponent();
            #region 加载皮肤
            Skins = Directory.GetFiles(Application.StartupPath + @"\Skins\", "*.ssk").ToList();
            skinEngine.SkinFile = Skins[3];
            skinEngine.Active = true;
            #endregion
            dataGridView1.DataSource = Skins.ToArray();

            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;

            System.Reflection.Assembly appDll = System.Reflection.Assembly.GetExecutingAssembly();

            //pictureBox1.MouseEnter += delegate (object sender, EventArgs e)
            //{
            //    pictureBox1.BackgroundImage = new Bitmap(@"D:\Projects\CSharp\ToolsFromGit\Tools\皮肤\2.png");  
            //};
            //pictureBox1.MouseLeave += delegate (object sender, EventArgs e)
            //{
            //    pictureBox1.BackgroundImage = new Bitmap(@"D:\Projects\CSharp\ToolsFromGit\Tools\皮肤\1.jpg");
            //};
            //pictureBox1.MouseDown += delegate (object sender, MouseEventArgs e)
            //{
            //    pictureBox1.BackgroundImage = new Bitmap(@"D:\Projects\CSharp\ToolsFromGit\Tools\皮肤\3.png");
            //};
            //pictureBox1.MouseUp += delegate (object sender, MouseEventArgs e)
            //{
            //    pictureBox1.BackgroundImage = new Bitmap(@"D:\Projects\CSharp\ToolsFromGit\Tools\皮肤\2.png");
            //};

        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.BorderStyle = BorderStyle.Fixed3D;
            pictureBox1.Refresh();//强制控件重新绘制
            pictureBox1.BorderStyle = BorderStyle.None;
            pictureBox1.Refresh();
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            skinEngine.SkinFile = Skins[e.RowIndex];
            skinEngine.Active = true;
        }

        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {

        }
    }
}
