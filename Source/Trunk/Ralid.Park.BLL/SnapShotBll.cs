using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Result;
using Ralid.Park.BusinessModel.Enum;
using Ralid.Park.BusinessModel.Model;
using Ralid.Park.BusinessModel.Report;
using Ralid.Park.BusinessModel.SearchCondition;
using Ralid.GeneralLibrary;

namespace Ralid.Park.BLL
{
    public class SnapShotBll
    {
        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repoUri">存储层的资源标识(可以是数据库连接字符串或文件名等，根据存储层的不同可以设置不同的值)</param>
        public SnapShotBll(string repoUri)
        {
            provider = ProviderFactory.Create<ISnapShotProvider>(repoUri);
        }
        #endregion

        #region 私有变量
        private ISnapShotProvider provider;
        #endregion

        #region 公共方法
        public List<SnapShot> GetSnapShots(DateTime shotDateTime, string cardID)
        {
            SnapShotSearchCondition con = new SnapShotSearchCondition();
            con.ShotDateTime = shotDateTime;
            con.CardID = cardID;
            return provider.GetItems(con).QueryObjects;
        }

        public Image GetFirstSnapShot(DateTime shotDateTime, string cardID)
        {
            Image img = null;
            SnapShotSearchCondition con = new SnapShotSearchCondition();
            con.ShotDateTime = shotDateTime;
            con.CardID = cardID;
            List<SnapShot> shots = provider.GetItems(con).QueryObjects;
            if (shots != null && shots.Count > 0)
            {
                img = shots[0].Image;
            }
            return img;
        }

        /// <summary>
        /// 保存抓拍图片到数据库
        /// </summary>
        /// <param name="shotDateTime"></param>
        /// <param name="photo"></param>
        public CommandResult Insert(SnapShot info)
        {
            CommandResult result = provider.Insert(info);
            return result;
        }

        public void Insert(DateTime shotAt, int videoSource, string cardID, string path)
        {
            SnapShot shot = new SnapShot(shotAt, videoSource, cardID, path);
            provider.Insert(shot);
        }

        /// <summary>
        /// 删除所有在shotDateTime之前抓拍的图片
        /// </summary>
        /// <param name="datetime"></param>
        public void DeleteAllSnapShotBefore(DateTime shotDatetime)
        {
            provider.DeleteAllSnapShotBefore(shotDatetime);
        }
        #endregion
    }
}
