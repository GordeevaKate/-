﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace Практика
{
    public partial class FormMain : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public FormMain()
        {
            InitializeComponent();
        }

        private void napravlenieToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
            var form = Container.Resolve<FormNapravlenies>();
            form.ShowDialog();
        }

        private void studentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormStudents>();
            form.ShowDialog();
        }

        private void отчетToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormReport>();
            form.ShowDialog();
        }
    }
}
