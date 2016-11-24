using Nancy;
using Nancy.ErrorHandling;
using Nancy.ViewEngines;

namespace FearServer
{
    public class PageNotFoundHandler : DefaultViewRenderer, IStatusCodeHandler
    {
        public PageNotFoundHandler(IViewFactory factory)
            : base(factory)
        {
        }

        public bool HandlesStatusCode(HttpStatusCode statusCode, NancyContext context)
        {
            return statusCode == HttpStatusCode.NotFound;
        }

        public void Handle(HttpStatusCode statusCode, NancyContext context)
        {
            var response = RenderView(context, "Errors/NotFound.html");
            response.StatusCode = HttpStatusCode.NotFound;
            context.Response = response;
        }
    }
}
