using System;
using UnityEngine;

///<summary>Scriptable object to channel the post message events</summary>
[CreateAssetMenu(menuName = "ScriptableObjects/Event Buses/Post message events", fileName = "New postMessageEventBusScrObj")]
public class PostMessageEventBusScrObj : ScriptableObject
{
    #region Action delegates
    public event Action<string> PostMessageEvent;
    #endregion

    ///<summary>Raises an event when anyone needs post a message on screen</summary>
    ///<param name="messageToPost">The message to post on screen</param>
    public void RaisePostMessageEvent(string messageToPost)
    {
        PostMessageEvent?.Invoke(messageToPost);
    }

}
