using Common.Interfaces.MetaData;
using System;

namespace fileserverapp.Models
{
    public class MetadataDto
    {
        public string MetadataDescription { get; set; }

        public MetadataDto(IMetadata metadata)
        {
            if (Math.Min(metadata.Size, Math.Pow(2, 10)) == metadata.Size)
            {
                MetadataDescription = $"Size: {metadata.Size} bytes";
            }
            else if (Math.Min(Math.Pow(2, 20), Math.Max(metadata.Size, Math.Pow(2, 10))) == metadata.Size)
            {
                MetadataDescription = $"Size: {Math.Round(metadata.Size / Math.Pow(2, 10), 1)} KiB";
            }
            else
            {
                MetadataDescription = $"Size: {Math.Round(metadata.Size / Math.Pow(2, 20), 1)} MiB";
            }
        }
    }
}