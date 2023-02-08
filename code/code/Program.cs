using code.Handlers;
using code.Models;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.Arm;

namespace code
{
    internal class Program
    {
        public delegate bool UserFilter(User user, long id);
        public delegate bool ProductFilter(Product prod, long id);
        
        static void Main(string[] args)
        {
            // handlers
            UserHandler userHandler = new UserHandler();
            ProductHandler prodHandler = new ProductHandler();

            // tipo de datos de delegado
            UserFilter userFilter = new UserFilter(UserHandler.GetUser);
            ProductFilter prodFilter = new ProductFilter(ProductHandler.FromUser);

            // listas de las clases
            List<User> users = new List<User>();
            List<Product> products = new List<Product>();

            Console.WriteLine("============== PROGRAMA DE SISTEMA DE GESTION " +
                "==============\n\n");
            int opcion, id;
            do
            {
                
                Console.WriteLine("Ingresa la opcion que deseas realizar\n\n" +
                "1) Ver usuario de acuerdo a un id\n" +
                "2) Ver productos ingresados por un usuario (usando id de usuario)\n" +
                "3) Ver productos vendidos por un usuario\n" +
                "4) Ver ventas realizadas por un usuario\n\n" +
                "-- OPCIONES GENERALES --\n" +
                "5) Iniciar sesion\n\n");
                opcion = IsInt(Console.ReadLine());
                switch (opcion)
                {
                    case 1:
                        Console.WriteLine("\n\n------ Funcion para ver usuario de" +
                            " acuerdo a un id ------\n");

                        users = UserHandler.SelectAll();
                        do
                        {
                            Console.Write("Escribe el usuario: ");
                        } while (int.TryParse(Console.ReadLine(), out id));

                        for (int i = 0; i < users.Count; i++)
                        {
                            if (!(userFilter(users[i], id)))
                            {
                                users.RemoveAt(i);
                            }
                        }

                        UserHandler.ShowUsers(users);
                        break;
                    case 2:
                        Console.WriteLine("\n\n------ Funcion para ver productos " +
                            "ingresados por un usuario (usando id de usuario) ------\n");

                        products = prodHandler.SelectAll();
                        do
                        {
                            Console.Write("Escribe el usuario: ");
                        } while (int.TryParse(Console.ReadLine(), out id));

                        for (int i = 0; i < products.Count; i++)
                        {
                            if (!(prodFilter(products[i], id)))
                            {
                                products.RemoveAt(i);
                            }
                        }

                        ProductHandler.ShowProducts(products);
                        break;
                    case 3:
                        Console.WriteLine("\n\n------ Funcion para ver productos " +
                            "vendidos por un usuario ------\n");
                        do
                        {
                            Console.Write("Escribe el usuario: ");
                        } while (int.TryParse(Console.ReadLine(), out id));

                        users = UserHandler.SelectAll();
                        for (int i = 0; i < users.Count; i++)
                        {
                            if (!userFilter(users[i], id))
                            {
                                users.RemoveAt(i);
                            }
                        }

                        if (users.Count == 1)
                        {
                            ProductHandler.ShowProducts(users[0].SoldProducts);
                        }
                        break;
                    case 4:
                        Console.WriteLine("\n\n------ Funcion para ver ventas " +
                            "realizadas por un usuario ------\n");
                        do
                        {
                            Console.Write("Escribe el usuario: ");
                        } while (int.TryParse(Console.ReadLine(), out id));

                        users = UserHandler.SelectAll();
                        for (int i = 0; i < users.Count; i++)
                        {
                            if (!userFilter(users[i], id))
                            {
                                users.RemoveAt(i);
                            }
                        }

                        if (users.Count == 1)
                        {
                            UserHandler.ShowSales(users[0].Sales);
                        }

                        break;
                    case 5:
                        Console.WriteLine("\n\n------ Funcion para inciar sesion " +
                            "------\n");
                        Console.Write("Ingresa nombre de usuario: ");
                        string userName = Console.ReadLine();
                        Console.Write("Ingresa contrasenia: ");
                        string password = Console.ReadLine();

                        User user = UserHandler.LogIn(userName, password);

                        Console.WriteLine("\nInformacion del usuario que ha iniciado" +
                            "sesion");
                        Console.WriteLine("{0, -5}{1, -30}{2, -30}{3, -20}{4, -20}" +
                            "{5, -30}", "Id", "Name", "Last Name", "User Name",
                            "Password", "Mail");
                        Console.WriteLine(user);
                        break;
                    default:
                        break;

                }
                Console.WriteLine("\n\n-- Quieres repetir alguna operacion? si(1) no (0)--");
                opcion = IsInt(Console.ReadLine());
            } while (opcion == 1);
            
        }

        public static int IsInt(string valorIngresado)
        {
            int num;
            bool boolean;
            boolean = int.TryParse(valorIngresado, out num);
            while (!boolean)
            {
                Console.Write("\n---------- No ingresaste valor correcto ----------\n" +
                    "\nPon un valor correcto: ");
                boolean = int.TryParse(Console.ReadLine(), out num);
                if (num <= 0 || num > 5)
                    boolean = false;
            }
            return num;
        }
    }
}