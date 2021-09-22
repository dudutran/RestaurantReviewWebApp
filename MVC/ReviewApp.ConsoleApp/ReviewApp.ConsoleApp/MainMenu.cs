using ReviewApp.DataAccess.Entities;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ReviewApp.DataAccess;
using ReviewApp.Domain;
using System;
using System.Collections.Generic;

namespace ReviewApp.ConsoleApp
{
    public class MainMenu:IMenu
    {
        private readonly IReviewRepo _repo;
        public MainMenu(IReviewRepo repo)
        {
            _repo = repo;
        }

        static List<string> reviews = new List<string>();
        public void Start()
        {
            bool repeat = true;
            do
            {
                Console.WriteLine("Welcome to Our Restaurant Review App!");
                Console.WriteLine("[0] Exit");
                Console.WriteLine("[1] Add a User");
                Console.WriteLine("[2] Find Restaurants");
                Console.WriteLine("[3] Add a Review");
                Console.WriteLine("[4] Admin");
                Console.WriteLine("[5] Search User by User Name");


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
                        FindARestaurant();
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

                    case "4":
                        GetAllCustomers();
                        break;

                    case "5":
                        SearchUserByUserName();
                        break;

                    case "6":
                        DeleteReview();
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
        
        private void AddAUser()
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
            Console.WriteLine("Password:");
            input4 = Console.ReadLine();
            Console.WriteLine("Email:");
            input5 = Console.ReadLine();
            //} while ((String.IsNullOrWhiteSpace(input1)) && (String.IsNullOrWhiteSpace(input2)) && (String.IsNullOrWhiteSpace(input3)) && (String.IsNullOrWhiteSpace(input4)));

            customerToAdd = new ReviewApp.Domain.Customer(input1, input2, input3, input4, input5);

            
            if (customerToAdd is not null)
            {
                _repo.AddAUser(customerToAdd);
            }

            Console.WriteLine($"Your account has been added {customerToAdd.Id}");
        }
        private void GetAllCustomers()
        {
            List<ReviewApp.Domain.Customer> customers = _repo.GetAllCustomers();
            foreach (ReviewApp.Domain.Customer customer in customers)
            {
                Console.WriteLine(customer.FirstName + customer.LastName + " " + customer.UserName);
            }
        }
        private void FindARestaurant()
        {
            Console.WriteLine("Enter the name of the restaurant to search: ");
            string input = Console.ReadLine();
            var foundRestaurant = _repo.FindARestaurant(input);
            if (foundRestaurant.Name is null)
            {
                Console.WriteLine($"{input} is might not on our list");
            }
            else
            {
                Console.WriteLine($"{foundRestaurant.Name}\nAddress: {foundRestaurant.Location}\nContact: {foundRestaurant.Contact}");
                Console.WriteLine("---------------------------");
            }
        }
        public void SearchUserByUserName()
        {
            string input = Console.ReadLine();
            var user = _repo.SearchUsersByUserName(input);
            Console.WriteLine($"{user.Password}");
        }
        public void DeleteReview()
        {
            _repo.DeleteReview(34);
            Console.WriteLine("it has been deleted");
        }
    }
}
       
            