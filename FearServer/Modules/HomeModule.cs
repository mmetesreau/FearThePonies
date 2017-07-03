using FearServer.Models;
using FearServer.Models.Params;
using FearServer.Repositories;
using Nancy;
using Nancy.ModelBinding;

namespace FearServer.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule(InMemoryUserRepository inMemoryUserRepository, InMemoryScoreRepository inMemoryScoreRepository)
        {
            Get("/", _ => View["Index"]);

            Get("/Dashboard", _ => View["Dashboard", new DashboardViewModel(inMemoryScoreRepository.GetAll())]);

            Get("/Register", _ => View["Register", RegisterViewModel.Empty()]);

            Post("/Register", _ => 
            {
                var registerParams = this.Bind<RegisterParams>();

                if (inMemoryUserRepository.Exist(registerParams.NormalizedPseudo))
                    return View["Register", RegisterViewModel.PseudoAlreadyExist()];

                inMemoryUserRepository.Add(registerParams.NormalizedPseudo);
                inMemoryScoreRepository.Add(registerParams.NormalizedPseudo, 0);

                return View["Register", RegisterViewModel.Successful(AuthToken.Generate(registerParams.NormalizedPseudo))];
            });
        }
    }
}
