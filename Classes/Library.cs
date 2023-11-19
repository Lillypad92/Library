using System.Text.Json;


namespace Uppgift2.Classes
{
    public class Library
    {
        public const string ListOfBooks = "C:/Users/linda/source/repos/Uppgift2/Files2/listofbooks.json";
        public const string ListOfBorrowers = "C:/Users/linda/source/repos/Uppgift2/Files2/listofborrowers.json";

        public List<Book> Books { get; set; }
        public List<Borrower> Borrowers { get; set; }


        public Library()
        {
            Books = GetBooks();
            Borrowers = GetBorrowers();
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
                //WRITE ERROR MESSAGE TO LOGFILE

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
            catch (Exception ex)
            {
                //WRITE ERROR MESSAGE TO LOGFILE

                return new List<Book>();
            }
        }

        public void AddBorrowerToJsonFile(List<Borrower> borrowers)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(borrowers, options);
                File.WriteAllText(ListOfBorrowers, jsonString);

                Console.WriteLine("Det finns en ny registrerad låntagare i biblioteket.");
            }
            catch (Exception ex)
            {
                //WRITE ERROR MESSAGE TO LOGFILE
            }
        }
        public void AddBookToJsonFile(List<Book> books)
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(books, options);
                File.WriteAllText(ListOfBooks, jsonString);

                Console.WriteLine("Det finns en ny bok registrerad i biblioteket.");
            }
            catch (Exception e)
            {
                //WRITE ERROR MESSAGE TO LOGFILE
            }
        }
        public void BorrowBook(int bookId, int borrowerId)
        {
            Book updatedBook = Books.Find(book => book.Id == bookId);
            if (updatedBook == null)
            {
                //TODO: TELL USER THERE IS NO BOOK WITH THAT TITLE!!
                return;
            }

            Borrower borrower = Borrowers.Find(borrower => borrower.Id == borrowerId);
            if (borrower == null)
            {
                //TODO: TELL USER THERE IS NO BORROWER WITH THAT ID!!
                return;
            }

            updatedBook.IsAvailable = false;
            borrower.LoanedBooks.Add(updatedBook);

            JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };

            string jsonBookString = JsonSerializer.Serialize(Books, options);
            File.WriteAllText(ListOfBooks, jsonBookString);

            string jsonBorrowerString = JsonSerializer.Serialize(Borrowers, options);
            File.WriteAllText(ListOfBorrowers, jsonBorrowerString);

            Console.WriteLine($"Boken {updatedBook.Title} är utlåndad till låntagare {borrower.FirstName} {borrower.LastName} ");
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
                return;
            }

            List<Book> availableBooks = Books.Where(book => book.IsAvailable).ToList();
            Console.WriteLine();
            Console.WriteLine("Dessa böcker finns tillgängliga: ");
            foreach (Book book in availableBooks)
            {
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine($"Titel: {book.Title}\n" +
                    $"Författare: {book.Author}\n" +
                    $"Språk: {book.Language}\n" +
                    $"Id: {book.Id}"
                    );
                
            }
        }
        public void ReturnBooks(int borrowerId, int bookId)
        {

            Book returnBook = Books.Find(book => book.Id == bookId);
             if (returnBook == null) 
            {
                Console.WriteLine("Hej hopp");
                return;
            }

            Borrower borrower = Borrowers.Find(borrower => borrower.Id == borrowerId);
            if (borrower == null) 
            {
                Console.WriteLine("Hej hopp 2");
                return;
            }

            returnBook.IsAvailable = true;
            
            JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };

            string jsonReturnBookString = JsonSerializer.Serialize(returnBook, options);
            
            //File.WriteText(ListOfBooks, jsonReturnBookString);
            
            Borrower borrowerLoanedBooks = Borrowers.Find(LoanedBooks => LoanedBooks == LoanedBooks);
    
            borrower.LoanedBooks.Remove(returnBook);
            
            //File.WriteAllText(ListOfBorrowers, );


            Console.WriteLine($"Boken {returnBook.Title}, {returnBook.Id} är återlämnad.");


            //string jsonBorrowerString = JsonSerializer.Serialize(Borrowers, options);
        }

        public void ShowBorrowers()
        {
            if (Borrowers.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("Det finns inga registrerade låntagare.\nVar vänlig och registrera nya låntagare.");
                Console.WriteLine("--------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.White;
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
            else
            {
                Console.WriteLine("------Låntagare----------");
                Console.WriteLine();
                foreach (Borrower borrower in Borrowers)
                {
                    Console.WriteLine($"Förnamn: {borrower.FirstName}\n" +
                        $"Efternamn: {borrower.LastName}\n" +
                        $"Personnumer: {borrower.SocialSecurityNumber}\n" +
                        $"Id: {borrower.Id}");

                    if (borrower.LoanedBooks.Count > 0)
                    {
                        
                        Console.WriteLine();
                        Console.WriteLine("Lånade böcker: ");
                        Console.WriteLine("----------------");
                        foreach (Book book in borrower.LoanedBooks) 
                        {
                            Console.WriteLine($"Författare: {book.Author}\n" +
                            $"Titel: {book.Title}\n" +
                            $"Kategori: {book.GetCategoryName()}\n" +
                            $"Språk: {book.Language}\n" +
                            $"Id: {book.Id}");
                            Console.WriteLine();
                        }
                    }
                    Console.WriteLine("--------------------------------------------------");
                    
                    Console.WriteLine();
                }

                Console.WriteLine("Tryck Enter för att komma vidare i programmet.");

                //Gör så att man kan komma vidare i programmet vid Enter knapptryckning, fungerar inte med någon annan knapptryckning. 
                while (true)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;
                    if (key.Equals(ConsoleKey.Enter))
                    {
                        break;
                    }
                }
                //Console.Clear();
            }
        }

       

    }
}
