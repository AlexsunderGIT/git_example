using System;
using System.Collections;
using System.Collections.Generic;


namespace Cslight.Model
{
    // Односвязный список
    public class LinkedList<T> : IEnumerable
    {
        // Первый элемент списка 
        public Item<T> Head { get; private set; }

        //Последний элемент списка
        public Item<T> Tail { get; private set; }

        //Количество элементов
        public int Count { get; private set; }
        //Создание пустого списка
        public LinkedList()
        {
            Clear();
        }

        //Создание списка с нач элементом
        public LinkedList(T data)
        {
            SetHeadAndTail(data);
        }
        //Добавление данные в конец списка
        public void Add(T data)
        {
            if (Tail != null)
            {
                var item = new Item<T>(data);
                Tail.Next = item;
                Tail = item;
                Count++;
            }
            else
            {
                SetHeadAndTail(data);
            }
        }
        //Удаление первое вхождение данных в список
        public void Delete(T data)
        {
            if (Head != null)
            {
                if (Head.Data.Equals(data))
                {
                    Head = Head.Next;
                    Count--;
                    return;
                }

                var current = Head.Next;
                var previous = Head;

                while (current != null)
                {
                    if (current.Data.Equals(data))
                    {
                        previous.Next = current.Next;
                        Count--;
                        return;
                    }

                    previous = current;
                    current = current.Next;
                }
            }
            else
            {
                SetHeadAndTail(data);
            }
        }
        // Добавление данных в начале списка
        public void AppendHead(T data)
        {
            var item = new Item<T>(data)
            {
                Next = Head
            };
            Head = item;
            Count++;
        }
        //Вставление данных после искомого значения
        public void InsertAfter(T target, T data)
        {
            if (Head != null)
            {
                var current = Head;
                while (current != null)
                {
                    if (current.Data.Equals(target))
                    {
                        var item = new Item<T>(data);
                        item.Next = current.Next;
                        current.Next = item;
                        Count++;
                        return;
                    }
                    else
                    {
                        current = current.Next;
                    }
                }
            }
        }
        //Очищение списка
        public void Clear()
        {
            Head = null;
            Tail = null;
            Count = 0;
        }

        private void SetHeadAndTail(T data)
        {
            var item = new Item<T>(data);
            Head = item;
            Tail = item;
            Count = 1;
        }

        //Получение перечисление всех элементов списка
        public IEnumerator GetEnumerator()
        {
            var current = Head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        public override string ToString()
        {
            return "Linked List " + Count + " элементов";
        }
        public void Replace(int targetIndex, T data)
        {
            if (targetIndex < 0)
            throw new ArgumentOutOfRangeException(nameof(targetIndex), "только положительный индекс");

            Item<T> current = Head;
            Item<T> prev = null;
            int currentIndex = 0;

            while (current != null && currentIndex < targetIndex)
            {
                prev = current;
                current = current.Next;
                currentIndex++;
            }

            if (current == null)
            throw new ArgumentOutOfRangeException(nameof(targetIndex), "индекс за пределеами листа");

            Item<T> newItem = new Item<T>(data);

            if (prev == null)
            {
                newItem.Next = Head.Next;
                Head = newItem;
            }
            else
            {
                newItem.Next = current.Next;
                prev.Next = newItem;
            }
            if (newItem.Next == null)
            Tail = newItem;
        }
    }
}