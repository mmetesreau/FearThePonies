using FearServer.Repositories;
using Microsoft.AspNet.SignalR;
using Nancy;
using System;

namespace FearServer.Modules
{
    public class StepsModule : NancyModule
    {
        private readonly DateTime _endOfTheWorld = new DateTime(2016,11,12,14,00,00);

        private readonly InMemoryUserRepository _inMemoryUserRepository;
        private readonly IHubContext _notificationHub;

        private const string Pincode = "9723";
        private const string Password = "poniesaresocute";

        public StepsModule(InMemoryUserRepository inMemoryUserRepository, IHubContext notificationHub)
        {
            _inMemoryUserRepository = inMemoryUserRepository;
            _notificationHub = notificationHub;

            Get["/endoftheworld"]   = GetEndOfTheWorld;
            Get["/step1"]           = GetStep1;
            Get["/step2"]           = GetWrongStep;
            Get["/step3"]           = GetWrongStep;
            Get["/step9861"]        = GetStep9861;
            Get["/step2345"]        = GetStep2345;
        }

        private dynamic GetEndOfTheWorld(dynamic _)
        {
            return _endOfTheWorld.ToString("yyyy/MM/dd HH:mm:ss");
        }

        private dynamic GetStep1(dynamic _)
        {
            var authToken = this.Request.Query["authtoken"];
            var pincode = this.Request.Query["pincode"];

            var pseudo = AuthToken.Parse(authToken);

            if (!_inMemoryUserRepository.Exist(pseudo) || string.IsNullOrEmpty(pincode) || pincode != Pincode)
                return View["Step1"];

            _notificationHub.Clients.All.send($"{pseudo} hacked step 1");

            return Response.AsRedirect("/Step9861");
        }

        private dynamic GetWrongStep(dynamic _)
        {
            return View["WrongStep"];
        }

        private dynamic GetStep9861(dynamic _)
        {
            var authToken = this.Request.Query["authtoken"];
            var password = this.Request.Query["password"];

            var pseudo = AuthToken.Parse(authToken);

            if (!_inMemoryUserRepository.Exist(pseudo) || string.IsNullOrEmpty(password) || password != Password)
                return View["Step2"];

            _notificationHub.Clients.All.send($"{pseudo} hacked step 2");

            return Response.AsRedirect("/Step2345");
        }

        private dynamic GetStep2345(dynamic _)
        {
            return View["Step3"];
        }
    }
}
