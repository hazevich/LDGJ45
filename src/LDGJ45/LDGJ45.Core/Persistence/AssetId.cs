namespace LDGJ45.Core.Persistence
{
    public struct AssetId
    {
        public AssetId(string assetId)
        {
            Value = assetId;
        }

        public string Value { get; }

        public static implicit operator string(AssetId assetId)
        {
            return assetId.Value;
        }

        public static implicit operator AssetId(string assetId)
        {
            return new AssetId(assetId);
        }

        public override string ToString()
        {
            return Value;
        }
    }
}