using Libs.Prism.Navigation.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WPFApp.Domain.Models;
using WPFApp.Extras.Abstracts;

namespace WPFApp.ViewModels.Tasks.Pages
{
	public class ListViewModel : AbstractViewModel, IResolvableNavigation
	{
		public ListViewModel(IServiceProvider provider) : base(provider)
		{
			Tasks = new ObservableCollection<TaskModel>();
		}

		private ObservableCollection<TaskModel> _tasks;
		public ObservableCollection<TaskModel> Tasks
		{
			get { return _tasks; }
			set 
			{ 
				_tasks = value;
				RaisePropertyChanged(() => Tasks);
			}
		}

		public bool OnResolved(IDictionary<string, object> resolved)
		{
			if (resolved["TaskList"] is List<TaskModel> tasks)
			{
				Tasks.Clear();
				foreach(var task in tasks)
					Tasks.Add(task);

			}

			return true;
		}
	}
}
