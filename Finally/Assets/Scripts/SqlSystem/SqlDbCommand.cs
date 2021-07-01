using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using System.Reflection;
using UnityEngine;
using Mono.Data.Sqlite;


namespace SqlSystem
{
    public class SqlDbCommand : SqlDbConnect
    {
        private SqliteCommand _sqlComm;

        //构造函数
        public SqlDbCommand(string dbPath) : base(dbPath)
        {
            _sqlComm = new SqliteCommand(_sqlConn);
        }

        //转换表
        public static string SqlToCsharp(string typename)
        {

            switch (typename)
            {
                case "bit": return "Boolean";
                case "bigint": return "Int64";
                case "int": return "Int32";
                case "smallint": return "Int16";
                case "tinyint": return "Byte";
                case "numeric":
                case "money":
                case "smallmoney":
                case "decimal": return "Decimal";
                case "float": return "Double";
                case "real": return "Single";
                case "smalldatetime":
                case "timestamp":
                case "datetime": return "DateTime";
                case "char":
                case "varchar":
                case "text":
                case "Unicode":
                case "nvarchar":
                case "ntext": return "string";
                case "binary":
                case "varbinary":
                case "image": return "Byte[]";
                case "uniqueidentifier": return "Guid";
                default: return "Object";
            }

        }


        #region 表的增删管理
        public int CreateTable<T>()
        {
            var type = typeof(T);
            var tableName = type.Name;
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append($"Create table {tableName}(");

            var properties = type.GetProperties();


            foreach (var p in properties)
            {

                var attribute = p.GetCustomAttribute<ModelHelp>();
                if (attribute.IsCreated)
                {
                    stringBuilder.Append($"{p.Name} {attribute.Type}");
                    if (attribute.IsPrimaryKey)
                    {
                        //注意前后空格
                        stringBuilder.Append(" primary key ");
                    }
                    if (attribute.IsCanBeNull)
                    {
                        stringBuilder.Append(" null ");
                    }
                    else
                    {
                        stringBuilder.Append(" not null ");
                    }
                    stringBuilder.Append(",");
                }

            }

            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            stringBuilder.Append(")");

            _sqlComm.CommandText = stringBuilder.ToString();
            Debug.Log(_sqlComm.CommandText);
            return _sqlComm.ExecuteNonQuery();
        }


        public int DeleteTable<T>()
        {
            var sql = $"drop table {typeof(T).Name}";
            _sqlComm.CommandText = sql;
            return _sqlComm.ExecuteNonQuery();
        }
        #endregion

        #region 插入数据
        public int Insert<T>(T t) where T : class
        {
            if (t == default(T))
            {
                Debug.LogError("Insert参数错误()!");
                return -1;
            }

            var type = typeof(T);
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append($"INSERT INTO {type.Name}(");

            var properties = type.GetProperties();
            foreach (var p in properties)
            {
                if (p.GetCustomAttribute<ModelHelp>().IsCreated)
                {
                    stringBuilder.Append(p.GetCustomAttribute<ModelHelp>().FieldName);
                    stringBuilder.Append(",");
                }
            }
            stringBuilder.Remove(stringBuilder.Length - 1, 1);//去掉多余的逗号
            stringBuilder.Append(") VALUES (");

            foreach (var p in properties)
            {
                if (p.GetCustomAttribute<ModelHelp>().Type == "text")
                {
                    stringBuilder.Append($"'{p.GetValue(t)}'");
                }
                else
                {
                    stringBuilder.Append(p.GetValue(t));
                }
                stringBuilder.Append(",");
            }
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            stringBuilder.Append(")");

            _sqlComm.CommandText = stringBuilder.ToString();
            return _sqlComm.ExecuteNonQuery();

        }



