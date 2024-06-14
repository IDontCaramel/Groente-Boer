using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace groenteBoer
{
    public class Product
    {
        public int ID { get; set; }
        public string groente { get; set; }
        public decimal prijs { get; set; }
        public string plaatje { get; set; }
        public string soort { get; set; }
        public string prijsPer { get; set; }
    }

    public class DatabaseHelper
    {
        private string connectionString;

        public DatabaseHelper()
        {
            connectionString = "Server=localhost;Database=groenteboer;user=root;";
        }


        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT id, groente, prijs, plaatje, soort, prijsPer FROM producten";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                try
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        

                        while (reader.Read())
                        {
                            Product product = new Product
                            {
                                ID = reader.GetInt32("id"),
                                groente = reader.GetString("groente"),
                                prijs = reader.GetDecimal("prijs"),
                                plaatje = reader.GetString("plaatje"),
                                soort = reader.GetString("soort"),
                                prijsPer = reader.GetString("prijsPer")
                            };

                            products.Add(product);
                        }
                    }
                }
                catch (IndexOutOfRangeException ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"General error: {ex.Message}");
                }
            }

            return products;
        }

    }
}
