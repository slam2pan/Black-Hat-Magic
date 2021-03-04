using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BalanceManager : MonoBehaviour
{
    [SerializeField] private int balance = 0;
    public int Balance { get {return balance;} set{} }

    private Text balanceText;

    void Start()
    {
        balanceText = GameObject.Find("Balance").GetComponent<Text>();
    }

    void Update()
    {
        balanceText.text = "Balance: " + balance + "g";
    }

    public void AddToBalance(int amount) 
    {
        balance += amount;
    }

}
