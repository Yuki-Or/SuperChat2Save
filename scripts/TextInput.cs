using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using HCD = HappyChatData;
using DT = System.DateTime;

public class TextInput : MonoBehaviour
{
    public TMP_InputField IF_message;
    public TMP_InputField IF_amount;
    public TMP_Text txt_amount;
    public Button submit_bttn;
    public Image chatBG;

    string message;
    float amount;
    DT date;
    HCD happyChatObj;


    void Start()
    {
        IF_message = IF_message.GetComponent<TMP_InputField>();
        IF_amount = IF_amount.GetComponent<TMP_InputField>();
        txt_amount = txt_amount.GetComponent<TMP_Text>();
        submit_bttn = submit_bttn.GetComponent<Button>();

        // disable button until user inputs the amount
        submit_bttn.interactable = false;

        // Add listener to the amount input field to grab text when it changes
        IF_amount.onValueChanged.AddListener(delegate { GrabText_amount(); });

        message = "";
        amount = 0.00f;
        date = DT.Now;
        happyChatObj = new HCD(0.00f, "NULL", "NULL");


    }

    void Update(){
        if (amount != 0.00f && !string.IsNullOrEmpty(IF_amount.text)){
            submit_bttn.interactable = true;
        }
        else{
            submit_bttn.interactable = false;
        }

        
        Sprite bg_sprite = Resources.Load<Sprite>("HappyChatBG/happychat_blue");
        
        if (0.00 <= amount && amount <= 199){
            bg_sprite = Resources.Load<Sprite>("HappyChatBG/happychat_blue");
        }
        else if (200 <= amount && amount <= 499){
            bg_sprite = Resources.Load<Sprite>("HappyChatBG/happychat_emerald");
        }
        else if (500 <= amount && amount <= 999){
            bg_sprite = Resources.Load<Sprite>("HappyChatBG/happychat_green");
        }
        else if (1000 <= amount && amount <= 1499){
            bg_sprite = Resources.Load<Sprite>("HappyChatBG/happychat_yellow");
        }
        else if (1500 <= amount && amount <= 2499){
            bg_sprite = Resources.Load<Sprite>("HappyChatBG/happychat_orange");
        }
        else if (2500 <= amount && amount <= 3999){
            bg_sprite = Resources.Load<Sprite>("HappyChatBG/happychat_red");
        }
        else if (4000 <= amount){
            bg_sprite = Resources.Load<Sprite>("HappyChatBG/happychat_gold");
        }

        chatBG.sprite = bg_sprite;
    }

    public void GrabText_amount()
    {   
        if (float.TryParse(IF_amount.text, out float result))
        {
            txt_amount.text = IF_amount.text;
            amount = result; 
            Debug.Log("amount that will be saved is " + amount);
        }
        else
        {
            Debug.Log("This is not float.");
            amount = 0.00f;
        }

    }


    // create and Save new HappyChat Object when button clicked
    public void CreateNewHappyChat(){
        message = IF_message.text;
        amount = float.Parse(IF_amount.text);

        if (amount == 0.00f|| IF_amount.text.Equals("")){
            Debug.Log("amount not entered");
        }

        Debug.Log("amount is " + amount + " msg is " + message);
        happyChatObj.updateChatData(amount, message, date.ToString("yyyy-MM-dd"));
        happyChatObj.printChatData();
        GameManager.Instance.Save(happyChatObj);
        StartCoroutine(ChangeSceneAfterSave());
    }

    private IEnumerator ChangeSceneAfterSave()
    {
        // Ensures the save operation is completed
        yield return new WaitForEndOfFrame(); 
        SceneManager.LoadScene("SampleScene");
        SceneManager.sceneLoaded += OnSampleSceneLoaded;
    }

    // This method will be called when the scene is loaded
    private void OnSampleSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "SampleScene")
        {
            SceneManager.sceneLoaded -= OnSampleSceneLoaded;
            GameManager.Instance.CallSuperChatConversations();
        }
    }
}
