using System;
using System.Collections;
using System.IO.Ports;
using System.Runtime.Remoting.Messaging;

namespace Cslight.Model    
{

    public class Item<T>
    {
        private T _data = default(T);
        public T Data
        {
            get => _data;
            set{ if (value != null) 
                    _data = value;
                else 
                    throw new ArgumentNullException(nameof(Data));}
        }
        public Item<T> Next { get ; set; }
        public Item(T data)
        {
            Data = data;
        }
        public override string ToString()
        {
            return Data.ToString();
        }
    }
}
