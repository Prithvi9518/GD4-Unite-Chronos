using UnityEngine;
/// <summary>
/// Resource: https://www.youtube.com/watch?v=LfU5xotjbPw
/// Accessed 06/03/24
/// </summary>
public class AudioOptionsManager : MonoBehaviour
{
    public static float musicVolume { get; private set; }
    public static float soundEffectsVolume { get; private set; }

    public void OnMusicSliderValueChanged(float value)
    {
        musicVolume = value;
    }
}
