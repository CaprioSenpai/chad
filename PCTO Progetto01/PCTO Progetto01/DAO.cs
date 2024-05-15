namespace PCTO_Progetto01
{
    class DAO
    {
        public static List<Person> repo { get; set; }
        public DAO()
        {
            repo = new List<Person>();
        }

        public List<Person> GetRepo()
        { 
            return repo;
        }
    }
}