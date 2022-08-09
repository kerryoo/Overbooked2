using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationObject : MonoBehaviour
{
    public enum Type
    {
        Unknown,
        CreditCard,
        Mortgage,
        CashCheck
    }

    public Type applicationType;
    public Type classifiedType;
    public string applierName;

    public List<Page> pages = new();

    // Update is called once per frame
    void Update()
    {
    }

    bool isCorrectlyClassified()
    {
        return applicationType == classifiedType;
    }
}
