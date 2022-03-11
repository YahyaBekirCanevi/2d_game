using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandle : MonoBehaviour
{
    public System.Action Action {get; set;}
    private void Event() => Action();
}
