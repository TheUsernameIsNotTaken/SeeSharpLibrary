using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Admin_Client.DataProviders;
using Library_Models;

namespace Admin_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class PersonPickerWindow : Window
    {
        private IList<Person> _people;

        public Person selectedPerson { get; private set; }

        public PersonPickerWindow()
        {
            InitializeComponent();

            UpdatePeople();
        }

        private void BorrowBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedPerson == null)
            {
                MessageBox.Show("Kölcsönzés előtt kérem válassza ki, hogy ki akar kölcsönözni!",
                                        "Kölcsönző nem található!",
                                        MessageBoxButton.OK);
            }
            else
            {
                DialogResult = true;
                Close();
            }
        }

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

        private void PeopleListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedPerson = PeopleListBox.SelectedItem as Person;
        }

        private void UpdatePeople()
        {
            _people = LibraryDataProvider.GetAllData<Person>(LibraryDataProvider.personUrl);
            PeopleListBox.ItemsSource = _people;
            selectedPerson = null;
        }
    }
}
