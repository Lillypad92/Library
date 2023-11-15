namespace Uppgift2.Classes
{
    public class Menu
    {
        
        public void RunMenu() 
        {
            Library library = new Library();

            bool endProgram = false;
            
            Console.WriteLine("Välkommen till Åshammars bibliotek!");
            
            while (!endProgram) 
            {
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("Vad vill du göra? ");
                Console.WriteLine();
                Console.WriteLine("1. Lägg till nya böcker i biblioteket.");
                Console.WriteLine("2. Låna ut böcker till låntagare.");
                Console.WriteLine("3. Återlämna böcker.");
                Console.WriteLine("4. Visa tillgängliga böcker.");
                Console.WriteLine("5. Visa låntagare och deras lånade böcker.");
                Console.WriteLine("6. Avsluta programmet.");
                Console.Write("Menyval: ");
                int menuChoice = int.Parse(Console.ReadLine());

                Console.WriteLine();

                switch (menuChoice)
                {
                    case 1:
                        //Lägg till nya böcker.
                        Console.Write("Titel på boken: ");
                        string? title = Console.ReadLine();

                        Console.Write("Bokens författare: ");
                        string? author = Console.ReadLine();

                        Console.Write("Bokens språk : ");
                        string? language = Console.ReadLine();

                        if (title == null) title = "";
                        if (author == null) author = "";
                        if (language == null) language = "";

                        library.AddNewBook(title, author, language);
                        break;
                    case 2:
                        //Låna ut böcker.
                        //library.LendingBooks();
                        break;
                    case 3:
                        //Återlämna böcker.
                        break;
                    case 4:
                        //Visa tillgängliga böcker.
                        Console.WriteLine();
                        library.ShowAvaibleBooks();
                        
                        break;
                    case 5:
                        //Visa låntagare och deras böcker.
                        library.ShowBorrowers();
                        break;
                    case 6:
                        //Avslutar programmet.
                        endProgram = true;
                        Console.WriteLine("-----------------------------------------------");
                        Console.WriteLine("Progammet kommer avslutas, tack för denna gång! ");
                        Thread.Sleep(1000);
                        break;
                    default:
                        //Om man trycker på fel knappval i menyn.
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("---------------------------------------");
                        Console.WriteLine("Du valde ett ogiltigt val, försök igen.");
                        Console.WriteLine("Tryck enter för att komma vidare.");
                        Console.WriteLine("---------------------------------------");
                        Console.ReadKey();
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }
            }

            

        }
    }
}
