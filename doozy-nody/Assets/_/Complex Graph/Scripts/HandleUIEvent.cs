using System;

namespace ItIron2019.DoozyNody.Runtime
{
    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;
    
    using Doozy.Engine;

    public class HandleUIEvent : MonoBehaviour
    {
        void OnEnable()
        {
            Message.AddListener<GameEventMessage>(OnDoozyMessage);
        }

        void OnDisable()
        {
            Message.RemoveListener<GameEventMessage>(OnDoozyMessage);
        }

        private void OnDoozyMessage(GameEventMessage message)
        {
            Debug.Log($"OnDoozyMessage");
            
            if (message != null)
            {
                Debug.Log($"message is not null");

                // Handle message according to the name
                if (message.EventName == "Going to Quit")
                {
                    Debug.Log($"message event: {message.EventName}");

                    // Handle to quit
                    Message.Send(new GameEventMessage("Game Clean Up", gameObject));
                }
            }
        }
    }
}
