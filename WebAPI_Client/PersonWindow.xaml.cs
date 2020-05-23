using System.Windows;
using Library_Models;
using Admin_Client.DataProviders;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Admin_Client
{
    /// <summary>
    /// Interaction logic for PersonWindow.xaml
    /// </summary>
    public partial class PersonWindow : Window
    {
        private readonly Person _person;

        public PersonWindow(Person person)
        {
            InitializeComponent();

            if (person != null)
            {
                //Get the person.
                _person = person;
                //Load the person's data into the viewer.
                FirstNameTextBox.Text = _person.FirstName;
                LastNameTextBox.Text = _person.LastName;
                DateOfBirthDatePicker.SelectedDate = _person.DateOfBirth;
                //Set the buttons' visibility
                CreateButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                //Blank person to save.
                _person = new Person();
                //Set the buttons' visibility
                UpdateButton.Visibility = Visibility.Collapsed;
                DeleteButton.Visibility = Visibility.Collapsed;
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidatePerson())
            {
                //Create the person.
                _person.FirstName = FirstNameTextBox.Text;
                _person.LastName = LastNameTextBox.Text;
                _person.DateOfBirth = DateOfBirthDatePicker.SelectedDate.Value;

                //Send the person to save it into the database.
                LibraryDataProvider.CreateData(LibraryDataProvider.personUrl, _person);

                //Close the dialog window.
                DialogResult = true;
                Close();
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidatePerson())
            {
                //Update the person.
                _person.FirstName = FirstNameTextBox.Text;
                _person.LastName = LastNameTextBox.Text;
                _person.DateOfBirth = DateOfBirthDatePicker.SelectedDate.Value;

                //Update the person in the database.
                LibraryDataProvider.UpdateData<Person>(LibraryDataProvider.personUrl, _person, _person.Id);

                //Update the archivated borrowing data if needed
                //TODO

                //Close the dialog window.
                DialogResult = true;
                Close();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            //Ask for permission.
            var result = MessageBox.Show("Biztos, hogy törölni akarja a felhasználót?",
                                            "Megerősítés",
                                            MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Question);

            //Update the archivated borrowing data if needed.
            //TODO

            //Delete after validation. 
            if (result == System.Windows.Forms.DialogResult.Yes) 
            { 
                LibraryDataProvider.DeleteData<Person>(LibraryDataProvider.personUrl, _person.Id);

                DialogResult = true;
                Close();
            }
        }

        private bool ValidatePerson()
        {
            //First name should not be empty.
            if (string.IsNullOrEmpty(FirstNameTextBox.Text))
            {
                MessageBox.Show("A keresztnév megadása kötelező!");
                return false;
            }
            //Last name should not be empty.
            if (string.IsNullOrEmpty(LastNameTextBox.Text))
            {
                MessageBox.Show("A vezetéknév megadása kötelező!");
                return false;
            }
            //Please select a date of birth date.
            if (!DateOfBirthDatePicker.SelectedDate.HasValue)
            {
                MessageBox.Show("A születési dátum megadása kötelező!");
                return false;
            }

            return true;
        }
    }
}
