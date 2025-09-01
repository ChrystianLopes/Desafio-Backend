using MediatR;
using System;
using System.IO;

namespace RentalPlatform.Application.Drivers.Commands.UploadCnhImage;

public record UploadCnhImageCommand(
    Guid DriverId,
    Stream ImageStream,
    string FileName,
    string ContentType) : IRequest;