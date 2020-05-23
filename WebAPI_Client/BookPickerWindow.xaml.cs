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

        //Cancel date picking
        private void BOD_SelectedDatesChanged(object sender, RoutedEventArgs e)
        {
            if(_borrower != null)
            {
                BorrowerDateOfBirthDatePicker.SelectedDate = _borrower.DateOfBirth;
                BorrowerEndDatePicker.SelectedDate = _selectedBook.ReturnUntil;
            }
            if(_selectedPerson != null)
            {
                UserDateOfBirthDatePicker.SelectedDate = _selectedPerson.DateOfBirth;
            }
        }

        //End the current borrower user's borrowing. Like a log-out.
        private void ExitUserButton_Click(object sender, RoutedEventArgs e)
        {
            _selectedPerson = null;
            SetUserVisibility();
        }

        //Update the visibility parameters
        private void SetBorrowerVisibility(bool isBookBorrowed)
        {
            //Set the Grid's visibility
            BorrowerGrid.Visibility = isBookBorrowed ? Visibility.Visible : Visibility.Collapsed;
            //View update
            if (isBookBorrowed)
            {
                BorrowerLastNameTextBox.Text = _borrower.LastName;
                BorrowerFirstNameTextBox.Text = _borrower.FirstName;
                BorrowerDateOfBirthDatePicker.SelectedDate = _borrower.DateOfBirth;
                BorrowerEndDatePicker.SelectedDate = _selectedBook.ReturnUntil;
            }
        }

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
                UserDateOfBirthDatePicker.SelectedDate = _selectedPerson.DateOfBirth;
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
            _books = _searced ? BookDataProvider.SearchBooks(CodeTextBox.Text) : LibraryDataProvider.GetAllData<Book>(LibraryDataProvider.bookUrl);
            BooksDataGrid.ItemsSource = _books;
            _selectedBook = null;
        }
    }
}
