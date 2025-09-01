using System;

namespace RentalPlatform.Application.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string name, object key)
        : base($"Entidade de nome \"{name}\" ({key}) não foi encontrada.")
    {
    }
}