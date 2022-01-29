using System;

namespace Application.Common.Exceptions;

// Todo: Remove. Take a look on Sieve.
public class PropertyNotFoundException : Exception
{
    public PropertyNotFoundException(string type, string property) : base($"Property {property} was not found in type {type}")
    {
    }
}
