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

            CreateMap<Shape, ShapeModel>();
            CreateMap<ShapeModel, Shape>();

            CreateMap<ShapeVersion, ShapeVersionModel>();
            CreateMap<ShapeVersionModel, ShapeVersion>();

            CreateMap<ShapeVersionCulture, ShapeVersionCultureModel>();
            CreateMap<ShapeVersionCultureModel, ShapeVersionCulture>();

            CreateMap<ShapeSide, ShapeSideModel>();
            CreateMap<ShapeSideModel, ShapeSide>();

            CreateMap<ShapeSideCulture, ShapeSideCultureModel>();
            CreateMap<ShapeSideCultureModel, ShapeSideCulture>();

            CreateMap<Box, BoxModel>();
            CreateMap<BoxModel, Box>();

            CreateMap<BoxVersion, BoxVersionModel>();
            CreateMap<BoxVersionModel, BoxVersion>();

            CreateMap<BoxSide, BoxSideModel>();
            CreateMap<BoxSideModel, BoxSide>();

            CreateMap<BoxSideCulture, BoxSideCultureModel>();
            CreateMap<BoxSideCultureModel, BoxSideCulture>();
        }
    }
}
