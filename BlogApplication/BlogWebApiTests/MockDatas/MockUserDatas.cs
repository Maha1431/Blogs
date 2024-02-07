using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlogWebApiTests.MockDatas
{
    public static class MockUserDatas
    {
        public static List<Users> GetAllUsersMock()
        {
            return new List<Users>
            {
                new Users
                {
                    Id=1,
                    FirstName="Hindhujha",
                    LastName="Sridhar",
                    EmailId="hindhujha@gmail.com",
                    Password="Hindhu123",
                },
                 new Users
                 {
                    Id=2,
                    FirstName="Reena",
                    LastName="Taj",
                    EmailId="reena12@gmail.com",
                    Password="Reena09",
                 },
                 new Users
                 {
                    Id=3,
                    FirstName="Lokesh",
                    LastName="Yash",
                    EmailId="lokesh12@gmail.com",
                    Password="Lokesh123",
                 },
           };
        }
        public static List<Users> GetEmptyUsersMock()
        {
            return null;
        }
        public static Users GetUserByIdMock()
        {
            return new Users
            {
                Id = 1,
                FirstName = "Hindhujha",
                LastName = "Sridhar",
                EmailId = "hindhujha@gmail.com",
                Password = "Hindhu123",
            };
        }
        public static Users GetEmptyUserByIdMock()
        {
            return null;
        }
    }
}
