using NetFundamentals.ClassLibrary;

namespace NetFundamentals.WinFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            salutationTextBox.Text = "Please, enter your name";
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nameTextBox.Text))
            {
                MessageBox.Show("Name can't be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            salutationTextBox.Text = string.Empty;

            var salutation = $"Hello, {nameTextBox.Text}";
            if (dateCheckBox.Checked)
            {
                salutation = salutation.AddTimeStamp();
            }

            salutationTextBox.Text = salutation;
        }
    }
}