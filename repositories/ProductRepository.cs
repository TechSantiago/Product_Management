using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Net;
using WebApplication3.Models;

namespace WebApplication3.Repositories
{
    public class ProductRepository
    {
        private string connectionString;

        // Constructor sin parámetros que asigna una cadena de conexión
        public ProductRepository()
        {
            // Forzar el uso de TLS 1.2
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            // Cadena de conexión para MySQL
            connectionString = "Server=localhost;Database=WebApplication3;User Id=root;Password=root;SslMode=Required;";
        }

        public void Create(Product product)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("INSERT INTO Product (NameP, Price, Stock) VALUES (@NameP, @Price, @Stock); SELECT LAST_INSERT_ID();", connection);
                command.Parameters.AddWithValue("@NameP", product.NameP);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Stock", product.Stock);

                // Recuperar el ID generado
                var id = Convert.ToInt32(command.ExecuteScalar());
                product.Id = id; // Asignar el ID al objeto product
            }
        }

        public List<Product> Read()
        {
            var products = new List<Product>();
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("SELECT * FROM Product", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            Id = reader.GetInt32("Id"),
                            NameP = reader.GetString("NameP"),
                            Price = reader.GetDecimal("Price"),
                            Stock = reader.GetInt32("Stock")
                        });
                    }
                }
            }
            return products;
        }

        public void Update(Product product)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("UPDATE Product SET NameP = @NameP, Price = @Price, Stock = @Stock WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", product.Id);
                command.Parameters.AddWithValue("@NameP", product.NameP);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@Stock", product.Stock);
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("DELETE FROM Product WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }
    }
}
