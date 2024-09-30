using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackClipType(typeof(FireGroupClip))]
public class FireGroupTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        foreach (var clip in GetClips())
        {
            SetClip(clip);
        }

        return base.CreateTrackMixer(graph, go, inputCount);
    }

    protected override void OnCreateClip(TimelineClip clip)
    {
        SetClip(clip);
        base.OnCreateClip(clip);
    }

    private void SetClip(TimelineClip clip)
    {
        var asset = clip.asset as FireGroupClip;
        asset.LoadDuration();
        clip.displayName = asset.FireGroup.ToString();
        clip.duration = asset.ClipDuration;
    }
}
