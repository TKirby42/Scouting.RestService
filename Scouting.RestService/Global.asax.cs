﻿using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using Funq;
using Scouting.DataLayer;
using Scouting.RestService.Api;
using Scouting.RestService.App_Start;
using Scouting.RestService.Dtos;
using ServiceStack;

namespace Scouting.RestService
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //SqlServerTypes.Utilities.LoadNativeAssemblies(Server.MapPath("~/bin")); The NuGet documentation said that this might be necessary.

            Mapper.CreateMap<GooglePlusLoginDto, User>()
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.CreateDate, opt => opt.Ignore())
                .ForMember(dest => dest.UpdateDate, opt => opt.Ignore())
                .ForMember(dest => dest.GoogleId, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.name))
                .ForMember(dest => dest.GivenName, opt => opt.MapFrom(src => src.given_name))
                .ForMember(dest => dest.FamilyName, opt => opt.MapFrom(src => src.family_name))
                .ForMember(dest => dest.Link, opt => opt.MapFrom(src => src.link))
                .ForMember(dest => dest.Picture, opt => opt.MapFrom(src => src.picture))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.gender))
                .ForMember(dest => dest.Locale, opt => opt.MapFrom(src => src.locale))
                .ForMember(dest => dest.FavoriteTeamId, opt => opt.Ignore());

            Mapper.AssertConfigurationIsValid();

            new AppHost().Init();
        }

        public class AppHost : AppHostBase
        {
            public AppHost() : base("Hello Web Services", typeof(TeamService).Assembly)
            {
                
            }

            public override void Configure(Container container)
            {
                SetConfig(new HostConfig { HandlerFactoryPath = "api" });
            }

            // TODO: Configure error logging.
        }
    }
}