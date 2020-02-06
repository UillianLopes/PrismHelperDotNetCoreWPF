using Libs.Prism.Abstracts;
using System;
using System.ComponentModel.DataAnnotations;

namespace WPFApp.Domain.Models.Commands
{
    public class TaskRegisterModel : ValidableModel
    {
        private string _name;
        [Required(ErrorMessage = "The task name is required.")]
        [MaxLength(75, ErrorMessage = "Name can't have more than 75 characters.")]
        [MinLength(5, ErrorMessage = "Name can't have less than 5 characters.")]
        public string Name
        {
            get { return _name; }
            set 
            { 
                _name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        private string _description;

        [MaxLength(1000, ErrorMessage = "Task descriptions can't have more than 1000 characters.")]
        public string Description
        {
            get { return _description; }
            set 
            { 
                _description = value;
                RaisePropertyChanged(() => Description);
            }
        }

        private DateTime _deadline;
        public DateTime Deadline
        {
            get { return _deadline; }
            set 
            {
                _deadline = value;
                RaisePropertyChanged(() => Deadline);
            }
        }


        public TaskRegisterModel()
        {
        }
    }
}
