﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace recipe_api.Models
{
    public class AutoMapping:Profile
    {
        public AutoMapping()
        {
            CreateMap<RecipeDto, Recipe>();
        }
    }
}
