﻿using ADTransport.Data.Model;
using ADTransport.Data.Wrapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADTransport.Forms
{
    public partial class AssistantsForm : Form
    {
        private string _lang;
        private Employee _employee;
        private bool _isEditMode = false;
        public AssistantsForm(Employee emp, string lang)
        {
            this._lang = lang;
            this._employee = emp;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Dock = DockStyle.Fill;
            this.TopLevel = false;
            InitializeComponent();
            ApplyTheme();
        }

        private void ApplyTheme()
        {
            int theme = _employee.Theme;
            //light
            if (theme == 0)
            {
                this.BackColor = Color.White;
            }
            //dark
            else if (theme == 1)
            {
                this.BackColor = Color.FromArgb(26, 35, 46);
                label1.ForeColor= Color.FromArgb(255, 140, 4);
                assistantsGBox.ForeColor= Color.FromArgb(255, 140, 4);
                fNameLbl.ForeColor = Color.FromArgb(255, 140, 4);
                lNameLbl.ForeColor = Color.FromArgb(255, 140, 4);
                usrNameLbl.ForeColor = Color.FromArgb(255, 140, 4);
                pswdLbl.ForeColor = Color.FromArgb(255, 140, 4);
                salaryLbl.ForeColor = Color.FromArgb(255, 140, 4);
            }
            //supercool
            else
            {
                this.BackColor = Color.FromArgb(255, 140, 4);
            }
        }
        private void AssistansForm_Load(object sender, EventArgs e)
        {
            button2.Visible = false;
            assistantsDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            List<Employee> assistants = EmployeeWrapper.GetAssistants();
            assistantsDGV.DataSource = assistants;
            assistantsDGV.Columns["ID"].Visible = false; ;
            assistantsDGV.Columns["isAdmin"].Visible = false;
            assistantsDGV.Columns["Theme"].Visible = false;
            assistantsDGV.Columns["Name"].Width = 121;
            assistantsDGV.Columns["SurName"].Width = 121;
            assistantsDGV.Columns["Salary"].Width = 70;
            assistantsDGV.Columns["Username"].Width = 121;
            assistantsDGV.Columns["Password"].Width = 250;
            if (_lang == "en-US")
            {
                assistantsDGV.Columns["SurName"].HeaderText = "Last name";
                
            }
            else
            {
                assistantsDGV.Columns["Name"].HeaderText = "Ime";
                assistantsDGV.Columns["SurName"].HeaderText = "Prezime";
                assistantsDGV.Columns["Username"].HeaderText = "Korisnicko ime";
                assistantsDGV.Columns["Password"].HeaderText = "Lozinka";
                assistantsDGV.Columns["Salary"].HeaderText = "Plata";
            }
            ToolStripMenuItem deleteMenuItem = new ToolStripMenuItem("Delete");
            if (_lang == "en-US")
                deleteMenuItem.Text = "Delete";
            else deleteMenuItem.Text = "Obriši";
            deleteMenuItem.Click += (sen, ee) =>
                {
                    if (assistantsDGV.SelectedRows.Count == 1)
                    {
                        DataGridViewRow selectedRow = assistantsDGV.SelectedRows[0];

                        int id;

                        int.TryParse(selectedRow.Cells["ID"].Value.ToString(), out id);
                        EmployeeWrapper.DeleteEmployee(id);
                        assistants = EmployeeWrapper.GetAssistants();
                        assistantsDGV.DataSource = assistants;

                    }
                    else
                    {
                        if (_lang == "en-US") MessageBox.Show("You must select enitre row", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        else MessageBox.Show("Potrebno je selektovati cijeli jedan red", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }

                };
            ContextMenuStrip contextMenuStripOrdrs = new ContextMenuStrip();
            contextMenuStripOrdrs.Items.Add(deleteMenuItem);
            assistantsDGV.ContextMenuStrip = contextMenuStripOrdrs;


            ToolStripMenuItem editMenuItem = new ToolStripMenuItem("Edit");
            if (_lang == "en-US")
                editMenuItem.Text = "Edit";
            else editMenuItem.Text = "Izmijeni";
            editMenuItem.Click += (sen, ee) =>
            {
                if (assistantsDGV.SelectedRows.Count == 1)
                {
                    _isEditMode = true;
                    DataGridViewRow selectedRow = assistantsDGV.SelectedRows[0];

                    string name = selectedRow.Cells["Name"].Value.ToString();
                    string lastname = selectedRow.Cells["Surname"].Value.ToString();
                    string username = selectedRow.Cells["Username"].Value.ToString();
                    string pswd = selectedRow.Cells["Password"].Value.ToString();
                    double salary;
                    double.TryParse(selectedRow.Cells["Salary"].Value.ToString(), out salary);
                    GroupBoxEditMode(name, lastname, username, pswd, salary);


                }
                else
                {
                    if (_lang == "en-US") MessageBox.Show("You must select enitre row", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    else MessageBox.Show("Potrebno je selektovati cijeli jedan red", "Upozorenje", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }

            };
            contextMenuStripOrdrs.Items.Add(editMenuItem);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!"".Equals(usrNameTBox.Text) && !"".Equals(pswdTBox.Text) && !"".Equals(fNameTBox.Text) && !"".Equals(lNameTBox.Text) && !"".Equals(salaryTBox.Text))
            {
                string username = usrNameTBox.Text;
                string password = pswdTBox.Text;
                string firstName = fNameTBox.Text;
                string lastName = lNameTBox.Text;
                double salary;
                double.TryParse(salaryTBox.Text, out salary);
                if (_isEditMode)
                {
                    DataGridViewRow selectedRow = assistantsDGV.SelectedRows[0];
                    int id;
                    int.TryParse(selectedRow.Cells["ID"].Value.ToString(), out id);
                    EmployeeWrapper.UpdateAssistant(id, firstName, lastName, username, password, salary);
                }
                else EmployeeWrapper.InsertEmployee(username, password, firstName, lastName, salary);
                List<Employee> assistants = EmployeeWrapper.GetAssistants();
                assistantsDGV.DataSource = assistants;
                assistantsDGV.Columns["ID"].Width = 30;
                usrNameTBox.Clear();
                pswdTBox.Clear();
                fNameTBox.Clear();
                lNameTBox.Clear();
                salaryTBox.Clear();
                GroupBoxAddMode();
                _isEditMode = false;
            }
            else
            {
                if (_lang == "en-US") MessageBox.Show("No data entered!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else MessageBox.Show("Niste unijeli potrebne podatke!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void GroupBoxAddMode()
        {
            button2.Visible = false;
            if (_lang == "en-US")
            {
                assistantsGBox.Text = "Add new assistant";
                button1.Text = "Add driver";
            }
            else
            {
                assistantsGBox.Text = "Dodavanje novog administrativnog radnika";
                button1.Text = "Dodajte administrativnog radnika";
            }
            fNameTBox.Clear();
            lNameTBox.Clear();
            usrNameTBox.Clear();
            pswdTBox.Clear();
            salaryTBox.Clear();
        }
        private void GroupBoxEditMode(string firstName, string lastName, string usrName, string pswd, double salary)
        {
            button2.Visible = true;
            if (_lang == "en-US")
            {
                assistantsGBox.Text = "Edit assistant info";
                button1.Text = "Update";
            }
            else
            {
                assistantsGBox.Text = "Izmijenite podatke o administrativnom radniku";
                button1.Text = "Ažuriraj";
            }
            fNameTBox.Text = firstName;
            lNameTBox.Text = lastName;
            usrNameTBox.Text = usrName;
            pswdTBox.Text = pswd;
            salaryTBox.Text = salary.ToString();
            usrNameTBox.Enabled = false;
            pswdTBox.Enabled = false;
        }

        private void assistantsDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _isEditMode = false;
            GroupBoxAddMode();
        }

        private void AssistantsForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (!_isEditMode)
                assistantsDGV.ClearSelection();
           // _isEditMode = false;
            //GroupBoxAddMode();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GroupBoxAddMode();
            assistantsDGV.ClearSelection();
        }
    }
}
