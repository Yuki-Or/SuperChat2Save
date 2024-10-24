using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using HCD = HappyChatData;

public class GameManager : MonoBehaviour
{
    // static GM instance that can be publically accessed and privately modified 
    public static GameManager Instance { get; private set; }
    private System.Random random = new System.Random();

    public float totalAmount = 0.00f;
    public HappyChatDataList chatDataList;
    private string saveFilePath;
    private int totalChatCount;

    // clicked chat obj in history scene
    public HCD clickedChatObj;

    // Textfile containing quotes
    [SerializeField] TextAsset TextFile;
    List<string> TextData = new List<string>();
    List<string> MorningTexts = new List<string>();
    List<string> AfternoonTexts = new List<string>();
    List<string> EveningTexts = new List<string>();
    List<string> HappyChatTexts = new List<string>();
    List<string> NormalTexts = new List<string>();

    Dictionary<string, List<string>> sections = new Dictionary<string, List<string>>()
    {
        { "MorningSTART", new List<string>() },
        { "AfternoonSTART", new List<string>() },
        { "EveningSTART", new List<string>() },
        { "AfterSuperChatSTART", new List<string>() },
        { "NormalSTART", new List<string>() }
    };

    private void Awake()
    {   
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Persist GM between scenes
        DontDestroyOnLoad(gameObject); 

        saveFilePath = Application.persistentDataPath + "/PlayerData.json";
        if (File.Exists(saveFilePath))
        {
            string loadChatData = File.ReadAllText(saveFilePath);
            chatDataList = JsonUtility.FromJson<HappyChatDataList>(loadChatData);
            if (chatDataList == null)
            {
                chatDataList = new HappyChatDataList();
            }
        }
        else
        {
            chatDataList = new HappyChatDataList();
        }
        Load();
    }

    void Start(){
        totalChatCount = 0;

        StringReader reader = new StringReader(TextFile.text);
        while (reader.Peek() != -1) 
        {
            string line = reader.ReadLine();
            TextData.Add(line);
        }

        int lineCount = TextData.Count;

        List<string> currentList = null;

        foreach (string line in TextData){
            if (sections.ContainsKey(line)){
                currentList = sections[line];      
            }

            else if (line.EndsWith("END")){
                currentList = null;              
            }

            else if (currentList != null){
                currentList.Add(line);
            }
        }
    }


    // --- save load system ---

    public void Save(HCD newChatData)
    {
        chatDataList.chatDataList.Add(newChatData);
        string saveChatData = JsonUtility.ToJson(chatDataList, true); 
        File.WriteAllText(saveFilePath, saveChatData);

    }

    public void Load()
    {
        Debug.Log("Load Called");

        if (File.Exists(saveFilePath))
        {
            string loadChatData = File.ReadAllText(saveFilePath);
            chatDataList = JsonUtility.FromJson<HappyChatDataList>(loadChatData);

            foreach (var chat in chatDataList.chatDataList)
            {
                totalChatCount++;
                // Debug.Log("Loaded chat data: happy-chatted " + chat.amount + " with message " + chat.message + " on " + chat.date);
            }

        }
        else
        {
            Debug.LogWarning("No save file found.");
        }
    }

    public void Delete()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("Save file deleted!");

            saveFilePath = Application.persistentDataPath + "/PlayerData.json";
            chatDataList = new HappyChatDataList();
        }

        else {
            Debug.Log("There is nothing to delete!");
        }       
    }

    public void PopulateChatHistory(){

        // Find the HappyChatListManager in the scene
        PopulateChatHistory Populate = FindObjectOfType<PopulateChatHistory>();

        Populate.PopulateHappyChatList(chatDataList);
    }

    // --- total happy chat amount ---

    public float CalculateTotalAmount()
    {
        //Debug.Log("CalculateTotalAmount called");
        totalAmount = 0.00f;
        foreach (var chat in chatDataList.chatDataList)
        {
            //Debug.Log("chat.amount is " + chat.amount);
            totalAmount += chat.amount;
        }

        return totalAmount;
    }

    public void DisplayTotal(TMP_Text displayText)
    {
        displayText.text = $"¥{CalculateTotalAmount()}";
    }

    // --- Quote System ---
    
    public void CallGreetings(){
        QuoteController Quote = FindObjectOfType<QuoteController>();
        Quote.display_quote("greetings");
    }

    public void CallNormalConversations(){
        QuoteController Quote = FindObjectOfType<QuoteController>();
        Quote.display_quote("normal");
    }

    public void CallSuperChatConversations(){
        QuoteController Quote = FindObjectOfType<QuoteController>();
        Quote.display_quote("superChat");
    }
    
    public string ChooseRandomQuoteFromSection(string sectionKey){
        if (sections.ContainsKey(sectionKey)){
            List<string> section = sections[sectionKey];

            if (section.Count > 0){
                int randomIndex = random.Next(section.Count);

                return section[randomIndex];
            }

            else{
                return "Section is empty!";
            }
        }

        else{
            return "Invalid section key!";
        }
    }

    // clicked chat history
    public void SetClickedChat(HCD clickedObj){
        clickedChatObj = clickedObj;
    }

    public HCD GetClickedChat(){
        return clickedChatObj;
    }

    public void DeleteClickedChat(){
        if (clickedChatObj != null && chatDataList != null){
            // Try to find the clicked chat object in the chatDataList
            if (chatDataList.chatDataList.Contains(clickedChatObj)){
                chatDataList.chatDataList.Remove(clickedChatObj);  // Remove the clicked chat
                Debug.Log("Clicked chat removed: " + clickedChatObj.message);

                // Update the save file after removing the chat
                string updatedChatData = JsonUtility.ToJson(chatDataList, true);
                File.WriteAllText(saveFilePath, updatedChatData);

                Debug.Log("Chat data updated after deletion.");
            }
            else{
            Debug.LogWarning("Clicked chat not found in the chat data list.");
            }
        }
        else{
            Debug.LogWarning("No clicked chat object set or chatDataList is null.");
        }  
    }
}