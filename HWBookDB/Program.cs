using Npgsql;

var connStr = new NpgsqlConnectionStringBuilder(
    "Server=c.testdb-lopatin.postgres.database.azure.com;Database=citus;Port=5432;User Id=citus;Password=0965041740Az;Ssl Mode=Require;Pooling = true; Minimum Pool Size=0; Maximum Pool Size =50;");
connStr.TrustServerCertificate = true;
using (var connection =new NpgsqlConnection(connStr.ToString()))
{
    connection.Open();
    //NpgsqlCommand cmd = new NpgsqlCommand(
    //    "create table books (id serial primary key, title varchar(20), author varchar(20), publishdate date, publisher varchar(20), adress_id int not null, foreign key(adress_id) references adress(id))",
    //    connection);
    //NpgsqlCommand cmd1 = new NpgsqlCommand(
    //    "create table adress (id serial primary key, country varchar(20), city varchar(20), street varchar(20))", connection);
    //cmd1.ExecuteNonQuery();
    //cmd.ExecuteNonQuery();

    //addAdress(connection, "country1", "city1", "street1");
    //addAdress(connection, "country2", "city2", "street2");
    //addBook(connection, "book1", "author1", DateTime.Parse("1999-01-08".ToString()), "publisher1", 1);
    //addBook(connection, "book2", "author2", DateTime.Parse("1999-01-08".ToString()), "publisher2", 2);
    //addBook(connection, "book3", "author3", DateTime.Parse("1999-01-08".ToString()), "publisher3", 1);
    //addBook(connection, "book4", "author3", DateTime.Parse("1999-01-08".ToString()), "publisher4", 2);
    //updateAdress(connection,3, "country3", "city3", "street3");
    //updateBook(connection,1, "book11", "author11", DateTime.Parse("1999-01-08".ToString()), "publisher11", 1);
    //deleteAdress(connection, 8);
    //deleteBook(connection, 4);
    showAdress(connection);
    showBooks(connection);
    //Console.WriteLine("DB was created...");
}

Console.ReadLine();

//Create adress
void addAdress(NpgsqlConnection connect, string country, string city, string street)
{
    NpgsqlCommand cmd = new NpgsqlCommand("insert into adress (country, city, street) values(@country, @city, @street)", connect);
    cmd.Parameters.AddWithValue("country", country);
    cmd.Parameters.AddWithValue("city", city);
    cmd.Parameters.AddWithValue("street", street);
    cmd.ExecuteNonQuery();
}
//Create book
void addBook(NpgsqlConnection connect, string title, string author, DateTime publishdate, string publisher, int adress_id)
{
    NpgsqlCommand cmd = new NpgsqlCommand("insert into books (title, author, publishdate, publisher, adress_id) values(@title, @author, @publishdate, @publisher, @adress_id)", connect);
    cmd.Parameters.AddWithValue("title", title);
    cmd.Parameters.AddWithValue("author", author);
    cmd.Parameters.AddWithValue("publishdate", publishdate);
    cmd.Parameters.AddWithValue("publisher", publisher);
    cmd.Parameters.AddWithValue("adress_id", adress_id);
    cmd.ExecuteNonQuery();
}
//Show adress
void showAdress(NpgsqlConnection connect)
{
    NpgsqlCommand cmd = new NpgsqlCommand("select * from adress",connect);
    var info = cmd.ExecuteReader();
    while (info.Read()){
        Console.WriteLine(string.Format("id: {0} country: {1} city: {2} street  {3}",
            info.GetInt32(0).ToString(), info.GetString(1), info.GetString(2), info.GetString(3)));
    }
    info.Close();
}
//Show books
void showBooks(NpgsqlConnection connect)
{
    NpgsqlCommand cmd = new NpgsqlCommand("select * from books", connect);
    var info = cmd.ExecuteReader();
    while (info.Read())
    {
        //title, author, date, publisher, adress_id
        Console.WriteLine(string.Format("id: {0} title: {1} author: {2} publishdate: {3} publisher: {4} adress_id: {5}",
            info.GetInt32(0).ToString(), info.GetString(1), info.GetString(2), info.GetDateTime(3).ToString(), info.GetString(4), info.GetInt32(5).ToString()));
    }
    info.Close();
}
//Update adress
void updateAdress(NpgsqlConnection connect, int id, string country, string city, string street)
{
    NpgsqlCommand cmd = new NpgsqlCommand("update adress set country=@country, city=@city, street=@street where id=@id ", connect);
    cmd.Parameters.AddWithValue("id", id);
    cmd.Parameters.AddWithValue("country", country);
    cmd.Parameters.AddWithValue("city", city);
    cmd.Parameters.AddWithValue("street", street);
    cmd.ExecuteNonQuery();
}
//Update book
void updateBook(NpgsqlConnection connect,int id, string title, string author, DateTime publishdate, string publisher, int adress_id)
{
    NpgsqlCommand cmd = new NpgsqlCommand("update books set title=@title, author=@author, publishdate=@publishdate, publisher=@publisher ,adress_id=@adress_id where id=@id", connect);
    cmd.Parameters.AddWithValue("id", id);
    cmd.Parameters.AddWithValue("title", title);
    cmd.Parameters.AddWithValue("author", author);
    cmd.Parameters.AddWithValue("publishdate", publishdate);
    cmd.Parameters.AddWithValue("publisher", publisher);
    cmd.Parameters.AddWithValue("adress_id", adress_id);
    cmd.ExecuteNonQuery();
}
//Delete Adress
 void deleteAdress(NpgsqlConnection connect, int id)
{
    NpgsqlCommand cmd = new NpgsqlCommand("delete from adress where id=@id", connect);
    cmd.Parameters.AddWithValue("id", id);
    cmd.ExecuteNonQuery();
}
//Delete Book
void deleteBook(NpgsqlConnection connect, int id)
{
    NpgsqlCommand cmd = new NpgsqlCommand("delete from books where id=@id", connect);
    cmd.Parameters.AddWithValue("id", id);
    cmd.ExecuteNonQuery();
}

