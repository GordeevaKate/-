using BusinessLogic.Interfaces;
using BusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Unity;

namespace Практика
{
    public partial class FormSN : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id
        {
            get { return Convert.ToInt32(comboBoxName.SelectedValue); }
            set { comboBoxName.SelectedValue = value; }
        }
        public string NapravlenieName { get { return comboBoxName.Text; } }
        public string Number { get { return comboBoxName.Text; } }
        public int Kurs
        {
            get { return Convert.ToInt32(textBoxKurs.Text); }
            set
            {
                textBoxKurs.Text = value.ToString();
            }
        }
        public FormSN(INapravlenie logic)
        {
            InitializeComponent();
            List<NapravlenieVM> list = logic.Read(null);
            if (list != null)
            {
                comboBoxName.DisplayMember = "NapravlenieName";
                comboBoxName.ValueMember = "Id";
                comboBoxName.DataSource = list;
                comboBoxName.SelectedItem = null;
                comboBoxNumber.DisplayMember = "Number";
                comboBoxNumber.ValueMember = "Id";
                comboBoxNumber.DataSource = list;
                comboBoxNumber.SelectedItem = null;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
          
            if (string.IsNullOrEmpty(textBoxKurs.Text))
            {
                MessageBox.Show("Заполните курс", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Regex.IsMatch(textBoxKurs.Text, @"^\d+") == false)
            {
                MessageBox.Show("Заполните курс правильно. Целое число", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxName.SelectedValue == null)
            {
                MessageBox.Show("Выберите направление ", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (comboBoxNumber.SelectedValue == null)
            {
                MessageBox.Show("Выберите номер", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }


        private void buttonCansel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void comboBoxName_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                comboBoxNumber.Text = (string)comboBoxNumber.Items[comboBoxName.SelectedIndex];
            }
            catch { }
        }

        private void comboBoxNumber_SelectedValueChanged(object sender, EventArgs e)
        {

            try
            {
                comboBoxName.Text = (string)comboBoxName.Items[comboBoxNumber.SelectedIndex];
            }
            catch { }
        }
    }
}
