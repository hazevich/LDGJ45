namespace LDGJ45.Core.Persistence
{
    public interface IAssetsDatabase
    {
        T Load<T>(AssetId assetId, bool cached = true);
        void Store<T>(T asset, AssetId assetId);
    }
}