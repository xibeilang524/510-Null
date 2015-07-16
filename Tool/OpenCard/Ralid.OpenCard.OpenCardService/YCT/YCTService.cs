using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.OpenCard.OpenCardService.YCT
{
    public class YCTService : IOpenCardService
    {
        #region 构造函数
        public YCTService()
        {
        }

        public YCTService(YCTSetting setting)
        {
            Setting = setting;
        }
        #endregion

        #region 私有变量
        private Dictionary<YCTReader, YCTItem> _Readers = new Dictionary<YCTReader, YCTItem>();
        #endregion

        #region 公共属性
        public YCTSetting Setting { get; set; }
        #endregion

        #region 实现接口IOpenCardService
        public event EventHandler<OpenCardEventArgs> OnReadCard;

        public event EventHandler<OpenCardEventArgs> OnPaying;

        public event EventHandler<OpenCardEventArgs> OnPaidOk;

        public event EventHandler<OpenCardEventArgs> OnPaidFail;

        public void Init()
        {
            if (Setting == null) throw new InvalidOperationException("没有提供羊城通参数");
            if (string.IsNullOrEmpty(Setting.ServiceCode)) throw new InvalidOperationException("没有提供服务商编号");
            Dictionary<YCTReader, YCTItem> temp = new Dictionary<YCTReader, YCTItem>();
            if (Setting.Items != null)
            {
                foreach (var item in Setting.Items)
                {
                    
                }
            }
        }

        public void Dispose()
        {
            foreach (var item in _Readers)
            {
                item.Key.Close();
            }
            _Readers.Clear();
        }
        #endregion
    }
}
