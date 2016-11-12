using System;
using FearServer.Models;
using FearServer.Models.Params;
using FearServer.Repositories;
using Nancy;
using Nancy.ModelBinding;

namespace FearServer.Modules
{
    public class HomeModule : NancyModule
    {
        private readonly InMemoryUserRepository _inMemoryUserRepository;
    
        public HomeModule(InMemoryUserRepository inMemoryUserRepository)
        {
            _inMemoryUserRepository = inMemoryUserRepository;

            Get["/"]            = GetIndex;
            Get["/Register"]    = GetRegister;
            Post["/Register"]   = PostRegister;
        }

        private dynamic GetIndex(dynamic _)
        {
            return View["Index"];
        }

        private dynamic GetRegister(dynamic _)
        {
            return View["Register", RegisterViewModel.Empty()];
        }

        private dynamic PostRegister(dynamic _)
        {
            var registerParams = this.Bind<RegisterParams>();

            if (_inMemoryUserRepository.Exist(registerParams.NormalizedPseudo))
                return View["Register", RegisterViewModel.PseudoAlreadyExist()];

            _inMemoryUserRepository.Add(registerParams.NormalizedPseudo);

            return View["Register", RegisterViewModel.Successful(AuthToken.Generate(registerParams.NormalizedPseudo))];
        }
    }
}
