using System;

namespace _20GRPED.MVC2.A02.Domain.Model.Exceptions
{
    public class EntityValidationException : Exception
    {
        public string PropertyName { get; }

        public EntityValidationException(string propertyName, string message)
            : base(message)
        {
            PropertyName = propertyName;
        }

        public EntityValidationException(string propertyName, string message, Exception inner)
            : base(message, inner)
        {
            PropertyName = propertyName;
        }
    }
}
