using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonSceneLoadSystem : MonoBehaviour
{
    [SerializeField, Header("ボタンの名前")]
    private string m_SceneName = "";

    public void ButtonSceneChange()
    {
        //シーンロード
        SceneManager.LoadScene(m_SceneName);
    }
}
