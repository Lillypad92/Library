using System.Text.Json;

namespace Uppgift2.Classes
{
    public class Library
    {
        public const string ListOfBooks = "D:/Programmering/Inlämningsuppgift-2/Uppgift2/Files/listofbooks.json";
        public const string ListOfBorrowers = "D:/Programmering/Inlämningsuppgift-2/Uppgift2/Files/listofborrowers.json";

        public List<Book> Books { get; set; }
        

        public Library()
        {
            Books = GetBooks();
        }
        
        public List<Book> GetBooks()
        {
            try
            {
                if (!File.Exists(ListOfBooks)) { return new List<Book>(); }
                string jsonData = File.ReadAllText(ListOfBooks);

                return JsonSerializer.Deserialize<List<Book>>(jsonData);
            }
            catch (Exception e)
            {
                return new List<Book>();
            }
        }
        public void AddNewBook(string title, string author, string language)
        {
            try
            {
                Book[] booksOfLists;

                if (File.Exists(ListOfBooks))
                {
                    string jsonData = File.ReadAllText(ListOfBooks);
                    booksOfLists = JsonSerializer.Deserialize<Book[]>(jsonData);
                }
                else
                {
                    booksOfLists = new Book[0];
                }

                var newBook = new Book(title, author, language, false);

                var combinedBooks = new Book[booksOfLists.Length + 1];
                Array.Copy(booksOfLists, combinedBooks, booksOfLists.Length);
                combinedBooks[booksOfLists.Length] = newBook;

                string newJsonData = JsonSerializer.Serialize(combinedBooks);

                File.WriteAllText(ListOfBooks, newJsonData);
                Books.Add(newBook);
            }
            catch (Exception ex)
            {
                //TODO HANDLE ERRORS?
            }
        }
        public void ShowAvaibleBooks() 
        {
            Console.WriteLine("----------Dessa böcker finns tillgängliga----------");
            foreach (Book book in Books) 
            {
                string isBorrowed = book.IsBorrowed ? "Ja" : "Nej";

                Console.WriteLine($"Titel: {book.Title}\n" +
                    $"Författare: {book.Author}\n" +
                    $"Utlånad: {isBorrowed}");
                Console.WriteLine();
            }
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Tryck enter för att komma vidare.");
            
        }

        //public void LendingBooks (string title)
        //{
        //    Console.WriteLine("---Dessa böcker finns tillgängliga för utlåning----");
        //    foreach (Book book in Books) 
        //    {
        //        Console.WriteLine($"Titel: {book.Title}\n" +
        //            $"Författare: {book.Author}\n");
        //    }
            
        //    Console.WriteLine("Vilken bok ska lånas ut?\nDet räcker om du skriver titeln på boken som ska lånas ut.");
        //    title = Console.ReadLine();

            


        //    Console.ReadKey();
        //}

        public void ShowBorrowers() 
        { 
            
        }

    }
}
