using FearServer.Repositories;
using Microsoft.AspNet.SignalR;
using Nancy;
using System;

namespace FearServer.Modules
{
    public class StepsModule : NancyModule
    {
        private readonly DateTime _endOfTheWorld = new DateTime(2016, 11, 12, 14, 00, 00);

        private readonly InMemoryUserRepository _inMemoryUserRepository;
        private readonly IHubContext _notificationHub;

        private const string Pincode = "9723";
        private const string Password = "poniesaresocute";

        public StepsModule(InMemoryUserRepository inMemoryUserRepository, IHubContext notificationHub)
        {
            _inMemoryUserRepository = inMemoryUserRepository;
            _notificationHub = notificationHub;

            Get["/endoftheworld"] = GetEndOfTheWorld;
            Get["/step1"] = GetStep1;
            Get["/step2"] = GetWrongStep;
            Get["/step3"] = GetWrongStep;
            Get["/step9861"] = GetStep9861;
            Get["/step2345"] = GetStep2345;
        }

        private dynamic GetEndOfTheWorld(dynamic _)
        {
            return _endOfTheWorld.ToString("yyyy/MM/dd HH:mm:ss");
        }

        private dynamic GetStep1(dynamic _)
        {
            var pincode = this.Request.Query["pincode"];

            if (CheckAnswerAndNotify(pincode, Pincode, "step 1"))
                return View["Step1"];

            return Response.AsRedirect("/Step9861");
        }

        private dynamic GetWrongStep(dynamic _)
        {
            return View["WrongStep"];
        }

        private dynamic GetStep9861(dynamic _)
        {
            var password = this.Request.Query["password"];

            if (CheckAnswerAndNotify(password, Password, "step 2"))
                return View["Step2"];

            return Response.AsRedirect("/Step2345");
        }

        private dynamic GetStep2345(dynamic _)
        {
            return View["Step3"];
        }

        private bool CheckAnswerAndNotify(string userAnswer, string realAnswer, string stepName)
        {
            var authToken = this.Request.Query["authtoken"];

            var pseudo = AuthToken.Parse(authToken);

            if (!_inMemoryUserRepository.Exist(pseudo) || string.IsNullOrEmpty(userAnswer) || userAnswer != realAnswer)
                return false;

            _notificationHub.Clients.All.send($"{pseudo} hacked {stepName}");

            return true;
        }
    }
}
