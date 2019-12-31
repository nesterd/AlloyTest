using System;
using System.Collections.Generic;
using System.Text;

namespace AlloyTestApp.Application.Exceptions
{
    /// <summary>
    /// Exception выбрасывается при разрешении зависимостей доступа к данным
    /// </summary>
    public class DataAccessResolveException : Exception
    {
        public DataAccessResolveException(string message)
            : base(message)
        {}
    }
}
