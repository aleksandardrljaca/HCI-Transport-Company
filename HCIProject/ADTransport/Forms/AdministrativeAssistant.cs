﻿using ADTransport.Data.HashUtils;
using ADTransport.Data.Model;
using ADTransport.Data.Wrapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADTransport.Forms
{
    public partial class AdministrativeAssistant : Form
    {
        public Employee administrativeAssistant;
        public string lang;
        public AdministrativeAssistant(Employee emp,string lang)
        {
            this.administrativeAssistant = emp;
            this.lang = lang;
            this.FormBorderStyle = FormBorderStyle.None;
            
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang);
            this.Controls.Clear();
            InitializeComponent();
            ApplyTheme();
        }
        public void RefreshForm(string l)
        {
            this.lang = l;
            administrativeAssistant = EmployeeWrapper.GetEmployee(administrativeAssistant.Username, administrativeAssistant.Password);
            this.FormBorderStyle = FormBorderStyle.None;
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang);
            this.Controls.Clear();
            InitializeComponent();
            ApplyTheme();
        }
        public void UpdateEmployee(string name,string password)
        {
            this.administrativeAssistant = EmployeeWrapper.GetEmployee(name, password);
        }
        private void ApplyTheme()
        {
                
                int theme = administrativeAssistant.Theme;
                //light
                if (theme == 0)
                {
                    this.welcomePanel.BackColor = Color.White;
                }
                //dark
                else if (theme == 1)
                {
                    this.welcomePanel.BackColor = Color.FromArgb(26, 35, 46);
                }
                //supercool
                else
                {
                    this.welcomePanel.BackColor = Color.FromArgb(255, 140, 4);
                }
            
        }
        private void ButtonMouseEnter(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.Height += 10;
            btn.Width += 10;
            btn.Font = new Font("Rockwell", 12, FontStyle.Bold | FontStyle.Italic);
            
            DropBtnIcon(btn);
        }
        private void ButtonMouseLeave(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.Height -= 10;
            btn.Width -= 10;
            btn.Font = new Font("Rockwell", 12, FontStyle.Bold | FontStyle.Italic);
            NormalizeBtnIcon(btn);
        }
        private void DropBtnIcon(Button button)
        {
            if (button == clientsBtn)
                clientsIcon.Location = new Point(clientsIcon.Location.X, clientsIcon.Location.Y + 5);
            if (button == ordersBtn)
                ordersIcon.Location = new Point(ordersIcon.Location.X, ordersIcon.Location.Y + 5);
            if (button == invoicesBtn)
                invoicesIcon.Location = new Point(invoicesIcon.Location.X, invoicesIcon.Location.Y + 5);
            if (button == lbltsBtn)
                lbltsIcon.Location = new Point(lbltsIcon.Location.X, lbltsIcon.Location.Y + 5);
            if (button == accntBtn)
                accntIcon.Location = new Point(accntIcon.Location.X, accntIcon.Location.Y + 5);
            if (button == stngsBtn)
                stngsIcon.Location = new Point(stngsIcon.Location.X, stngsIcon.Location.Y + 5);
            if (button == logoutBtn)
                logoutIcon.Location = new Point(logoutIcon.Location.X, logoutIcon.Location.Y + 5);
        }
        private void NormalizeBtnIcon(Button button)
        {
            if (button == clientsBtn)
                clientsIcon.Location = new Point(clientsIcon.Location.X, clientsIcon.Location.Y - 5);
            if (button == ordersBtn)
                ordersIcon.Location = new Point(ordersIcon.Location.X, ordersIcon.Location.Y - 5);
            if (button == invoicesBtn)
                invoicesIcon.Location = new Point(invoicesIcon.Location.X, invoicesIcon.Location.Y - 5);
            if (button == lbltsBtn)
                lbltsIcon.Location = new Point(lbltsIcon.Location.X, lbltsIcon.Location.Y - 5);
            if (button == accntBtn)
                accntIcon.Location = new Point(accntIcon.Location.X, accntIcon.Location.Y - 5);
            if (button == stngsBtn)
                stngsIcon.Location = new Point(stngsIcon.Location.X, stngsIcon.Location.Y - 5);
            if (button == logoutBtn)
                logoutIcon.Location = new Point(logoutIcon.Location.X, logoutIcon.Location.Y - 5);
        }

        private void AdministrativeAssistant_Load(object sender, EventArgs e)
        {

        }

        private void LogoutBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ClientsBtnClick(object sender, EventArgs e)
        {
            ClientsForm cf = new ClientsForm(this.administrativeAssistant.Theme,lang);
            welcomePanel.Controls.Clear();
            welcomePanel.Controls.Add(cf);
            cf.Show();
        }

        private void AdministrativeAssistant_Load_1(object sender, EventArgs e)
        {

        }

        private void ordersBtn_Click(object sender, EventArgs e)
        {
            OrdersForm form = new OrdersForm(this.administrativeAssistant, lang);
            welcomePanel.Controls.Clear();
            welcomePanel.Controls.Add(form);
            form.Show();
        }

        private void stngsBtn_Click(object sender, EventArgs e)
        {
            SettingsForm form = new SettingsForm(this.administrativeAssistant,lang);
            welcomePanel.Controls.Clear();
            welcomePanel.Controls.Add(form);
            form.Show();
        }

        private void accntBtn_Click(object sender, EventArgs e)
        {
            MyAccountForm form = new MyAccountForm(this.administrativeAssistant,lang);
            welcomePanel.Controls.Clear();
            welcomePanel.Controls.Add(form);
            form.Show();
        }

        private void lbltsBtn_Click(object sender, EventArgs e)
        {
            LiabilitiesForm form = new LiabilitiesForm(this.administrativeAssistant,lang);
            welcomePanel.Controls.Clear();
            welcomePanel.Controls.Add(form);
            form.Show();
        }

        private void invoicesBtn_Click(object sender, EventArgs e)
        {
            InvoicesForm form = new InvoicesForm(this.administrativeAssistant, lang);
            welcomePanel.Controls.Clear();
            welcomePanel.Controls.Add(form);
            form.Show();
        }
    }
}
