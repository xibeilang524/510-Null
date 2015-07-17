using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Runtime.Serialization;

namespace Ralid.Park.BusinessModel.Model
{
    /// <summary>
    /// 视频
    /// </summary>
    [Serializable()]
    [DataContract]
    public class VideoSourceInfo
    {
        #region 重载操作符
        public static bool operator ==(VideoSourceInfo v1, VideoSourceInfo v2)
        {
            return object.Equals(v1, v2);
        }

        public static bool operator !=(VideoSourceInfo v1, VideoSourceInfo v2)
        {
            return !object.Equals(v1, v2);
        }

        #endregion

        #region 构造函数
        public VideoSourceInfo()
        {
            ControlPort = 6001;
            StreamPort = 6002;
            Channel = 1;
            AutoReconnect = true;
            AutoReconnectInterval = 10;
            ConnectTimeOut = 5;
            IsForCarPlate = false;
        }
        #endregion

        #region 公共方法和属性
        /// <summary>
        /// 获取或设置ID
        /// </summary>
        [DataMember(Name = "VideoID")]
        public int VideoID { get; set; }
        /// <summary>
        /// 获取或设置视频名称
        /// </summary>
        [DataMember(Name = "VideoName")]
        public string VideoName { get; set; }

        /// <summary>
        /// 获取或设置视频服务器IP地址
        /// </summary>
        [DataMember(Name = "MediaSource")]
        public string MediaSource { get; set; }

        /// <summary>
        /// 获取或设置视频服务器登录名
        /// </summary>
        [DataMember(Name = "UserName")]
        public string UserName { get; set; }

        /// <summary>
        /// 获取或设置视频登录密码
        /// </summary>
        [DataMember(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// 获取或设置视频服务器控制端口
        /// </summary>
        [DataMember(Name = "ControlPort")]
        public int ControlPort { get; set; }

        /// <summary>
        /// 获取或设置视频服务器流端口号
        /// </summary>
        [DataMember(Name = "StreamPort")]
        public int StreamPort { get; set; }

        /// <summary>
        /// 获取或设置视频的通道号(有的视频服务器有多个通道一个通道代表一个视频)
        /// </summary>
        [DataMember(Name = "Channel")]
        public int Channel { get; set; }

        /// <summary>
        /// 获取或设置是否在断开时自动重连视频服务器
        /// </summary>
        [DataMember(Name = "AutoReconnect")]
        public bool AutoReconnect { get; set; }

        /// <summary>
        /// 获取或设置自动重连视频服务器时间间隔
        /// </summary>
        [DataMember(Name = "AutoReconnectInterval")]
        public int AutoReconnectInterval { get; set; }

        /// <summary>
        /// 获取或设置视频所属的控制器ID
        /// </summary>
        [DataMember(Name = "EntranceID")]
        public int EntranceID { get; set; }

        /// <summary>
        /// 获取或设置此视频是否用于拍摄车牌号
        /// </summary>
        [DataMember(Name = "IsForCarPlate")]
        public bool IsForCarPlate { get; set; }

        /// <summary>
        /// 获取或设置连接超时时间
        /// </summary>
        [DataMember(Name = "ConnectTimeOut")]
        public int ConnectTimeOut { get; set; }

        /// <summary>
        /// 获取或设置视频服务类型
        /// </summary>
        [DataMember(Name = "VideoType")]
        public int? VideoType { get; set; }
        #endregion

        #region 只读属性
        /// <summary>
        /// 获取视频服务类型，有设置的，返回设置的，没有设置的，返回系统常规设置中的默认频服务类型，默认返回0
        /// </summary>
        public int VideoSourceType
        {
            get
            {
                if (VideoType.HasValue)
                {
                    return VideoType.Value;
                }
                else if (UserSetting.Current != null)
                {
                    return UserSetting.Current.VideoType;
                }
                return 0;
            }
        }
        #endregion

        #region 重写基类方法
        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                if (obj is VideoSourceInfo)
                {
                    VideoSourceInfo video = obj as VideoSourceInfo;
                    return (video.VideoID == this.VideoID);
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion

        #region 视频服务器操作
        /// <summary>
        /// 同步视频服务器时间
        /// </summary>
        /// <param name="dt">要同步的时间</param>
        /// <param name="timezone">时间所属时区</param>
        public void SyncDateTime(DateTime dt, int timezone)
        {
            try
            {
                WebClient wc = new WebClient();
                //http://ip:port/cgi-bin/system?USER=Admin&PWD=123456&DATE_CONFIG=1,010100002004,00:00:00,+00
                string url = string.Format(@"http://{0}:{1}/cgi-bin/system?USER={2}&PWD={3}&DATE_CONFIG={4},{5},{6},+{7}",
                   MediaSource, 80, UserName, Password, 1,
                   dt.ToString("MMdd0000yyyy"), dt.ToString("HH:mm:ss"), timezone);
                wc.OpenReadAsync(new Uri(url));
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }

        /// <summary>
        /// 重启
        /// </summary>
        public void Reboot()
        {
            try
            {
                WebClient wc = new WebClient();
                string url = string.Format(@"http://{0}:{1}/cgi-bin/system?USER={2}&PWD={3}&REBOOT", MediaSource, 80, UserName, Password);
                wc.OpenReadAsync(new Uri(url));
            }
            catch (Exception ex)
            {
                Ralid.GeneralLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex);
            }
        }
        #endregion

        #region SQL语句

        /// <summary>
        /// 获取插入包括主键值的记录的SQL语句
        /// </summary>
        public string SQLInsertWithPrimaryCmd
        {
            get
            {
                string cmd = string.Format(@"INSERT INTO [VideoSource](
           [VideoID]
           ,[VideoName]
           ,[MediaSource]
           ,[Channel]
           ,[UserName]
           ,[Password]
           ,[ControlPort]
           ,[StreamPort]
           ,[AutoReconnect]
           ,[AutoReconnectInterval]
           ,[ConnectTimeOut]
           ,[IsForCarPlate]
           ,[EntranceID])
VALUES
({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12});",
                   SQLStringHelper.FromInt(this.VideoID),
                   SQLStringHelper.FromString(this.VideoName),
                   SQLStringHelper.FromString(this.MediaSource),
                   SQLStringHelper.FromInt(this.Channel),
                   SQLStringHelper.FromString(this.UserName),
                   SQLStringHelper.FromString(this.Password),
                   SQLStringHelper.FromInt(this.ControlPort),
                   SQLStringHelper.FromInt(this.StreamPort),
                   SQLStringHelper.FromBool(this.AutoReconnect),
                   SQLStringHelper.FromInt(this.AutoReconnectInterval),
                   SQLStringHelper.FromInt(this.ConnectTimeOut),
                   SQLStringHelper.FromBool(this.IsForCarPlate),
                   SQLStringHelper.FromInt(this.EntranceID));
                return cmd;
            }
        }
        #endregion
    }
}
