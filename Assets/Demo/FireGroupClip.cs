using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[System.Serializable]
public class FireGroupClip : PlayableAsset
{
    [SerializeField] private FireGroupEnum _fireGroup;

    [SerializeField] private GroupTrack _track;

    private FireGroupBehaviour _fireGroupBehaviour;
    
    // クリップが再生される際のロジックを設定
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        _fireGroupBehaviour = new FireGroupBehaviour
        {
            FireGroup = _fireGroup
        };
        
        return ScriptPlayable<FireGroupBehaviour>.Create(graph, _fireGroupBehaviour);
    }
}
