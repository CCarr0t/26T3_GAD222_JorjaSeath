using TMPro;
using UnityEngine.Playables;

public class TypewriterBehaviour : PlayableBehaviour
{
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        TMP_Text textComponent = playerData as TMP_Text;
        if (textComponent == null) return;

        // Ensure TMP has parsed the text info so characterCount is accurate
        textComponent.ForceMeshUpdate();
        int totalCharacters = textComponent.textInfo.characterCount;

        // Calculate normalized progress (0.0 to 1.0) across the Timeline clip
        double duration = playable.GetDuration();
        double currentTime = playable.GetTime();
        float progress = duration > 0 ? (float)(currentTime / duration) : 1f;

        textComponent.maxVisibleCharacters = UnityEngine.Mathf.RoundToInt(totalCharacters * progress);
    }
}