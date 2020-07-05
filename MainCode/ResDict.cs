using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;

namespace TypePracticeLite {
    static class ResDict {
        public static ResourceDictionary PreSetting = new ResourceDictionary {
            Source = new Uri("Styles/PreSetting.xaml", UriKind.Relative)
        };
    }
}
