using System.Windows;
using Admin_Client.DataProviders;
using Library_Models;

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
                _person = person;

                FirstNameTextBox.Text = _person.FirstName;
                LastNameTextBox.Text = _person.LastName;
                DateOfBirthDatePicker.SelectedDate = _person.DateOfBirth;

                CreateButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                _person = new Person();

                UpdateButton.Visibility = Visibility.Collapsed;
                DeleteButton.Visibility = Visibility.Collapsed;
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidatePerson())
            {
                _person.FirstName = FirstNameTextBox.Text;
                _person.LastName = LastNameTextBox.Text;
                _person.DateOfBirth = DateOfBirthDatePicker.SelectedDate.Value;

                LibraryDataProvider.CreateData(LibraryDataProvider.personUrl, _person);

                DialogResult = true;
                Close();
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidatePerson())
            {
                _person.FirstName = FirstNameTextBox.Text;
                _person.LastName = LastNameTextBox.Text;
                _person.DateOfBirth = DateOfBirthDatePicker.SelectedDate.Value;

                LibraryDataProvider.UpdateData<Person>(LibraryDataProvider.personUrl, _person, _person.Id);

                DialogResult = true;
                Close();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you really want to delete?", "Question", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                LibraryDataProvider.DeleteData<Person>(LibraryDataProvider.personUrl, _person.Id);

                DialogResult = true;
                Close();
            }
        }

        private bool ValidatePerson()
        {
            if (string.IsNullOrEmpty(FirstNameTextBox.Text))
            {
                MessageBox.Show("First name should not be empty.");
                return false;
            }

            if (string.IsNullOrEmpty(LastNameTextBox.Text))
            {
                MessageBox.Show("Last name should not be empty.");
                return false;
            }

            if (!DateOfBirthDatePicker.SelectedDate.HasValue)
            {
                MessageBox.Show("Please select a date of birth date.");
                return false;
            }

            return true;
        }
    }
}
