namespace Uppgift2.Classes
{
    public class Book
    {
        public int Id {  get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Language { get; set; }
        public bool IsAvailable { get; set; }
        public int CategoryId { get; set; }
        
        public Book(string title, string author, string language, bool isAvailable, int categoryId)
        {
            Title = title;
            Author = author;
            Language = language;
            IsAvailable = isAvailable;
            CategoryId = categoryId;

        }

        public string GetCategoryName()
        {
            switch (CategoryId)
            {
                case 1: return "Djur";
                case 2: return "Historia";
                case 3: return "Thriller";
                default: return "";
            }
        }
    }
}

