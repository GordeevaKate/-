using BusinessLogic.BindingModel;
using BusinessLogic.Interfaces;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Unity;
namespace Практика
{
    public partial class FormNapravlenie : Form
    {
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private readonly INapravlenie logic;
        private int? id;
        public FormNapravlenie(INapravlenie logic)
        {
            InitializeComponent();
            this.logic = logic;
        }
        private void FormNapravlenie_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var view = logic.Read(new NapravlenieBM { Id = id })?[0];
                    if (view != null)
                    {
                        textBoxName.Text = view.NapravlenieName;
                        textBoxNumber.Text = view.Number;
                        textBoxNumber.Text = view.Number;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (Regex.IsMatch(textBoxName.Text, @"^\S+\D+") == false)
            {
                MessageBox.Show("Заполните название направления буквами", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxNumber.Text))
            {
                MessageBox.Show("Заполните номер", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (Regex.IsMatch(textBoxNumber.Text, @"^\d{2}.\d{2}.\d{2}") == false)
            {
                MessageBox.Show("Заполните номер правильно. Пример 11.11.11", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
      
            try
            {
                logic.CreateOrUpdate(new NapravlenieBM
                {
                    Id = id,
                    NapravlenieName = textBoxName.Text,
                    Number= textBoxNumber.Text
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
