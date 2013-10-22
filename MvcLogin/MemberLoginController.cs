using MvcLogin;
using System;
using System.Web.Mvc;
using System.Web.Security;
using Umbraco.Web.Mvc;

    //I could not get plugincontroller to work [PluginController("MvcLogin")]
    public class MemberLoginController : Umbraco.Web.Mvc.SurfaceController
    {

        // The MemberLogin Action returns the view, which we will create later. It also instantiates a new, empty model for our view:

        [HttpGet]
        [ActionName("MemberLogin")]
        public ActionResult MemberLoginGet()
        {
            var model = new MemberLoginModel { ReturnUrl = Request.Url.AbsoluteUri };
            return PartialView("MemberLogin", model);
        }

        // The MemberLogout Action signs out the user and redirects to the site home page:

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MemberLogout()
        {
            TempData["MemberLoginMessage"] = umbraco.library.GetDictionaryItem("You have been logged out");
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToCurrentUmbracoPage();
        }

        // The MemberLoginPost Action checks the entered credentials using the standard Asp Net membership provider and redirects the user to the same page. Either as logged in, or with a message set in the TempData dictionary:

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("MemberLogin")]
        public ActionResult MemberLoginPost(MemberLoginModel model)
        {
            if (Membership.ValidateUser(model.Username, model.Password))
            {

                FormsAuthentication.SetAuthCookie(model.Username, model.RememberMe);

                if (!string.IsNullOrEmpty(model.ReturnUrl))
                {
                    var redirect = new Uri(model.ReturnUrl);
                    if (redirect.Host == Request.Url.Host) return Redirect(model.ReturnUrl);
                }
                return RedirectToCurrentUmbracoPage();

            }
            else
            {
                TempData["MemberLoginMessage"] = umbraco.library.GetDictionaryItem("Invalid username or password");
                return RedirectToCurrentUmbracoPage();
            }
        }
    }
