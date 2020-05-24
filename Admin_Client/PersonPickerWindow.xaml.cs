using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Admin_Client.DataProviders;
using Library_Models;using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Admin_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class PersonPickerWindow : Window
    {

        private bool _searched;
        private IList<Person> _people;

        public Person selectedPerson { get; private set; }

        public PersonPickerWindow()
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
        private void BorrowBookButton_Click(object sender, RoutedEventArgs e)
        {

            

            if (selectedPerson == null)
            {
                MessageBox.Show("Kölcsönzés előtt kérem válassza ki, hogy ki akar kölcsönözni!",
                                        "Kölcsönző nem található!",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult = true;
                Close();
            }
        }

        //Add/Update/Delete person
        private void AddPersonButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedPerson != null)
            {
                var window = new PersonWindow(selectedPerson);
                if (window.ShowDialog() ?? false)
                {
                    UpdatePeople();
                }
            }
            else
            {
                var window = new PersonWindow(null);
                if (window.ShowDialog() ?? false)
                {
                    UpdatePeople();
                }
            }
            PeopleListBox.UnselectAll();
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
            selectedPerson = PeopleListBox.SelectedItem as Person;
        }

        //Update the person list
        private void UpdatePeople()
        {
            _people = _searched ? LibraryDataProvider.SearchByStringData<Person>(LibraryDataProvider.personUrl + "/search/", SearchPersonTextBox.Text) : LibraryDataProvider.GetAllData<Person>(LibraryDataProvider.personUrl);
            PeopleListBox.ItemsSource = _people;
            _searched = false;
            SearchPersonTextBox.Text = string.Empty;
            selectedPerson = null;
        }
    }
}
