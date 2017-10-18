using System;
using System.Configuration;
using System.Web.Mvc;
using Stripe;

namespace StripTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public string Charge(string stripeEmail, string stripeToken, int amount)
        {
            try {
                var customers = new StripeCustomerService();
                var charges = new StripeChargeService();

                var customer = customers.Create(new StripeCustomerCreateOptions
                {
                    Email = stripeEmail,
                    SourceToken = stripeToken
                });

                var charge = charges.Create(new StripeChargeCreateOptions
                {
                    Amount = amount,
                    Description = "Test Donation",
                    Currency = "gbp",
                    CustomerId = customer.Id
                });

                return Url.Action("ThankYou");
            }
            catch
            {
                return Url.Action("Error");
            }
        }
        public ActionResult Error()
        {
            return View();
        }
        public ActionResult ThankYou()
        {
            return View();
        }
    }
}