        public int Insert<T>(List<T> tList) where T : class
        {
            if (tList == null || tList.Count == 0)
            {
                Debug.LogError("Insert参数错误()!");
                return -1;
            }

            var type = typeof(T);
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append($"INSERT INTO {type.Name}(");

            var properties = type.GetProperties();
            foreach (var p in properties)
            {
                if (p.GetCustomAttribute<ModelHelp>().IsCreated)
                {
                    stringBuilder.Append(p.GetCustomAttribute<ModelHelp>().FieldName);
                    stringBuilder.Append(",");
                }
            }
            stringBuilder.Remove(stringBuilder.Length - 1, 1);//去掉多余的逗号
            stringBuilder.Append(") VALUES");

            foreach (var t in tList)
            {
                stringBuilder.Append("(");
                foreach (var p in properties)
                {
                    if (p.GetCustomAttribute<ModelHelp>().Type == "text")
                    {
                        stringBuilder.Append($"'{p.GetValue(t)}'");
                    }
                    else
                    {
                        stringBuilder.Append(p.GetValue(t));
                    }
                    stringBuilder.Append(",");
                }
                stringBuilder.Remove(stringBuilder.Length - 1, 1);
                stringBuilder.Append("),");
            }

            stringBuilder.Remove(stringBuilder.Length - 1, 1);

            _sqlComm.CommandText = stringBuilder.ToString();
            Debug.Log(_sqlComm.CommandText);
            return _sqlComm.ExecuteNonQuery();

        }
        #endregion

        #region 删除数据
        public int DeleteById<T>(int id)
        {
            var sql = $"DELETE FROM {typeof(T).Name} where Id = {id}";
            _sqlComm.CommandText = sql;
            return _sqlComm.ExecuteNonQuery();
        }

        public int DeleteByIds<T>(List<int> ids)
        {
            int count = 0;
            foreach (int id in ids)
            {
                count += DeleteById<T>(id);

            }
            return count;
        }

        public int DeleteBySql<T>(string sql)
        {
            _sqlComm.CommandText = $"DELETE FROM {typeof(T).Name} where {sql}";
            Debug.Log(_sqlComm.CommandText);
            return _sqlComm.ExecuteNonQuery();
        }


        #endregion

        #region 修改数据
        //Id指CharacterData中的Id
        public int UpdateById<T>(T t) where T : BaseData
        {
            if (t == default(T))
            {
                Debug.LogError("Insert参数错误()!");
                return -1;
            }
            var type = typeof(T);

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"UPDATE {type.Name} SET ");

            var properties = type.GetProperties();
            foreach (var p in properties)
            {
                if (p.GetCustomAttribute<ModelHelp>().IsCreated)
                {
                    stringBuilder.Append($"{p.GetCustomAttribute<ModelHelp>().FieldName}=");
                    if (p.GetCustomAttribute<ModelHelp>().Type == "text")
                    {
                        stringBuilder.Append($"'{p.GetValue(t)}'");
                    }
                    else
                    {
                        stringBuilder.Append(p.GetValue(t));
                    }
                    stringBuilder.Append(",");
                }
            }
            stringBuilder.Remove(stringBuilder.Length - 1, 1);//去掉多余的逗号
            stringBuilder.Append($" WHERE Id ={t.Id}");

            _sqlComm.CommandText = stringBuilder.ToString();
            Debug.Log(_sqlComm.CommandText);
            return _sqlComm.ExecuteNonQuery();
        }

        //Order指按照在CharacterData中的声明顺序。例 Order == 3即选用 Sex为条件
        public int UpdateByOrder<T>(T t, int Order) where T : BaseData
        {
            if (t == default(T))
            {
                Debug.LogError("Insert参数错误()!");
                return -1;
            }
            var type = typeof(T);

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"UPDATE {type.Name} SET ");

            int pptryCount = 0;
            string nameByOrder = null;
            string valueByOrder = null;
            string typeByOrder = null;

