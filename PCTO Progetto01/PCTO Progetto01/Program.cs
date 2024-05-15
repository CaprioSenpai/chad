namespace PCTO_Progetto01
{
    class Program
    {
        public static void Main(string[] args)
        {
            int i = 0;
            View x = new View();
            DAO DAO = new DAO();
            DAODB DAODB = new DAODB();
            DAODB.OpenConn();
            Paginazione paginazione = new Paginazione(3, 1);
            string choice;
            do
            {
                x.MainMenu();
                choice = x.ReadString("Input");
                switch (choice)
                {
                    case "1":
                        i++;
                        Person p;
                        p = x.InsertPerson(i);
                        DAODB.Insert(p);
                        DAO.GetRepo().Add(p);//I

                        break;
                    case "2":
                        if (DAO.GetRepo().Count() > 0)
                            x.EditPerson(DAO.GetRepo());//Modifica
                        else
                            Console.WriteLine("Non ci sono utenti registrati");
                        break;
                    case "3":
                        if (DAO.GetRepo().Count() > 0)
                            x.DeletePerson(DAO.GetRepo());//Cancella
                        else
                            Console.WriteLine("Non ci sono utenti registrati");

                        break;
                    case "4":
                        if (DAO.GetRepo().Count() > 0)
                        {
                            string option;
                            do
                            {
                                x.SearchMenu();
                                option = x.ReadString("Input");
                                switch (option)
                                {
                                    case "1":
                                        x.SearchById(DAO.GetRepo());
                                        break;
                                    case "2":
                                        x.SearchByName(DAO.GetRepo());
                                        break;
                                    case "3":
                                        x.SearchBySurname(DAO.GetRepo());
                                        break;
                                    default:
                                        if (option != "4")
                                            Console.WriteLine("Input non conforme");
                                        break;
                                }
                                if (choice != "4")
                                    x.ReadString("premi INVIO per continuare");
                            } while (option != "4");
                        }
                        else
                            Console.WriteLine("Non ci sono utenti registrati");

                        break;
                    case "5":
                        Console.Clear();
                        if (DAO.GetRepo().Count() > 0)
                            x.PrintInList(DAO.GetRepo(), paginazione);                  //Output Lista Utenti
                        else
                            Console.WriteLine("Non ci sono utenti registrati");

                        break;
                    case "6":
                        if (DAO.GetRepo().Count() > 0)
                            x.PrintRepo(DAO.GetRepo(), paginazione);                    //Output schede Persona
                        else
                            Console.WriteLine("Non ci sono utenti registrati");

                        break;

                    default:
                        if (choice != "7")
                            Console.WriteLine("Input non conforme");
                        break;
                }
                if (choice != "7")
                    x.ReadString("premi INVIO per continuare");
            } while (choice != "7");
            DAODB.CloseConn();
        }
    }
}