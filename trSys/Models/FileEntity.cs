using System;
using trSys.Interfaces;

namespace trSys.Models;

public class FileEntity : IEntity
{

    public int Id { get; set; }
    public string FileName { get; set; }
    public string ContentType { get; set; }
    public byte[] Data { get; set; }
    public DateTime UploadDate { get; set; } = DateTime.UtcNow;

}
