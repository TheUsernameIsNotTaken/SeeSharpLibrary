using Library_Models;
using System;
using System.Windows;
using Admin_Client.DataProviders;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Admin_Client
{
    /// <summary>
    /// Interaction logic for BookWindow.xaml
    /// </summary>
    public partial class BookWindow : Window
    {

        private Book _book;

        public BookWindow(Book book)
        {
            InitializeComponent();

            if (book != null)
            {
                _book = book;
                //Load the book's data into the viewer.
                AuthorTextBox.Text = _book.Author;
                TitleTextBox.Text = _book.Title;
                CodeTextBox.Text = _book.Code;
                YearPicker.SelectedDate = new DateTime(_book.Year, 1, 1);
                //Set the buttons' visibility
                CreateButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                //Blank book to save.
                _book = new Book();
                //Set the buttons' visibility
                UpdateButton.Visibility = Visibility.Collapsed;
                DeleteButton.Visibility = Visibility.Collapsed;
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateBook())
            {
                //Create the book.
                _book.Author = AuthorTextBox.Text;
                _book.Title = TitleTextBox.Text;
                _book.Code = CodeTextBox.Text;
                _book.Year = Convert.ToUInt16(YearPicker.SelectedDate.Value.Year);
                _book.IsAvailable = true;

                //Send the book to save it into the database.
                LibraryDataProvider.CreateData<Book>(LibraryDataProvider.bookUrl, _book);

                //Close the dialog window.
                DialogResult = true;
                Close();
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateBook())
            {
                //Update the book
                _book.Author = AuthorTextBox.Text;
                _book.Title = TitleTextBox.Text;
                _book.Code = CodeTextBox.Text;
                _book.Year = Convert.ToUInt16(YearPicker.SelectedDate.Value.Year);

                //Send the person to save it into the database.
                LibraryDataProvider.UpdateData<Book>(LibraryDataProvider.bookUrl, _book, _book.Id);

                //Update the archivated borrowing data if needed
                //TODO

                //Close the dialog window.
                DialogResult = true;
                Close();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            //Ask for permission.
            var result = MessageBox.Show(   "Biztos, hogy törölni akarja a felhasználót?",
                                            "Megerősítés",
                                            MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Question);

            //Update the archivated borrowing data if needed.
            //TODO

            //Delete after validation. 
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                LibraryDataProvider.DeleteData<Book>(LibraryDataProvider.bookUrl, _book.Id);
                //Close the dialog window
                DialogResult = true;
                Close();
            }
        }

        private bool ValidateBook()
        {
            //Author should not be empty.
            if (string.IsNullOrEmpty(AuthorTextBox.Text))
            {
                MessageBox.Show("Az író megadása kötelező!");
                return false;
            }
            //Title should not be empty.
            if (string.IsNullOrEmpty(TitleTextBox.Text))
            {
                MessageBox.Show("A cím megadása kötelező!");
                return false;
            }
            //Code should not be empty.
            if (string.IsNullOrEmpty(CodeTextBox.Text))
            {
                MessageBox.Show("A könyvek kötelező megadni az ISBN vagy valamilyen más könyvtári azonosítóját!");
                return false;
            }
            //Year should not be empty
            if (!YearPicker.SelectedDate.HasValue)
            {
                var result = MessageBox.Show("A kiadási év megadása nem kötelező, de ajánlott.\nBiztos hogy kiadási év megadása nélkül akarja elmenteni a könyvet?",
                                            "Megerősítés",
                                            MessageBoxButtons.YesNo,
                                            MessageBoxIcon.Question);
                if(result == System.Windows.Forms.DialogResult.No)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
