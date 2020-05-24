using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Reader_Client.DataProviders;
using Library_Models;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Reader_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class UserPickerWindow : Window
    {

        private bool _searched;
        private IList<Person> _people;
        private Person _selectedPerson;

        public UserPickerWindow()
        {
            InitializeComponent();

            //Initialize private variables
            _searched = false;

            UpdatePeople();
        }

        private void UpdateAllDataButton_Click(object sender, RoutedEventArgs e)
        {
            _searched = false;
            UpdatePeople();
        }

        //Set the borrowing user
        private void BorrowedBooksButton_Click(object sender, RoutedEventArgs e)
        {
            _selectedPerson = PeopleListBox.SelectedItem as Person;
            if (_selectedPerson == null)
            {
                MessageBox.Show("Kölcsönzési adatok megtekintéséhez válasszon ki egy olvasót!",
                                        "Olvasó nem található!",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
            }
            else
            {
                //Open a dialog window with the correct data. 
                var window = new BookDataWindow(_selectedPerson);
                if (window.ShowDialog() ?? false)
                {
                    UpdatePeople();
                }
            }
        }

        //Search a person
        private void SearchPersonButton_Click(object sender, RoutedEventArgs e)
        {
            _searched = true;
            UpdatePeople();
        }

        //Update the currently selected person
        private void PeopleListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedPerson = PeopleListBox.SelectedItem as Person;
        }

        //Update the person list
        private void UpdatePeople()
        {
            _people = _searched ? LibraryDataProvider.SearchByStringData<Person>(LibraryDataProvider.personUrl + "/search/", SearchPersonTextBox.Text) : LibraryDataProvider.GetAllData<Person>(LibraryDataProvider.personUrl);
            PeopleListBox.ItemsSource = _people;
            _searched = false;
            SearchPersonTextBox.Text = string.Empty;
            _selectedPerson = null;
        }
    }
}
