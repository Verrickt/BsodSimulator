using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace BsodSimulator.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName]string propertyName=null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetProperty<T>(ref T property,T value, [CallerMemberName]string propertyName = null,Action callback=null)
        {
            if (!EqualityComparer<T>.Default.Equals(property,value))
            {
                property = value;
                OnPropertyChanged(propertyName);
                callback?.Invoke();
            }
        }
    }
}
