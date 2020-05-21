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

                AuthorTextBox.Text = _book.Author;
                TitleTextBox.Text = _book.Title;
                CodeTextBox.Text = _book.Code;
                YearPicker.SelectedDate = new DateTime(_book.Year, 1, 1);
                CreateButton.Visibility = Visibility.Collapsed;
            }
            else
            {
                _book = new Book();

                UpdateButton.Visibility = Visibility.Collapsed;
                DeleteButton.Visibility = Visibility.Collapsed;
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateBook())
            {
                _book.Author = AuthorTextBox.Text;
                _book.Title = TitleTextBox.Text;
                _book.Code = CodeTextBox.Text;
                _book.Year = Convert.ToUInt16(YearPicker.SelectedDate.Value.Year);
                _book.IsAvailable = true;

                BookDataProvider.CreateBook(_book);

                DialogResult = true;
                Close();
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateBook())
            {
                _book.Author = AuthorTextBox.Text;
                _book.Title = TitleTextBox.Text;
                _book.Code = CodeTextBox.Text;
                _book.Year = Convert.ToUInt16(YearPicker.SelectedDate.Value.Year);

                BookDataProvider.UpdateBook(_book);

                DialogResult = true;
                Close();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you really want to delete?", "Question", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                BookDataProvider.DeleteBook(_book.Id);

                DialogResult = true;
                Close();
            }
        }

        private bool ValidateBook()
        {
            if (string.IsNullOrEmpty(AuthorTextBox.Text))
            {
                MessageBox.Show("Author should not be empty.");
                return false;
            }

            if (string.IsNullOrEmpty(TitleTextBox.Text))
            {
                MessageBox.Show("Title should not be empty.");
                return false;
            }

            if (string.IsNullOrEmpty(CodeTextBox.Text))
            {
                MessageBox.Show("Code should not be empty.");
                return false;
            }

            if (!YearPicker.SelectedDate.HasValue)
            {
                MessageBox.Show("Please select a date of release.");
                return false;
            }

            return true;
        }
    }
}
