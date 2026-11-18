
namespace LibraryManagementSystem
{

    public class Book
    {
        // title
        public string Title { get; set; }

        // author
        public string Author { get; set; }

        // year
        public int PublicationYear { get; set; }

        // konstruqtori
        public Book(string title, string author, int publicationYear)
        {
            Title = title;
            Author = author;
            PublicationYear = publicationYear;
        }

        // informaciis chveneba 
        public virtual void DisplayInfo()
        {
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Author: {Author}");
            Console.WriteLine($"Year: {PublicationYear}");
        }
    }
}
