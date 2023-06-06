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
        private readonly IResourceService _resourceService;
        private readonly IRegisterService _registerService;

        public App(IUserService userService, IResourceService resourceService, IRegisterService registerService)
        {
            _userService = userService;
            _resourceService = resourceService;
            _registerService = registerService;
        }

        public async Task Start()
        {
            var getUsers = Task.Run(async () => await _userService.GetUsersByPage(2)); // Task 1
            var getUser1 = Task.Run(async () => await _userService.GetUserById(2)); // Task 2   
            var getUser2 = Task.Run(async () => await _userService.GetUserById(23)); // Task 3
            var getResources = Task.Run(async () => await _resourceService.GetResourcesList()); // Task 4
            var getResource1 = Task.Run(async () => await _resourceService.GetResourceById(2)); // Task 5
            var getResource2 = Task.Run(async () => await _resourceService.GetResourceById(23)); //Task 6
            var createUser = Task.Run(async () => await _userService.CreateUser("Morpheus", "leader")); // Task 7
            var updateUser = Task.Run(async () => await _userService.UpdateUser(2, "Morpheus", "Zion resident")); // Task 8
            var patchUser = Task.Run(async () => await _userService.PatchUpdateUser(2, "Morpheus", "zion resident")); // Task 9
            var deleteUser = Task.Run(async () => await _userService.DeleteUserById(2)); // Task 10
            var registerUser1 = Task.Run(async () => await _registerService.RegisterUser("eve.holt@reqres.in", "pistol")); // Task 11
            var registerUser2 = Task.Run(async () => await _registerService.RegisterUser("sydney@fife")); // Task 12
            var loginUser1 = Task.Run(async () => await _registerService.LoginUser("eve.holt@reqres.in", "cityslicka")); // Task 13
            var loginUser2 = Task.Run(async () => await _registerService.LoginUser("peter@klaven")); // Task 14
            var getUsersDelayed = Task.Run(async () => await _userService.GetUsersDelayed()); // Task 15

            await Task.WhenAll(
                getUsers,
                getUser1,
                getUser2,
                getResources,
                getResource1,
                getResource2,
                createUser,
                updateUser,
                patchUser,
                deleteUser,
                registerUser1,
                registerUser2,
                loginUser1,
                loginUser2,
                getUsersDelayed);
        }
    }
}
