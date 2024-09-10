using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonCallStatic:MonoBehaviour
{
    public void ChangeScene(string name) { Actions.ChangeScene(name); }
    public void SetStage(int num) { Actions.SetStage(num); }
    public void DDoTAudioPlay(AudioClip clip)
    {
        DdoT.Instans.PlayAudio(clip);
    }
}
public static class Actions
{
    public static int Stage;
    public static void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public static void SetStage(int StageNum)
    {
        Stage = StageNum;
    }
    public static void AudioPlay(AudioClip clip)
    {
        DdoT.Instans.PlayAudio(clip);
    }
}
