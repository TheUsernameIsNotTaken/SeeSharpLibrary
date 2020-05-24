using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Reader_Client.DataProviders;
using Library_Models;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;
using System;

namespace Reader_Client
{
    /// <summary>
    /// Interaction logic for BookPickerWindow.xaml
    /// </summary>
    public partial class BookDataWindow : Window
    {
        //Basic data
        private Person _borrower;
        private IList<Book> _books;
        private Book _selectedBook;
        private Person _selectedPerson;
        //Helping data
        private bool _searced;

        public BookDataWindow(Person person)
        {
            InitializeComponent();

            //Initialize private variables
            _searced = false;
            _borrower = null;
            _selectedBook = null;
            _selectedPerson = person;

            UpdateBooks();
        }

        //Update the books list with all of the database
        private void AllBookButton_Click(object sender, RoutedEventArgs e)
        {
            _searced = false;
            UpdateBooks();
        }

        //Update Picked Book data
        private void BooksDataGrid_SelectionChanged(object sender, RoutedEventArgs e)
        {
            //Update the book
            _selectedBook = BooksDataGrid.SelectedItem as Book;

            //Update the book's data
            //TODO
        }

        //Search by Author
        private void SearchByAuthorButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO
            _searced = !string.IsNullOrEmpty(SearchTextBox.Text);
            UpdateBooks();
        }

        //Search by Title
        private void SearchByTitleButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO
            _searced = !string.IsNullOrEmpty(SearchTextBox.Text);
            UpdateBooks();
        }

        //Search by Code
        //TODO?

        //Extend the borrowing time by a week
        private void ExtendButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        //Update the book list
        private void UpdateBooks()
        {
            //Update the list
            _books = BookDataProvider.SearchBooksByBorrower(_borrower.Id);
            BooksDataGrid.ItemsSource = _books;
            //No chosen item
            BooksDataGrid.UnselectAll();
            _selectedBook = null;
        }
    }
}
