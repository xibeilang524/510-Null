using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ralid.Park.DAL.IDAL;
using Ralid.Park.BusinessModel.Model;

namespace Ralid.Park.DAL.LinqDataProvider
{
    public class VideoSourceProvider : ProviderBase<VideoSourceInfo,int>, IVideoSourceProvider
    {
        public VideoSourceProvider()
        {
        }

        public VideoSourceProvider(string connStr)
            : base(connStr)
        {
        }

        #region 重写模板方法
        protected override VideoSourceInfo GetingItemByID(int id, ParkDataContext parking)
        {
            return parking.VideoSource.SingleOrDefault(v => v.VideoID == id);
        }
        #endregion
    }
}
