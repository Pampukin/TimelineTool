using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackClipType(typeof(FireGroupClip))]
public class FireGroupTrack : TrackAsset
{
    /// <summary>
    /// クリップの更新
    /// </summary>
    /// <param name="graph"></param>
    /// <param name="go"></param>
    /// <param name="inputCount"></param>
    /// <returns></returns>
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        foreach (var clip in GetClips())
        {
            SetClip(clip);
        }

        return base.CreateTrackMixer(graph, go, inputCount);
    }

    /// <summary>
    /// クリップの作成時の値の設定
    /// </summary>
    /// <param name="clip"></param>
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
