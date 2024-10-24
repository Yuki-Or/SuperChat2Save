using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeleteOneChatButton : MonoBehaviour
{
    public Button yesBttn;
    public Button noBttn;
    public GameObject PopUpWindow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeleteClicked(){
        GameManager.Instance.DeleteClickedChat();
        PopUpWindow.SetActive(false);
        SceneManager.LoadScene("HistoryScene");
        SceneManager.sceneLoaded += OnHistorySceneLoaded;
    }

    // This method will be called when the scene is loaded
    private void OnHistorySceneLoaded(Scene scene, LoadSceneMode mode){
        if (scene.name == "HistoryScene"){
            GameManager.Instance.PopulateChatHistory();  
            SceneManager.sceneLoaded -= OnHistorySceneLoaded;  
        }
    }

    public void NoBttnClicked(){
        PopUpWindow.SetActive(false);
    }
}
