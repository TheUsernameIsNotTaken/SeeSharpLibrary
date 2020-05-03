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
using WebAPI_Client.DataProviders;
using Library_Models;

namespace WebAPI_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IList<Person> _people;

        public MainWindow()
        {
            InitializeComponent();

            UpdatePeople();
        }

        private void AddPersonButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new PersonWindow(null);
            if (window.ShowDialog() ?? false)
            {
                UpdatePeople();
            }
        }

        private void PeopleListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedPerson = PeopleListBox.SelectedItem as Person;

            if (selectedPerson != null)
            {
                var window = new PersonWindow(selectedPerson);
                if (window.ShowDialog() ?? false)
                {
                    UpdatePeople();
                }

                PeopleListBox.UnselectAll();
            }
        }

        private void UpdatePeople()
        {
            _people = PersonDataProvider.GetPeople();
            PeopleListBox.ItemsSource = _people;
        }
    }
}
