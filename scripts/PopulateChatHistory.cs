using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PopulateChatHistory: MonoBehaviour
{
    //public GameObject happyChatItemPrefab; 

    private List<HappyChatData> happyChatList = new List<HappyChatData>();
    public GameObject prefab;
    public GameObject PopUpWindow;

    void Start(){
        PopUpWindow.SetActive(false);
    }

    // Call this method when loading the chat data
    public void PopulateHappyChatList(HappyChatDataList chatDataList)
    {
        // getting number of total chats made (display max 30)
        int count = Mathf.Min(chatDataList.Count, 30);
        Debug.Log("count is " + chatDataList.Count);
        List<HappyChatData> latestChats = chatDataList.GetRange(chatDataList.Count - count, count);
        Debug.Log("latestChats len of " + latestChats.Count);

        populate(count, latestChats);
    }

    public void populate(int max_num, List<HappyChatData> chatList){
        GameObject newObj;
        for (int i = 0; i < max_num; i++){
            newObj = (GameObject)Instantiate(prefab, transform);
            HappyChatData chat = chatList[i];

            TMP_Text[] textFields = newObj.GetComponentsInChildren<TMP_Text>();
            textFields[0].text = "¥" + chat.amount;
            textFields[1].text = chat.date;
            textFields[2].text = chat.message;

            ChangeChatColour(chat.amount, newObj);

            Button button = newObj.GetComponent<Button>();
            if (button != null){
                // Pass the current chat data to the listener
                button.onClick.AddListener(() => OnChatItemClicked(chat));
            }
            else{
                Debug.LogError("No Button component found on the instantiated prefab.");
            }
        }
    }

    private void ChangeChatColour(float amount, GameObject obj){
        Image button_img = obj.GetComponent<Image>();
        Sprite bg_sprite = Resources.Load<Sprite>("HappyChatBG/happychat_blue");

        if (button_img == null)
        {
            Debug.LogError("No Image component found on the button.");
            return;
        }
        
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

        button_img.sprite = bg_sprite;
    }

    // Handle item click (for example, showing more details or editing)
    public void OnChatItemClicked(HappyChatData chat)
    {
        Debug.Log("Clicked on HappyChat with amount: " + chat.amount + " and message: " + chat.message);
        PopUpWindow.SetActive(true);
        GameManager.Instance.SetClickedChat(chat);
    }
}