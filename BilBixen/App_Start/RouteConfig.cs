using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace BilBixen
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var settings = new FriendlyUrlSettings
            {
                AutoRedirectMode = RedirectMode.Permanent
            };
            routes.EnableFriendlyUrls(settings);

            routes.MapPageRoute("Home_Page", "", "~/Pages/Default.aspx");
            routes.MapPageRoute("About_Page", "About", "~/Pages/About.aspx");
            routes.MapPageRoute("Contact_Page", "Contact", "~/Pages/Contact.aspx");

            routes.MapPageRoute("Login_Page", "Login", "~/Pages/Login.aspx");
            routes.MapPageRoute("ForgotPassword_Page", "ForgotPassword", "~/Pages/ForgottenPassword.aspx");

            routes.MapPageRoute("Sale_Page", "Sale", "~/Pages/CarsForSale.aspx");
            routes.MapPageRoute("Sell_Page", "Sell", "~/Pages/SellYourCar.aspx");
        }
    }
}