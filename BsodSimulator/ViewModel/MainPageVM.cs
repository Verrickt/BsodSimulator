using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using System.Reflection;
using Windows.UI.Xaml.Media;

namespace BsodSimulator.ViewModel
{
    public class MainPageVM:ViewModelBase
    {
        public MyColor SelectedColor { get { return selectedColor; } set { SetPropertyValue(ref selectedColor, value); } }

        private MyColor selectedColor;

        private string emoji;

        public string Emoji
        {
            get { return emoji; }
            set { SetPropertyValue(ref emoji, value); }
        }


        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        private int percent;

        public int Percentage
        {
            get { return percent; }
            set { SetPropertyValue(ref percent, value); }
        }


        private bool dynamicPercentage;

        public bool DynamicPercentage
        {
            get { return dynamicPercentage; }
            set { dynamicPercentage = value;if (value) Percentage = 0; }
        }


        private string url;

        public string Url
        {
            get { return url; }
            set { url = value; }
        }


        private string stopCode;

        public string StopCode
        {
            get { return stopCode; }
            set { stopCode = value; }
        }

        public List<MyColor> MyColors;

        private bool classicBSOD;

        public bool ClassicBSOD
        {
            get { return classicBSOD; }
            set { SetPropertyValue(ref classicBSOD, value);
                if (classicBSOD)
                {
                    SelectedColor = MyColor.GetColorByName("DodgerBlue");
                }
            }
        }

        private bool insiderGSOD;

        public bool InsiderGSOD
        {
            get { return insiderGSOD; }
            set {
                SetPropertyValue(ref insiderGSOD, value);
                if (InsiderGSOD)
                {
                    SelectedColor = MyColor.GetColorByName("LimeGreen");
                }
            }
        }

        public bool RestartUponComplete { get; set; }


        public MainPageVM()
        {
            MyColors = new List<MyColor>(MyColor.GetColors());

            Emoji = ":(";

            Description = "Your PC ran into a problem and needs to restart. We're just collecting some error info, and then we'll restart for you";

            Percentage = 0;

            Url = "http://windows.com/stopcode";

            StopCode = "KERNEL_MODE_HEAP_CORRUPTION";

            ClassicBSOD = true;

            DynamicPercentage = true;

            RestartUponComplete = true;
            
        }

        public async Task UpdateProgress()
        {
            int progress = Percentage;

            await Task.Delay(1000);//wait until navigation finishes

            Random r = new Random();
            if (!DynamicPercentage)
            {
                return;
            }
            while (percent<100)
            {
                if (r.Next()%2!=0)
                {
                    int interval = r.Next(1000, 2000);
                    await Task.Delay(interval);
                }
                else
                {
                    int step = r.Next(5, 15);
                    int interval = r.Next(500, 1000);
                    await Task.Delay(interval);
                    Percentage += step;
                }
                
            }

            Percentage = progress;

        }
       
    }

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
