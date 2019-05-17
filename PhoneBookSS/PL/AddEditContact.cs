using System;
using System.Windows.Forms;

namespace PhoneBookSS.PL
{
    public partial class AddEditContact : Form
    {
        string action;
        PbContext db;
        int id;
        Form frm;

        public AddEditContact(Form form, int id, string action)
        {
            this.id = id;
            db = new PbContext();
            this.frm = form;
            this.action = action;
            InitializeComponent();

        }

        private void AddEditContact_Load(object sender, EventArgs e)
        {
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (action == "add")
            {
                Contact c = new Contact();
                c.FirstName = FirstName.Text;
                c.LastName = LastName.Text;
                c.Address = Address.Text;
                c.HomePhone = HomePhone.Text;
                c.MobilePhone = MobilePhone.Text;
                c.Email = Email.Text;
                db.Contacts.Add(c);

            }
            else
            {
                Contact c;
                c = db.Contacts.Find(id);
                c.FirstName = FirstName.Text;
                c.LastName = LastName.Text;
                c.Address = Address.Text;
                c.HomePhone = HomePhone.Text;
                c.MobilePhone = MobilePhone.Text;
                c.Email = Email.Text;
            }
            db.SaveChanges();
            (frm as MainForm).ReloadGrid();
            Close();
        }
    }
}
