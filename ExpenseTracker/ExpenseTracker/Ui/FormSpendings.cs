﻿using ExpenseTracker.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;

namespace ExpenseTracker.Ui
{
    partial class FormSpendings : Form
    {
        private AppController appController;
        private string email;
        public FormSpendings(string email, AppController appController)
        {
            InitializeComponent();

            this.appController = appController;
            this.email = email;
        }

        private void FormSpendings_Load(object sender, EventArgs e)
        {
            labelMonth.Text = DateTime.Now.ToString("MMMM yyyy");
            DateTime now = new DateTime();
            now = DateTime.Now;
            UpdatePage(sender, e, now);
        }

        public void UpdatePage(object sender, EventArgs e, DateTime date)
        {
            int idUser = appController.getUserController().getIdByEmail(email);


            DataTable expensesTable = appController.getExpensesController().getExpensesOfUser(idUser);
            DataTable categoriesTable = appController.getCategoriesController().getCategories();

            var filteredExpenses = expensesTable.AsEnumerable()
                .Where(expenseRow =>
                {
                    DateTime expenseDate = expenseRow.Field<DateTime>("Date");
                    return expenseDate.Year == date.Year && expenseDate.Month == date.Month;
                });

            var query = from categoryRow in categoriesTable.AsEnumerable()
                        join expenseRow in filteredExpenses
                        on categoryRow.Field<int>("CategoryId") equals expenseRow.Field<int>("Category_Id") into joined
                        from expenseGroup in joined.DefaultIfEmpty()
                        group expenseGroup by categoryRow.Field<string>("CategoryName") into grouped
                        select new
                        {
                            CategoryName = grouped.Key,
                            TotalAmount = grouped.Sum(r => r?.Field<decimal>("Amount") ?? 0)
                        };

            DataTable resultTable = new DataTable();
            resultTable.Columns.Add("Category Name", typeof(string));
            resultTable.Columns.Add("Total Amount Spent", typeof(decimal));

            foreach (var item in query)
            {
                resultTable.Rows.Add(item.CategoryName, item.TotalAmount);
            }

            dataGridView1.DataSource = resultTable;
            dataGridView1.Columns["Total Amount Spent"].DefaultCellStyle.Format = "C";

        }

        private void buttonLeft_Click(object sender, EventArgs e)
        {
            DateTime current = DateTime.Parse(labelMonth.Text);
            DateTime previousMonth = current.AddMonths(-1);
            labelMonth.Text = previousMonth.ToString("MMMM yyyy");

            UpdatePage(sender, e, previousMonth);
        }

        private void buttonRight_Click(object sender, EventArgs e)
        {
            DateTime current = DateTime.Parse(labelMonth.Text);
            DateTime nextMonth = current.AddMonths(1);
            labelMonth.Text = nextMonth.ToString("MMMM yyyy");

            UpdatePage(sender, e, nextMonth);
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormMainMenu mainMenu = new FormMainMenu(this.email, this.appController);
            mainMenu.ShowDialog();
            this.Close();
        }
    }
}