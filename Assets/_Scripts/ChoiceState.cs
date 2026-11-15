using UnityEngine;
using UnityEngine.SceneManagement;

public static class ChoiceState
{
    public static bool HasChosen;
    public static int LastPromptCloseFrame = -1;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void Init()
    {
        SceneManager.sceneLoaded += (_, __) => Reset();
        Reset();
    }

    public static void Reset()
    {
        HasChosen = false;
        LastPromptCloseFrame = -1;
    }
}