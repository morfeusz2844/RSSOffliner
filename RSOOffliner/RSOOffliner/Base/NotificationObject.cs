using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RSOOffliner.Base
{
    public abstract class NotificationObject:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
