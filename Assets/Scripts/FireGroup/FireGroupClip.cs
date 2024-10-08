using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[System.Serializable]
public class FireGroupClip : PlayableAsset
{
    [SerializeField] private FireGroupEnum _fireGroup;

    public FireGroupEnum FireGroup => _fireGroup;

    private GameObject _fireInstancePrefab = default;

    private double _clipDuration = 1;

    public double ClipDuration => _clipDuration;

    /// <summary>
    /// クリップ生成時にデータのロードと実行内容の追加
    /// </summary>
    /// <param name="graph"></param>
    /// <param name="owner"></param>
    /// <returns></returns>
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        // ランタイム時のみ実行するように設定
        if (!Application.isPlaying)
        {
            return Playable.Null; // エディタモードでは何もしない
        }

        if (_fireInstancePrefab == default)
        {
            LoadData();
        }

        return ScriptPlayable<FireGroupBehaviour>.Create(graph, SetBehaviour());
    }

    /// <summary>
    /// データのロード
    /// </summary>
    private void LoadData()
    {
        var handleFireInstance = Addressables.LoadAssetAsync<GameObject>("FireInstance");
        _fireInstancePrefab = handleFireInstance.WaitForCompletion();
        Addressables.Release(handleFireInstance);
    }

    /// <summary>
    /// 具体的な処理内容の設定
    /// </summary>
    /// <returns></returns>
    private FireGroupBehaviour SetBehaviour()
    {
        var playableDirectorObject = Instantiate(_fireInstancePrefab);
        var playableDirector = playableDirectorObject.GetComponent<PlayableDirector>();
        return new FireGroupBehaviour
        {
            FireGroup = _fireGroup,
            PlayableDirector = playableDirector
        };
    }

    /// <summary>
    /// クリップの時間を設定
    /// </summary>
    public void LoadDuration()
    {
        var fireGroupTimelineHandle = Addressables.LoadAssetAsync<TimelineAsset>("FireGroupTimeline");

        var fireGroupTimeline = fireGroupTimelineHandle.WaitForCompletion();
        Addressables.Release(fireGroupTimelineHandle);

        foreach (var track in fireGroupTimeline.GetRootTracks())
        {
            if (track.name == _fireGroup.ToString())
            {
                foreach (var child in track.GetChildTracks())
                {
                    var markers = child.GetMarkers();

                    if (markers.Any())
                    {
                        // 時間でソートし、最後のマーカーを取得
                        var lastMarker = markers.OrderBy(marker => marker.time).Last();
                        _clipDuration = lastMarker.time;
                    }
                    else
                    {
                        _clipDuration = 0;
                    }
                }
            }
        }
    }
}
