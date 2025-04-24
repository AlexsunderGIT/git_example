using System;
using System.Collections;
using System.IO.Ports;
using System.Runtime.Remoting.Messaging;

namespace Cslight.Model    
{
    // Ячейка списка
    public class Item<T>
    {
        private T _data = default(T);
        // Данные хранимые в ячейке списка
        private Item<T> _next = null;
        public T Data
        {
            get => _data;
            set{ if (value != null) 
                    _data = value;
                else 
                    throw new ArgumentNullException(nameof(Data));}
        }
        // След ячейка списка
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
