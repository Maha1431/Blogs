using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogWebApiTests.MockDatas
{
    public static class MockBlogDatas
    {
        public static List<Blogs> GetEmptyBlogMock()
        {
            return null;
        }

        public static List<Blogs> GetAllBlogsMock()
        {
            return new List<Blogs>
            {
                new Blogs
                {
                    Id=1,
                    Title="Language",
                    Description="C#"
                },
                 new Blogs
                 {
                    Id=2,
                    Title="Knowledge",
                    Description="IsPower"
                 },
                 new Blogs
                 {
                    Id=3,
                    Title="Company",
                    Description="IT"
                 },
           };
        }

        public static Blogs GetBlogByIdMock()
        {
            return new Blogs
            {
                Id = 1,
                Title = "Company",
                Description = "IT"
            };
        }
        public static Blogs GetEmptyBlogByIdMock()
        {
            return null;
        }
    }
}
