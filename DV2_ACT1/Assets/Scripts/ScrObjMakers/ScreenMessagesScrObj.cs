using UnityEngine;

///<summary>Scriptable object to store different messages</summary>
[CreateAssetMenu(menuName = "ScriptableObjects/Settings/Messages", fileName = "New messagesScrObj")]
public class ScreenMessagesScrObj : ScriptableObject
{
    [TextArea(minLines: 1, maxLines: 2)]
    public string _accessCardTaken;

    [TextArea(minLines: 1, maxLines: 2)]
    public string _interactDoor;

    [TextArea(minLines:1, maxLines:2)]
    public string _blockedDoor;

    [TextArea(minLines: 1, maxLines: 2)]
    public string _interactElevator;

}
