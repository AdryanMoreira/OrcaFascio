using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrcaFascio.Presentation
{
    public partial class PasswordForm : Form
    {
        public PasswordForm()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text == "ints@123")
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                txtPassword.Clear();
                MessageBox.Show("Senha Inválida.");
                txtPassword.Focus();
            }
        }
        protected void OnKeyDown(object sender, KeyPressEventArgs e)
        {
            
            if (e.KeyChar == (char)13)
            {
                e.Handled = true;
                btnOk_Click(sender, e);
            }
                
        }
    }
}
