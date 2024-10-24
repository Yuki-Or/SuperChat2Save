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
