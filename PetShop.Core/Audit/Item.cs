﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Core.Audit
{
    public partial class AuditHelper
    {
        public class Item
        {
            public Item(string key, string value)
            {
                Key = key;
                Value = value;
            }

            public string Key { get; set; }
            public string Value { get; set; }
        }
    }
}
