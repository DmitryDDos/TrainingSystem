using System;

namespace trSys.DTOs;

public class FileDownloadDto
{
    public byte[] Data { get; set; }
    public string ContentType { get; set; }
    public string FileName { get; set; }
}
