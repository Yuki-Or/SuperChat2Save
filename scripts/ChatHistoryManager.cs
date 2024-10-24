using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HappyChatListManager : MonoBehaviour
{
    //public GameObject happyChatItemPrefab; 

    private List<HappyChatData> happyChatList = new List<HappyChatData>();
    public GameObject prefab;

    void Start(){

    }

    // Call this method when loading the chat data
    public void PopulateHappyChatList(HappyChatDataList chatDataList)
    {
        // getting number of total chats made (display max 30)
        int count = Mathf.Min(chatDataList.Count, 30);
        Debug.Log("count is " + chatDataList.Count);
        List<HappyChatData> latestChats = chatDataList.GetRange(chatDataList.Count - count, count);
        Debug.Log("latestChats len of " + latestChats.Count);

        //Populate.populate(count, latestChats);


        // Populate the scrollable list with HappyChatData
        /*
        foreach (var chat in latestChats)
        {
            GameObject newItem = Instantiate(happyChatItemPrefab, contentPanel);
            
            // Access the text fields and set values
            TMP_Text[] textFields = newItem.GetComponentsInChildren<TMP_Text>();
            textFields[0].text = "Amount: $" + chat.amount;
            textFields[1].text = "Date: " + chat.date;
            textFields[2].text = "Message: " + chat.message;

            // Add click functionality to the item 
           // Button button = newItem.GetComponent<Button>();
            //button.onClick.AddListener(() => OnChatItemClicked(chat));
        }
        */
    }

    public void populate(int max_num, List<HappyChatData> chatList){
        GameObject newObj;
        for (int i = 0; i < max_num; i++){
            newObj = (GameObject)Instantiate(prefab, transform);
        }
    }


    // Handle item click (for example, showing more details or editing)
    public void OnChatItemClicked(HappyChatData chat)
    {
        Debug.Log("Clicked on HappyChat with amount: " + chat.amount + " and message: " + chat.message);
    }
}
