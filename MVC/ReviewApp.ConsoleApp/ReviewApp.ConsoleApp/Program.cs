using System;
using System.Collections.Generic;
using ReviewApp.DataAccess.Entities;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ReviewApp.DataAccess;
using ReviewApp.Domain;
using ReviewApp.ConsoleApp;

//namespace ReviewApp.ConsoleApp
{
    string connectionString = File.ReadAllText("D:/Revature/P1/P1-time2/ReviewApp.ConsoleApp/Reviewdb-connection-string.txt");
    var options = new DbContextOptionsBuilder<ReviewDbContext>()
        .LogTo(message => Debug.WriteLine(message))
        .UseSqlServer(connectionString)
        .Options;
    var context = new ReviewDbContext(options);
    IReviewRepo repo = new ReviewRepo(context);
    IMenu menu = new MainMenu(new ReviewRepo(context));
    menu.Start();

    /*class Program
    {
        private static IReviewRepo _repo;
        public Program(IReviewRepo repo)
        {
            _repo = repo;
        }

        static List<string> reviews = new List<string>();
        static void Main(string[] args)
        {
            // dependencies
            string connectionString = File.ReadAllText("D:/Revature/P1/P1-time2/ReviewApp.ConsoleApp/Reviewdb-connection-string.txt");
            var options = new DbContextOptionsBuilder<ReviewDbContext>()
                .LogTo(message => Debug.WriteLine(message))
                .UseSqlServer(connectionString)
                .Options;
            var context = new ReviewDbContext(options);
            IReviewRepo repo = new ReviewRepo(context);
            // Manage UI

            bool repeat = true;
            do
            {
                Console.WriteLine("Welcome to Our Restaurant Review App!");
                Console.WriteLine("[0] Exit");
                Console.WriteLine("[1] Add a User");
                Console.WriteLine("[2] Find Restaurants");
                Console.WriteLine("[3] Add a Review");
                Console.WriteLine("[4] Admin");


                switch (Console.ReadLine())
                {
                    case "0":
                        Console.WriteLine("Goodbye!");
                        repeat = false;
                        break;

                    case "1":
                        AddAUser();
                        break;

                    case "2":
                        //FindARestaurant();
                        break;

                    case "3":
                        AddAReview();

                        //Console.WriteLine("Please enter your review");
                        //string input = Console.ReadLine();
                        //
                        //reviews.Add(input);
                        //if (reviews.Count == 0)
                        //{
                        //    Console.WriteLine("None");
                        //}
                        //foreach (var review in reviews)
                        //{
                        //    Console.WriteLine(review);
                        //}
                        break;

                    default:
                        Console.WriteLine("Please select number to go to where you want to go to or press 0 to exit");
                        break;
                }
            } while (repeat);
        }
        public static void AddAReview()
        {

            Console.WriteLine("Please enter your review");
            string input = Console.ReadLine();
            Console.WriteLine("-------");

            //Review reviewToAdd = new Review(input);
            //reviewToAdd = AddAReview(reviewToAdd);
            reviews.Add(input);
            if (reviews.Count == 0)
            {
                Console.WriteLine("None");
            }
            foreach (var review in reviews)
            {
                Console.WriteLine(review);
            }
        }
        private static void AddAUser()
        {
            string input1;
            string input2;
            string input3;
            string input4;
            string input5;
            ReviewApp.Domain.Customer customerToAdd;
            Console.WriteLine("Enter your information to add");


            Console.WriteLine("Name: ");
            input1 = Console.ReadLine();
            Console.WriteLine("LastName:");
            input2 = Console.ReadLine();
            Console.WriteLine("username:");
            input3 = Console.ReadLine();
            Console.WriteLine("Email:");
            input4 = Console.ReadLine();
            Console.WriteLine("Password:");
            input5 = Console.ReadLine();
            //} while ((String.IsNullOrWhiteSpace(input1)) && (String.IsNullOrWhiteSpace(input2)) && (String.IsNullOrWhiteSpace(input3)) && (String.IsNullOrWhiteSpace(input4)));

            customerToAdd = new ReviewApp.Domain.Customer(input1, input2, input3, input4, input5);



            //var customer = new DataAccess.Entities.Customer { FirstName = input1, LastName = input2, UserName = input3, Email = input4, Password = input5 };
            if (customerToAdd is not null)
            {
                _repo.AddAUser(customerToAdd);
            }

            Console.WriteLine("Your account has been added");
        }*/
}



