using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] int firefliesEaten = 0;
    public TextMeshProUGUI firefliesText;

    public void EatFirefly(){
        firefliesEaten ++;
        firefliesText.text = $"X {firefliesEaten}";
    }
}
