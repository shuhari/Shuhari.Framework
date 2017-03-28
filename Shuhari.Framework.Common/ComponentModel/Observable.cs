using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Shuhari.Framework.Utils;

namespace Shuhari.Framework.ComponentModel
{
    /// <summary>
    /// Base implementation for INotifyPropertyChanged, normally used in WPF applications
    /// </summary>
    public abstract class Observable : INotifyPropertyChanged
    {
        /// <inheritdoc />
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notify property changed
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Set field value, and raise <see cref="PropertyChanged"/> if value changed
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propName"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        protected void SetProperty<T>(string propName, ref T field, T value)
        {
            Expect.IsNotBlank(propName, nameof(propName));

            if (!Equals(field, value))
            {
                field = value;
                OnPropertyChanged(propName);
            }
        }
    }
}
