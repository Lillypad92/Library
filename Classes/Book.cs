namespace Uppgift2.Classes
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Language { get; set; }
        public bool IsBorrowed { get; set; }

        public Book(string title, string author, string language, bool isborrowed)
        {
            Title = title;
            Author = author;
            Language = language;
            IsBorrowed = isborrowed;
        }
    }
}

