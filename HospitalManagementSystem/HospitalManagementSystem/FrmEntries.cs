﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalManagementSystem
{
    public partial class FrmEntries : Form
    {
        public FrmEntries()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmPatientLogin frmPatientLogin = new FrmPatientLogin();
            frmPatientLogin.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmDoctorLogin frmDoctorLogin = new FrmDoctorLogin();
            frmDoctorLogin.Show(); 
            this.Hide();    
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmSecretaryLogin frmSecretaryLogin = new FrmSecretaryLogin(); 
            frmSecretaryLogin.Show();
            this.Hide();
        }
    }
}
