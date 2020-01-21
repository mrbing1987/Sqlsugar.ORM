using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * [SugarColumn(IsPrimaryKey=true,IsIdentity=true)] // 如果是主键并且是自增列就加上2个属性
 * [SugarColumn(IsPrimaryKey=true)] // 如果只是主键只能加一个
 */

namespace Sqlsugar.ORM.Application.Model
{
    /// <summary>
    /// 
    /// </summary>
    //如果实体类名称和表名不一致可以加上SugarTable特性指定表名
    [SugarTable("AntiCodeInfo")]
    class AntiCodeInfo
    {
        // 指定主键，当然数据库中也要设置主键和自增列才会有效
        [SugarColumn(IsPrimaryKey = true)]
        public string AntiCode { get; set; }

        public string CheckCode { get; set; }

        public string ProductType { get; set; }

        public string Status { get; set; }

        public int QueryTimes { get; set; }

        public string LastQueryDate { get; set; }

        public string PrintingState { get; set; }
    }
}
