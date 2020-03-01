using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Shared.Dto
{
    public class ERPComboboxItem
    {
        public long Value { get; set; }

        public string DisplayText { get; set; }
        public string DisplayCode { get; set; }

        public ERPComboboxItem()
        {
        }

        public ERPComboboxItem(long value, string displayText, string displayCode)
        {
            Value = value;
            DisplayText = displayText;
            DisplayCode = displayCode;
        }
    }
}
