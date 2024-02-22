using System.Security.Claims;
using Trabea.Business.Interfaces;

namespace Trabea.Presentasion.Web.Services {
    public class Helper {
        
        //protected string GetUsername() {
        //    var claims = User.Claims;
        //    foreach (var claim in claims) {
        //        if (claim.Type == ClaimTypes.NameIdentifier) {
        //            return claim.Value;
        //        }
        //    }
        //    return "DefaultUsername";
        //}


        protected string GenerateEmail(string name, IAccountRepository account) {
            string generatedEmail = $"{name}@trabea.com";
            var acc = account.GetById(generatedEmail);

            int counter = 1;
            while (acc != null) {
                generatedEmail = $"{name}{counter}@trabea.com";
                acc = account.GetById(generatedEmail);
                counter++;
            }

            return generatedEmail;
        }
    }
}
