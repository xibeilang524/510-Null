using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Xml;
using System.Runtime.Serialization;
using Ralid.Park.BusinessModel .Model ;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel .Notify ;
using Ralid.Park.DAL.IDAL;
using Ralid.GeneralLibrary.ExceptionHandling;

namespace Ralid.Park.BLL
{
    /// <summary>
    ///用于从SysParameter表中保存或获取一些系统设置,这些设置的实例被序列化为XML字串保存在ParameterValue字段中
    /// </summary>
    public class SysParaSettingsBll
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repoUri">存储层的资源标识(可以是数据库连接字符串或文件名等，根据存储层的不同可以设置不同的值)</param>
        public SysParaSettingsBll(string repoUri)
        {
            _RepoUri = repoUri;
        }
        #endregion

        #region 私有变量
        private string _RepoUri;
        #endregion

        #region 公共方法
        /// <summary>
        ///保存到数据库
        /// </summary>
        /// <param name="info"></param>
        public CommandResult SaveSetting<T>(T info) where T : class
        {
            try
            {
                SystemParameterBll bll = new SystemParameterBll(_RepoUri);
                Type t = typeof(T);
                SysparameterInfo para;
                if (info != null)
                {
                    DataContractSerializer ser = new DataContractSerializer(t);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        ser.WriteObject(stream, info);
                        stream.Position = 0;
                        byte[] data = new byte[stream.Length];
                        stream.Read(data, 0, (int)stream.Length);
                        string value = Encoding.UTF8.GetString(data);
                        para = new SysparameterInfo(t.Name, value, string.Empty);
                    }
                }
                else
                {
                    para = new SysparameterInfo(t.Name, string.Empty, string.Empty);
                }
                return bll.Insert(para);
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex);
                return new CommandResult(ResultCode.Fail, ex.Message);
            }
        }

        public CommandResult DeleteSetting<T>(T info, int parkID) where T : class
        {
            try
            {
                SystemParameterBll bll = new SystemParameterBll(_RepoUri);
                Type t = typeof(T);
                SysparameterInfo para;
                if (info != null)
                {
                    DataContractSerializer ser = new DataContractSerializer(t);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        ser.WriteObject(stream, info);
                        stream.Position = 0;
                        byte[] data = new byte[stream.Length];
                        stream.Read(data, 0, (int)stream.Length);
                        string value = Encoding.UTF8.GetString(data);
                        para = new SysparameterInfo(t.Name + parkID, value, string.Empty);
                    }
                }
                else
                {
                    para = new SysparameterInfo(t.Name + parkID, string.Empty, string.Empty);
                }
                return bll.Delete(para);
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex);
                return new CommandResult(ResultCode.Fail, ex.Message);
            }
        }

        /// <summary>
        /// 从数据库中获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetSetting<T>() where T : class
        {
            try
            {
                SystemParameterBll bll = new SystemParameterBll(_RepoUri );
                SysparameterInfo para = null;
                QueryResult<SysparameterInfo> result = null;

                Type t = typeof(T);
                result = bll.GetSysParameterByID(t.Name);

                if (result.Result == ResultCode.Successful)
                {
                    para = result.QueryObject;
                    string value = para.ParameterValue;
                    if (!string.IsNullOrEmpty(value))
                    {
                        byte[] data = Encoding.UTF8.GetBytes(value);
                        using (MemoryStream stream = new MemoryStream(data))
                        {
                            stream.Position = 0;
                            DataContractSerializer ser = new DataContractSerializer(t);
                            return ser.ReadObject(stream) as T;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex);
            }
            return null;
        }

        /// <summary>
        /// 从持久层获取设置，如果不存在就创建一个
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetOrCreateSetting<T>() where T : class ,new()
        {
            T t = GetSetting<T>();

            if (t == null)
            {
                t = new T();
            }
            return t;
        }

        /// <summary>
        /// 为停车场支持单独费率新增的函数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parkID"></param>
        /// <returns></returns>
        public T GetOrCreateSetting<T>(string name) where T : class ,new()
        {
            T t = GetSetting<T>(name);
            if (t == null)
            {
                t = new T();
            }
            return t;
        }

        /// <summary>
        ///保存到数据库
        /// </summary>
        /// <param name="info"></param>
        public CommandResult SaveSetting<T>(T info,string name) where T : class
        {
            try
            {
                SystemParameterBll bll = new SystemParameterBll(_RepoUri);
                Type t = typeof(T);
                SysparameterInfo para;
                if (info != null)
                {
                    DataContractSerializer ser = new DataContractSerializer(t);
                    using (MemoryStream stream = new MemoryStream())
                    {
                        ser.WriteObject(stream, info);
                        stream.Position = 0;
                        byte[] data = new byte[stream.Length];
                        stream.Read(data, 0, (int)stream.Length);
                        string value = Encoding.UTF8.GetString(data);
                        para = new SysparameterInfo(name, value, string.Empty);
                    }
                }
                else
                {
                    para = new SysparameterInfo(t.Name, string.Empty, string.Empty);
                }
                return bll.Insert(para);
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex);
                return new CommandResult(ResultCode.Fail, ex.Message);
            }
        }

        /// <summary>
        /// 从数据库中获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetSetting<T>(string name) where T : class
        {
            try
            {
                SystemParameterBll bll = new SystemParameterBll(_RepoUri);
                SysparameterInfo para = null;
                QueryResult<SysparameterInfo> result = null;

                Type t = typeof(T);
                result = bll.GetSysParameterByID(name);

                if (result.Result == ResultCode.Successful)
                {
                    para = result.QueryObject;
                    string value = para.ParameterValue;
                    if (!string.IsNullOrEmpty(value))
                    {
                        byte[] data = Encoding.UTF8.GetBytes(value);
                        using (MemoryStream stream = new MemoryStream(data))
                        {
                            stream.Position = 0;
                            DataContractSerializer ser = new DataContractSerializer(t);
                            return ser.ReadObject(stream) as T;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex);
            }
            return null;
        }
        #endregion
    }
}
