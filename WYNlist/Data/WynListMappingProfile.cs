using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Wynlist.Data.Entities;
using WYNlist.ViewModels;

namespace WYNlist.Data
{
    public class WynListMappingProfile : Profile
    {
        public WynListMappingProfile()
        {
            CreateMap<Recipe, RecipeViewModel>()
                .ForMember(r => r.RecipeId, ex => ex.MapFrom(r => r.Id))
                .ReverseMap();

            CreateMap<List, ListViewModel>()
                .ForMember(l => l.ListId, ex => ex.MapFrom(l => l.Id))
                .ReverseMap();
        }
    }
}
