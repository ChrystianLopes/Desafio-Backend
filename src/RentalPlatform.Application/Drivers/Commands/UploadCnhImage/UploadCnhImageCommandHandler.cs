using MediatR;
using RentalPlatform.Application.Common.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RentalPlatform.Application.Drivers.Commands.UploadCnhImage;

public class UploadCnhImageCommandHandler : IRequestHandler<UploadCnhImageCommand>
{
    private readonly IDriverRepository _driverRepository;
    private readonly IFileStorageService _fileStorageService;
    private readonly IUnitOfWork _unitOfWork;

    public UploadCnhImageCommandHandler(IDriverRepository driverRepository, IFileStorageService fileStorageService, IUnitOfWork unitOfWork)
    {
        _driverRepository = driverRepository;
        _fileStorageService = fileStorageService;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UploadCnhImageCommand request, CancellationToken cancellationToken)
    {
        var driver = await _driverRepository.GetByIdAsync(request.DriverId, cancellationToken);
        if (driver is null)
        {
            throw new InvalidOperationException("Entregador não encontrado.");
        }

        var allowedContentTypes = new[] { "image/png", "image/bmp" };
        if (!allowedContentTypes.Contains(request.ContentType.ToLower()))
        {
            throw new InvalidOperationException("Formato de arquivo inválido. Apenas PNG e BMP são permitidos.");
        }

        var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(request.FileName)}";
        var imageUrl = await _fileStorageService.UploadFileAsync(request.ImageStream, uniqueFileName, request.ContentType, cancellationToken);
        driver.UpdateCnhImage(imageUrl);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}