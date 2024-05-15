namespace PCTO_Progetto01
{
    class View
    {
        public void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("--Menu Principale--");
            Console.WriteLine("[1]Inserisci Persona");
            Console.WriteLine("[2]Modifica Persona");
            Console.WriteLine("[3]Elimina Persona");
            Console.WriteLine("[4]Trova persona");
            Console.WriteLine("[5]Visualizza Elenco Persona");
            Console.WriteLine("[6]Visualizza Schede Persona");
            Console.WriteLine("[7]Esci");

        }

        public void SearchMenu()
        {
            Console.WriteLine("--Menu di Ricerca--");
            Console.WriteLine("[1]Trova per Id");
            Console.WriteLine("[2]Trova per Nome");
            Console.WriteLine("[3]Trova per Cognome");
            Console.WriteLine("[4]Esci");

        }

        public int ReadInt(string message)
        {
            int a = 0;
            while (!int.TryParse(ReadString(message), out a))
            {
                Console.WriteLine("errore, inserire un intero.");
            }
            return a;
        }

        public string ReadString(string message)
        {
            string a;
            Console.Write($"{message}: ");
            a = Console.ReadLine();
            return a;
        }

        public DateTime ReadDate()
        {
            DateTime a = new DateTime();
            while (!DateTime.TryParse(ReadString("inserire data (yyyy/MM/dd)"), out a))
            {
                Console.WriteLine("errore,Riprova");
            }
            return a;
        }

        public void PrintPersonSheet(Person person)
        {
            Console.WriteLine($"--Scheda[{person.Id}]------------------------------");
            Console.WriteLine($"{person.FirstName} {person.LastName}");
            Console.WriteLine($"Nato/a il {person.BirthDate.Day}/{person.BirthDate.Month}/{person.BirthDate.Year}");
            Console.WriteLine($"Genere {person.Gender}");
            Console.WriteLine($"CF: {person.CF}");
            Console.WriteLine("-----------------------------------------");
        }

        public void PrintLine(Person person)
        {
            Console.WriteLine($"[#{person.Id}]{person.FirstName} {person.LastName} - {person.BirthDate.ToString()} {person.BirthPlace}");
        }
        public Person InsertPerson(int a)
        {
            Person p = new Person();
            p.FirstName = ReadString("Inserire Nome");
            p.LastName = ReadString("Inserire Cognome");
            p.BirthPlace = ReadString("Inserire comune di Nascita");
            p.BirthDate = ReadDate();
            p.Gender = ReadString("Inserire genere (M - F - Altro)");
            p.CF = ReadString("Inserire codice fiscale");
            p.Id = a;
            return p;
        }

        public void EditPerson(List<Person> a)
        {
            int index = ReadInt("cerca l'id dell'utente");
            bool found = false;
            string choice = "";
            for (int i = 0; i < a.Count(); i++)
            {
                if (a[i].Id == index)
                {
                    DAODB dAODB = new DAODB();
                    do
                    {
                        PrintPersonSheet(a[i]);
                        Console.WriteLine($"[1]Modifica Nome");
                        Console.WriteLine($"[2]Modifica Cognome");
                        Console.WriteLine($"[3]Comune di nascita");
                        Console.WriteLine($"[4]Modifica Data di Nascita");
                        Console.WriteLine($"[5]Modifica Sesso");
                        Console.WriteLine($"[6]Modifica CF");
                        Console.WriteLine($"[7]Esci");
                        choice = ReadString("input");
                        switch (choice)
                        {
                            case "1":
                                a[i].FirstName = ReadString($"[Nome corrente {a[i].FirstName}] Inserire nuovo nome");
                                break;

                            case "2":
                                a[i].LastName = ReadString($"[Cognome corrente {a[i].LastName}] Inserire nuovo cognome");
                                break;

                            case "3":
                                a[i].BirthPlace = ReadString($"[Valore corrente {a[i].BirthPlace}] Inserire nuovo comune di nascita");
                                break;

                            case "4":
                                a[i].BirthDate = ReadDate();
                                break;

                            case "5":
                                a[i].Gender = ReadString($"[Valore corrente {a[i].Gender}] Inserire nuovo genere");
                                break;

                            case "6":
                                a[i].CF = ReadString($"[Valore corrente {a[i].CF}] Inserire nuovo CF");
                                break;

                            default:
                                if (choice != "7")
                                    Console.WriteLine("input non conforme");
                                break;
                        }
                        if (choice != "7")
                            ReadString("premi INVIO per continuare");
                    } while (choice != "7");
                    dAODB.Edit(a[i]);
                    break;
                }

            }
            if (!found && choice != "7")
                Console.WriteLine("Utente non trovato");
        }

        public void DeletePerson(List<Person> a)
        {
            int index = ReadInt("cerca l'id della persona da eliminare");
            bool found = false;
            for (int i = 0; i < a.Count(); i++)
            {
                if (a[i].Id == index)
                {
                    DAODB dAODB = new DAODB();
                    string choice;
                    found = true;
                    PrintPersonSheet(a[i]);
                    do
                    {
                        choice = ReadString("Procedere all'eliminazione? (Y/N)");
                        if (choice.ToLower() == "y")
                        {
                            dAODB.Delete(i);
                            a.RemoveAt(i);
                            Console.WriteLine("Rimozione effettuata");
                        }
                        else if (choice.ToLower() != "n")
                            Console.WriteLine("input non conforme");
                    } while (choice.ToLower() != "y" && choice.ToLower() != "n");
                }
            }
            if (!found)
                Console.WriteLine("utente non trovato");
        }

        public void SearchById(List<Person> a)
        {
            int index = ReadInt("Inserire id di ricerca");
            bool found = false;
            Person person = null;
            foreach (Person item in a)
            {
                if (item.Id == index)
                {
                    found = true;
                    person = item;
                    break;
                }
            }
            if (found)
                PrintLine(person);
            else
                Console.WriteLine("Utente non trovato");
        }

        public void SearchByName(List<Person> a)
        {
            string index = ReadString("Inserire nome da ricercare");
            bool found = false;
            List<Person> founded = new List<Person>();
            foreach (Person item in a)
            {
                if (item.FirstName.Contains(index))
                {
                    found = true;
                    founded.Add(item);
                }
            }
            if (found)
            {
                //PrintInList(founded, paginazione);
                int idToSearch;
                idToSearch = ReadInt("Inserire id da ricercare");
                foreach (Person item in founded)
                {
                    if (item.Id == idToSearch)
                    {
                        PrintPersonSheet(item);
                        break;
                    }
                }
            }
            else
                Console.WriteLine("Utente non trovato");
        }

        public void SearchBySurname(List<Person> a)
        {
            string index = ReadString("Inserire cognome da ricercare");
            bool found = false;
            List<Person> founded = new List<Person>();
            foreach (Person item in a)
            {
                if (item.LastName.Contains(index))
                {
                    found = true;
                    founded.Add(item);
                }
            }
            if (found)
            {
                //PrintInList(founded);
                //PrintInList(founded);
                int idToSearch;
                idToSearch = ReadInt("Inserire id da Ricercare");
                foreach (Person item in founded)
                {
                    if (item.Id == idToSearch)
                    {
                        PrintPersonSheet(item);
                        break;
                    }
                }


            }
            else
                Console.WriteLine("Utente non trovato");

        }

        public void PrintRepo(List<Person> a, Paginazione paginazione)
        {
            int scelta;
            int offset = (paginazione.CurrentPage - 1) * paginazione.PageLenght;
            for (int i = offset; i < paginazione.PageLenght + offset; i++)
            {
                if (i < a.Count)
                    PrintPersonSheet(a[i]);
                else
                    Console.WriteLine("");
            }
            scelta = ReadInt("[1] Prima pagina [2] pagina precedente [3] pagina successiva [4] ultima pagina [5] esci");
        }

        public void PrintInList(List<Person> a, Paginazione paginazione)
        {
            int scelta = 0;
            while (scelta != 5)
            {


                int offset = (paginazione.CurrentPage - 1) * paginazione.PageLenght;
                double x = a.Count / Convert.ToDouble(paginazione.PageLenght);
                int ultimaPagina = Convert.ToInt32(Math.Ceiling(x));
                for (int i = offset; i < paginazione.PageLenght + offset; i++)
                {
                    if (i < a.Count)
                        PrintPersonSheet(a[i]);
                    else
                        Console.WriteLine("");
                }
                scelta = ReadInt("[1] Prima pagina [2] pagina precedente [3] pagina successiva [4] ultima pagina [5] esci");
                if (scelta == 1)
                    paginazione.CurrentPage = 1;
                else if (scelta == 2)
                {
                    if (paginazione.CurrentPage > 1)
                        paginazione.CurrentPage--;
                }
                else if (scelta == 3)
                    if (paginazione.CurrentPage < ultimaPagina)
                        paginazione.CurrentPage++;
            }
        }

    }
}