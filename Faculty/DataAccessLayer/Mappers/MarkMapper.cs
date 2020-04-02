using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Models;
using DataAccessLayer.Models;

namespace DataAccessLayer.Mappers
{
    public static class MarkMapper
    {
        public static Mark Map(this MarkEntity markEntity)
        {
            var resultMark = new Mark
            {
                MarkId = markEntity.MarkEntityId,
                Course = markEntity.Course.MapFlat(),
                Student = markEntity.Student.Map(),
                Grade = markEntity.Mark
                
            };
            return resultMark;
        }
    }
    public static class MarkEntityMapper
    {
        public static MarkEntity Map(this Mark mark)
        {
            var resultMark = new MarkEntity
            {
                MarkEntityId =  mark.MarkId,
                Course = mark.Course.Map(),
                Student = mark.Student.Map(),
                Mark = mark.Grade
                
            };
            return resultMark;
        }
    }
}
