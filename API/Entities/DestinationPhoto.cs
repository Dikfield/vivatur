﻿using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("DestinationPhotos")]
    public class DestinationPhoto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string? PublicId { get; set; }

        public Destination Destination { get; set; }
        public int DestinationId { get; set; }
    }
}