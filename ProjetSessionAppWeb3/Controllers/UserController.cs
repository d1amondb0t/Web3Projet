using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjetSessionAppWeb3.Models;
using ProjetSessionAppWeb3.Respositories;
using System.Threading.Tasks;

namespace ProjetSessionAppWeb3.Controllers
{
    /// <summary>
    /// Le controlleur User qui permet de relier le modèle de User avec les vues des User et aussi de faire des manipulations sur le User
    /// </summary>
    /// <author>A. Alperen, B. Shajaan et I. Gafran</author>
    public class UserController : Controller
    {
        // GET: UtilisateurController
        private readonly IUserRepository _ur;
        public UserController(IUserRepository u)
        {
            _ur = u;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {


            return View();
        }

        /// <summary>
        /// Cette méthode permet de créer un compte
        /// </summary>
        /// <param name="username">Nom de l'utilisateur</param>
        /// <param name="email">Email de l'utilisateur</param>
        /// <param name="password">Mot de passe de l'utilisateur</param>
        /// <param name="password2">Mot de passe de l'utilisateur</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Register(string username, string email, string password, string password2)
        {
            ViewBag.ErorMessage = "";
            if (username == null || email == null || password == null || password2 == null)
            {
                ViewBag.ErrorMessage = ("Remplir tous les champs");
                return View();
            }
            if (!password.Equals(password2))
            {
                ViewBag.ErrorMessage = ("Les deux mots de passes ne sont pas les memes");
                return View();
            }
            else if (password.Equals(password2))
            {
                User userRegister = new User();
                userRegister.Email = email;
                userRegister.Username = username;
                userRegister.Password = password;
                await _ur.Create(userRegister);
            }
            return View("Login");
        }

        /// <summary>
        /// Cette méthode permet de se login
        /// </summary>
        /// <param name="username">Nom de l'utilisateur</param>
        /// <param name="password">Mot de passe de l'utilisateur</param>
        /// <returns>La page de l'utilisateur</returns>
        [HttpPost]
        public async Task<ActionResult> Login(string username, string password)
        {

            if (username == null || password == null)
            {
                ViewBag.ErrorMessage = ("Remplir tous les champs");
                return View();
            }
            else
            {
                User user = await _ur.UserLogin(username, password);
                if (user.IdUser == 0)
                {
                    ViewBag.ErrorMessage = ("Vous avez entrer des informations invalides");
                    return View();
                }
                else
                {
                    //HttpContext.Session.SetInt32("userSession", user.IdUser);
                    HttpContext.Session.SetString("userSession", JsonConvert.SerializeObject(user));
                    ViewBag.userSession = ("userSession", user);
                    return View("../Home/Index");
                }
            }
        }

        [HttpGet]

        public ActionResult Logout()
        {
            ViewBag.UserSession = null;
            return View("../Home/Index");
        }

        [HttpGet]
        public ActionResult Profil () {
            var user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("userSession"));
            ViewBag.userSession = user;
            return View(); 
        }

        /// <summary>
        /// Cette méthode permet de changer des information sur l'utilisateur
        /// </summary>
        /// <param name="username">Nom de l'utilisateur</param>
        /// <param name="email">Email de l'utilisateur</param>
        /// <param name="password">Mot de passe de l'utilisateur</param>
        /// <returns>La page principale</returns>
        [HttpPost]
        public async Task<ActionResult> Update(string username, string email, string password)
        {
            User userModife = new User();
            var user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("userSession"));
            int id = user.IdUser;

            userModife.IdUser = id;
            userModife.Email = email;
            userModife.Username = username;
            userModife.Password = password;
            await _ur.Update(userModife);
            HttpContext.Session.SetString("userSession", JsonConvert.SerializeObject(userModife));
            ViewBag.userSession = ("userSession", userModife);

            return View("../Home/Index");
        }
    }
}
