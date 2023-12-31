using ExpenseTracker.Business_Logic;
using ExpenseTracker.Controller;
using ExpenseTracker.Ui;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ExpenseTracker
{
    partial class FormMain : Form
    {
        private AppController appController;

        public FormMain(AppController appController)
        {
            InitializeComponent();
            this.appController = appController;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            labelError.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBoxEmail.Text;
            string pw = textBoxPw.Text;

            if (email.Length <= 0 || pw.Length <= 0)
            {
                labelError.Visible = true;
                return;
            }

            UsersBL user = appController.getUserController().AuthenticateUser(email, pw);


            if (user != null)
            {
                this.Hide();
                FormMainMenu formMainMenu = new FormMainMenu(user, appController);
                formMainMenu.FormClosed += (s, args) => this.Close();
                formMainMenu.ShowDialog();
            }
            else
            {
                MessageBox.Show("Error when loggin in!");
            }
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormCreateAccount createAccount = new FormCreateAccount(this.appController);
            createAccount.FormClosed += (s, args) => this.Close();
            createAccount.ShowDialog();
        }
    }
}