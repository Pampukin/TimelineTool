using UnityEngine;
using UnityEngine.Timeline;

[TrackClipType(typeof(FireGroupClip))]
public class FireGroupTrack : TrackAsset
{
    protected override void OnCreateClip(TimelineClip clip)
    {
        base.OnCreateClip(clip);

        // クリップのdurationを設定
        clip.duration = 1f;

        // 追加されたクリップのdurationをデバッグ表示
        Debug.Log($"Clip '{clip.displayName}' added with duration: {clip.duration}");
    }
}
