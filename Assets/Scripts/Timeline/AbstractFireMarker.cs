using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public abstract class AbstractFireMarker : Marker, INotification
{
    protected GameObject _bullet = default;
    protected BulletData _bulletData = default;

    public PropertyName id { get; }

    /// <summary>
    /// 発射処理
    /// </summary>
    public virtual void Fire()
    {
        if (_bullet == default || _bulletData == default)
        {
            _Load();
        }
    }

    /// <summary>
    /// データのロード
    /// </summary>
    private void _Load()
    {
        var handleBullet = Addressables.LoadAssetAsync<GameObject>("Bullet");
        var handleData = Addressables.LoadAssetAsync<BulletData>("FireData");

        _bullet = handleBullet.WaitForCompletion();
        _bulletData = handleData.WaitForCompletion();

        Addressables.Release(handleBullet);
        Addressables.Release(handleData);
    }

    /// <summary>
    /// マーカーの変数コピー
    /// </summary>
    /// <param name="copyMaker">コピー先</param>
    /// <param name="origin">コピー元</param>
    public abstract void Copy(ref AbstractFireMarker copyMaker, AbstractFireMarker origin);
}
