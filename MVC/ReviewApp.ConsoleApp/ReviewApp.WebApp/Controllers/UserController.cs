using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReviewApp.Domain;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ReviewApp.WebApp.Controllers
{
    public class UserController : Controller
    {
        public static int userid;
        private readonly IReviewRepo _repo;
        private readonly ILogger<UserController> _logger;
        public UserController(IReviewRepo repo, ILogger<UserController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [Authorize ( Roles = "Admin")]
        public IActionResult Index()
        {
            var users = _repo.GetAllCustomers();

            return View(users);
            
        }
        public IActionResult Result()
        {
            TempData["Message"] = "you login successfully!";
            return View();
        }

        // Create an Account
        public IActionResult Details(int id)
        {

            return View(_repo.GetAllCustomers().First(x => x.Id == id));
        }
        [HttpGet]
        public IActionResult CreateAnAccount()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAnAccount(ReviewApp.Domain.Customer customer)
        {
         
            //get the id of the customer who has just been added to return the details page
            try
            {
                _repo.AddAUser(customer);
            }
            catch( InvalidOperationException e)
            {
                
                ModelState.AddModelError("Email", e.Message);
                ModelState.AddModelError("Username", e.Message);
                _logger.LogError("Annotation Validation Error");
                return View();
            }
            List<ReviewApp.Domain.Customer> customers = _repo.GetAllCustomers();
            int id = customers[customers.Count - 1].Id;
            return RedirectToAction("Details", new { id });
        }

        //Log-in
        
        [HttpGet("login")]
        public IActionResult LogIn(string returnUrl)
        {
            
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost("login")]
        public async Task<IActionResult> Validate(string username, string password, string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            
            var user = _repo.SearchUsersByUserName(username);
            userid = user.Id; //stored this value for add reference to ReviewJoin table

            if (username == user.UserName && password == user.Password)
            {
                var claims = new List<Claim>();
                //claims.Add(new Claim("name", user.LastName));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, username));
                ViewBag.user = claims;
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                if (returnUrl == null) return RedirectToAction("Result");
                return Redirect(returnUrl);
                
            }
            
            TempData["Error"] = "Login fail. Password or Username is invalid";
            return View("login");
        }

        //log out
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

        //Denied page if a user is not an admin
        [HttpGet("denied")]
        public IActionResult Denied()
        {
            return View();
        }

        //Confirm page
        [HttpGet]
        public IActionResult ConfirmPage()
        {
            return View();
        }

        //Delete users
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation("Admin get access to the page");

            var customer = _repo.GetAllCustomers().First(x => x.Id == id);
            return View(customer);
        }
        [HttpPost]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            ReviewApp.Domain.Customer foundCustomer = _repo.SearchUsersById(id);
            List<ReviewApp.Domain.ReviewJoin> reviewjoins = _repo.GetReviewJoins();
            for (int i = 0; i < reviewjoins.Count; i++)
            {
                if (reviewjoins[i].CustomerId == foundCustomer.Id)
                {
                    _repo.DeleteReviewJoin(reviewjoins[i].Id);
                    _repo.DeleteReview(reviewjoins[i].ReviewId);
                }
            }
            _repo.DeleteUser(id);
            return RedirectToAction("ConfirmPage");
        }

        //Update users' email
        [HttpGet]
        public IActionResult UpdateEmail(int id)
        {
            var customer = _repo.GetAllCustomers().First(x => x.Id == id);
            return View();
        }
        [HttpPost]
        public IActionResult UpdateEmail(string email, int id)
        {
            _repo.Update(email, id);
            return View();
        }

        public IActionResult MyAccount()
        {
            var customer = _repo.SearchUsersById(userid);
            return View(customer);
        }

    }
}
