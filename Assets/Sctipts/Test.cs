using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class Test : MonoBehaviour
{
    [SerializeField] private PlayableDirector _playableDirector;

    [SerializeField] private FireGroupEnum _fireGroup;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Resume();
        }
    }

    private void Resume()
    {
        var director = Instantiate(_playableDirector);
        var timeline = (TimelineAsset)director.playableAsset;
        // 特定のGroupTrackを取得
        foreach (var track in timeline.GetRootTracks())
        {
            if (track is GroupTrack groupTrack)
            {
                groupTrack.muted = groupTrack.name != _fireGroup.ToString();
            }
        }
        
        director.Play();
    }
}
