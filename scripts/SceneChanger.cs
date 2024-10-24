using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    bool showGreetings = true;

    // ---Main Scene---

    // Button for calling HappyChatScene
    public void ToHappyChatScene(){
        SceneManager.LoadScene("HappyChatScene");
    }
    // Button for calling History Scene
    public void ToHistoryScene(){
        SceneManager.LoadScene("HistoryScene");
        SceneManager.sceneLoaded += OnHistorySceneLoaded;
    }

    private void OnHistorySceneLoaded(Scene scene, LoadSceneMode mode){
        if (scene.name == "HistoryScene")
        {
            GameManager.Instance.PopulateChatHistory();
            SceneManager.sceneLoaded -= OnHistorySceneLoaded;
        }
    }

    // ---HappyChat Scene---


    // ---general---

    // Button for calling Main Scene (First time after starting the app)
    public void ToMainScene(){
        SceneManager.LoadScene("SampleScene");
        SceneManager.sceneLoaded += OnMainSceneLoaded;
    }

    private void OnMainSceneLoaded(Scene scene, LoadSceneMode mode){
        if (scene.name == "SampleScene"){
            if(showGreetings){
                showGreetings = false;
                GameManager.Instance.CallGreetings();
            }
            else{
                GameManager.Instance.CallNormalConversations();
            }
            
            SceneManager.sceneLoaded -= OnMainSceneLoaded;
        }
    }
    
    // Button for calling Main Scene (make calls to normal conversation)
    public void ToMainScene_Normal(){
        SceneManager.LoadScene("SampleScene");
        Debug.Log("SceneChanger: ToMainScene_Normal called");
        SceneManager.sceneLoaded += OnMainSceneLoaded_Normal;
    }

    private void OnMainSceneLoaded_Normal(Scene scene, LoadSceneMode mode){
        if (scene.name == "SampleScene"){
            GameManager.Instance.CallNormalConversations();
            SceneManager.sceneLoaded -= OnMainSceneLoaded_Normal;
        }
    }

    // Button for calling Start Scene
    public void ToStartScene(){
        SceneManager.LoadScene("StartScene");
    }

    // Calling DeleteFileScene
    public void ToDeleteFileScene(){
        SceneManager.LoadScene("DeleteFileScene");
    }

}
