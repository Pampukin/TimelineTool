using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class FireGroupBehaviour : PlayableBehaviour
{
    public FireGroupEnum FireGroup;
    public PlayableDirector PlayableDirector;

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        var graph = playable.GetGraph();
        var director = graph.GetResolver() as PlayableDirector;
        var timeline = director?.playableAsset as TimelineAsset;

        if (timeline == null) return;
        GroupTrack targetGroupTrack = null;

        foreach (var track in timeline.GetRootTracks())
        {
            if (track is GroupTrack groupTrack)
            {
                if (groupTrack.name == FireGroup.ToString())
                {
                    targetGroupTrack = groupTrack;
                    break;
                }
            }
        }

        if (targetGroupTrack == null) return;

        var newGraph = PlayableGraph.Create();
        var newDirector = PlayableDirector;
        var newTimeline = ScriptableObject.CreateInstance<TimelineAsset>();
        newDirector.playableAsset = newTimeline;

        // 複製する新しいGroupTrackを作成
        var newGroupTrack = newTimeline?.CreateTrack<GroupTrack>(null, targetGroupTrack.name);

        // 元のGroupTrack内のすべてのトラックをコピー
        foreach (var track in targetGroupTrack.GetChildTracks())
        {
            var newTrack = newTimeline?.CreateTrack(track.GetType(), newGroupTrack, track.name);

            newDirector?.SetGenericBinding(newTrack,newDirector.gameObject.GetComponent<FireReceiver>());

            foreach (var marker in track.GetMarkers())
            {
                Debug.Log(marker.ToString());
                // INotificationをコピー
                if (marker is INotification notificationMarker)
                {
                    var newMaker = newTrack.CreateMarker(marker.GetType(), marker.time);
                }
            }
        }

        newDirector?.RebuildGraph();
        newDirector?.Play();
    }

    private void OnDisable()
    {
        // PlayableGraphを破棄してリソースを解放
        //director.playableGraph.Destroy();
    }
}
