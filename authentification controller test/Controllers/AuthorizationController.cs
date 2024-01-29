using authentification_controller_test.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Serialization;

namespace authentification_controller_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly DataContext _dataContext;
        [HttpPost("Registration")]
        public async Task<IActionResult> Post(string username, string password)
        {
            using(_dataContext) 
            {
            if (await _dataContext.Users.AnyAsync(u=>u.login == username))
            {
                return Conflict("Такой пользователь уже существует.");
            }
            Random rand = new Random();
            User user = new User();
            user.login = username;
            user.password = PasswordHandler.HashPassword(password);
            user.UserRoles = new List<UserRole>
            {
                new UserRole{RoleId = 2, UserId = user.UserId} //автоматом выдает простого юзера
            };
            _dataContext.Users.Add(user);
            await _dataContext.SaveChangesAsync();
            return Ok("Успешная регистрация.");

            }
        }

        [HttpGet("ById")]
        public async Task<IActionResult> Get(int id)
        {
            using (_dataContext)
            {
            if (await _dataContext.Users.AnyAsync(u => u.UserId == id))
            {
               var user = _dataContext.Users.Find(id);
               return Ok(user);
            }
            return BadRequest("Пользователь не найден. Попробуйте снова.");

            }
        }
        [HttpDelete("Delete Account")]
        public async Task<IActionResult> Delete(int id)
        {
            using (_dataContext)
            {
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.UserId == id);
            if (user == null)
            {
                return BadRequest("Данные неправильно введены.");
            }
            _dataContext.Users.Remove(user);
            await _dataContext.SaveChangesAsync();
            return Ok("Запись успешно удалена");

            }
        }
        [HttpPut("Reset Password")]
        public async Task<IActionResult> Put(string Username, string newpassword)
        {
            using(_dataContext)
            {
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.login == Username);
            if (user == null)
            {
                return BadRequest("Данные неправильно введены");
            }
            user.password = PasswordHandler.HashPassword(newpassword);
            await _dataContext.SaveChangesAsync();
            return Ok("Сброс пароля успешен");

            }
        }
        [HttpPost("Authorization")]
        public async Task<IActionResult> Authorize(string username, string Password)
        {
            using (_dataContext)
            {
                var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.login == username && u.password == PasswordHandler.HashPassword(Password));
                if (user == null)
                {
                    return BadRequest("Данные неправильно введены");
                }
                return Ok("Вход успешен");
            }
        }
    }
}
