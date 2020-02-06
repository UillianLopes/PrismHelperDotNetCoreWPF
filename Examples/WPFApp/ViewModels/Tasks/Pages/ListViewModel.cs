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
			Tasks = new ObservableCollection<TaskListItemModel>();
		}

		private ObservableCollection<TaskListItemModel> _tasks;
		public ObservableCollection<TaskListItemModel> Tasks
		{
			get { return _tasks; }
			set 
			{ 
				_tasks = value;
				RaisePropertyChanged(() => Tasks);
			}
		}

		public void OnResolved(IDictionary<string, object> resolved)
		{
			if (resolved["TaskList"] is List<TaskListItemModel> tasks)
			{
				Tasks.Clear();
				foreach(var task in Tasks)
					Tasks.Add(task);

			}
			

		}
	}
}
