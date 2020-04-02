using BusinessLogicLayer.Models;
using DataAccessLayer.Models;

namespace DataAccessLayer.Mappers
{
    /// <summary>
    ///     Mark entity to Mark mapper class
    /// </summary>
    public static class MarkMapper
    {
        /// <summary>
        ///     map method
        /// </summary>
        /// <param name="markEntity">mark entity instance</param>
        /// <returns>Mark instance (for BLL)</returns>
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

    /// <summary>
    ///     Mark to MarkEntity mapper class
    /// </summary>
    public static class MarkEntityMapper
    {
        /// <summary>
        ///     map method
        /// </summary>
        /// <param name="mark">Mark instance</param>
        /// <returns>MarkEntity (for DAL)</returns>
        public static MarkEntity Map(this Mark mark)
        {
            var resultMark = new MarkEntity
            {
                MarkEntityId = mark.MarkId,
                Course = mark.Course.Map(),
                Student = mark.Student.Map(),
                Mark = mark.Grade
            };
            return resultMark;
        }
    }
}