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
        private const string FinalAnswer = "42";

        public StepsModule(AppConfiguration appConfiguration, InMemoryUserRepository inMemoryUserRepository, InMemoryScoreRepository inMemoryScoreRepository, IHubContext notificationHub)
        {
            Func<string,string,int,bool> checkAnswerAndNotify = (string userAnswer, string realAnswer, int step) =>
            {
                var authToken = this.Request.Query["authtoken"];

                var pseudo = AuthToken.Parse(authToken);

                if (!inMemoryUserRepository.Exist(pseudo) || string.IsNullOrEmpty(userAnswer) || userAnswer != realAnswer)
                    return false;
                
                inMemoryScoreRepository.Update(pseudo, step);
                notificationHub.Clients.All.send(new { pseudo = pseudo, step = step });

                return true;
            };

            Get("/endoftheworld", _ => appConfiguration.EndOfTheWorld.ToString("yyyy/MM/dd HH:mm:ss"));

            Get("/step1", _ =>
            {
                var pincode = this.Request.Query["pincode"];

                if (!checkAnswerAndNotify(pincode, Pincode, 1))
                    return View["Step1"];

                return Response.AsRedirect("/Step9861");
            });

            Get("/step2", _ => View["WrongStep"]);

            Get("/step3", _ => View["WrongStep"]);

            Get("/step9861", _ =>
            {
                var password = this.Request.Query["password"];

                if (!checkAnswerAndNotify(password, Password, 2))
                    return View["Step2"];

                return Response.AsRedirect("/Step2345");
            });

            Get("/step2345", _ => View["Step3"]);

            Get("/dsamfds", _ =>
            {
                var finalAnswer = this.Request.Query["answer"];

                if (!checkAnswerAndNotify(finalAnswer, FinalAnswer, 3))
                    return Response.AsRedirect("/step2345");
                
                return View["Step4"];
            });
        }
    }
}
