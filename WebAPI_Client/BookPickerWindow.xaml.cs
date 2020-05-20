using Library_Models;
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
using System.Windows.Shapes;
using WebAPI_Client.DataProviders;

namespace WebAPI_Client
{
    /// <summary>
    /// Interaction logic for BookPickerWindow.xaml
    /// </summary>
    public partial class BookPickerWindow : Window
    {

        private IList<Book> _books;
        private Book _selectedBook;
        private Person _selectedPerson = null;

        public BookPickerWindow(Person person)
        {
            InitializeComponent();

            _selectedPerson = person;
            CodeTextBox.Text = _selectedPerson.ToString();

            UpdateBooks();
        }

        private void SearchBookButton_Click(object sender, RoutedEventArgs e)
        {
            //Search by Code
            //TODO
        }

        private void BorrowBookButton_Click(object sender, RoutedEventArgs e)
        {
            //Borrow a book
            PersonDataProvider.BorrowBook(_selectedPerson, _selectedBook);
        }

        private void AddOrUpdateBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedBook != null)
            {
                var window = new BookWindow(_selectedBook);
                if (window.ShowDialog() ?? false)
                {
                    UpdateBooks();
                }
            }
            else
            {
                var window = new BookWindow(null);
                if (window.ShowDialog() ?? false)
                {
                    UpdateBooks();
                }
            }
            BooksDataGrid.UnselectAll();
        }

        private void BooksDataGrid_SelectionChanged(object sender, RoutedEventArgs e)
        {
            _selectedBook = BooksDataGrid.SelectedItem as Book;
        }

        private void UpdateBooks()
        {
            _books = BookDataProvider.GetBooks();
            BooksDataGrid.ItemsSource = _books;
            _selectedBook = null;
        }
    }
}
