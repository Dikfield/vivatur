﻿using API.Entities;

namespace API.Dtos
{
    public class RegisterDestinationDescriptionDto
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public DescriptionPhoto DescriptionPhoto { get; set; }

    }
}