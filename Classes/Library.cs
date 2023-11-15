using System.Text.Json;

namespace Uppgift2.Classes
{
    public class Library
    {
        public const string ListOfBooks = "C:/Users/linda/source/repos/Uppgift2/Files2/listofbooks.json";
        public const string ListOfBorrowers = "C:/Users/linda/source/repos/Uppgift2/Files2/listofborrowers.json";
        //D:/Programmering/Inlämningsuppgift-2/Uppgift2/Files2/listofbooks.json"
            //D:/Programmering/Inlämningsuppgift-2/Uppgift2/Files2/listofborrowers.json"
        public List<Book> Books { get; set; }
        public List<Borrower> Borrower { get; set; }
        

        public Library()
        {
            Books = GetBooks();
            Borrower = GetBorrowers();
        }
        
        public List<Borrower> GetBorrowers() 
        {
            try 
            {
                if (!File.Exists(ListOfBorrowers)) { return new List<Borrower>(); }
                string jsonData = File.ReadAllText(ListOfBorrowers);

                return JsonSerializer.Deserialize<List<Borrower>>(jsonData);
            } 
            catch (Exception ex) 
            { 
                return new List<Borrower>();
            }
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

        public void AddNewBorrowers(string name, string lastname, double socialsecuritynumber, int id) 
        {
            try 
            {
                Borrower[] addedBorrowers;

                if (File.Exists(ListOfBorrowers))
                {
                    string jsonData = File.ReadAllText(ListOfBorrowers);
                    addedBorrowers = JsonSerializer.Deserialize<Borrower[]>(jsonData);
                }
                else 
                { 
                    addedBorrowers = new Borrower[0];
                }

                var newBorrowers = new Borrower(name, lastname, socialsecuritynumber, id);

                var combinedBorrowers = new Borrower[addedBorrowers.Length + 1];
                Array.Copy(addedBorrowers, combinedBorrowers, addedBorrowers.Length);
                combinedBorrowers[addedBorrowers.Length] = newBorrowers;

                string newJsonData = JsonSerializer.Serialize(combinedBorrowers);

                File.WriteAllText(ListOfBorrowers, newJsonData);
                Borrower.Add(newBorrowers);

                Console.WriteLine("Det finns en ny registrerad låntagare i biblioteket.");

                //foreach (Borrower borrower in addedBorrowers)
                //{
                //    Console.WriteLine($"Namn: {borrower.Name}\n" +
                //        $"Efternamn: {borrower.LastName}\n" +
                //        $"Personnummer: {borrower.SocialSecurityNumber}\n" +
                //        $"Biblioteks ID: {borrower.ID}");
                //}

            } 
            catch (Exception e)
            { 
            
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
            if (Books.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("Det finns inga böcker att visa.\nLägg till nya böcker i biblioteket för att se böcker i listan.");
                Console.WriteLine("--------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else 
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
            }

            Console.WriteLine();
            Console.WriteLine("Tryck Enter för att komma vidare.");

            //Gör så att man kan komma vidare i programmet vid Enter knapptryckning, fungerar inte med någon annan knapptryckning.
            while (true) 
            { 
                ConsoleKey key = Console.ReadKey(true).Key;
                if (key.Equals(ConsoleKey.Enter)) 
                {
                    break;
                }
            }
            Console.Clear();
        }

        public void ShowBorrowers() 
        {
            if (Borrower.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("Det finns inga registrerade låntagare.\nVar vänlig och registrera nya låntagare.");
                Console.WriteLine("--------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else 
            {
                Console.WriteLine("------Låntagare----------");
                foreach (Borrower borrowers in Borrower)
                {

                    Console.WriteLine($"Förnamn: {borrowers.Name}\n" +
                        $"Efternamn: {borrowers.LastName}\n" +
                        $"Lånade böcker: {borrowers.LoanedBooks}\n" +
                        $"Personnumer: {borrowers.SocialSecurityNumber}");
                    Console.WriteLine();
                }
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("Tryck Enter för att komma vidare.");

                //Gör så att man kan komma vidare i programmet vid Enter knapptryckning, fungerar inte med någon annan knapptryckning. 
                while (true)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;
                    if (key.Equals(ConsoleKey.Enter))
                    {
                        break;
                    }
                }
                Console.Clear();
            }
         
        }
        public void LendingBooks() 
        {
            
        }
        public void ReturBooks() 
        { 
               
        }

    }
}
