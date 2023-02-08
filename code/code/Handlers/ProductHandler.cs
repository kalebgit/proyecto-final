using code.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code.Handlers
{
    internal class ProductHandler
    {
        // delegado para verificar los tipos de dato de acuerdo a la base de datos
        public delegate object Verificator(object obj);
        


        // connection string
        protected string _connectionString = "Data Source=LAPTOP-VQVR3Q8R;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        public static object IsDecimal(object obj)
        {
            decimal num;
            bool boolean;
            boolean = decimal.TryParse((string)obj, out num);
            while (!boolean)
            {
                Console.Write("\n---------- No ingresaste valor correcto ----------\n" +
                    "\nPon un valor correcto: ");
                boolean = decimal.TryParse(Console.ReadLine(), out num);
                if (num <= 0)
                    boolean = false;
            }
            obj = num;
            return obj;
        }
        public static object IsInt(object obj)
        {
            int num;
            bool boolean;
            boolean = int.TryParse((string)obj, out num);
            while (!boolean)
            {
                Console.Write("\n---------- No ingresaste valor correcto ----------\n" +
                    "\nPon un valor correcto: ");
                boolean = int.TryParse(Console.ReadLine(), out num);
                if (num <= 0)
                    boolean = false;
            }
            obj = num;
            return obj;
        }
        public static object NonNullable(object obj)
        {
            while (String.IsNullOrWhiteSpace((string)obj))
            {
                Console.Write("\n---------- No ingresaste valor correcto ----------\n" +
                    "\nPon un valor correcto: ");
                obj = Console.ReadLine();
            }

            return obj;
        }

        public List<Product> SelectAll()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"SELECT * FROM Producto",
                    connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            products.Add(new Product(dataReader.GetInt64(0), dataReader.GetString(1),
                                dataReader.GetDecimal(2), dataReader.GetDecimal(3),
                                dataReader.GetInt32(4), dataReader.GetInt64(5)));
                        }
                    }
                }
            }

            return products;
        }

        // para despues usarlo en un delegado en los metodos de list
        public static bool FromUser(Product prod, long id)
        {
            return prod.UserId == id;
        }

        public static void ShowProducts(List<Product> products)
        {
            if (products.Count > 0)
            {
                Console.WriteLine("{0, -5}{1, -30}{2, -20}{3, -20}{4, -20}",
                "Id", "Description", "Cost", "Selling Price", "Stock");
                foreach (Product product in products)
                {
                    Console.WriteLine(product);
                }
                Console.WriteLine("\n\n");
            }
            else
                Console.WriteLine("%%% NO HAY PRODUCTOS %%%\n\n");
            
        }

    }
}
