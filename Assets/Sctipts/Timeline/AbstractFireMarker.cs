using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public abstract class AbstractFireMarker : Marker, INotification
{
    protected GameObject _bullet = default;
    protected FireData _fireData = default;
    
    public PropertyName id { get; }

    public virtual void Fire()
    {
        if (_bullet == default || _fireData == default)
        {
            _Load();
        }
    }

    private void _Load()
    {
        var handleBullet = Addressables.LoadAssetAsync<GameObject>("Bullet"); 
        var handleData = Addressables.LoadAssetAsync<FireData>("FireData"); 
        
        _bullet = handleBullet.WaitForCompletion();
        _fireData = handleData.WaitForCompletion();
        
        Addressables.Release(handleBullet);
        Addressables.Release(handleData);
    }
}
