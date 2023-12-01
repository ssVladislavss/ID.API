﻿using System.ComponentModel.DataAnnotations;

namespace ID.Host.Infrastracture.Models.Clients
{
    public class ClientClaimViewModel
    {
        [Required(ErrorMessage = "Поле - Type - обязательно к заполнению")]
        public string Type { get; set; } = string.Empty;
        [Required(ErrorMessage = "Поле - Value - обязательно к заполнению")]
        public string Value { get; set; } = string.Empty;
    }
}
