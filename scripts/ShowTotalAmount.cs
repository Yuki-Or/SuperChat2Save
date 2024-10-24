using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ShowTotalAmount : MonoBehaviour
{
    public TMP_Text txt_total;
    public Image box;
    public float amount;

    void Start()
    {
        box = box.GetComponent<Image>();
        amount = GameManager.Instance.CalculateTotalAmount();

        txt_total = txt_total.GetComponent<TMP_Text>();
        GameManager.Instance.DisplayTotal(txt_total);
        ChangeBoxColor();
    }

    private void ChangeBoxColor(){
        Sprite bg_sprite = Resources.Load<Sprite>("TotalCostBG/totalCost_white");

        if (0 <= amount && amount <= 999){
            bg_sprite = Resources.Load<Sprite>("TotalCostBG/totalCost_white");
        }
        else if (1000 <= amount && amount <= 1999){
            bg_sprite = Resources.Load<Sprite>("TotalCostBG/totalCost_blue");
        }
        else if(2000 <= amount && amount <= 3999){
            bg_sprite = Resources.Load<Sprite>("TotalCostBG/totalCost_green");
        }
        else if (4000 <= amount && amount <= 5999){
            bg_sprite = Resources.Load<Sprite>("TotalCostBG/totalCost_yellow");
        }
        else if (6000 <= amount && amount <= 7999){
            bg_sprite = Resources.Load<Sprite>("TotalCostBG/totalCost_orange");
        }
        else if (8000 <= amount && amount <= 9999){
            bg_sprite = Resources.Load<Sprite>("TotalCostBG/totalCost_red");
        }
        else {
            bg_sprite = Resources.Load<Sprite>("TotalCostBG/totalCost_frame");
        }

        box.sprite = bg_sprite;
    }
}
