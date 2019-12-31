using System;
using System.Collections.Generic;
using System.Text;

namespace AlloyTestApp.Application.Exceptions
{
    /// <summary>
    /// Кастомный exception, для отслеживания проблем при обновлении данных
    /// </summary>
    public class UpdateDataException : Exception
    {
        /// <summary>
        /// Название свойства, являющегося причиной возникновения проблемы
        /// </summary>
        public string PropertyName { get;}

        /// <param name="message">Собщение об ошибке передается в базовый класс Exception</param>
        /// <param name="propertyName">Название свойства, являющегося причиной возникновения проблемы</param>
        public UpdateDataException(string message, string propertyName)
            :base(message)
        {
            PropertyName = propertyName;
        }

    }
}
