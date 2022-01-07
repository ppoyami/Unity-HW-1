using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
  public static event Action MonsterDieEvent;

  public static void RunMonsterDieEvent(){
      if (MonsterDieEvent != null)
      {
          MonsterDieEvent();
      }
  }
}
