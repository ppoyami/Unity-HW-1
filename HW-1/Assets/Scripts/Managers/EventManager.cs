using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
  public static event Action DieEvent;

  public static void RunDieEvent(){
      if (DieEvent != null)
      {
          DieEvent();
      }
  }
}
