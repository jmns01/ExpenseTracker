﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExpenseTracker.Controller;
using ExpenseTracker.Ui;

namespace ExpenseTracker
{
    partial class FormMainMenu : Form
    {
        private AppController appController;
        private string UserEmail;

        public FormMainMenu(string email, AppController appController)
        {
            InitializeComponent();

            this.UserEmail = email;
            this.appController = appController;
        }

        public void FormMainMenu_Load(object sender, EventArgs e)
        {
            string welcomeString = "Welcome " + appController.getUserController().getNameByEmail(this.UserEmail);
            labelWelcome.Text = welcomeString;
        }

        private void buttonSpendings_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormSpendings formSpendings = new FormSpendings(this.UserEmail, this.appController);
            formSpendings.FormClosed += (s, args) => this.Close();
            formSpendings.ShowDialog();
        }
    }
}
