using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewApp.Domain
{
    public class ReviewBL
    {
        private readonly IReviewRepo _repo;
        public ReviewBL(IReviewRepo repo)
        {
            _repo = repo;
        }
        public Customer AddAUser(Customer customer)
        {
            return _repo.AddAUser(customer);
        }
        public List<Customer> GetAllCustomers()
        {
            return _repo.GetAllCustomers();
        }
    }
}
