using System;
using System.Windows.Forms;

namespace PhoneBookSS.PL
{
    public partial class ShowContact : Form
    {
        PbContext db;
        int id;
        public ShowContact(int id)
        {
            this.id = id;
            db = new PbContext();
            InitializeComponent();
        }

        private void ShowContact_Load(object sender, EventArgs e)
        {
            var contact = db.Contacts.Find(id);
            FirstName.Text = contact.FirstName;
            LastName.Text = contact.LastName;
            Address.Text = contact.Address;
            HomePhone.Text = contact.HomePhone;
            MobilePhone.Text = contact.MobilePhone;
            Email.Text = contact.Email;
            Text = Text + contact.FirstName+" " + contact.LastName;
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(FirstName.Text+" "+LastName + ", " +Address.Text
                + ", " +HomePhone.Text + ", " +MobilePhone.Text + ", " +Email.Text);

        }

    }
}
