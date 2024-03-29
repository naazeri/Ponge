﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScoringZone : MonoBehaviour
{
    // TODO: convert to c# event. currenty deppends unity inspector
    public EventTrigger.TriggerEvent scoreTrigger;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var ball = collision.gameObject.GetComponent<Ball>();

        if (ball != null)
        {
            var eventData = new BaseEventData(EventSystem.current);
            scoreTrigger.Invoke(eventData);
        }
    }

}
