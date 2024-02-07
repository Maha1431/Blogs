using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogWebApiTests.MockDatas
{
    public class MockCommentDatas
    {
        public static List<BlogComments> GetEmptyCommentMock()
        {
            return null;
        }

        public static List<BlogComments> GetAllCommentsMock()
        {
            return new List<BlogComments>
            {
                new BlogComments
                {
                    Id=1,
                    Comments="Good"
                },
                 new BlogComments
                 {
                    Id=2,
                    Comments="Bad"
                 },
                 new BlogComments
                 {
                    Id=3,
                    Comments="Excellent"
                 },
           };
        }

        public static BlogComments GetCommentByIdMock()
        {
            return new BlogComments
            {
                Id = 1,
               Comments="Not Bad"
            };
        }
        public static BlogComments GetEmptyCommentByIdMock()
        {
            return null;
        }
    }
}
