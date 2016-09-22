using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MovieDesktop
{
    public partial class Setting : Form
    {
        public Setting()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            textBox1.Text = openFileDialog1.FileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ConfigurationManager.AppSettings.Set("wplpath", textBox1.Text);
            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            config.AppSettings.Settings["wplpath"].Value = textBox1.Text;
            config.Save(ConfigurationSaveMode.Modified);
            MessageBox.Show("设置已经保存，请重新启动应用程序");
            this.Close();
        }

        private void Setting_Load(object sender, EventArgs e)
        {
            textBox1.Text = ConfigurationManager.AppSettings["wplpath"];
        }
    }
}
