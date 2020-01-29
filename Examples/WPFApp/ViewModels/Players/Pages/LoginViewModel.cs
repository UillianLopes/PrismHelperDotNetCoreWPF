using Libs.Prism.Implementations;
using System;
using System.Net.Http;
using System.Reactive.Linq;
using System.Windows.Input;
using WPFApp.Domain.Http.Responses;
using WPFApp.Domain.Models.Configurations;
using WPFApp.Domain.Models.Login;
using WPFApp.Domain.Services;
using WPFApp.Extras.Abstracts;
using WPFApp.Extras.Extensions;

namespace WPFApp.ViewModels.Players.Pages
{
    public class LoginViewModel : AbstractViewModel
    {
        private readonly HttpClient _client;
        private readonly AuthenticationService _authService;

        public LoginViewModel(
            AuthenticationService authService,
            IHttpClientFactory factory,
            IServiceProvider provider) : base(provider)
        {
            _client = factory.CreateClient("OAC");
            _authService = authService;
        }

        public LoginModel LoginModel { get; } = new LoginModel();

        public ICommand Authenticate => new DelegateCommand(() =>
        {
            var observable = _client
                .Post<Response<Authentication>>("/player/authenticate", LoginModel)
                .Catch((Exception ex) => 
                {
                    Error(ex.Message, areaName: "MainArea");
                    return Observable.Return<Response<Authentication>>(null); 
                }).Publish();

            observable
                .Where((response) => response != null && response.Success)
                .Do((response) =>
                {
                    foreach (var message in response.Messages)
                        Success(message, areaName: "MainArea");
                })
                .Select((response) => response.Data)
                .Subscribe((response) =>
                {
                    _authService.Save(response);
                    Navigate("MainArea", "home");
                });

            observable
                .Where((response) => response != null && !response.Success)
                .Select((response) => response.Messages)
                .Subscribe((messages) =>
                {
                    foreach (var message in messages)
                        Error(message, areaName: "MainArea");
                });

            observable.Connect();

        });
    }
}
