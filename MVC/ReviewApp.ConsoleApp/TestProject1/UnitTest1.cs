using System;
using Xunit;
using ReviewApp.Domain;
using ReviewApp.DataAccess;
using ReviewApp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using ReviewApp.WebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace TestProject1
{
    public class RepoTest
    {

        private readonly DbContextOptions<ReviewDbContext> options;

        public RepoTest()
        {
            options = new DbContextOptionsBuilder<ReviewDbContext>().UseSqlite("Filename=Test.db").Options;
            Seed();
        }

      

        [Fact]
        public void AddAUserShouldAddAUser()
        {
            using (var testcontext = new ReviewDbContext(options))
            {
                IReviewRepo _repo = new ReviewRepo(testcontext);

                //Act
                _repo.AddAUser(
                    new ReviewApp.Domain.Customer
                    {
                        Id = 1,
                        FirstName = "Emma",
                        LastName = "Lee",
                        UserName = "el12",
                        Email = "emmaw@gmail.com",
                        Password = "emlee12"
                    }
                );
            }
            using (var assertContext = new ReviewDbContext(options))
            {
                ReviewApp.DataAccess.Entities.Customer customer = assertContext.Customers.FirstOrDefault(customer => customer.Id == 1);


                Assert.NotNull(customer);
                Assert.Equal(1, customer.Id);
                Assert.Equal("Emma", customer.FirstName);
                Assert.Equal("Lee", customer.LastName);
                Assert.Equal("emmaw@gmail.com", customer.Email);
            }
        }

        [Fact]
        public void GetAllRestaurantsShouldGetAllRestaurants()
        {
            using (var context = new ReviewDbContext(options))
            {
                IReviewRepo _repo = new ReviewRepo(context);
                var restaurants = _repo.GetAllRestaurants();

                Assert.Equal(2, restaurants.Count);
            }
        }
   
        private void Seed()
        {
            using (var context = new ReviewDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Restaurants.AddRange(
                    new ReviewApp.DataAccess.Entities.Restaurant
                    {
                        Id = 1,
                        Name = "JimmyJohn",
                        Location = "Cherry road",
                        Contact = "123-456",
                        Zipcode = "49544"
                    },
                    new ReviewApp.DataAccess.Entities.Restaurant
                    {
                        Id = 2,
                        Name = "Burger King",
                        Location = "Division road",
                        Contact = "789-456",
                        Zipcode = "49504"
                    }

               );
                context.SaveChanges();
            }
        }
        
    }
}

