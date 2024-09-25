using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Playables;

[System.Serializable]
public class FireGroupClip : PlayableAsset
{
    [SerializeField] private FireGroupEnum _fireGroup;

    private GameObject _fireInstancePrefab = default;

    // クリップが再生される際のロジックを設定
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        // ランタイム時のみ実行するように設定
        if (!Application.isPlaying)
        {
            return Playable.Null; // エディタモードでは何もしない
        }

        if (_fireInstancePrefab == default)
        {
            var handleFireInstance = Addressables.LoadAssetAsync<GameObject>("FireInstance");
            _fireInstancePrefab = handleFireInstance.WaitForCompletion();
            Addressables.Release(handleFireInstance);
        }

        var playableDirectorObject = Instantiate(_fireInstancePrefab);
        var playableDirector = playableDirectorObject.GetComponent<PlayableDirector>();
        var fireGroupBehaviour = new FireGroupBehaviour
        {
            FireGroup = _fireGroup,
            PlayableDirector = playableDirector
        };

        return ScriptPlayable<FireGroupBehaviour>.Create(graph, fireGroupBehaviour);
    }
}
