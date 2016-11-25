namespace FearServer.Models
{
    public class RegisterViewModel
    {
        public bool IsSuccessful { get; set; }
        public string Error { get; set; }
        public string AuthToken { get; set; }

        public static RegisterViewModel Empty()
        {
            return new RegisterViewModel();
        }

        public static RegisterViewModel PseudoAlreadyExist()
        {
            return new RegisterViewModel()
            {
                Error = "Sorry buddy, this pseudo is already taken. Please choose another one."
            };
        }

        public static RegisterViewModel Successful(string authToken)
        {
            return new RegisterViewModel()
            {
                AuthToken = authToken,
                IsSuccessful = true
            };
        }
    }
}
