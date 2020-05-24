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
        private readonly Person _borrower;
        private IList<Book> _books;
        private Book _selectedBook;

        public BookDataWindow(Person person)
        {
            InitializeComponent();

            //Initialize private variables
            _borrower = person;

            UpdateBooks();
        }

        //Update the books list with all of the database
        private void AllBookButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateBooks();
        }

        //Update Picked Book data
        private void BooksDataGrid_SelectionChanged(object sender, RoutedEventArgs e)
        {
            //Update the book
            _selectedBook = BooksDataGrid.SelectedItem as Book;

            //Update the book's data
            BorrowDateTextBox.Text = ArchiveDataProvider.GetSpecificBorrowDateTime(_selectedBook.Id, _borrower.Id).ToString();
            ReturnDateTextBox.Text = _selectedBook.ReturnUntil.ToString();
            NumberOfExtendsTextBox.Text = _selectedBook.TimesExtended.ToString() + " alkalommal";
            switch (BookDataProvider.Returnable(_selectedBook, _borrower))
            {
                case ReturnStatus.INVALID:
                    ReturnableLabel.Content = "Hiba! Keressen fel egy könyvtárost!";
                    break;
                case ReturnStatus.RULEBREAK:
                    ReturnableLabel.Content = "A könyvet díjmegfizetés terheli.";
                    break;
                case ReturnStatus.RETURNABLE:
                    ReturnableLabel.Content = "A könyv visszaszolgáltatható.";
                    break;
            }
        }

        //Search by Author
        private void SearchByAuthorButton_Click(object sender, RoutedEventArgs e)
        {
            //Update the list
            _books = LibraryDataProvider.SearchByStringData<Book>(LibraryDataProvider.bookUrl + "/searchByAuthor/", SearchTextBox.Text);
            BooksDataGrid.ItemsSource = _books;
        }

        //Search by Title
        private void SearchByTitleButton_Click(object sender, RoutedEventArgs e)
        {
            //Update the list
            _books = LibraryDataProvider.SearchByStringData<Book>(LibraryDataProvider.bookUrl + "/searchByTitle/", SearchTextBox.Text);
            BooksDataGrid.ItemsSource = _books;
        }

        //Extend the borrowing time by a week
        private void ExtendButton_Click(object sender, RoutedEventArgs e)
        {
            switch (BookDataProvider.ExtendBorrow(_selectedBook))
            {
                case null:
                    //TODO
                    break;
                case false:
                    //TODO
                    break;
                default:
                    //TODO
                    break;
            }
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

            //Set all View to empty//Update the book's data
            BorrowDateTextBox.Text = string.Empty;
            ReturnDateTextBox.Text = string.Empty;
            NumberOfExtendsTextBox.Text = string.Empty;
            ReturnableLabel.Content = string.Empty;
        }
    }
}
