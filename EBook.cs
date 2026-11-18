
namespace LibraryManagementSystem
{
    public class EBook : Book
    {
        public double FileSize { get; set; }

        // konstruqori
        public EBook(string title, string author, int publicationYear, double fileSize)
            : base(title, author, publicationYear)
        {
            FileSize = fileSize;
        }

        // metodis dafarva
        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"File Size: {FileSize} MB");
        }
    }
}
