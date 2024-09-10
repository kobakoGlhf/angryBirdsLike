using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonCallStatic:MonoBehaviour
{
    public void ChangeScene(string name) { SceneChanger.ChangeScene(name); }
    public void SetStage(int num) { SceneChanger.SetStage(num); }
}
public static class SceneChanger
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
}
