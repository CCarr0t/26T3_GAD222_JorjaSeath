using UnityEngine;
using UnityEngine.Playables;

public class TypewriterClip : PlayableAsset
{
    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        return ScriptPlayable<TypewriterBehaviour>.Create(graph);
    }
}