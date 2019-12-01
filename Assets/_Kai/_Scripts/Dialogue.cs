using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue {
    public DialoguePortrait portrait;

    [TextArea(3, 15)]
    public string[] sentences;
}

public enum DialoguePortrait {
    MAIN_CHAR = 0,
    SIDE_CHAR
}
