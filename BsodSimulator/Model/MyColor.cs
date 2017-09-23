using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace BsodSimulator.Model
{
    public class MyColor
    {
        public string Name { get; set; }
        public Brush Brush { get; set; }

        private static IReadOnlyList<MyColor> myColors;

        public static MyColor GetColorByName(string name)
        {
            return myColors.Single(c => c.Name == name);
        }

        public static IReadOnlyList<MyColor> GetColors()
        {
            if (myColors == null)
            {
                var propertyInfo = typeof(Colors).GetRuntimeProperties();
                var solidBrushs = from info in propertyInfo
                                  select new MyColor
                                  {
                                      Name = info.Name,
                                      Brush = new SolidColorBrush(
                                      (Color)info.GetValue(null))
                                  };
                myColors = solidBrushs.ToList();
            }
            return myColors;
        }
    }
}
