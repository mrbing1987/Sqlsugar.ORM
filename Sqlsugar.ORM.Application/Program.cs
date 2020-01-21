using Sqlsugar.ORM.Application.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sqlsugar.ORM.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            // 设置SQL Server 2016数据库连接字符串
            SqlsugarHelper.ConnectionString = string.Format("server={0};uid={1};pwd={2};database={3}", "18.216.245.248", "sa", "Mrbing4inlovr", "HoriProductQuery");

            // 连接SQL Server 2016数据库
            bool contectedResult = SqlsugarHelper.ContectedDataBase();
            if (contectedResult)
            {
                // 查询全部
                var result = SqlsugarHelper.QueryAllData<AntiCodeInfo>().ToList();
                result = result;
            }
            else
            {
                Console.WriteLine("数据库连接失败！");
            }

            Console.ReadKey();
        }
    }
}
