namespace LibraryManagementSystem
{
    public class Book
    {
        // Book properties
        // private set means that properties can be changed only inside the class

        public string Title { get; private set; }

        public string Author { get; private set; }

        public int PublicationYear { get; private set; }

        // Constructor
        public Book(string title, string author, int publicationYear)
        {
            Title = title;
            Author = author;
            PublicationYear = publicationYear;
        }

        // Update book information
        public void UpdateBook(string title, string author, int year)
        {
            Title = title;
            Author = author;
            PublicationYear = year;
        }

        // Display book information
        // virtual allows child classes to override this method

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Author: {Author}");
            Console.WriteLine($"Year: {PublicationYear}");
        }
    }
}