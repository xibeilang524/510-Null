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
        private Dictionary<YCTItem, YCTReader> _Readers = new Dictionary<YCTItem, YCTReader>();
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
            List<YCTItem> keys = _Readers.Keys.ToList();
            if (keys != null && keys.Count > 0)//将所有不在新设置中的读卡器删除
            {
                foreach (var key in keys)
                {
                    var item = Setting.Items != null ? Setting.Items.SingleOrDefault(it => it.Comport == key.Comport) : null;
                    if (item == null)
                    {
                        var reader = _Readers[key];
                        reader.Close();
                        _Readers.Remove(key);
                    }
                    else
                    {
                        key.EntranceID = item.EntranceID;
                    }
                }
            }
            if (Setting.Items != null)
            {
                foreach (var item in Setting.Items)
                {
                    if (keys == null || !keys.Exists(it => it.Comport == item.Comport))
                    {
                        var reader = new YCTReader((byte)item.Comport, 57600);
                        reader.Open();
                        _Readers[item] = reader;
                    }
                }
            }
        }

        public void Dispose()
        {
            foreach (var item in _Readers)
            {
                item.Value.Close();
            }
            _Readers.Clear();
        }
        #endregion
    }
}
