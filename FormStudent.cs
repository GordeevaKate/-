using BusinessLogic.BindingModel;
using BusinessLogic.Interfaces;
using BusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;

namespace Практика
{
    public partial class FormStudent : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private readonly IStudent logic;
        private int? id;
        private Dictionary<int, (string,string, int)> sNs;
        public FormStudent(IStudent service)
        {
            InitializeComponent();
            this.logic = service;
        }

        private void FormStudent_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    StudentVM view = logic.Read(new StudentBM
                    {
                        Id =
                       id.Value
                    })?[0];
                    if (view != null)
                    {
                        textBoxName.Text = view.StudentName;
                      
                        sNs = view.SNs;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
            else
            {
                sNs = new Dictionary<int, (string, string,int)>();
            }
        }
        private void LoadData()
        {
            try
            {
                if (sNs != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var pc in sNs)
                    {
                        dataGridView.Rows.Add(new object[] { pc.Key, pc.Value.Item1,
pc.Value.Item2,pc.Value.Item3 });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
MessageBoxIcon.Error);
            }
        }
        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormSN>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (sNs.ContainsKey(form.Id))
                {
                    sNs[form.Id] = (form.NapravlenieName, form.Number,form.Kurs);
                }
                else
                {
                    sNs.Add(form.Id, (form.NapravlenieName,form.Number, form.Kurs));
                }
                LoadData();
            }
        }
        private void ButtonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormSN>();
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                form.Id = id;
                form.Kurs = sNs[id].Item3;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    sNs[form.Id] = (form.NapravlenieName, form.Number,form.Kurs);
                    LoadData();
                }
            }
        }
        private void ButtonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {

                        sNs.Remove(Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }
        private void ButtonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (sNs == null || sNs.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }

            try
            {
                logic.CreateOrUpdate(new StudentBM
                {

                    Id = id,

                    StudentName = textBoxName.Text,

                    SNs = sNs
                }
                );
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ОшибкаsushiSeafoods ", MessageBoxButtons.OK,
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
