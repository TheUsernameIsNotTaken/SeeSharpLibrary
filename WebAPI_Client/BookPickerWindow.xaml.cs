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
using Admin_Client.DataProviders;

namespace Admin_Client
{
    /// <summary>
    /// Interaction logic for BookPickerWindow.xaml
    /// </summary>
    public partial class BookPickerWindow : Window
    {
        private bool _searced;
        private IList<Book> _books;
        private Book _selectedBook;
        private Person _borrower;
        private Person _selectedPerson;

        public BookPickerWindow()
        {
            InitializeComponent();

            //Get default data for private variables
            _searced = false;
            _borrower = null;
            _selectedBook = null;
            _selectedPerson = null;

            //Hide the Borrow button
            BorrowBookButton.Visibility = Visibility.Collapsed;

            UpdateBooks();
        }

        public BookPickerWindow(Person person)
        {
            InitializeComponent();

            //Get default data for private variables
            _searced = false;
            _borrower = null;
            _selectedBook = null;
            _selectedPerson = person;

            //Hide the Reader Picker button
            PickReaderButton.Visibility = Visibility.Collapsed;

            UpdateBooks();
        }

        private void AllBookButton_Click(object sender, RoutedEventArgs e)
        {
            _searced = false;
            UpdateBooks();
        }

        //Search by part of a Code
        private void SearchBookButton_Click(object sender, RoutedEventArgs e)
        {
            _searced = !string.IsNullOrEmpty(CodeTextBox.Text); 
            UpdateBooks();
        }

        //Borrow a book to a Person
        private void BorrowBookButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        // Add/Update/Delete books
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

        //Update Picked Book and Borrower data
        private void BooksDataGrid_SelectionChanged(object sender, RoutedEventArgs e)
        {
            //Update the book
            _selectedBook = BooksDataGrid.SelectedItem as Book;

            //Check if we have a borrower
            bool borrowed = _selectedBook != null && !_selectedBook.IsAvailable;
            
            //Borrower Update
            _borrower = borrowed ? LibraryDataProvider.GetSingleData<Person>(LibraryDataProvider.personUrl, _selectedBook.BorrowerId.Value) : null;
            //View update
            LastNameTextBox.Text = borrowed ? _borrower.LastName : string.Empty;
            FirstNameTextBox.Text = borrowed ? _borrower.FirstName : string.Empty;
            if (borrowed)   DateOfBirthDatePicker.SelectedDate = _borrower.DateOfBirth;
            else            DateOfBirthDatePicker.SelectedDate = null;
        }

        //Cancel date picking
        private void BOD_SelectedDatesChanged(object sender, RoutedEventArgs e)
        {
            if(_borrower == null)
            {
                DateOfBirthDatePicker.SelectedDate = null;
            }
            else
            {
                DateOfBirthDatePicker.SelectedDate = _borrower.DateOfBirth;
            }
        }

        private void UpdateBooks()
        {
            _books = _searced ? BookDataProvider.SearchBooks(CodeTextBox.Text) : LibraryDataProvider.GetAllData<Book>(LibraryDataProvider.bookUrl);
            BooksDataGrid.ItemsSource = _books;
            _selectedBook = null;
        }
    }
}
