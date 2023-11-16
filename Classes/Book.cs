namespace Uppgift2.Classes
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Language { get; set; }
        public bool IsAvailable { get; set; } = true;
        public Book(string title, string author, string language, bool isavailable)
        {
            Title = title;
            Author = author;
            Language = language;
            IsAvailable = isavailable;
        }
    }
}

