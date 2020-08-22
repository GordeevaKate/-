using BusinessLogic.BindingModel;
using BusinessLogic.BusinessLogics;
using Database.Models;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Unity;
using Microsoft.Office.Interop.Word;
namespace Практика
{
  public partial class FormReport : Form
  {
    [Dependency]
    public new IUnityContainer Container { get; set; }
    private readonly ReportLogic logic;
    public FormReport(ReportLogic logic)
    {
      InitializeComponent();
      this.logic = logic;
    }

    private void buttonSave_Click(object sender, EventArgs e)
    {
            try
            {
                Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();
                Document doc = app.Documents.Add(Visible: true);
                Range r = doc.Range();
                r.Text = "";
                Table t = doc.Tables.Add(r, dataGridViewSave.RowCount, dataGridViewSave.ColumnCount);
                t.Borders.Enable = 1;
                int j = 0;
                foreach (Row row in t.Rows)
                {
                    int column = 0;
                    foreach (Cell cell in row.Cells)
                    {
                        if (cell.RowIndex == 1)
                        {
                            if (column == 0) { cell.Range.Text = Column2.HeaderText; }
                            if (column == 1)
                            {
                                cell.Range.Text = Column2.HeaderText;
                            }
                            if (column == 2)
                            {
                                cell.Range.Text = Column3.HeaderText;
                            }
                            if (column == 3)
                            {
                                cell.Range.Text = Column4.HeaderText;
                            }
                            if (column == 4)
                            {
                                cell.Range.Text = Column5.HeaderText;
                            }
                            column++;
                            cell.Range.Bold = 1;
                            cell.Range.Font.Name = "verdana";
                            cell.Range.Font.Size = 10;
                            cell.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                            cell.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                        }
                        else
                        {
                            cell.Range.Text = Convert.ToString(dataGridViewSave.Rows[cell.RowIndex - 1].Cells[cell.ColumnIndex - 1].Value);
                        }
                    }
                    j++;
                }
                doc.Save();
                doc.Close();
                MessageBox.Show("Отчет сохранен", "сообщение", MessageBoxButtons.OK,
MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Ошибка при сохранении", "сообщение", MessageBoxButtons.OK,
MessageBoxIcon.Error);
            }
        }

    private void buttonMake_Click(object sender, EventArgs e)
    {

      try
      {
        var dict = logic.GetStudent();
        if (dict != null)
        {
          dataGridView.Rows.Clear();
          foreach (var date in dict)
          {
            dataGridView.Rows.Add(new object[] { "", date.StudentName, date.NapravlenieName, date.Number, date.Kurs });

          }
        }

      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void buttonSortFIo_Click(object sender, EventArgs e)
    {
      dataGridView.Sort(dataGridView.Columns[1], ListSortDirection.Ascending);
      dataGridViewSave.Sort(dataGridViewSave.Columns[1], ListSortDirection.Ascending);
    }

    private void buttonSortNap_Click(object sender, EventArgs e)
    {
      dataGridView.Sort(dataGridView.Columns[2], ListSortDirection.Ascending);
      dataGridViewSave.Sort(dataGridViewSave.Columns[2], ListSortDirection.Ascending);
    }

    private void buttonSortNumber_Click(object sender, EventArgs e)
    {
      dataGridView.Sort(dataGridView.Columns[3], ListSortDirection.Ascending);
      dataGridViewSave.Sort(dataGridViewSave.Columns[2], ListSortDirection.Ascending);
    }

    private void buttonSortKurs_Click(object sender, EventArgs e)
    {
      dataGridView.Sort(dataGridView.Columns[4], ListSortDirection.Ascending);
      dataGridViewSave.Sort(dataGridViewSave.Columns[2], ListSortDirection.Ascending);
    }

    private void buttonAdd_Click(object sender, EventArgs e)
    {
      object[] items = new object[dataGridView.ColumnCount];
      for (int i = 0; i < dataGridView.RowCount-1; i++)
      {
        
         if ((dataGridView.Rows[i].Cells[1].Value.ToString() == textBoxFIO.Text)
        ||( dataGridView.Rows[i].Cells[2].Value.ToString() == textBoxNapravlenie.Text) 
        || (dataGridView.Rows[i].Cells[3].Value.ToString() == textBoxNumber.Text)
        || (dataGridView.Rows[i].Cells[4].Value.ToString() == textBoxKurs.Text))
        {

          for (int z = 0; z < dataGridView.ColumnCount; z++)
            {
              items[z] = dataGridView.Rows[i].Cells[z].Value;
            }

            dataGridViewSave.Rows.Add(new object[] { "", items[1], items[2], items[3], items[4] });

        }
      }
      for (int i = 0; i < dataGridViewSave.RowCount - 1; i++)
      {


        for (int z = i + 1; z < dataGridViewSave.ColumnCount; z++)
        {
          if (dataGridViewSave.Rows[z] == dataGridViewSave.Rows[i])
          {
            dataGridViewSave.Rows.RemoveAt(z);
            z--;
          }
        }
      }
        
    }

          private void buttonRemove_Click(object sender, EventArgs e)
    {
          for (int i = dataGridViewSave.RowCount - 2; i >=0; i--)
           {
        if ((dataGridViewSave.Rows[i].Cells[1].Value.ToString() == textBoxFIO.Text)  
                    || ((dataGridViewSave.Rows[i].Cells[2].Value.ToString() == textBoxNapravlenie.Text)    || ((dataGridViewSave.Rows[i].Cells[3].Value.ToString() == textBoxNumber.Text) && (dataGridViewSave.Rows[i].Cells[2].Value.ToString() != null))
         || (dataGridViewSave.Rows[i].Cells[4].Value.ToString() == textBoxKurs.Text) ))
        {

                 dataGridViewSave.Rows.RemoveAt(i);
     
               }
           }
    }

    private void buttonClear_Click(object sender, EventArgs e)
    {
      dataGridViewSave.Rows.Clear();
    }

    private void buttonAll_Click(object sender, EventArgs e)
    {
      object[] items = new object[dataGridView.ColumnCount];
      dataGridViewSave.Rows.Clear();
      for (int i = 0; i < dataGridView.RowCount - 1; i++)
      {
        

          for (int z = 0; z < dataGridView.ColumnCount; z++)
          {
            items[z] = dataGridView.Rows[i].Cells[z].Value;
          }

          dataGridViewSave.Rows.Add(new object[] { "", items[1], items[2], items[3], items[4] });

        }
      }
    }
  }

