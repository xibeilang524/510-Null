using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Park.UserControls
{
    /// <summary>
    /// 表示一个名称值对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class TextValueItem<T>
    {
        /// <summary>
        /// 值
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Text { get; set; }

        public TextValueItem()
        {
        }

        public TextValueItem(T value, string text)
        {
            Value = value;
            Text = text;
        }
    }
}
