using Library_Models;
using System.Collections.Generic;
using System.Windows;
using Admin_Client.DataProviders;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

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
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Asterisk);
                    UpdateBooks();
                }
                else
                {
                    MessageBox.Show("A választott könyv jelenleg ki van kölcsönözve, legkésőbb az alábbi időpontig: " + _selectedBook.ReturnUntil.ToString() + "!",
                                            "Kölcsönzés sikertelen!",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Kérem válassza ki, hogy melyik könyvet kölcsönzi ki az olvasó!",
                                        "Könyv nem található!",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Exclamation);
            }
        }

        //Return a book
        private void ReturnBookButton_Click(object sender, RoutedEventArgs e)
        {
            var status = BookDataProvider.ReturnBook(_selectedBook, _selectedPerson, false);
            switch (status)
            {
                case null: MessageBox.Show( "Az alábbi könyvet nem szolgáltathatja vissza az olvasó, mivel nem ő bérelte ki!",
                                            "Rossz adatok!",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error);
                    break;
                case false:
                    var result = MessageBox.Show(   "Az alábbi könyvet késedelmi, vagy más büntetési díj megfizetése terheli! " +
                                                    "Amennyiben ezt már a felhasználó megtette, akkor az igen gomb megnyomásával végérvényesítheti a könyv visszaszolgáltatásának folyamatát." +
                                                    "\n\nKifizette a felhasználó a díjat? ",
                                                    "Büntetés kifizetése kötelező !",
                                                    MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Question);
                    if(result == System.Windows.Forms.DialogResult.Yes)
                    {
                        BookDataProvider.ReturnBook(_selectedBook, _selectedPerson, true);
                    }
                    break;
                default:break;
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
