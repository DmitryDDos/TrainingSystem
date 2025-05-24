using System;
using System.IO;
using System.Threading.Tasks;
using trSys.DTOs;
using trSys.Interfaces;
using trSys.Models;
using trSys.Mappers;

namespace trSys.Services
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;

        public FileService(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public async Task<int> UploadFileAsync(FileUploadDto fileDto)
        {
            if (fileDto.File == null || fileDto.File.Length == 0)
                throw new ArgumentException("Файл не выбран");

            using var memoryStream = new MemoryStream();
            await fileDto.File.CopyToAsync(memoryStream);

            var fileEntity = new FileEntity
            {
                FileName = fileDto.File.FileName,
                ContentType = fileDto.File.ContentType,
                Data = memoryStream.ToArray()
            };

            return await _fileRepository.AddAsync(fileEntity);
        }

        public async Task<FileDownloadDto> DownloadFileAsync(int id)
        {
            var fileEntity = await _fileRepository.GetByIdAsync(id);
            if (fileEntity == null)
                throw new FileNotFoundException("Файл не найден");

            return Mapper.MapToDto(fileEntity); // Используем ваш собственный маппер
        }
    }
}
