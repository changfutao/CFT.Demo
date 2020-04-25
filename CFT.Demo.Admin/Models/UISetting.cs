using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CFT.Demo.Admin.Models
{
    public class UISetting
    {
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public EditorSetting Editor { get; set; }

        public class EditorSetting
        {
            public string Background { get; set; }
            public string Foreground { get; set; }
        }
    }
}
