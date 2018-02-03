﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YaziBasicV2.Models
{
    public class VerityForDisplayDto
    {
        public Guid VerityId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string MetaDescription { get; set; }

        public string ImagePath { get; set; }

        public string Tags { get; set; }

        public bool IsPublic { get; set; }

        public string CreatedTime { get; set; }

        public string UpdatedTime { get; set; }

        public string CategoryName { get; set; }

        public string AuthorName { get; set; }

        public int Agree { get; set; }

        public int DisAgree { get; set; }
    }
}
