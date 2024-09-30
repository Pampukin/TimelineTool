using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[System.Serializable]
public class FireGroupClip : PlayableAsset
{
    [SerializeField] private FireGroupEnum _fireGroup;

    private GameObject _fireInstancePrefab = default;

    // クリップが再生される際のロジックを設定
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var fireGroupTimelineHandle = Addressables.LoadAssetAsync<TimelineAsset>("FireGroupTimeline");

        var fireGroupTimeline = fireGroupTimelineHandle.WaitForCompletion();
        Addressables.Release(fireGroupTimelineHandle);

        double clipTime = 0;
        foreach (var track in fireGroupTimeline.GetRootTracks())
        {
            if (track.name == _fireGroup.ToString())
            {
                foreach (var child in track.GetChildTracks())
                {
                    if (child.GetMarkerCount() == 0) break;

                    var marker = child.GetMarkers().Last();
                    clipTime = marker.time;
                    Debug.Log(clipTime);
                }
            }
        }


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

        // Create the ScriptPlayable
        var playable = ScriptPlayable<FireGroupBehaviour>.Create(graph, fireGroupBehaviour);

        // Set the duration of the clip to clipTime
        playable.SetDuration(clipTime);

        return playable;
    }
}
