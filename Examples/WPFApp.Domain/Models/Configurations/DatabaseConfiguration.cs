using Libs.Prism.Abstracts;
using System.Text.Json.Serialization;
using WPFApp.Domain.Extensions;

namespace WPFApp.Domain.Models.Configurations
{
	public class DatabaseConfiguration : BindableModel
	{
		private int? _port;
		public int? Port
		{
			get { return _port; }
			set 
			{ 
				_port = value;
				RaisePropertyChanged("Port");
			}
		}

		private string _host;
		public string Host
		{
			get { return _host; }
			set 
			{
				_host = value;
				RaisePropertyChanged("Host");
			}
		}

		private string _database;
		public string Database
		{
			get { return _database; }
			set 
			{
				_database = value;
				RaisePropertyChanged("Database");
			}
		}

		private string _username;
		public string Username
		{
			get { return _username; }
			set 
			{ 
				_username = value;
				RaisePropertyChanged("Username");
			}
		}

		private string _password;
		public string Password
		{
			get { return _password; }
			set 
			{ 
				_password = value;
				RaisePropertyChanged("Password");
			}
		}

		[JsonIgnore]
		public string DecryptedPassword
		{
			get
			{
				if (_password != null && _password.TryDecrypt(out string decrypted))
					return decrypted;

				return _password;
			}
			set
			{
				if (value != null && value.TryCrypt(out string encrypted))
					_password = encrypted;
				else
					_password = null;

				RaisePropertyChanged("DecryptedPassword");
			}
		}
	}
}
