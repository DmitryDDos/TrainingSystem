using System;
using trSys.DTOs;
using trSys.Models;

namespace trSys.Mappers;

public static class Mapper
{
    public static FileDownloadDto MapToDto(FileEntity fileEntity)
    {
        if (fileEntity == null) return null;

        return new FileDownloadDto
        {
            Data = fileEntity.Data,
            ContentType = fileEntity.ContentType,
            FileName = fileEntity.FileName
        };
    }
}

