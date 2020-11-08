namespace DataAccess
{
    public class User 
    {
        public string Uconst { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; }

        public override string ToString()
        {
            return $"Uconst = {Uconst}, FirstName = {FirstName}, LastName = {LastName}, Email = {Email}," +
                   $"Password = {Password}, UserName ={UserName}";
        }
        
    }
}