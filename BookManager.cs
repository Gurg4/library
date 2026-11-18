namespace LibraryManagementSystem
{
    public class BookManager
    {
        // List for storing books
        private List<Book> books = new List<Book>();

        // Add new book
        public void AddBook(Book book)
        {
            // Check title
            if (string.IsNullOrWhiteSpace(book.Title))
            {
                Console.WriteLine("Title cannot be empty");
                return;
            }

            // Check author
            if (string.IsNullOrWhiteSpace(book.Author))
            {
                Console.WriteLine("Author cannot be empty");
                return;
            }

            // Check publication year
            if (book.PublicationYear <= 0 ||
                book.PublicationYear > DateTime.Now.Year)
            {
                Console.WriteLine("Invalid publication year");
                return;
            }

            // Check EBook file size
            if (book is EBook ebook && ebook.FileSize <= 0)
            {
                Console.WriteLine("Invalid file size");
                return;
            }

            // Check duplicate books
            foreach (Book existingBook in books)
            {
                if (existingBook.Title.Equals(
                    book.Title,
                    StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("This book already exists");
                    return;
                }
            }

            // Add book to list
            books.Add(book);

            // Save changes
            SaveBooks();

            Console.WriteLine("Book added successfully");
        }

        // Show all books
        public void ShowAllBooks()
        {
            if (books.Count == 0)
            {
                Console.WriteLine("There are no books");
                return;
            }

            Console.WriteLine("\n==== ALL BOOKS ====");

            foreach (Book book in books)
            {
                // Polymorphism works here
                book.DisplayInfo();

                Console.WriteLine("----------------");
            }

            Console.WriteLine($"Total books: {books.Count}");
        }

        // Search book by title
        public void SearchBook(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Search title cannot be empty");
                return;
            }

            bool found = false;

            foreach (Book book in books)
            {
                // Search by full title or part of title
                if (book.Title.Contains(
                    title,
                    StringComparison.OrdinalIgnoreCase))
                {
                    book.DisplayInfo();
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Book not found");
            }
        }

        // Delete book by title
        public void DeleteBook(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Title cannot be empty");
                return;
            }

            Book bookToDelete = null;

            foreach (Book book in books)
            {
                if (book.Title.Equals(
                    title,
                    StringComparison.OrdinalIgnoreCase))
                {
                    bookToDelete = book;
                    break;
                }
            }

            if (bookToDelete != null)
            {
                books.Remove(bookToDelete);

                // Save changes
                SaveBooks();

                Console.WriteLine("Book deleted successfully!");
            }
            else
            {
                Console.WriteLine("Book not found");
            }
        }

        // Edit book information
        public void EditBook(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Title cannot be empty");
                return;
            }

            Book bookToEdit = null;

            // Find book
            foreach (Book book in books)
            {
                if (book.Title.Equals(
                    title,
                    StringComparison.OrdinalIgnoreCase))
                {
                    bookToEdit = book;
                    break;
                }
            }

            if (bookToEdit == null)
            {
                Console.WriteLine("Book not found");
                return;
            }

            // Enter new information
            Console.Write("Enter new title: ");
            string newTitle = Console.ReadLine() ?? "";

            Console.Write("Enter new author: ");
            string newAuthor = Console.ReadLine() ?? "";

            Console.Write("Enter new publication year: ");
            int newYear;

            if (!int.TryParse(Console.ReadLine(), out newYear))
            {
                Console.WriteLine("Invalid year");
                return;
            }

            // Validate new information
            if (string.IsNullOrWhiteSpace(newTitle))
            {
                Console.WriteLine("Title cannot be empty");
                return;
            }

            if (string.IsNullOrWhiteSpace(newAuthor))
            {
                Console.WriteLine("Author cannot be empty");
                return;
            }

            if (newYear <= 0 || newYear > DateTime.Now.Year)
            {
                Console.WriteLine("Invalid publication year");
                return;
            }

            // Check duplicate title
            foreach (Book book in books)
            {
                if (book != bookToEdit &&
                    book.Title.Equals(
                        newTitle,
                        StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("This book already exists");
                    return;
                }
            }

            // Update EBook information
            if (bookToEdit is EBook ebook)
            {
                Console.Write("Enter new file size (MB): ");
                double newFileSize;

                if (!double.TryParse(
                    Console.ReadLine(),
                    out newFileSize) || newFileSize <= 0)
                {
                    Console.WriteLine("Invalid file size");
                    return;
                }

                ebook.UpdateFileSize(newFileSize);
            }

            // Update book information
            bookToEdit.UpdateBook(newTitle, newAuthor, newYear);

            // Save changes
            SaveBooks();

            Console.WriteLine("Book updated successfully");
        }

        // Save books to text file
        public void SaveBooks()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("books.txt"))
                {
                    foreach (Book book in books)
                    {
                        if (book is EBook ebook)
                        {
                            // Save EBook data
                            writer.WriteLine(
                                $"E|{ebook.Title}|{ebook.Author}|{ebook.PublicationYear}|{ebook.FileSize}"
                            );
                        }
                        else
                        {
                            // Save normal book data
                            writer.WriteLine(
                                $"B|{book.Title}|{book.Author}|{book.PublicationYear}"
                            );
                        }
                    }
                }

                Console.WriteLine("Books saved successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving books: " + ex.Message);
            }
        }

        // Load books from text file
        public void LoadBooks()
        {
            try
            {
                if (File.Exists("books.txt"))
                {
                    string[] lines = File.ReadAllLines("books.txt");

                    foreach (string line in lines)
                    {
                        string[] data = line.Split('|');

                        // Load normal book
                        if (data.Length == 4 && data[0] == "B")
                        {
                            if (int.TryParse(data[3], out int year))
                            {
                                Book book = new Book(
                                    data[1],
                                    data[2],
                                    year
                                );

                                books.Add(book);
                            }
                        }

                        // Load EBook
                        else if (data.Length == 5 && data[0] == "E")
                        {
                            if (int.TryParse(data[3], out int year) &&
                                double.TryParse(data[4], out double fileSize))
                            {
                                EBook ebook = new EBook(
                                    data[1],
                                    data[2],
                                    year,
                                    fileSize
                                );

                                books.Add(ebook);
                            }
                        }
                    }

                    Console.WriteLine("Books loaded successfully!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading books: " + ex.Message);
            }
        }

        // Show book titles using array
        public void ShowBookTitlesArray()
        {
            if (books.Count == 0)
            {
                Console.WriteLine("There are no books");
                return;
            }

            string[] bookTitles = new string[books.Count];

            // Copy titles to array
            for (int i = 0; i < books.Count; i++)
            {
                bookTitles[i] = books[i].Title;
            }

            Console.WriteLine("\n==== BOOK TITLES ====");

            foreach (string title in bookTitles)
            {
                Console.WriteLine(title);
            }
        }

        // Delete books text file
        public void DeleteFile()
        {
            try
            {
                if (File.Exists("books.txt"))
                {
                    File.Delete("books.txt");

                    // Clear books list
                    books.Clear();

                    Console.WriteLine("File deleted successfully!");
                }
                else
                {
                    books.Clear();

                    Console.WriteLine("File does not exist");
                    Console.WriteLine("Books list cleared");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting file: " + ex.Message);
            }
        }
    }
}
