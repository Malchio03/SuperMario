using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Pla()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void MainScene()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        // save any game data here
        #if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    // TODO = sistemare il menu aggiungendo tasti come nella GMBJ12

    /*


    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingStart : SettingsElements
{
    [SerializeField] int sceneToLoad;
    [SerializeField] GameObject textToActive;
    [SerializeField] GameObject objToActive;

    public override void OnSelect(bool value)
    {
        if (value)
        {
            //textValue.material = selected;
            textToActive.SetActive(true);
        }
        else textToActive.SetActive(false);
        //textValue.material = notSelected;
    }


    public override void OnClick() 
    {
        objToActive.SetActive(true);
        
    }
    public void changeScene() 
    {
        AudioManager.instance.StopMusic();
        SceneManager.LoadScene(sceneToLoad);
    }
}


     */
}
