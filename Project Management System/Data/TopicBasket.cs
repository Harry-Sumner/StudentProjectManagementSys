﻿using System.ComponentModel.DataAnnotations;

namespace Project_Management_System.Data
{
    public class TopicBasket
    {
        [Required]
        public string StudentID { get; set; }

        [Required]
        public int TopicID { get; set; }

    }
}