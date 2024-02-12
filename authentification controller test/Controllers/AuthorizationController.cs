using authentification_controller_test.Data;
using authentification_controller_test.Entities;
using authentification_controller_test.Handlers;
using authentification_controller_test.Interfaces;
using Azure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Serialization;

namespace authentification_controller_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IGenericRepository<User> _userRepository;

        public AuthorizationController(IGenericRepository<User> repository)
        {
            _userRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        [HttpPost("Registration")]
        public async Task<IActionResult> Post(string username, string password)
        {
            if (Validator.Validate(username) && Validator.Validate(password))
                {
                if (await _userRepository.AnyAsync(u => u.Login == username))
                {
                    return Conflict("Такой пользователь уже существует.");
                }
                Random rand = new Random();
                User user = new User();
                user.Login = username;
                user.Password = PasswordHandler.HashPassword(password);
                user.UserRoles = new List<UserRole>
                    {
                        new UserRole{RoleId = 2, UserId = user.UserId} //автоматом выдает простого юзера
                    };
                await _userRepository.CreateAsync(user);
                return Ok("Успешная регистрация.");
            }
            else
            {
                return BadRequest("Одно из введенных данных пустое");
            }
        }

        [HttpGet("ById")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
            {
                BadRequest("Пользователя с таким идентификатором нет");
            }
            return Ok(user);
        }

        [HttpDelete("Delete Account")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
            {
                BadRequest("Пользователя с таким идентификатором нет");
            }
            await _userRepository.DeleteAsync(user);
            return Ok("Запись успешно удалена");
        }
        [HttpPut("Reset Password")]
        public async Task<IActionResult> Put(string Username, string newpassword)
        {
            if(Validator.Validate(Username) && Validator.Validate(newpassword))
            {
                var user = await _userRepository.FirstOrDefaultAsync(u => u.Login == Username);
            if(user == null)
            {
                return BadRequest("Данные неправильно введены");
            }
            user.Password = PasswordHandler.HashPassword(newpassword);
            await _userRepository.UpdateAsync(user);
            return Ok("Сброс пароля успешен");
            }
            return BadRequest("Введенные данные неверны.");

        }
        [HttpPost("Authorization")]
        public async Task<IActionResult> Authorize(string username, string Password)
        {
            if (Validator.Validate(username) && Validator.Validate(Password))
            {
                var user = await _userRepository.FirstOrDefaultAsync(u => u.Login == username && u.Password == PasswordHandler.HashPassword(Password));
                if (user == null)
                {
                    return BadRequest("Данные неправильно введены");
                }
                return Ok("Вход успешен");
            }
            else { return BadRequest("Введенные данные неверны."); }
        }
    }
}
