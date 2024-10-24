using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DeleteFileButton : MonoBehaviour
{
    public Button confirmedDelete;
    public TMP_InputField confirmText;
    private bool confirm;

    // Start is called before the first frame update
    void Start()
    {
        confirmedDelete.interactable = false;
        confirm = false;
    }

    void Update(){
        if (confirmText.text == "DELETE"){
            confirmedDelete.interactable = true;
            confirm = true;
        }
        else{
            confirmedDelete.interactable = false;
            confirm = false;
        }
    }

    public void DeleteConfirmed(){
        if(confirm == true){
            GameManager.Instance.Delete();
            SceneManager.LoadScene("StartScene");
        }
    }
}
