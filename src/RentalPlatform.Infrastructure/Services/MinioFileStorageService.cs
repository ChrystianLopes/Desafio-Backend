using Microsoft.Extensions.Configuration;
using Minio;
using Minio.DataModel.Args;
using RentalPlatform.Application.Common.Interfaces;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace RentalPlatform.Infrastructure.Services;

public class MinioFileStorageService : IFileStorageService
{
    private readonly IMinioClient _minioClient;
    private readonly string _bucketName;
    private readonly string _endpoint;

    public MinioFileStorageService(IConfiguration configuration)
    {
        _endpoint = configuration["Minio:Endpoint"] ?? throw new ArgumentNullException("Minio:Endpoint");
        var accessKey = configuration["Minio:AccessKey"] ?? throw new ArgumentNullException("Minio:AccessKey");
        var secretKey = configuration["Minio:SecretKey"] ?? throw new ArgumentNullException("Minio:SecretKey");
        _bucketName = configuration["Minio:BucketName"] ?? throw new ArgumentNullException("Minio:BucketName");

        _minioClient = new MinioClient()
            .WithEndpoint(_endpoint)
            .WithCredentials(accessKey, secretKey)
            .Build();
    }

    public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType, CancellationToken cancellationToken)
    {
        var beArgs = new BucketExistsArgs().WithBucket(_bucketName);
        if (!await _minioClient.BucketExistsAsync(beArgs, cancellationToken))
        {
            await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(_bucketName), cancellationToken);
        }

        var putObjectArgs = new PutObjectArgs().WithBucket(_bucketName).WithObject(fileName).WithStreamData(fileStream).WithObjectSize(fileStream.Length).WithContentType(contentType);
        await _minioClient.PutObjectAsync(putObjectArgs, cancellationToken);

        return $"http://{_endpoint}/{_bucketName}/{fileName}";
    }
}