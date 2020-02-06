﻿using Libs.Prism.Abstracts;
using System;
using WPFApp.Domain.Enums;

namespace WPFApp.Domain.Models
{
    public class TaskListItemModel : BindableModel
    {
        public Guid Id { get; set; }
        
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged(() => Name); }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; RaisePropertyChanged(() => Description); }
        }

        private TaskState _currentState;
        public TaskState CurrentState
        {
            get { return _currentState; }
            set { _currentState = value; RaisePropertyChanged(() => CurrentState); }
        }

        public TaskListItemModel()
        {
        }
    }
}
