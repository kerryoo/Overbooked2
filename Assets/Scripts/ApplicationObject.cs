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

    public enum ApplicationId
    {
        Spongebob,
        Sandy,
        Patrick,
        MrKrabs
    }

    public ApplicationId id;

    // Update is called once per frame
    void Update()
    {
    }

    bool isCorrectlyClassified()
    {
        return applicationType == classifiedType;
    }

    public static int Length(ApplicationId id)
    {
        switch (id)
        {
            case ApplicationId.Spongebob:
                return 3;
            case ApplicationId.Sandy:
                return 3;
            case ApplicationId.Patrick:
                return 2;
            case ApplicationId.MrKrabs:
                return 15;
            default:
                return 0;
        }
    }
}