            var properties = type.GetProperties();
            foreach (var p in properties)
            {
                //properties的遍历并不按照实例化时的顺序，而是类本身的顺序
                pptryCount++;

                var attribute = p.GetCustomAttribute<ModelHelp>();
                if (attribute.IsCreated)
                {
                    stringBuilder.Append($"{attribute.FieldName}=");
                    if (p.GetCustomAttribute<ModelHelp>().Type == "text")
                    {
                        stringBuilder.Append($"'{p.GetValue(t)}'");
                    }
                    else
                    {
                        stringBuilder.Append(p.GetValue(t));
                    }
                    stringBuilder.Append(",");
                }
                if (pptryCount == Order)
                {
                    nameByOrder = attribute.FieldName;
                    valueByOrder = p.GetValue(t).ToString();
                    typeByOrder = attribute.Type.ToString();
                }
            }
            stringBuilder.Remove(stringBuilder.Length - 1, 1);

            if (nameByOrder == null)
            {
                Debug.LogError("参数异常");
                throw new IndexOutOfRangeException("Order参数多于class中数据的数量");
            }
            if (typeByOrder == "text")
            {
                stringBuilder.Append($" WHERE {nameByOrder}='{valueByOrder}'");
            }
            else
            {
                stringBuilder.Append($" WHERE {nameByOrder}={valueByOrder}"); //Id ={ t.Id}

            }

