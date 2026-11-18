namespace LibraryManagementSystem
{
    // EBook inherits from Book class
    public class EBook : Book
    {
        // EBook additional property
        public double FileSize { get; private set; }

        // Constructor
        public EBook(string title, string author, int publicationYear, double fileSize)
            : base(title, author, publicationYear)
        {
            FileSize = fileSize;
        }

        // Update EBook file size
        public void UpdateFileSize(double fileSize)
        {
            FileSize = fileSize;
        }

        // Override DisplayInfo method
        // This is an example of polymorphism

        public override void DisplayInfo()
        {
            base.DisplayInfo();

            Console.WriteLine($"File Size: {FileSize} MB");
            Console.WriteLine("Type: EBook");
        }
    }
}
