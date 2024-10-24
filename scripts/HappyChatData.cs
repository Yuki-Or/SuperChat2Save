using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HappyChatData
{
    public float amount;
    public string message;
    public string date;

    public HappyChatData(float _amount, string _msg, string _date){
        amount = _amount;
        message = _msg;
        date = _date;
    }

    public void updateChatData(float _amount, string _msg, string _date){
        amount = _amount;
        message = _msg;
        date = _date;
    }

    public void printChatData(){
        Debug.Log("happy-chatted " + amount + " with message " + message + " on " + date);
    }
}

// Wrapper Class
[System.Serializable]
public class HappyChatDataList {
    public List<HappyChatData> chatDataList = new List<HappyChatData>();

    public int Count => chatDataList.Count; 

    public List<HappyChatData> GetRange(int index, int count)
    {
        return chatDataList.GetRange(index, count);
    }
}
