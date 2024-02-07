using CommonLayer.DataTransferObjects;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.BaseEntities
{
    public partial class BaseEntity : IBaseEntity
    {
        public int Id { get ; set ; }
    }
}
