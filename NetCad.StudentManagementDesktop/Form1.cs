using NetCad.Entity;
using NetCad.Services.Interfaces.Domain;
using NetCad.StudentManagementDesktop.Extensions;
using NetCad.StudentManagementDesktop.UITemplates.DataGridViewTemplate;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetCad.StudentManagementDesktop
{
    public partial class Form1 : Form
    {
        private readonly IStudentService _studentService;
        private List<Student> _students;
        public Form1(IStudentService studentService)
        {
            InitializeComponent();
            _studentService = studentService;
        }

        private async void Form1_Load(object sender, System.EventArgs e)
        {
            await RefreshGridData();
            ArrangeDataGridViewColumns();
        }

        private void ArrangeDataGridViewColumns()
        {
            dataGridView1.Columns[0].Visible = false; // Id kolonu
            dataGridView1.Columns[1].Visible = false; // UniqueId kolonu

            // Tarih alanı için DateTimePicker kullanımı
            var birthDateColumn = new DataGridViewDateTimePickerColumn
            {
                DataPropertyName = "BirthDate",
                HeaderText = "Birth Date"
            };

            dataGridView1.Columns.Remove("BirthDate");

            dataGridView1.Columns.Insert(3, birthDateColumn);

            dataGridView1.Columns[6].ReadOnly = true; // Registration kolonu
        }

        private async void btnRefresh_Click(object sender, System.EventArgs e)
        {
            await RefreshGridData();
        }

        private async Task RefreshGridData()
        {
            _students = await _studentService.GetAllAsync();
            dataGridView1.DataSource = _students;
        }

        private async void btnSave_Click(object sender, System.EventArgs e)
        {
            Student std = new Student
            {
                UniqueId = StringExtensions.GenerateUniqueId(),
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                BirthDate = dtpBirthDate.Value,
                PlaceOfBirth = txtPlaceOfBirth.Text
            };

            int affectedRows = await _studentService.InsertAsync(std);

            ShowAffectedMessage(affectedRows);
        }

        private async void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var updatedStudent = _students[e.RowIndex];

                int affectedRows = await _studentService.UpdateAsync(updatedStudent);

                ShowAffectedMessage(affectedRows);
            }
        }

        private void ShowAffectedMessage(int affectedRows)
        {
            if (affectedRows > 0)
            {
                MessageBox.Show("Başarılı");
            }
            else
            {
                MessageBox.Show("Hata oluştu");
            }
        }

        private async void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            var student = e.Row.DataBoundItem as Student;
            if (student != null)
            {
                int affectedRows = await _studentService.DeleteAsync(student.Id);

                ShowAffectedMessage(affectedRows);
            }
        }
    }
}
