using CommonLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.BaseEntities
{
    public interface IBaseEntity
    {
        public int Id { get; set; } 
    }
}
