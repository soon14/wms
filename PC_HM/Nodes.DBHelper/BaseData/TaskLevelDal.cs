using System;
using System.Collections.Generic;
using System.Text;
using Nodes.Entities;
using Nodes.Dapper;

namespace Nodes.DBHelper
{
    public class TaskLevelDal
    {
        public static List<TaskLevelEntity> Select()
        {
            string sql = "SELECT L.T_ID, L.TASK_TYPE, C.ITEM_DESC TASK_TYPE_DESC, " +
                "L.TASK_LEVEL, L.BEGIN_TIME, L.END_TIME, L.DIFF_VALUE FROM TASK_LEVEL L " +
                "LEFT JOIN WM_BASE_CODE C ON L.TASK_TYPE = C.ITEM_VALUE " +
                "ORDER BY L.BEGIN_TIME DESC, L.TASK_LEVEL ASC";
            IMapper map = DatabaseInstance.Instance();
            return map.Query<TaskLevelEntity>(sql);
        }

        public static int Insert(TaskLevelEntity entity)
        {
            string sql = "INSERT TASK_LEVEL(TASK_TYPE, TASK_LEVEL, BEGIN_TIME, END_TIME, DIFF_VALUE) " +
                "VALUES(@TaskType, @TaskLevel, @BeginTime, @EndTime, @DiffValue)";
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql, new 
            { 
                TaskType = entity.TaskType, 
                TaskLevel = entity.TaskLevel, 
                BeginTime = entity.BeginTime, 
                EndTime = entity.EndTime,
                DiffValue = entity.DiffValue
            });
        }

        public static int Insert(List<TaskLevelEntity> list)
        {
            int result = 0;
            foreach (TaskLevelEntity entity in list)
            {
                result = Insert(entity);
            }
            return result;
        }

        public static int Update(TaskLevelEntity entity)
        {
            string sql = "UPDATE TASK_LEVEL SET TASK_TYPE = @TaskType, TASK_LEVEL = @TaskLevel, " +
                "BEGIN_TIME = @BeginTime, END_TIME = @EndTime, DIFF_VALUE = @DiffValue " +
                "WHERE T_ID = @ID ";
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql, new
            {
                TaskType = entity.TaskType,
                TaskLevel = entity.TaskLevel,
                BeginTime = entity.BeginTime,
                EndTime = entity.EndTime,
                DiffValue = entity.DiffValue,
                ID = entity.ID
            });
        }

        public static int Delete(TaskLevelEntity entity)
        {
            string sql = "DELETE FROM TASK_LEVEL WHERE T_ID = " + entity.ID;
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql);
        }

        public static int Delete()
        {
            string sql = "DELETE FROM TASK_LEVEL";
            IMapper map = DatabaseInstance.Instance();
            return map.Execute(sql);
        }
    }
}
