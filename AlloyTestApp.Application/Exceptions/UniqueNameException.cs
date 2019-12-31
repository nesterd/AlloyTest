using System;
using System.Collections.Generic;
using System.Text;

namespace AlloyTestApp.Application.Exceptions
{
    /// <summary>
    /// Exception возникающий при попытке добавить(или обновить) запись в таблицу, с именем уже имеющемся в таблице
    /// </summary>
    public class UniqueNameException : UpdateDataException
    {
        
        /// <param name="tableName">Имя таблицу в хранилеще данных</param>
        public UniqueNameException(string tableName)
            : base($"Запись с таким же значение поля 'Name' уже содержится в таблице '{tableName}'", "Name")
        {}
    }
}
