using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IWorldEvent
{
    string EventName { get; }
    string Description { get; }
    DateTime TimeStamp { get; }
}
