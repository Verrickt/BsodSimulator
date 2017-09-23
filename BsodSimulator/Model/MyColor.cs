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
        public string Name { get; private set; }
        public Brush Brush { get; private set; }
        public byte R { get;private set; }
        public byte B { get; private set; }
        public byte G { get; private set; }
        public byte A { get; private set; }

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
                                  let color = (Color)info.GetValue(null)
                                  select new MyColor
                                  {
                                      Name = info.Name,
                                      Brush = new SolidColorBrush(color),
                                      R = color.R,
                                      G = color.G,
                                      B = color.B,
                                      A = color.A
                                  };
                myColors = solidBrushs.ToList();
            }
            return myColors;
        }
    }
}
