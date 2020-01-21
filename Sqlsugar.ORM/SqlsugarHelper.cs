using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sqlsugar.ORM
{
    /// <summary>
    /// SqlSugar帮助类
    /// </summary>
    public static class SqlsugarHelper
    {
        #region 字段
        /// <summary>
        /// 
        /// </summary>
        private static SqlSugarClient _db = null;

        /// <summary>
        /// 
        /// </summary>
        private static string _connectionString = string.Empty;
        #endregion 字段

        #region 属性
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                return _connectionString;
            }
            set
            {
                _connectionString = value;
            }
        }
        #endregion 属性

        #region 公有方法
        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <param name="isAutoCloseConnection">(默认false)自动释放数据务，如果存在事务，在事务结束后释放</param>
        /// <param name="initKeyType">(默认SystemTable)从实体特性中读取主键自增列信息</param>
        /// <param name="isOnLogExecuting">是否打印SQL</param>
        /// <returns></returns>
        public static bool ContectedDataBase(DbType dbType= DbType.SqlServer, bool isAutoCloseConnection = true, InitKeyType initKeyType = InitKeyType.Attribute, bool isOnLogExecuting = false)
        {
            try
            {
                if (!string.IsNullOrEmpty(_connectionString))
                {
                    _db = new SqlSugarClient(new ConnectionConfig()
                    {
                        ConnectionString = _connectionString, // 连接字符串
                        DbType = dbType,
                        IsAutoCloseConnection = isAutoCloseConnection,
                        InitKeyType = initKeyType,
                    });

                    // 打印SQL调式  
                    if (isOnLogExecuting)
                    {
                        _db.Aop.OnLogExecuting = (sql, pars) =>
                        {
                            Console.WriteLine(sql + "\r\n" + _db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                        };
                    }

                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 断开数据库
        /// </summary>
        public static void DiscontectedDataBase()
        {
            if (_db != null)
            {
                _db.Close();
            }
        }

        /// <summary>
        /// 是否连接数据库
        /// </summary>
        /// <returns>true-已连接</returns>
        public static bool IsContectedDataBase()
        {
            return false;
        }

        /// <summary>
        /// 根据实体类创建数据表
        /// </summary>
        /// <typeparam name="T">待创建数据表所对应的类型</typeparam>
        /// <param name="varcharLength">设置varchar长度(默认长度为255)</param>
        public static bool CreatDataBase<T>(int varcharLength = 255) where T : class, new()
        {
            try
            {
                if ((_db != null))
                {
                    _db.CodeFirst.SetStringDefaultLength(varcharLength).InitTables(typeof(T));
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 根据数据库表生成实体类
        /// </summary>
        /// <param name="directoryPath">实体类生成的路径</param>
        /// <param name="nameSpace">实体类命名空间</param>
        public static bool CreatEntity(string directoryPath, string nameSpace)
        {
            try
            {
                if ((_db != null) && !string.IsNullOrEmpty(directoryPath) && !string.IsNullOrEmpty(nameSpace))
                {
                    _db.DbFirst.IsCreateDefaultValue().CreateClassFile(directoryPath, nameSpace);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 数据库中数据是否存在
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression">表达式</param>
        /// <returns>true-数据存在，null-表达式错误</returns>
        public static bool? IsDataExists<T>(Expression<Func<T, bool>> expression)
        {
            if (expression == null)
            {
                return null;
            }
            else
            {
                return _db.Queryable<T>().Any(expression);
            }
        }

        /// <summary>
        /// 查询数据库中的全部数据
        /// </summary>
        /// <typeparam name="T">数据库中某数据表所对应的实例类型</typeparam>
        /// <returns>数据集合</returns>
        public static List<T> QueryAllData<T>() where T : class
        {
            try
            {
                if (_db != null)
                {
                    return _db.Queryable<T>().ToList();
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T">数据库中某数据表所对应的实例类型</typeparam>
        /// <param name="expression">查询条件</param>
        /// <returns>数据集合</returns>
        public static List<T> QueryData<T>(Expression<Func<T, bool>> expression) where T : class
        {
            try
            {
                if ((_db != null) && (expression != null))
                {
                    return _db.Queryable<T>().Where(expression).ToList();
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        public static void UpdateData<T>(T data) where T : class, new()
        {
            _db.Updateable(data).ExecuteCommand();
        }

        /// <summary>
        /// 向数据库中插入数据
        /// </summary>
        /// <typeparam name="T">待插入的数据类型</typeparam>
        /// <param name="data">待插入数据</param>
        /// <returns>true-插入完成</returns>
        public static bool InserData<T>(T data) where T : class, new()
        {
            try
            {
                if (data != null)
                {
                    int effectNum = _db.Insertable(data).ExecuteCommand();
                    if (effectNum > 0)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 删除模型
        /// </summary>
        /// <typeparam name="T">待删除的数据类型</typeparam>
        /// <param name="expression">删除条件</param>
        /// <returns>true-删除完成</returns>
        public static bool DeleteData<T>(Expression<Func<T, bool>> expression) where T : class, new()
        {
            try
            {
                if (expression != null)
                {
                    int effectNum = _db.Deleteable(expression).ExecuteCommand();
                    if (effectNum > 0)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        #endregion 公有方法
    }
}
