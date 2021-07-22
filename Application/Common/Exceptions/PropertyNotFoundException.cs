using System;

namespace Application.Common.Exceptions
{
    public class PropertyNotFoundException : Exception
    {
        public PropertyNotFoundException(string type, string property) : base($"Property {property} was not found in type {type}")
        {
        }
    }
}