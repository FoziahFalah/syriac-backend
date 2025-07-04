﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyriacSources.Backend.Domain.Entities;


namespace SyriacSources.Backend.Application.Sources
{
    public class CoverPhotoDto
    {
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public string FileExtension { get; set; } = string.Empty;


        public class Mapping : Profile
        {
            public Mapping()
            {

                CreateMap<CoverPhoto, CoverPhotoDto>().ReverseMap();
            }
        }

    }
}
