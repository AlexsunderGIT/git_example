using System.Net;
using System.Net.Mime;
using Microsoft.AspNetCore.Diagnostics;
using ConsoleProject.NET.Exceptions;
namespace ConsoleProject.NET.Services
{
    public class ExceptionHandler : IExceptionHandler
    {
        // Метод, реализацию которого требует интерфейс;
        // Нужно добавить слово async, если его нет (о нем поговорим в другой раз).
        public async ValueTask<bool> TryHandleAsync(
        // Содержит ВСЮ информацию о запросе к нашему API.// В том числе и ответ, доступный через свойство .Response.
        // Если middleware изменяет ответ, оно не должно передавать вызов дальше,
        // так как могут быть ошибки, если кто-то его перепишет.
        HttpContext httpContext,
        // Объект исключения, которое было брошено где-то в программе.
        // Используется полиморфизм.
        // Так как базовый класс для всех исключений - Exception, мы можем
        // передать сюда объект любого исключения таким образом.
        Exception exception,
        // Пока не будем использовать, узнаем о нем вместе с async.
        CancellationToken cancellationToken)
        {
            // Проверяем, что ошибка, которая пришла к нам - ошибка, которую мы бросаем,
            // если пользователя нет.
            // (Когда у нас много ошибок, тут используем switch.)
            if (exception is UserNotFoundException userNotFoundException)
            {
                // Указываем, что в ответ мы будем писать текст.
                httpContext.Response.ContentType = MediaTypeNames.Text.Plain;
                // Указываем код ошибки.
                // Ожидает int, но мы используем встроенный enum, чтобы не было
                // "магических" чисел в коде.
                // В данном случае нам отлично подходит вариант с NotFound.
                httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                // Записываем ответ на запрос через свойство для ответа.
                // Тут важно указать await, чтобы все работало (о нем поговорим с async).
                // Используем свойство .Message у объекта исключения, чтобы получить
                // то сообщение, которое указывали в конструкторе исключения.
                await httpContext.Response.WriteAsync(userNotFoundException.Message);
                // Так как эта ошибка из тех, что мы ожидаем, при ее обработке
                // возвращаем true.
                // true - сигнал об успешной обработке ошибки.
                // Такую сигнатуру метода определили microsoft.
                return true;
            }
            if (exception is NoteNotFoundException noteNotFoundException)
            {
                // Указываем, что в ответ мы будем писать текст.
                httpContext.Response.ContentType = MediaTypeNames.Text.Plain;
                // Указываем код ошибки.
                // Ожидает int, но мы используем встроенный enum, чтобы не было
                // "магических" чисел в коде.
                // В данном случае нам отлично подходит вариант с NotFound.
                httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                // Записываем ответ на запрос через свойство для ответа.
                // Тут важно указать await, чтобы все работало (о нем поговорим с async).
                // Используем свойство .Message у объекта исключения, чтобы получить
                // то сообщение, которое указывали в конструкторе исключения.
                await httpContext.Response.WriteAsync(noteNotFoundException.Message);
                // Так как эта ошибка из тех, что мы ожидаем, при ее обработке
                // возвращаем true.
                // true - сигнал об успешной обработке ошибки.
                // Такую сигнатуру метода определили microsoft.
                return true;
            }
            // Если у нас ошибка, которую мы не ожидаем, то пишем InternalServerError.
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            // Затираем ответ, чтобы ничего лишнего не попало на вывод пользователю.
            await httpContext.Response.WriteAsync(string.Empty);
            // Возвращаем false, так как этот сценарий нами не запланирован.
            return false;
        }
    }

}


