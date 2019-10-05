namespace LDGJ45.Persistence
{
    public interface IAssetsDatabase
    {
        T Load<T>(AssetId assetId, bool cached = true);
    }
}