using ContactsSolution.buisness;
using System;
using System.Data;
using System.Windows.Forms;

namespace ConstactsSolutionDesktopApp
{
    public partial class FormListAllContacts : Form
    {
        public FormListAllContacts()
        {
            InitializeComponent();
            this.Text = "List All Contacts";

            dataGridView1.ContextMenuStrip = contextMenuStrip1;

           
            

        }

        private void _loadDataGridData()
        {
     
            DataTable dataTable = ContactInfo.GetAll();

            dataGridView1.DataSource = ContactInfo.GetAll();

        }



        private void Form1_Load(object sender, EventArgs e)
        {


            _loadDataGridData();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            EmailTextBox addnewContactForm = new EmailTextBox(-1);
            
            addnewContactForm.ShowDialog();
            _loadDataGridData();


        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            EmailTextBox editContactForm = new EmailTextBox((int)dataGridView1.CurrentRow.Cells[0].Value);
            editContactForm.ShowDialog();
            _loadDataGridData();

        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {

           
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this contact?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                ContactInfo.Delete((int)dataGridView1.CurrentRow.Cells[0].Value);
                _loadDataGridData();
            }

        }
    }
}
