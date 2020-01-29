using Libs.Prism.Abstracts;
using System.ComponentModel.DataAnnotations;

namespace WPFApp.Domain.Models.Login
{
	public class LoginModel : ValidableModel
    {
		private string _password;

		[Required(AllowEmptyStrings = false, ErrorMessage = "Senha inválida")]
		[MaxLength(20, ErrorMessage = "Senha inválida")]
		[MinLength(5, ErrorMessage = "Senha inválida")]
		public string Password
		{
			get => _password;
			set 
			{ 
				_password = value;
				RaisePropertyChanged(() => Password);
			}
		}

		private string _username;

		[Required(AllowEmptyStrings = false, ErrorMessage = "Usuário inválido")]
		public string Username
		{
			get => _username;
			set 
			{ 
				_username = value;
				RaisePropertyChanged(() => Username);
			}
		}


	}
}
