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
        private Person _selectedPerson;

        public PersonPickerWindow()
        {
            InitializeComponent();

            UpdatePeople();
        }

        private void BorrowBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedPerson != null)
            {
                //var window = new BookPickerWindow();
                //if (window.ShowDialog() ?? false)
                //{
                //    UpdatePeople();
                //}
                //PeopleListBox.UnselectAll();
            }
            else
            {
                MessageBox.Show("Kölcsönzés előtt kérem válassza ki, hogy ki akar kölcsönözni!",
                                        "Kölcsönző nem található!",
                                        MessageBoxButton.OK);
            }
        }

        private void AddBookButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new BookWindow(null);
            if (window.ShowDialog() ?? false)
            {
                UpdatePeople();
            }
            PeopleListBox.UnselectAll();
        }

        private void AddPersonButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedPerson != null)
            {
                var window = new PersonWindow(_selectedPerson);
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
            _selectedPerson = PeopleListBox.SelectedItem as Person;
        }

        private void UpdatePeople()
        {
            _people = PersonDataProvider.GetPeople();
            PeopleListBox.ItemsSource = _people;
            _selectedPerson = null;
        }
    }
}
