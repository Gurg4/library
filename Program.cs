namespace LibraryManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create BookManager object
            BookManager manager = new BookManager();

            bool running = true;

            // Load books from file
            manager.LoadBooks();

            // Main menu cycle
            while (running)
            {
                Console.WriteLine("\n==== LIBRARY MANAGEMENT SYSTEM ====");
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. Show All Books");
                Console.WriteLine("3. Search Book");
                Console.WriteLine("4. Exit");
                Console.WriteLine("5. Show Book Titles");
                Console.WriteLine("6. Delete Book");
                Console.WriteLine("7. Edit Book");
                Console.WriteLine("8. Add EBook");
                Console.WriteLine("9. Delete Books File");

                Console.Write("\nChoose an option: ");
                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    // Add normal book
                    case "1":
                        Console.Write("Enter book title: ");
                        string title = Console.ReadLine() ?? "";

                        Console.Write("Enter author: ");
                        string author = Console.ReadLine() ?? "";

                        Console.Write("Enter publication year: ");
                        int year;

                        if (int.TryParse(Console.ReadLine(), out year))
                        {
                            Book book = new Book(title, author, year);
                            manager.AddBook(book);
                        }
                        else
                        {
                            Console.WriteLine("Invalid year");
                        }
                        break;

                    // Show all books
                    case "2":
                        manager.ShowAllBooks();
                        break;

                    // Search book
                    case "3":
                        Console.Write("Enter book title to search: ");
                        string searchTitle = Console.ReadLine() ?? "";

                        manager.SearchBook(searchTitle);
                        break;

                    // Exit program
                    case "4":
                        manager.SaveBooks();
                        running = false;
                        Console.WriteLine("Goodbye!");
                        break;

                    // Show titles array
                    case "5":
                        manager.ShowBookTitlesArray();
                        break;

                    // Delete book
                    case "6":
                        Console.Write("Enter book title to delete: ");
                        string deleteTitle = Console.ReadLine() ?? "";

                        manager.DeleteBook(deleteTitle);
                        break;

                    // Edit book
                    case "7":
                        Console.Write("Enter book title to edit: ");
                        string editTitle = Console.ReadLine() ?? "";

                        manager.EditBook(editTitle);
                        break;

                    // Add EBook
                    case "8":
                        Console.Write("Enter EBook title: ");
                        string ebookTitle = Console.ReadLine() ?? "";

                        Console.Write("Enter author: ");
                        string ebookAuthor = Console.ReadLine() ?? "";

                        Console.Write("Enter publication year: ");
                        int ebookYear;

                        if (!int.TryParse(Console.ReadLine(), out ebookYear))
                        {
                            Console.WriteLine("Invalid year");
                            break;
                        }

                        Console.Write("Enter file size (MB): ");
                        double fileSize;

                        if (!double.TryParse(Console.ReadLine(), out fileSize))
                        {
                            Console.WriteLine("Invalid file size");
                            break;
                        }

                        EBook ebook = new EBook(
                            ebookTitle,
                            ebookAuthor,
                            ebookYear,
                            fileSize
                        );

                        manager.AddBook(ebook);
                        break;

                    // Delete books file
                    case "9":
                        Console.Write("Are you sure? (yes/no): ");
                        string answer = Console.ReadLine() ?? "";

                        if (answer.ToLower() == "yes")
                        {
                            manager.DeleteFile();
                        }
                        else
                        {
                            Console.WriteLine("Operation cancelled");
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }
    }
}