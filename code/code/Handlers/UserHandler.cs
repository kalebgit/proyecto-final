using code.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code.Handlers
{
    internal class UserHandler
    {
        // delegado para verificar los tipos de dato de acuerdo a la base de datos
        public delegate object Verificator(object obj);

        // connection string
        protected static string _connectionString = "Data Source=LAPTOP-VQVR3Q8R;Initial Catalog=SistemaGestion;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        // metodo para verificar correo
        public static object IsMail(object obj)
        {
            string mail = (string)obj;
            while (!mail.Contains("@"))
            {
                Console.Write("\n---------- No ingresaste correo, debe " +
                    "tener @ ----------\n" +
                    "\nPon un correo: ");
                obj = Console.ReadLine();
            }
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
        // select
        public static List<User> SelectAll()
        {
            List<User> users = new List<User>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand($"SELECT * FROM Usuario",
                    connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            users.Add(new User(dataReader.GetInt64(0), dataReader.GetString(1),
                                dataReader.GetString(2), dataReader.GetString(3),
                                dataReader.GetString(4), dataReader.GetString(5)));
                        }
                    }
                }
            }

            return users;
        }

        // para despues usarlo en un delegado en los metodos de list
        public static bool GetUser(User user, long id)
        {
            return user.Id == id;
        }
        public static User LogIn(string userName, string password)
        {
            User user;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Usuario" +
                    $" WHERE NombreUsuario = {userName} AND Contraseña = {password}", 
                    connection);
                using(SqlDataReader dataReader = command.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        Console.WriteLine("\n %%%%% HAS INICIADO SESION %%%%%\n");
                        user = new User(dataReader.GetInt64(0), dataReader.GetString(1),
                                dataReader.GetString(2), dataReader.GetString(3),
                                dataReader.GetString(4), dataReader.GetString(5));
                        return user;
                    }
                    else
                    {
                        Console.WriteLine("\n *** ERROR, NO HAY COINCIDENCIAS ***\n");
                        return null;
                    }
                        
                }
            }
        }
        public static List<Product> GetSoldProducts(long userId)
        {
            List<Product> products = new List<Product>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Producto " +
                    $"WHERE IdUsuario = {userId}", connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            products.Add(new Product(dataReader.GetInt64(0),
                                dataReader.GetString(1), dataReader.GetDecimal(2),
                                dataReader.GetDecimal(3), dataReader.GetInt32(4),
                                dataReader.GetInt64(5)));
                        }
                    }
                }
            }
            return products;
        }
        public static List<Sale> GetSales(long userId)
        {
            List<Sale> sales = new List<Sale>();
            using(SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand($"SELECT * FROM Venta " +
                    $"WHERE IdUsuario = {userId}", connection);
                using(SqlDataReader dataReader = command.ExecuteReader())
                {
                    if (dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            sales.Add(new Sale(dataReader.GetInt64(0), dataReader.GetString(1),
                                dataReader.GetInt64(2)));
                        }
                    }
                }
            }
            return sales;
        }

        public static void ShowUsers(List<User> users)
        {
            if(users.Count > 0)
            {
                Console.WriteLine("{0, -5}{1, -30}{2, -30}{3, -20}{4, -20}{5, -30}",
                "Id", "Name", "Last Name", "User Name", "Password", "Mail");
                foreach (User user in users)
                {
                    Console.WriteLine(user);
                }
                Console.WriteLine("\n\n");
            }
            else
                Console.WriteLine("%%% NO HAY USUARIOS %%%");

        }
        public static void ShowSales(List<Sale> sales)
        {
            if(sales.Count > 0)
            {
                Console.WriteLine("{0, -10}{1, -50}{2, -10}", "Id", "Comentarios", "Id Usuario");
                foreach(Sale sale in sales)
                {
                    Console.WriteLine(sale);
                }
                Console.WriteLine("\n\n");
            }
            else
                Console.WriteLine("%%% NO HAY VENTAS %%%");
        }

        
    }
}
