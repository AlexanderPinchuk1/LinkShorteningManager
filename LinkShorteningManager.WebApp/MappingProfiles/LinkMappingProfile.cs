using AutoMapper;
using LinkShorteningManager.WebApp.Models;

namespace LinkShorteningManager.WebApp.Profiles
{
    public class LinkMappingProfile : Profile
    {
        public LinkMappingProfile()
        {
            CreateMap<Link, LinkViewModel>().ReverseMap();
        }
    }
}
