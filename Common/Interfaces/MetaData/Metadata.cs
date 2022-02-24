namespace Common.Interfaces.MetaData
{
    public class Metadata : IMetadata
    {
        private readonly long size;

        public Metadata()
        {
            size = 0;
        }

        public Metadata(long size)
        {
            this.size = size;
        }

        public long Size => size;
    }
}
