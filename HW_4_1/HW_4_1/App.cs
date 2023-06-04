using HW_4_1.Dtos.Responses;
using HW_4_1.Services;
using HW_4_1.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HW_4_1
{
    public class App
    {
        private readonly IUserService _userService;

        public App(IUserService userService)
        {
            _userService = userService;
        }

        public async Task Start()
        {
            await _userService.GetUsersByPage(2); // Task 1
            await _userService.GetUserById(2); // Task 2   
            await _userService.GetUserById(23); // Task 3
            await _userService.GetResourcesList(); // Task 4
            await _userService.GetResourceById(2); // Task 5
            await _userService.GetResourceById(23); //Task 6
            await _userService.CreateUser("Morpheus", "leader"); // Task 7
            await _userService.UpdateUser(2, "Morpheus", "Zion resident"); // Task 8
            await _userService.PatchUpdateUser(2, "Morpheus", "zion resident"); // Task 9
            await _userService.DeleteUserById(2); // Task 10
            await _userService.RegisterUser("eve.holt@reqres.in", "pistol"); // Task 11
            await _userService.RegisterUser("sydney@fife"); // Task 12
            await _userService.LoginUser("eve.holt@reqres.in", "cityslicka"); // Task 13
            await _userService.LoginUser("peter@klaven"); // Task 14
            await _userService.GetUsersDelayed(); // Task 15
        }
    }
}
