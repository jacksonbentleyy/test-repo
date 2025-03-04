using System.Text.Json;
using api.Models;
using MySqlConnector;

namespace api
{
    public class Database
    {
        private string cs = "Server=127.0.0.1;User ID=root;Password=Fifi0251!;Database=mis321";
        public async Task<List<Book>> GetAllBooksAsync()
        {
            List<Book> myBooks = [];

            using var connection = new MySqlConnection(cs);
            await connection.OpenAsync();

            using var command = new MySqlCommand("SELECT * FROM mis321.book WHERE deleted = 'n'", connection);

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                myBooks.Add(new Book(){
                    ID = reader.GetInt32(0), 
                    Title = reader.GetString(1), 
                    Author = reader.GetString(2)
                });
            }
            
            return myBooks;
        }
        public async Task<Book> GetBookAsync(int id){
            
            try
            {
            
                using var connection = new MySqlConnection(cs);
                await connection.OpenAsync();

                using var command = new MySqlCommand($"SELECT * FROM mis321.book where id = {id}", connection);

                using var reader = await command.ExecuteReaderAsync();
                await reader.ReadAsync();

                Book book = new() {ID = reader.GetInt32(0), Title = reader.GetString(1), Author = reader.GetString(2)};
                return book;
            }
                
            catch
            {
                return new Book();
            }
        }

        public async Task InsertBookAsync(Book myBook)
        {
            try
            {
            
                using var connection = new MySqlConnection(cs);
                await connection.OpenAsync();

                string sql = $"INSERT INTO `mis321`.`book` (`title`, `author`, `deleted`) VALUES (@title, @author, @deleted)";

                using var command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("@title", myBook.Title);
                command.Parameters.AddWithValue("@author", myBook.Author);
                command.Parameters.AddWithValue("@deleted", "n");
                command.Prepare();

                using var reader = await command.ExecuteReaderAsync();
                connection.Close();
                
            }
                
            catch(Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }

        public async Task DeleteBookAsync(int id)
        {
            try
            {
            
                using var connection = new MySqlConnection(cs);
                await connection.OpenAsync();

                string sql = $"DELETE FROM `mis321`.`book` WHERE (`ID` = @ID);";

                using var command = new MySqlCommand(sql, connection);
                
                command.Prepare();

                using var reader = await command.ExecuteReaderAsync();
                connection.Close();
                
            } catch(Exception e){
                System.Console.WriteLine(e.Message);
            }
        }

        public async Task UpdateBookAsync(Book myBook)
        {
            try
            {
            
                using var connection = new MySqlConnection(cs);
                await connection.OpenAsync();

                string sql = $"UPDATE `mis321`.`book` SET title = @title, author = @author where id = @id";

                using var command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("@title", myBook.Title);
                command.Parameters.AddWithValue("@author", myBook.Author);
                command.Parameters.AddWithValue("@id", myBook.ID);
                command.Prepare();

                using var reader = await command.ExecuteReaderAsync();
                connection.Close();
                
            }catch(Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }
    }
}