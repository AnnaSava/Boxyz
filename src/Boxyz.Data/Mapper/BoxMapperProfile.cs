using AutoMapper;
using Boxyz.Data.Contract;
using Boxyz.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data.Mapper
{
    public class BoxMapperProfile : Profile
    {
        public BoxMapperProfile()
        {
            CreateMap<ShapeBoard, ShapeBoardModel>()
                .ForMember(x => x.Cultures, y => y.Ignore())
                .ForMember(x => x.ChildBoards, y => y.Ignore());
            CreateMap<ShapeBoardModel, ShapeBoard>();

            CreateMap<ShapeBoardCulture, ShapeBoardCultureModel>();
            CreateMap<ShapeBoardCultureModel, ShapeBoardCulture>();

            CreateMap<Shape, ShapeModel>()
                .ForMember(x => x.Versions, y => y.Ignore());
            CreateMap<ShapeModel, Shape>();

            CreateMap<ShapeVersion, ShapeVersionModel>()
                .ForMember(x => x.Cultures, y => y.Ignore())
                .ForMember(x => x.Sides, y => y.Ignore());
            CreateMap<ShapeVersionModel, ShapeVersion>();

            CreateMap<ShapeVersion, ShapeVersionCultureModel>();
            CreateMap<ShapeVersionCultureModel, ShapeVersion>();

            CreateMap<ShapeSide, ShapeSideModel>()
                .ForMember(x => x.Cultures, y => y.Ignore());
            CreateMap<ShapeSideModel, ShapeSide>();

            CreateMap<ShapeSideCulture, ShapeSideCultureModel>();
            CreateMap<ShapeSideCultureModel, ShapeSideCulture>();

            CreateMap<Box, BoxModel>()
                .ForMember(x => x.Shape, y => y.Ignore())
                .ForMember(x => x.Versions, y => y.Ignore());
            CreateMap<BoxModel, Box>();

            CreateMap<BoxVersion, BoxVersionModel>()
                .ForMember(x => x.ShapeVersion, y => y.Ignore())
                .ForMember(x => x.Sides, y => y.Ignore());
            CreateMap<BoxVersionModel, BoxVersion>();

            CreateMap<BoxSide, BoxSideModel>()
                .ForMember(x => x.ShapeSide, y => y.Ignore())
                .ForMember(x => x.Cultures, y => y.Ignore());
            CreateMap<BoxSideModel, BoxSide>();

            CreateMap<BoxSideCulture, BoxSideCultureModel>();
            CreateMap<BoxSideCultureModel, BoxSideCulture>();
        }
    }
}
