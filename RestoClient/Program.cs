using RestoShared;
using RestoShared.DTO;
using RestoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var s = RestoManager.Security;
            string EmployeeNumber;

            bool IsLogged = false;

            while (!IsLogged) 
            {
                Console.Clear();

                Console.WriteLine("---------------------------");
                Console.WriteLine("LOGIN\n");
                Console.WriteLine("---------------------------");
                Console.Write("Enter your employee number: ");
                EmployeeNumber = Console.ReadLine();

                Console.Clear();

                Console.WriteLine("---------------------------");
                ServiceResponse<bool> LoginResponse = s.Login(EmployeeNumber);
                Console.WriteLine(LoginResponse.Message);
                IsLogged = LoginResponse.Data;
                Console.ReadKey();
                Console.WriteLine("---------------------------");

                Console.Clear();
            }

            Console.WriteLine($"Welcome! {s.LoggedUser.FirstName}");

            int Opcion;
            if (s.IsLoggedAsManager)
            {
                Console.WriteLine("---------------------------");
                Console.WriteLine("Manager menu");
                Console.WriteLine("---------------------------");
                Console.WriteLine("1. Employees");
                Console.WriteLine("2. Orders");
                Console.WriteLine("3. Products");
                Console.WriteLine("4. Reports");
                Console.WriteLine("5. Logout");
                Console.WriteLine("---------------------------");
                Console.Write("Enter an option: ");
                Opcion = int.Parse(Console.ReadLine());
                /*
                if (Opcion == 1)
                {
                    if (s.IsAuthorized(Route.EMPLOYEES))
                    {
                        Console.WriteLine("Employees");
                    }
                    else
                    {
                        Console.WriteLine("You are not authorized to access this route");
                    }
                }
                */

                if (Opcion == 5)
                {
                    var response = s.Logout();
                    Console.Clear();
                    Console.WriteLine(response.Message);
                    Console.ReadKey();
                    
                    Console.Clear();
                    Console.WriteLine("---------------------------");
                    Console.WriteLine("LoggedUserService");
                    Console.Write("is logged: ");
                    Console.Write(s.IsLogged);
                    Console.WriteLine("Firstname");
                    Console.WriteLine(s.LoggedUser.FirstName);
                };
            }
            else
            {
                Console.WriteLine("---------------------------");
                Console.WriteLine("Waiter menu");
                Console.WriteLine("---------------------------");
                Console.WriteLine("1. Employees");
                Console.WriteLine("2. Logout");
                Console.WriteLine("---------------------------");
                Console.Write("Enter an option: ");
                Opcion = int.Parse(Console.ReadLine());

                /*
                if (Opcion == 1)
                {
                    if (s.IsAuthorized(RouteName.EMPLOYEES))
                    {
                        Console.WriteLine("Employees");
                    }
                    else
                    {
                        Console.WriteLine("You are not authorized to access this route");
                    }
                }
                */

                if (Opcion == 5)
                {
                    var response = s.Logout();
                    Console.Clear();
                    Console.WriteLine(response.Message);
                    Console.ReadKey();
                    return;
                };
            }
           
        }
    }
}
