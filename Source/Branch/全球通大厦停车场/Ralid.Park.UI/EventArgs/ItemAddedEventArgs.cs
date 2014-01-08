using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Monitor
{
    public class ItemAddedEventArgs : EventArgs
    {
        public object AddedItem { get; set; }

        public ItemAddedEventArgs()
        {
        }

        public ItemAddedEventArgs(object item)
        {
            this.AddedItem = item;
        }
    }
}
