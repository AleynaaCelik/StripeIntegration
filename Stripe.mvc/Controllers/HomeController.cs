using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.Diagnostics;

namespace Stripe.mvc.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GoPayment()
        {
            StripeConfiguration.ApiKey = "sk_test_51PXjNKJgz43nDhcBBjWuYu1IkJZgxm2L8xJVrWNJMjo9ixzCJFG6Swuc2xIqSzTn11BJNZR9WGVdscnrtcpbZ5Q300155L8nVs";
            return View();
        }

        [HttpPost]
        public ActionResult GoPayment(string productId)
        {
            var domain = "http://localhost:7108";
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    // Provide the exact Price ID (for example, pr_1234) of the product you want to sell
                    Price = "price_1PXjTpJgz43nDhcBZXoIuacV",
                    Quantity = 1,
                  },
                },
                Mode = "subscription",
                SuccessUrl = domain + "/Home/Success",
                CancelUrl = domain + "/Home/Cancel",
            };
            var service = new SessionService();
            Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Cancel()
        {
            return View();
        }
    }
}
