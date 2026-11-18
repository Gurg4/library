namespace LibraryManagementSystem
{

    public class BookManager
    {
        // list
        private List<Book> books = new List<Book>();

        // add new book

        public void AddBook(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Title))
            {
                Console.WriteLine("Title cannot be empty");
                return;
            }

            if (string.IsNullOrWhiteSpace(book.Author))
            {
                Console.WriteLine("Author cannot be empty");
                return;
            }

            if (book.PublicationYear <= 0 || book.PublicationYear > DateTime.Now.Year)
            {
                Console.WriteLine("Invalid publication year");
                return;
            }

            books.Add(book);
            Console.WriteLine("Book added successfully");
        }


        // show all bbook
        public void ShowAllBooks()
        {
            if (books.Count == 0)
            {
                Console.WriteLine("araa wignebi");
                return;
            }

            foreach (Book book in books)
            {
                book.DisplayInfo();
                Console.WriteLine("----------------");
            }
        }

        // mozebbna
        public void SearchBook(string title)
        {
            bool found = false;

            foreach (Book book in books)
            {
                if (book.Title.ToLower() == title.ToLower())
                {
                    book.DisplayInfo();
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Ver ipova");
            }
        }


        // წიგნის წაშლა სათაურის მიხედვით
        public void DeleteBook(string title)
        {
            Book bookToDelete = null;

            foreach (Book book in books)
            {
                if (book.Title.ToLower() == title.ToLower())
                {
                    bookToDelete = book;
                    break;
                }
            }

            if (bookToDelete != null)
            {
                books.Remove(bookToDelete);
                Console.WriteLine("Book deleted successfully!");
            }
            else
            {
                Console.WriteLine("Book not found.");
            }
        }

        // redaqtireba
        public void EditBook(string title)
        {
            Book bookToEdit = null;

            foreach (Book book in books)
            {
                if (book.Title.ToLower() == title.ToLower())
                {
                    bookToEdit = book;
                    break;
                }
            }

            if (bookToEdit != null)
            {
                Console.Write("enter new title: ");
                bookToEdit.Title = Console.ReadLine();

                Console.Write("enter new author: ");
                bookToEdit.Author = Console.ReadLine();

                Console.Write("enter new publication year: ");

                int year;

                if (int.TryParse(Console.ReadLine(), out year))
                {
                    bookToEdit.PublicationYear = year;
                    Console.WriteLine("book updated successfully");
                }
                else
                {
                    Console.WriteLine("invalid year");
                }
            }
            else
            {
                Console.WriteLine("book not found");
            }
        }

        // save
        public void SaveBooks()
        {
            using (StreamWriter writer = new StreamWriter("books.txt"))
            {
                foreach (Book book in books)
                {
                    writer.WriteLine($"{book.Title}|{book.Author}|{book.PublicationYear}");
                }
            }

            Console.WriteLine("Books saved successfully!");
        }

        // load from file
        public void LoadBooks()
        {
            if (File.Exists("books.txt"))
            {
                string[] lines = File.ReadAllLines("books.txt");

                foreach (string line in lines)
                {
                    string[] data = line.Split('|');

                    if (data.Length == 3 &&
                        int.TryParse(data[2], out int year))
                    {
                        Book book = new Book(data[0], data[1], year);
                        books.Add(book);
                    }
                }
            }
        }

        // sataurebis masivi
        public void ShowBookTitlesArray()
        {
            string[] bookTitles = new string[books.Count];

            for (int i = 0; i < books.Count; i++)
            {
                bookTitles[i] = books[i].Title;
            }

            Console.WriteLine("Book titles:");

            foreach (string title in bookTitles)
            {
                Console.WriteLine(title);
            }
        }



    }

}
