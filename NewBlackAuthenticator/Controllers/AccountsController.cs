using System;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using dotAPNS;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NewBlackAuthenticator.Data;
using NewBlackAuthenticator.Models;

namespace NewBlackAuthenticator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("ApiCorsPolicy")]

    public partial class AccountsController : ControllerBase
    {
        private readonly NBADBcontext _context;

        public AccountsController(NBADBcontext context)
        {
            _context = context;
        }

        // POST: api/Accounts
        [HttpPost]
        public ActionResult<User> CheckCredentials(LoginCredentials loginCredentials)
         {
            var username = loginCredentials.Username;
            var password = Sha256Hash(loginCredentials.Password);

            var user = _context.Users
                .Where(r => r.Username == username)
                .FirstOrDefault();

            if (user == null)
            {
                return NotFound();
            }
            else
            {
                if (user.Password == password)
                {
                    SendPush(user.DeviceToken2);
                    return user;
                }
            }
            return NotFound();
        }

        public async void SendPush(string deviceToken)
        {
            var options = new dotAPNS.ApnsJwtOptions()
            {
                // bundleID of iOS app to get push on iPhone
                BundleId = "com.natalia.NewBlackAuthenticator",
                // bundleID of iOS app to get push on apple watch
                //BundleId = "com.natalia.NewBlackAuthenticator.watchkitapp",
                CertFilePath = @"C:\Users\notas\Desktop\New Black\AuthKey_2N5WTJ4J6S.p8", // use either CertContent or CertFilePath, not both
                KeyId = "2N5WTJ4J6S",
                TeamId = "J6XDULFLT4"
            };

            var apns = ApnsClient.CreateUsingJwt(new HttpClient(new WinHttpHandler()), options).UseSandbox();

            var push = new ApplePush(ApplePushType.Alert)
    .AddAlert("Hi there! Is that you logging in?", "Hello from the other side :)")
    .AddSound("default")
    .AddToken(deviceToken);
            //.AddToken("e707a1e7a8bbe63accead3c1204cfb84aac88d13bdd4890390a756b1b3d0c1ca"); // // iPhone token
            //.AddToken("5a97844a163127ea851ccf1bc484cd62e6a06d39e16e55e0dc152bafcc3a46dd"); // Apple watch token

            try
            {
                var response = await apns.SendAsync(push);
                if (response.IsSuccessful)
                {
                    Console.WriteLine("An alert push has been successfully sent!");
                }
                else
                {
                    switch (response.Reason)
                    {
                        case ApnsResponseReason.BadCertificateEnvironment:
                            // The client certificate is for the wrong environment.
                            // TODO: retry on another environment
                            break;
                        // TODO: process other reasons we might be interested in
                        default:
                            throw new ArgumentOutOfRangeException(nameof(response.Reason), response.Reason, null);
                    }
                    Console.WriteLine("Failed to send a push, APNs reported an error: " + response.ReasonString);
                }
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine("Failed to send a push: HTTP request timed out.");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Failed to send a push. HTTP request failed: " + ex);
            }
            catch (ApnsCertificateExpiredException)
            {
                Console.WriteLine("APNs certificate has expired. No more push notifications can be sent using it until it is replaced with a new one.");
            }
        }

        public string Sha256Hash(string password)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

    }
}
