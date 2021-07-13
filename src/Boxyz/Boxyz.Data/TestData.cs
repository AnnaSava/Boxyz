using Boxyz.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxyz.Data
{
    public static class TestData
    {
        public static IEnumerable<BoxShapeBoard> GetBoards()
        {
            var boards = new List<BoxShapeBoard>
            {
                new BoxShapeBoard
                {
                    //Id = 1,
                    Level = 0,
                    Name = "media",
                    Cultures = new List<BoxShapeBoardCulture>
                    {
                        new BoxShapeBoardCulture { Culture = "ru", BoardId = 1, Title = "Медиа" },
                        new BoxShapeBoardCulture { Culture = "en", BoardId = 1, Title = "Media" }
                    },
                },
                new BoxShapeBoard
                {
                    //Id = 1,
                    Level = 0,
                    Name = "collections",
                    Cultures = new List<BoxShapeBoardCulture>
                    {
                        new BoxShapeBoardCulture { Culture = "ru", BoardId = 2, Title = "Коллекции" },
                        new BoxShapeBoardCulture { Culture = "en", BoardId = 2, Title = "Collections" }
                    },
                }
            };

            return boards;
        }

        public static IEnumerable<BoxShape> GetShapes()
        {
            var shapes = new List<BoxShape>
            {
                new BoxShape
                {
                    //Id = 1,
                    BoardId = 1,
                    ConstName = "books",
                    LastUpdated = DateTime.Now,
                    Versions = new List<BoxShapeVersion>
                    {
                        new BoxShapeVersion
                        {
                            //Id = 1,
                            ShapeId = 1,
                            IsApproved = false,
                            Created = DateTime.Now,
                            Cultures = new List<BoxShapeVersionCulture>
                            {
                                new BoxShapeVersionCulture { Culture = "ru", ShapeVersionId = 1, Title = "Книги" },
                                new BoxShapeVersionCulture { Culture = "en", ShapeVersionId = 1, Title = "Books" },
                            },
                            Sides = new List<BoxShapeSide>
                            {
                                new BoxShapeSide
                                {
                                    //Id = 1,
                                    ConstName = "authorName",
                                    DataType = "nativeText",
                                    Cultures = new List<BoxShapeSideCulture>
                                    {
                                        new BoxShapeSideCulture { Culture = "ru", ShapeSideId = 1, Title = "Имя автора" },
                                        new BoxShapeSideCulture { Culture = "en", ShapeSideId = 1, Title = "Author's name" }
                                    }
                                },
                                new BoxShapeSide
                                {
                                    //Id = 2,
                                    ConstName = "bookName",
                                    DataType = "nativeText",
                                    Cultures = new List<BoxShapeSideCulture>
                                    {
                                        new BoxShapeSideCulture { Culture = "ru", ShapeSideId = 2, Title = "Название книги" },
                                        new BoxShapeSideCulture { Culture = "en", ShapeSideId = 2, Title = "Title of the book" }
                                    }
                                },
                                new BoxShapeSide
                                {
                                    //Id = 3,
                                    ConstName = "year",
                                    DataType = "year",
                                    Cultures = new List<BoxShapeSideCulture>
                                    {
                                        new BoxShapeSideCulture { Culture = "ru", ShapeSideId = 3, Title = "Год издания" },
                                        new BoxShapeSideCulture { Culture = "en", ShapeSideId = 3, Title = "The year of publishing" }
                                    }
                                }
                            }
                        }
                    }
                }
            };
            return shapes;
        }

        public static IEnumerable<Box> GetBoxes()
        { 
            var boxes = new List<Box>
            {
                new Box
                {
                    //Id = 1,
                    ShapeId = 1,
                    Versions = new List<BoxVersion>
                    {
                        new BoxVersion
                        {
                            //Id = 1,
                            BoxId = 1,
                            ShapeVersionId = 1,     
                            Created = DateTime.Now,
                            IsApproved = false,
                            Sides = new List<BoxSide>
                            {
                                new BoxSide
                                {
                                    //Id = 1,
                                    BoxVersionId = 1,
                                    ShapeSideId = 1,
                                    UniversalValue = "Анна Ахматова",
                                    Cultures = new List<BoxSideCulture>
                                    {
                                        new BoxSideCulture { Culture = "ru", BoxSideId = 1, Value = "Анна Ахматова" },
                                        new BoxSideCulture { Culture = "en", BoxSideId = 1, Value = "Anna Akhmatova" }
                                    }
                                },
                                new BoxSide
                                {
                                    //Id = 2,
                                    BoxVersionId = 1,
                                    ShapeSideId = 2,
                                    UniversalValue = "Вечер",
                                    Cultures = new List<BoxSideCulture>
                                    {
                                        new BoxSideCulture { Culture = "ru", BoxSideId = 2, Value = "Вечер" },
                                        new BoxSideCulture { Culture = "en", BoxSideId = 2, Value = "Vecher (Evening)" }
                                    }
                                },
                                new BoxSide
                                {
                                    //Id = 3,
                                    BoxVersionId = 1,
                                    ShapeSideId = 3,
                                    UniversalValue = "1912"
                                },
                            }
                        }
                    }
                }
            };
            return boxes;
        }
    }
}
