using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuoteController : MonoBehaviour
{
    int current_hour;
    public TMP_Text quote;
    private MotionPlayer motionPlayer;
    public GameObject model;

    void Awake(){
        Debug.Log("QuoteController Awake called");
        current_hour = System.DateTime.Now.Hour;
        if (model == null){
            Debug.LogError("Model is not assigned in the inspector!");
            return;
        }

        motionPlayer = model.GetComponent<MotionPlayer>();

        if (motionPlayer == null){
            Debug.LogError("MotionPlayer component is not found on the model!");
            return;
        }
    }

    void Start()
    {
        quote = quote.GetComponent<TMP_Text>();
    }

    public void display_quote(string quoteType){
        string txt = "";
        string animation_name = "";

        if(quoteType == "greetings"){
            txt = Greetings();
            animation_name = GetAnimationName(txt[0]); 
        }

        else if(quoteType == "normal"){
            txt = GameManager.Instance.ChooseRandomQuoteFromSection("NormalSTART");
            animation_name = GetAnimationName(txt[0]);
        }

        else if(quoteType == "superChat"){
            txt = GameManager.Instance.ChooseRandomQuoteFromSection("AfterSuperChatSTART");
            animation_name = GetAnimationName(txt[0]);
        }
        Debug.Log("QuoteController: animation is " + animation_name);
        Debug.Log($"Quote Controller: text is {txt}");
        quote.text = txt.Remove(0,1);
        
        motionPlayer.PlayMotionFromName(animation_name);
    }

    public string Greetings(){
        if (current_hour >= 6 && current_hour < 9){
            return GameManager.Instance.ChooseRandomQuoteFromSection("MorningSTART");
        }
        else if (current_hour >= 9 && current_hour < 17){
            return GameManager.Instance.ChooseRandomQuoteFromSection("AfternoonSTART");
        }

        else {
            return GameManager.Instance.ChooseRandomQuoteFromSection("EveningSTART");
        }
    }

    private string GetAnimationName(char letter){
        if (letter == 'N'){
            return "Neutral";
        }

        else if (letter == 'C'){
            return "Confused";
        }

        else if (letter == 'H'){
            return "Niko";
        }

        else if (letter == 'S'){
            return "Shobon";
        }

        else if (letter == 'P'){
            return "Passion";
        }

        else {
            return "Neutral";
        }
    }
}
