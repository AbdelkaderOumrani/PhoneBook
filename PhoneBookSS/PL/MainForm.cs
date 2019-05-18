using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PhoneBookSS.PL
{
    public partial class MainForm : Form
    {
        PbContext db;
        public MainForm()
        {
            InitializeComponent();
            db = new PbContext();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ReloadGrid();
            FilterBy.SelectedIndex = 0;
        }
        public void ReloadGrid()
        {
            ContactGrid.DataSource = db.Contacts.ToList();
            count.Text = ContactGrid.Rows.Count.ToString();
            RestyleGrid();
        }
        public void RestyleGrid()
        {

                //autosizing
            ContactGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; // Autosize Show Btn
            ContactGrid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; // Autosize Edit
            ContactGrid.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; // Autosize Delete
            //change buttons index
            ContactGrid.Columns[2].DisplayIndex = ContactGrid.Columns[1].DisplayIndex =
                ContactGrid.Columns[0].DisplayIndex = 9;
            //Rename Headers
            ContactGrid.Columns[4].HeaderText = "First Name";
            ContactGrid.Columns[4].SortMode = DataGridViewColumnSortMode.Automatic;
            ContactGrid.Columns[5].HeaderText = "Last Name";
            ContactGrid.Columns[6].HeaderText = "Address";
            ContactGrid.Columns[8].HeaderText = "Home Phone";
            ContactGrid.Columns[8].HeaderText = "Mobile Phone";
            ContactGrid.Columns[9].HeaderText = "Email";
            // Hide ID Column
            ContactGrid.Columns[3].Visible = false;
        } 


        private void AddButton_Click(object sender, EventArgs e)
        {
            AddEditContact add = new AddEditContact(this,0, "add");
            add.Show();

        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            //FirstName
            if (FilterBy.SelectedIndex == 0)
            {
                var result = db.Contacts.Where(c => c.FirstName.Contains(SearchBox.Text)).ToList();
                ContactGrid.DataSource = result;
                count.Text = ContactGrid.Rows.Count.ToString();
                RestyleGrid();
            }
            //LastName
            if (FilterBy.SelectedIndex == 1)
            {
                var result = db.Contacts.Where(c => c.LastName.Contains(SearchBox.Text)).ToList();
                ContactGrid.DataSource = result;
                count.Text = ContactGrid.Rows.Count.ToString();
                RestyleGrid();
            }
            //address
            if (FilterBy.SelectedIndex == 2)
            {
                var result = db.Contacts.Where(c => c.Address.Contains(SearchBox.Text)).ToList();
                ContactGrid.DataSource = result;
                count.Text = ContactGrid.Rows.Count.ToString();
                RestyleGrid();
            }

            if (FilterBy.SelectedIndex == 3)
            {
                var result = db.Contacts.Where(c => c.HomePhone.Contains(SearchBox.Text)).ToList();
                ContactGrid.DataSource = result;
                count.Text = ContactGrid.Rows.Count.ToString();
                RestyleGrid();
            }

            if (FilterBy.SelectedIndex == 4)
            {
                var result = db.Contacts.Where(c => c.MobilePhone.Contains(SearchBox.Text)).ToList();
                ContactGrid.DataSource = result;
                count.Text = ContactGrid.Rows.Count.ToString();
                RestyleGrid();
            }

            if (FilterBy.SelectedIndex == 5)
            {
                var result = db.Contacts.Where(c => c.Email.Contains(SearchBox.Text)).ToList();
                ContactGrid.DataSource = result;
                count.Text = ContactGrid.Rows.Count.ToString();
                RestyleGrid();
            }
        }

        private void ContactGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >=0)
            {


                int id = Convert.ToInt32(ContactGrid.SelectedRows[0].Cells[3].Value.ToString());
                // show contact infos;
                if (e.ColumnIndex == 0)
                {
                    ShowContact contact = new ShowContact(id);
                    contact.ShowDialog();

                }
                //Edit contact infos;

                if (e.ColumnIndex == 1)
                {
                    AddEditContact edit = new AddEditContact(this, id, "edit");
                    edit.WindowTitle.Text = edit.Text = "Edit contact information";
                    var contact = db.Contacts.Find(id);
                    edit.FirstName.Text = contact.FirstName;
                    edit.LastName.Text = contact.LastName;
                    edit.Address.Text = contact.Address;
                    edit.HomePhone.Text = contact.HomePhone;
                    edit.MobilePhone.Text = contact.MobilePhone;
                    edit.Email.Text = contact.Email;
                    edit.AddButton.Text = "Update Contact";
                    edit.ShowDialog();
                }
                if (e.ColumnIndex == 2)
                {
                    string f = ContactGrid.Rows[e.RowIndex].Cells[4].Value.ToString();
                    string l = ContactGrid.Rows[e.RowIndex].Cells[5].Value.ToString();

                    DialogResult result = MessageBox.Show("Do you want to delete this contact, " + f + " " + l + " ?", "Delete Contact", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        var contact = db.Contacts.Find(id);
                        db.Contacts.Remove(contact);
                        db.SaveChanges();
                        ReloadGrid();
                    }
                }
            }
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
