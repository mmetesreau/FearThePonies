using System;
using FearServer.Repositories;
using Microsoft.AspNetCore.SignalR;
using Nancy;

namespace FearServer.Modules
{
    public class StepsModule : NancyModule
    {
        private const string Pincode = "9723";
        private const string Password = "poniesaresocute";

        public StepsModule(AppConfiguration appConfiguration, InMemoryUserRepository inMemoryUserRepository, IHubContext notificationHub)
        {
            Func<string,string,string, bool> checkAnswerAndNotify = (string userAnswer, string realAnswer, string stepName) =>
            {
                var authToken = this.Request.Query["authtoken"];

                var pseudo = AuthToken.Parse(authToken);

                if (!inMemoryUserRepository.Exist(pseudo) || string.IsNullOrEmpty(userAnswer) || userAnswer != realAnswer)
                    return false;

                notificationHub.Clients.All.send($"{pseudo} hacked {stepName}");

                return true;
            };

            Get("/endoftheworld", _ => "2017/05/19 17:00:00";

            Get("/step1", _ =>
            {
                var pincode = this.Request.Query["pincode"];

                if (!checkAnswerAndNotify(pincode, Pincode, "step 1"))
                    return View["Step1"];

                return Response.AsRedirect("/Step9861");
            });

            Get("/step2", _ => View["WrongStep"]);

            Get("/step3", _ => View["WrongStep"]);

            Get("/step9861", _ =>
            {
                var password = this.Request.Query["password"];

                if (!checkAnswerAndNotify(password, Password, "step 2"))
                    return View["Step2"];

                return Response.AsRedirect("/Step2345");
            });

            Get("/step2345", _ => View["Step3"]);
        }
    }
}
