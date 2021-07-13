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
            CreateMap<BoxShapeBoard, BoxShapeBoardModel>();
            CreateMap<BoxShapeBoardModel, BoxShapeBoard>();

            CreateMap<BoxShapeBoardCulture, BoxShapeBoardCultureModel>();
            CreateMap<BoxShapeBoardCultureModel, BoxShapeBoardCulture>();

            CreateMap<BoxShape, BoxShapeModel>();
            CreateMap<BoxShapeModel, BoxShape>();

            CreateMap<BoxShapeVersion, BoxShapeVersionModel>();
            CreateMap<BoxShapeVersionModel, BoxShapeVersion>();

            CreateMap<BoxShapeVersionCulture, BoxShapeVersionCultureModel>();
            CreateMap<BoxShapeVersionCultureModel, BoxShapeVersionCulture>();

            CreateMap<BoxShapeSide, BoxShapeSideModel>();
            CreateMap<BoxShapeSideModel, BoxShapeSide>();

            CreateMap<BoxShapeSideCulture, BoxShapeSideCultureModel>();
            CreateMap<BoxShapeSideCultureModel, BoxShapeSideCulture>();

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
