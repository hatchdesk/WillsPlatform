﻿using System.ComponentModel.DataAnnotations;

namespace WillsPlatform.Web.Models.Manage
{
    public class EditFormViewModel : BaseViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please enter the Name.")]
        public string Name { get; set; }
    }
}
