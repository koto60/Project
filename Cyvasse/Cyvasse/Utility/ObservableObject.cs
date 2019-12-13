using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cyvasse.Utility {
    /// <summary>
    /// Standard implementation of INotifyPropertyChanged. It is made into ObservableObject to avoid code repetition.
    /// </summary>
    public abstract class ObservableObject : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName) {

            if (PropertyChanged != null) {

                var e = new PropertyChangedEventArgs(propertyName);
                PropertyChanged(this, e);
            }
        }
    }
}