            _sqlComm.CommandText = stringBuilder.ToString();
            Debug.Log(_sqlComm.CommandText);
            return _sqlComm.ExecuteNonQuery();
        }

        //完整的Condition语句（不包括where，仅包括where之后的条件）
        public int UpdateBySql<T>(T t, String sql) where T : BaseData
        {
            if (t == default(T))
            {
                Debug.LogError("Insert参数错误()!");
                return -1;
            }
            var type = typeof(T);

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"UPDATE {type.Name} SET ");

            var properties = type.GetProperties();
            foreach (var p in properties)
            {
                if (p.GetCustomAttribute<ModelHelp>().IsCreated)
                {
                    stringBuilder.Append($"{p.GetCustomAttribute<ModelHelp>().FieldName}=");
                    if (p.GetCustomAttribute<ModelHelp>().Type == "text")
                    {
                        stringBuilder.Append($"'{p.GetValue(t)}'");
                    }
                    else
                    {
                        stringBuilder.Append(p.GetValue(t));
                    }
                    stringBuilder.Append(",");
                }
            }
            stringBuilder.Remove(stringBuilder.Length - 1, 1);//去掉多余的逗号
            stringBuilder.Append($" WHERE ");
            stringBuilder.Append(sql);

            _sqlComm.CommandText = stringBuilder.ToString();
            Debug.Log(_sqlComm.CommandText);
            return _sqlComm.ExecuteNonQuery();
        }

        //将多个条件用某种逻辑运算符连接（and或者or），合并成一句sql条件语句。logical == 1时为and，logical == 0 时为or
        public int UpdateBySql_Conditions<T>(T t, List<string> conditionsList, bool useAND) where T : BaseData
        {
            if (t == default(T))
            {
                Debug.LogError("Insert参数错误()!");
                return -1;
            }
            var type = typeof(T);

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"UPDATE {type.Name} SET ");

            var properties = type.GetProperties();
            foreach (var p in properties)
            {
                if (p.GetCustomAttribute<ModelHelp>().IsCreated)
                {
                    if (!p.GetCustomAttribute<ModelHelp>().IsPrimaryKey)
                    {
                        stringBuilder.Append($"{p.GetCustomAttribute<ModelHelp>().FieldName}=");
                        if (p.GetCustomAttribute<ModelHelp>().Type == "text")
                        {
                            stringBuilder.Append($"'{p.GetValue(t)}'");
                        }
                        else
                        {
                            stringBuilder.Append(p.GetValue(t));
                        }
                        stringBuilder.Append(",");
                    }
                }
            }

            stringBuilder.Remove(stringBuilder.Length - 1, 1);//去掉多余的逗号
            stringBuilder.Append($" WHERE ");
            foreach (string condition in conditionsList)
            {
                if (useAND)
                {
                    stringBuilder.Append($"{condition} and ");//写入一个or条件
                }
                else
                {
                    stringBuilder.Append($"{condition} or ");//写入一个and条件
                }
            }
            stringBuilder.Remove(stringBuilder.Length - 4, 4);//去掉多余的末尾逻辑运算符

            _sqlComm.CommandText = stringBuilder.ToString();
            Debug.Log(_sqlComm.CommandText);
            return _sqlComm.ExecuteNonQuery();
        }

        #endregion

        #region 读取数据

        //根据填入的id返回数据
        public T SelectById<T>(int id) where T : BaseData
        {
            var type = typeof(T);
            var sql = $"SELECT *FROM {type.Name} WHERE Id = {id}";
            _sqlComm.CommandText = sql;
            var dr = _sqlComm.ExecuteReader();
            if (dr != null && dr.Read())
            {
                return DataReaderToData<T>(dr);
            }
            return default(T);
        }

        //返回全部数据
        public List<T> SelectAll<T>(int id) where T : BaseData
        {
            var tempList = new List<T>();
            var type = typeof(T);
            var sql = $"SELECT *FROM {type.Name} WHERE Id = {id}";
            _sqlComm.CommandText = sql;
            var dr = _sqlComm.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    tempList.Add(DataReaderToData<T>(dr));
                }
            }
            return tempList;
        }

        public List<T> SelectBySql<T>(string sqlWhere="") where T : BaseData
        {
            var tempList = new List<T>();
            var type = typeof(T);
            string sql;
            if (string.IsNullOrEmpty(sqlWhere))
            {
                sql = $"SELECT *FROM {type.Name}";
            }
            else
            {
                sql = $"SELECT *FROM {type.Name} WHERE {sqlWhere}";
            }
            _sqlComm.CommandText = sql;
            var dr = _sqlComm.ExecuteReader();
            if (dr != null)
            {
                while (dr.Read())
                {
                    tempList.Add(DataReaderToData<T>(dr));
                }
            }
            return tempList;
        }

        //转换成可用数据
        public T DataReaderToData<T>(SqliteDataReader dr) where T : BaseData
        {
            try
            {
                List<string> fieldNames = new List<string>();
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    fieldNames.Add(dr.GetName(i));
                }

                var type = typeof(T);
                var properies = type.GetProperties();
                T data = Activator.CreateInstance<T>();

                //debug用计数器
                //int count = 0;

                foreach (var p in properies)
                {
                   
                    //count++;
                    if (!p.CanWrite) continue;
                    var fieldName = p.GetCustomAttribute<ModelHelp>().FieldName;
                    if (fieldNames.Contains(fieldName) && p.GetCustomAttribute<ModelHelp>().IsCreated)
                    {
                        //bool类型数据以integer形式存储,默认转换成int64类型
                        if (p.PropertyType == typeof(bool))
                        {
                            string Temp = dr[fieldName].ToString();

                            if (Temp == "1")
                            {
                                p.SetValue(data, true);

                            }
                            else if (Temp == "0")
                            {
                                p.SetValue(data, false);
                            }
                        }
                        //由于navicat使用sqlite时，数据类型只能选择integer,默认转换成int64长整型，即long，而非int32。所以需要靠函数修改
                        else if(p.PropertyType == typeof(int))
                        {
                            int Temp = 0;
                            int.TryParse(dr[fieldName].ToString(),out Temp);
                            p.SetValue(data, Temp);
                        }
                        else
                        {
                            p.SetValue(data, dr[fieldName]);
                        }
                    }
                }

                return data;
            }
            catch (Exception e)
            {
                Debug.LogError($"DataRenderToData转换出错:{typeof(T).Name}");
                return null;
            }
        }

        #endregion
    }
} 

