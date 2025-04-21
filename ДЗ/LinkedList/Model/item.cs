using System;
using System.Collections;
using System.IO.Ports;
using System.Runtime.Remoting.Messaging;

namespace Cslight.Model    
{

    // Ячейка списка

    public class Item<T>
    {
        private T data = default(T);

        // Данные хранимые в ячейке списка


        private Item<T> next = null;

        public T Data
        {
            get => data;
            set{ if (value != null) 
                    data = value;
                else 
                    throw new ArgumentNullException(nameof(value)); }
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
