using PCTO_Progetto01;
using MySql.Data.MySqlClient;

class DAODB
{
    public string ConnString { get; set; }
    public MySqlConnection Conn { get; set; }
    public DAODB()
    {
        ConnString = "Server=localhost; Database=x; User id=root; Password=;";
        OpenConn();
    }
    public bool OpenConn()
    {
        Conn = new MySqlConnection(ConnString);
        try
        {
            Conn.Open();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
        return true;
    }
    public bool CloseConn()
    {
        try
        {
            Conn.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
        return true;
    }

    public void Insert(Person a)
    {
        try
        {
            string query = "Insert into people (firstname, lastname, gender, birthplace, birthdate, cf)" +
                "values(@firstname, @lastname,@gender, @birthplace, @birthdate, @cf)";
            using (MySqlCommand cmd = new MySqlCommand(query, Conn))
            {
                cmd.Parameters.AddWithValue("@firstname", a.FirstName);
                cmd.Parameters.AddWithValue("@lastname", a.LastName);
                cmd.Parameters.AddWithValue("@gender", a.Gender);
                cmd.Parameters.AddWithValue("@birthplace", a.BirthPlace);
                cmd.Parameters.AddWithValue("@birthdate", a.BirthDate);
                cmd.Parameters.AddWithValue("@cf", a.CF);
                cmd.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public List<Person> Read()
    {
        try
        {
            string query = "SELECT * from people;";
            MySqlCommand cmd = new MySqlCommand(query, Conn);
            List<Person> a = new List<Person>();
            Person person = new Person();
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    a.Add(new Person());
                    a.Last().Id = Convert.ToInt32(reader["id"]);
                    a.Last().FirstName = Convert.ToString(reader["firstname"]);
                    a.Last().LastName = Convert.ToString(reader["lastname"]);
                    a.Last().Gender = Convert.ToString(reader["gender"]);
                    a.Last().BirthPlace = Convert.ToString(reader["birthplace"]);
                    a.Last().BirthDate = Convert.ToDateTime(reader["birthdate"]);
                    a.Last().CF = Convert.ToString(reader["cf"]);
                }
            }
            return a;

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public void Edit(Person a)
    {
        try
        {
            string query = "Update people set firstname = @firstname, lastname = @lastname, gender = @gender, birthplace = @birthplace," +
                "birthdate = @birthdate, cf = @cf where id = @id";
            using (MySqlCommand cmd = new MySqlCommand(query, Conn))
            {
                cmd.Parameters.AddWithValue("@firstname", a.FirstName);
                cmd.Parameters.AddWithValue("@lastname", a.LastName);
                cmd.Parameters.AddWithValue("@gender", a.Gender);
                cmd.Parameters.AddWithValue("@birthplace", a.BirthPlace);
                cmd.Parameters.AddWithValue("@birthdate", a.BirthDate);
                cmd.Parameters.AddWithValue("@cf", a.CF);
                cmd.Parameters.AddWithValue("@id", a.Id);
                cmd.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public void Delete(int id)
    {
        try
        {
            string query = $"delete from people where id = @id";
            using (MySqlCommand cmd = new MySqlCommand(query, Conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}