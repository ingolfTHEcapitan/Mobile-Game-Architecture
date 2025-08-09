using System;
using System.Collections.Generic;

[Serializable]
public class KillData
{
    public List<string> SlainSpawners;

    public KillData()
    {
        SlainSpawners = new List<string>();
    }
}