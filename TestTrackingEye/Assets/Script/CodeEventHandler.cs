using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeEventHandler : MonoBehaviour
{
    public static event Action StartBrewing;
    public static void Trigger_StartBrewing() { StartBrewing.Invoke(); }
}
