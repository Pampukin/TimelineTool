using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class FireGroupBehaviour : PlayableBehaviour
{
    public FireGroupEnum FireGroup;
    public PlayableDirector PlayableDirector;

    private PlayableDirector _newDirector;

    /// <summary>
    /// 弾幕パターンの取得
    /// </summary>
    /// <param name="tracks"></param>
    /// <returns></returns>
    private GroupTrack GetTargetTrack(IEnumerable<TrackAsset> tracks)
    {
        GroupTrack targetGroupTrack = null;

        foreach (var track in tracks)
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
        return targetGroupTrack;
    }

    /// <summary>
    /// 実行用のタイムラインの作成
    /// </summary>
    /// <param name="newTimeline"></param>
    /// <param name="targetGroupTrack"></param>
    private void DuplicateTimeline(ref TimelineAsset newTimeline, GroupTrack targetGroupTrack)
    {
        PlayableDirector.playableAsset = newTimeline;
        var newGroupTrack = newTimeline?.CreateTrack<GroupTrack>(null, targetGroupTrack.name);

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
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        var graph = playable.GetGraph();
        var director = graph.GetResolver() as PlayableDirector;
        var timeline = director?.playableAsset as TimelineAsset;
        if (timeline == null) return;

        var targetGroupTrack = GetTargetTrack(timeline.GetRootTracks());
        if (targetGroupTrack == null) return;

        var newTimeline = ScriptableObject.CreateInstance<TimelineAsset>();

        DuplicateTimeline(ref newTimeline, targetGroupTrack);

        PlayableDirector?.RebuildGraph();
        PlayableDirector?.Play();
    }
}
