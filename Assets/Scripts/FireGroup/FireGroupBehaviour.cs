using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class FireGroupBehaviour : PlayableBehaviour
{
    public FireGroupEnum FireGroup;
    public PlayableDirector PlayableDirector;

    private PlayableDirector _newDirector;

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

        var newTimeline = ScriptableObject.CreateInstance<TimelineAsset>();
        PlayableDirector.playableAsset = newTimeline;

        // 複製する新しいGroupTrackを作成
        var newGroupTrack = newTimeline?.CreateTrack<GroupTrack>(null, targetGroupTrack.name);

        // 元のGroupTrack内のすべてのトラックをコピー
        foreach (var track in targetGroupTrack.GetChildTracks())
        {
            var newTrack = newTimeline?.CreateTrack(track.GetType(), newGroupTrack, track.name);

            PlayableDirector?.SetGenericBinding(newTrack,PlayableDirector.gameObject.GetComponent<FireReceiver>());

            foreach (var marker in track.GetMarkers())
            {
                // INotificationをコピー
                if (marker is AbstractFireMarker fireMarker)
                {
                    var newMaker = newTrack?.CreateMarker(fireMarker.GetType(), fireMarker.time);

                    if (newMaker is AbstractFireMarker maker)
                    {
                        maker.Copy(ref maker, fireMarker);
                    }
                }
            }
        }

        PlayableDirector?.RebuildGraph();
        PlayableDirector?.Play();
    }
}
