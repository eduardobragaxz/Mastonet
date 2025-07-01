using System;
using System.IO;

namespace Mastonet;

public class MediaDefinition(Stream media, string fileName)
{
    public Stream Media { get; set; } = media ?? throw new ArgumentException("All the params must be defined", nameof(media));

    public string FileName { get; set; } = fileName ?? throw new ArgumentException("All the params must be defined", nameof(fileName));

    internal string? ParamName { get; set; }
}
