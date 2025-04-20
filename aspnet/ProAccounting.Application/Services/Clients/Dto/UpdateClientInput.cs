﻿namespace ProAccounting.Application.Services.Clients.Dto
{
    public class UpdateClientInput
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }

    }
}
