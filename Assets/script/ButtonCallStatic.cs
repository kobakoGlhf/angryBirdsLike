using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonCallStatic:MonoBehaviour
{
    public void ChangeScene(string name) { GameManager.ChangeScene(name); }
    public void SetStage(int num) { GameManager.SetStage(num); }
    public void PlayAudio(AudioClip clip)
    {
        AudioManager.Instans.PlayAudio(clip);
    }
}
public static class GameManager
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
        AudioManager.Instans.PlayAudio(clip);
    }
}
