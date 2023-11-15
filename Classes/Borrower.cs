namespace Uppgift2.Classes
{
    public class Borrower
    {
        
        public string Name { get; set; }
        public string LastName { get; set; }
        public bool LoanedBooks { get; set; }
        public double SocialSecurityNumber { get; set; }
        public int ID { get; set; }
        public Borrower(string name, string lastname, bool loanedbooks, double socialsecuritynumber, int id) 
        { 
            Name = name;
            LastName = lastname;
            LoanedBooks = loanedbooks;
            SocialSecurityNumber = socialsecuritynumber;
            ID = id;
        }

        public Borrower(string name, string lastname, double socialsecuritynumber, int id)
        {
            Name = name;
            LastName = lastname;
            SocialSecurityNumber = socialsecuritynumber;
            ID = id;
        }
    }
}
