namespace LibraryManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create 
            BookManager manager = new BookManager();
            bool running = true;
            manager.LoadBooks();

            // cikli
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

                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter book title: ");
                        string title = Console.ReadLine();

                        Console.Write("Enter author: ");
                        string author = Console.ReadLine();

                        Console.Write("Enter publication year: ");
                        int year;

                        if (int.TryParse(Console.ReadLine(), out year))
                        {
                            Book book = new Book(title, author, year);
                            manager.AddBook(book);
                        }
                        else
                        {
                            Console.WriteLine("Invalid year ");
                        }
                        break;

                    case "2":
                        manager.ShowAllBooks();
                        break;

                    case "3":
                        Console.Write("enter book title to search: ");
                        string searchTitle = Console.ReadLine();

                        manager.SearchBook(searchTitle);
                        break;

                    case "4":
                        manager.SaveBooks();
                        running = false;
                        Console.WriteLine("goodbye");
                        break;

                    case "5":
                        manager.ShowBookTitlesArray();
                        break;

                    case "6":
                        Console.Write("enter book title to delete: ");
                        string deleteTitle = Console.ReadLine();
                        manager.DeleteBook(deleteTitle);
                        break;

                    case "7":
                        Console.Write("enter book title to edit: ");
                        string editTitle = Console.ReadLine();
                        manager.EditBook(editTitle);
                        break;





                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }
    }
}
