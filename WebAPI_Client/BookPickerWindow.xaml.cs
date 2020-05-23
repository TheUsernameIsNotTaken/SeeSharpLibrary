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

            //Initialize private variables
            _searced = false;
            _borrower = null;
            _selectedBook = null;
            _selectedPerson = null;

            //Set the correct visibility settings
            SetBorrowerVisibility(false);
            SetUserVisibility();

            UpdateBooks();
        }

        //Update the books list with all of the database
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

        //Pick a person who will borrow books
        private void PickReaderButton_Click(object sender, RoutedEventArgs e)
        {
            //Get the borrower user's data, if chosen
            var window = new PersonPickerWindow();
            if (window.ShowDialog() ?? false)
            {
                _selectedPerson = window.selectedPerson;
            }
            else
            {
                _selectedPerson = null;
            }
            SetUserVisibility();
        }

        //Borrow a book to the picked person
        private void BorrowBookButton_Click(object sender, RoutedEventArgs e)
        {
            if(_selectedBook != null && _selectedPerson != null)
            {
                if(_selectedBook.IsAvailable)
                {
                    BookDataProvider.BorrowBook(_selectedBook, _selectedPerson);
                    var updatedBook = LibraryDataProvider.GetSingleData<Book>(LibraryDataProvider.bookUrl, _selectedBook.Id);
                    MessageBox.Show("Az olvasó sikeresen kikölcsönözte a könyvet az alábbi időpontig: " + updatedBook.ReturnUntil.ToString() + "!",
                                            "Sikeres kölcsönzés!",
                                            MessageBoxButton.OK);
                    UpdateBooks();
                }
                else
                {
                    MessageBox.Show("A választott könyv jelenleg ki van kölcsönözve, legkésőbb az alábbi időpontig: " + _selectedBook.ReturnUntil.ToString() + "!",
                                            "Sikeres kölcsönzés!",
                                            MessageBoxButton.OK);
                }
            }
            else
            {
                MessageBox.Show("Kérem válassza ki, hogy melyik könyvet kölcsönzi ki az olvasó!",
                                        "Könyv nem található!",
                                        MessageBoxButton.OK);
            }
        }

        //Return a book
        private void ReturnBookButton_Click(object sender, RoutedEventArgs e)
        {

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
            SetBorrowerVisibility(borrowed);
        }

        //Show only the books borrowed by user
        private void ShowBorrowedButton_Click(object sender, RoutedEventArgs e)
        {
            BookDataProvider.SearchBooksByBorrower(_selectedPerson.Id);
        }

        //End the currently borrowing user's procces. Like a log-out.
        private void ExitUserButton_Click(object sender, RoutedEventArgs e)
        {
            _selectedPerson = null;
            SetUserVisibility();
        }

        //Update the borrower's visibility parameters
        private void SetBorrowerVisibility(bool isBookBorrowed)
        {
            //Set the Grid's visibility
            BorrowerGrid.Visibility = isBookBorrowed ? Visibility.Visible : Visibility.Collapsed;
            //View update
            if (isBookBorrowed)
            {
                BorrowerLastNameTextBox.Text = _borrower.LastName;
                BorrowerFirstNameTextBox.Text = _borrower.FirstName;
                BorrowerDateOfBirthTextBox.Text = _borrower.DateOfBirth.Date.ToString().Split(' ')[0];
                BorrowerEndTextBox.Text = _selectedBook.ReturnUntil.ToString();
            }
        }

        //Update the borrowing procces's interface visibility
        private void SetUserVisibility()
        {
            if (_selectedPerson != null)
            {
                //View book borrowing interface 
                PickedUserGrid.Visibility = Visibility.Visible;
                BorrowBookButton.Visibility = Visibility.Visible;
                //Hide borrow starting button
                PickReaderButton.Visibility = Visibility.Collapsed;
                //View the user's data
                UserLastNameTextBox.Text = _selectedPerson.LastName;
                UserFirstNameTextBox.Text = _selectedPerson.FirstName;
                UserDateOfBirthTextBox.Text = _selectedPerson.DateOfBirth.Date.ToString().Split(' ')[0];
            }
            else
            {
                //View borrow starting button
                PickReaderButton.Visibility = Visibility.Visible;
                //Hide book borrowing interface
                PickedUserGrid.Visibility = Visibility.Collapsed;
                BorrowBookButton.Visibility = Visibility.Collapsed;
            }
        }

        //Update the book list
        private void UpdateBooks()
        {
            _books = _searced ? LibraryDataProvider.SearchByStringData<Book>(LibraryDataProvider.bookUrl + "/search/", CodeTextBox.Text) : LibraryDataProvider.GetAllData<Book>(LibraryDataProvider.bookUrl);
            BooksDataGrid.ItemsSource = _books;
            _selectedBook = null;
        }
    }
}
