using UnityEngine;

///<summary>Scriptable object to store the game settings</summary>
[CreateAssetMenu(menuName = "ScriptableObjects/Settings/Game settings", fileName = "New gameSettingsScrObj")]
public class GameSettingsScrObj : ScriptableObject
{
    public float _gravity = 20f;
}