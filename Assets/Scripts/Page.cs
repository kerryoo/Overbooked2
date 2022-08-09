using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Page
{
    public List<string> lines;

    public Page(List<string> lines)
    {
        this.lines = lines;
    }
}
