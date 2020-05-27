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

            //Allow editing
            SetFocusable(true);
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidatePerson())
            {
                //Cannot allow editing while saving
                SetFocusable(false);

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
                //Cannot allow editing while saving
                SetFocusable(false);

                //Update the person.
                _person.FirstName = FirstNameTextBox.Text;
                _person.LastName = LastNameTextBox.Text;
                _person.DateOfBirth = DateOfBirthDatePicker.SelectedDate.Value;

                //Update the person in the database.
                LibraryDataProvider.UpdateData<Person>(LibraryDataProvider.personUrl, _person, _person.Id);

                //Update the archivated borrowing data if needed
                bool allowed = true;
                //Only allow if set
                if (allowed)
                {
                    //Get all occurance
                    var occurances = ArchiveDataProvider.GetManyBySingleId(false, _person.Id);
                    foreach (var occurance in occurances)
                    {
                        //Update
                        occurance.FirstName = FirstNameTextBox.Text;
                        occurance.LastName = LastNameTextBox.Text;
                        occurance.DateOfBirth = DateOfBirthDatePicker.SelectedDate.Value;
                        //Save
                        LibraryDataProvider.UpdateData<ArchiveData>(LibraryDataProvider.archiveUrl, occurance, occurance.Id);
                    }
                }

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

            //Delete after validation. 
            if (result == System.Windows.Forms.DialogResult.Yes) 
            {
                //Cannot allow editing while deleting
                SetFocusable(false);

                //Update the archivated borrowing data if needed
                bool allowed = true;
                //Only allow if set
                if (allowed)
                {
                    //Get all occurance
                    var occurances = ArchiveDataProvider.GetManyBySingleId(false, _person.Id);
                    foreach (var occurance in occurances)
                    {
                        //Update
                        occurance.BorrowerId = null;
                        //Save
                        LibraryDataProvider.UpdateData<ArchiveData>(LibraryDataProvider.archiveUrl, occurance, occurance.Id);
                    }
                }

                //Delete from the database
                LibraryDataProvider.DeleteData<Person>(LibraryDataProvider.personUrl, _person.Id);

                //Close the bialog window
                DialogResult = true;
                Close();
            }
        }

        private bool ValidatePerson()
        {
            //First name should not be empty.
            if (string.IsNullOrEmpty(FirstNameTextBox.Text))
            {
                MessageBox.Show("A keresztnév megadása kötelező!",
                                "Hiányzó adatok!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return false;
            }
            //Last name should not be empty.
            if (string.IsNullOrEmpty(LastNameTextBox.Text))
            {
                MessageBox.Show("A vezetéknév megadása kötelező!",
                                "Hiányzó adatok!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return false;
            }
            //Please select a date of birth date.
            if (DateOfBirthDatePicker.SelectedDate.HasValue is false)
            {
                MessageBox.Show("A születési dátum megadása kötelező!",
                                "Hiányzó adatok!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return false;
            }
            //Validate the values
            if( Person.IsValidName(FirstNameTextBox.Text, LastNameTextBox.Text) is false || Book.IsDateTextValid(DateOfBirthDatePicker.Text, false) == null)
            {
                MessageBox.Show("Nem megfelelően töltötte ki a mezőket!",
                                "Rossz adatok!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void SetFocusable(bool isAllowed)
        {
            FirstNameTextBox.Focusable = isAllowed;
            LastNameTextBox.Focusable = isAllowed;
            DateOfBirthDatePicker.Focusable = isAllowed;
        }
    }
}
