using ContactsSolution.buisness;
using System;
using System.Data;
using System.Windows.Forms;

namespace ConstactsSolutionDesktopApp
{
    public partial class EmailTextBox : Form
    {

        private enum enMode
        {
            INSERT, UPDATE

        }

        private enMode Mode { get; set; }

        private int _ContactID { get; set; }

        private ContactInfo _contactInfo;


        public EmailTextBox(int ContactID)
        {
            InitializeComponent();
            _ContactID = ContactID;
            if (_ContactID == -1)
            {
                Mode = enMode.INSERT;
            }
            else
            {
                Mode = enMode.UPDATE;
            }

            _initForm();
        }

        private void _intFormInAddNewMode()
        {
            this.removeImageLablel.Visible = false;
            this.AddOrEditContactIdLabel.Text = "Add New Contact";
            _contactInfo = new ContactInfo();
            this.removeImageLablel.Visible = !string.IsNullOrEmpty(_contactInfo.imagePath);

            _fillCountryComboBox();
        }

        private void _initFormInUpdateMode(ContactInfo contactInfo)
        {

            this._contactInfo = contactInfo;
            this.AddOrEditContactIdLabel.Text = "Updating Contact ID = " + contactInfo.id.ToString(); 
            this.ContactIdValueLabel.Text = contactInfo.id.ToString();
            this.FirstNameTextBox.Text = contactInfo.firstName;
            this.LastNameTextBox.Text = contactInfo.lastName;
            this.EmailBox.Text = contactInfo.email;
            this.PhoneTextBox.Text = contactInfo.phoneNumber;
            this.AddressTextBox.Text = contactInfo.address;
            this.DateOfBirthDateTimePicker.Text = contactInfo.dateOfBirth.ToString();
            this.CountryComboBox.Text = clsCountryInfo.FindById(contactInfo.countryId).countryName;
            this.imageControl.ImageLocation = contactInfo.imagePath;
            this.removeImageLablel.Visible = !string.IsNullOrEmpty(contactInfo.imagePath);
           


        }

        private void _initForm()
        {
            if (Mode == enMode.INSERT)

            {
                _intFormInAddNewMode();
                

            }
            else
            {
                 _contactInfo = ContactInfo.Find(_ContactID);
                _fillCountryComboBox();
                _initFormInUpdateMode(_contactInfo);
            }

            


        }

        private void _fillCountryComboBox()
        {
            DataTable contacts = clsCountryInfo.getAllCountries();
            
            CountryComboBox.DataSource = contacts;
            CountryComboBox.DisplayMember = "CountryName";
            CountryComboBox.ValueMember = "CountryID";

        }

        private bool _validateInputs()
        {
            if (FirstNameTextBox.Text.Trim() == "" ||
                LastNameTextBox.Text.Trim() == "" ||
                EmailBox.Text.Trim() == "" ||
                PhoneTextBox.Text.Trim() == "" ||
                AddressTextBox.Text.Trim() == ""
                )
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void AddNewAndEditContactFOrm_Load(object sender, EventArgs e)
        {


        }

        private void setImageLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            

            OpenFileDialog imageDialog = new OpenFileDialog();
            imageDialog.Title = "Select an Image";
            imageDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            imageDialog.Multiselect = false;
            imageDialog.CheckFileExists = true;
            imageDialog.CheckPathExists = true;

            if (imageDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedImagePath = imageDialog.FileName;
                imageControl.ImageLocation = selectedImagePath;
            }

         
           _refreshRemoveImageLinkLabelState();
          

        }

        private void _refreshRemoveImageLinkLabelState()
        {
            this.removeImageLablel.Visible = !string.IsNullOrEmpty(this.imageControl.ImageLocation);
        }

        private void imageControl_Click(object sender, EventArgs e)
        {

        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (_validateInputs())
            {
                

               
                _contactInfo.firstName = FirstNameTextBox.Text.Trim();
                _contactInfo.lastName = LastNameTextBox.Text.Trim();
                _contactInfo.email = EmailBox.Text.Trim();
                _contactInfo.phoneNumber = PhoneTextBox.Text.Trim();
                _contactInfo.address = AddressTextBox.Text.Trim();
                _contactInfo.dateOfBirth = DateOfBirthDateTimePicker.Value;
                _contactInfo.countryId =  clsCountryInfo.FindByName(CountryComboBox.Text).countryID;

              
                if (imageControl.ImageLocation!=null)
                {
                    _contactInfo.imagePath = imageControl.ImageLocation;
                }
                else
                {
                    _contactInfo.imagePath = "";

                }

                if (_contactInfo.save())
                {
                    MessageBox.Show("Contact saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  
                    Mode = enMode.UPDATE;
                    _initFormInUpdateMode(_contactInfo);

                    
                 
                }
                else
                {
                  MessageBox.Show("An error occurred while saving the contact. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }
            }
        }

        private void ColoseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void removeImageLablel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.imageControl.ImageLocation = "";
            this.removeImageLablel.Visible = false;
        }

       
    }
}
