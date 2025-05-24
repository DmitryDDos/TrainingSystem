using System.Threading.Tasks;
using trSys.DTOs;

namespace trSys.Interfaces
{
    public interface IFileService
    {
        Task<int> UploadFileAsync(FileUploadDto fileDto);
        Task<FileDownloadDto> DownloadFileAsync(int id);
    }
}

