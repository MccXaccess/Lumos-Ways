using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TryGetComponentOnStart : MonoBehaviour
{
    public void ResetProgress()
    {
        SaveChapters.Instance.ResetProgress();
    }
}