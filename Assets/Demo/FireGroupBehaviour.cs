using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class FireGroupBehaviour : PlayableBehaviour
{
    public FireGroupEnum FireGroup;
    
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        Debug.Log($"{FireGroup.ToString()}");

        // // Playable から PlayableGraph を取得
        // var graph = playable.GetGraph();
        // var rootPlayable = graph.GetRootPlayable(0);
        // var director = rootPlayable.GetGraph().GetResolver() as PlayableDirector;
        // var timeline = director?.playableAsset as TimelineAsset;
        // GroupTrack targetGroupTrack = null;
        //
        // director.Stop();
        //
        //
        // // 特定のGroupTrackを取得
        // if (timeline != null)
        // {
        //     foreach (var track in timeline.GetRootTracks())
        //     {
        //         if (track is GroupTrack groupTrack)
        //         {
        //             if (groupTrack.name == FireGroup.ToString())
        //             {
        //                 targetGroupTrack = groupTrack;
        //                 break;
        //             }
        //         }
        //     }
        // }
        //
        // if (targetGroupTrack != null)
        // {
        //     // 複製する新しいGroupTrackを作成
        //     GroupTrack newGroupTrack = timeline.CreateTrack<GroupTrack>(null, "NewGroupTrack");
        //
        //     // 元のGroupTrack内のすべてのトラックをコピー
        //     foreach (var track in targetGroupTrack.GetChildTracks())
        //     {
        //         TrackAsset newTrack = timeline.CreateTrack(track.GetType(), newGroupTrack, track.name);
        //         director.SetGenericBinding(newTrack,director.gameObject.GetComponent<FireReceiver>());
        //         // トラック内のクリップをコピー
        //         foreach (var clip in track.GetClips())
        //         {
        //             //var newClip = newTrack.CreateClip<Fire>();
        //             //newClip.start = clip.start;
        //             //newClip.duration = clip.duration;
        //         }
        //     }
        //
        //     
        //     // 新しいGroupTrackを任意のタイミングで実行
        //     director.RebuildGraph();
        //     director.Play();
        // }
    }
}