using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace gRPC
{
    public class UserService : Users.UsersBase
    {
        private readonly ILogger<UserService> _logger;
        public UserService(ILogger<UserService> logger)
        {
            _logger = logger;
        }

        List<UserResponse> GetUsersFromDb(int companyId)
        {
            List<UserResponse> users = new List<UserResponse>();
            users.Add(new UserResponse(){ FirstName = "Jason", LastName = "Volney", Address = "Some Street", Email = "some@email.com"});
            users.Add(new UserResponse(){ FirstName =  "Carl", LastName =  "Jackson", Address = "Some Street", Email = "some@email.com"});
            users.Add(new UserResponse(){ FirstName = "Issaac", LastName = "Newton", Address = "Some Street", Email = "some@email.com"});
            users.Add(new UserResponse(){ FirstName = "Abraham", LastName = "Lincoln", Address = "Some Street", Email = "some@email.com"});
            users.Add(new UserResponse(){ FirstName = "Benjamin", LastName = "Franklin", Address = "Some Street", Email = "some@email.com"});
            users.Add(new UserResponse(){ FirstName = "Frèderic", LastName = "Chopin", Address = "Some Street", Email = "some@email.com"});
            users.Add(new UserResponse(){ FirstName = "Wolfgang", LastName = "Mozart", Address = "Some Street", Email = "some@email.com"});
            users.Add(new UserResponse(){ FirstName = "Ludwig", LastName = "Beethoven", Address = "Some Street", Email = "some@email.com"});
            users.Add(new UserResponse(){ FirstName = "Karl", LastName = "Marx", Address = "Some Street", Email = "some@email.com"});
            users.Add(new UserResponse(){ FirstName = "Charles", LastName = "Darwin", Address = "Some Street", Email = "some@email.com"});
            return users;
        }

        public override async Task getUsers(UserRequest request, IServerStreamWriter<UserResponse> responseStream, ServerCallContext context)
        {
          // Get Users from the database.
            var users = GetUsersFromDb(request.CompanyId);
            foreach(UserResponse user in users)
            {
                //emulate a long running process to do something with each user.
                await Task.Delay(1000);
                await responseStream.WriteAsync(user);
            }

            //
        }
    }
}
