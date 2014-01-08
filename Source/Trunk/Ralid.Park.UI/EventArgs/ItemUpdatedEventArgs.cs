using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ralid.Monitor
{
    public class ItemUpdatedEventArgs : EventArgs
    {
        public ItemUpdatedEventArgs()
        { }

        public ItemUpdatedEventArgs(object updatedItem)
        {
            this.UpdatedItem = updatedItem;
        }
        public object UpdatedItem { get; set; }
    }
}
