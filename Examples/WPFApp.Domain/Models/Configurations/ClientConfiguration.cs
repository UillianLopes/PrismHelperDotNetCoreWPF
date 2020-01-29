using Libs.Prism.Abstracts;

namespace WPFApp.Domain.Models.Configurations
{
	public class ClientConfiguration : BindableModel
    {
		private string _apiUrl;
		public string ApiUrl
		{
			get => _apiUrl;
			set
			{ 
				_apiUrl = value;
				RaisePropertyChanged(() => ApiUrl);
			}
		}

	}
}
