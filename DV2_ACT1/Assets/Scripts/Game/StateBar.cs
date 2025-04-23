using UnityEngine;
using UnityEngine.UI;

///<summary>Class to control a character screen state bar</summary>
public abstract class StateBar : MonoBehaviour
{
    [Header("---------- Event buses ----------")]
    [SerializeField] private Slider _slider;
    [SerializeField] private Gradient _gradient;
    [SerializeField] private Image _fill;


    ///<summary>Establishes the max state bar level</summary>
    ///<param name="maxLevel">The max bar level</param>
    public void SetMaxLevelBar(float maxLevel)
    {
        _slider.maxValue = maxLevel;
        _slider.value = maxLevel;
        _fill.color = _gradient.Evaluate(1f);
    }

    ///<summary>Establishes the current state bar level</summary>
    ///<param name="level">The current bar level</param>
    public void SetBarLevel(float level)
    {
        _slider.value = level;
        _fill.color = _gradient.Evaluate(_slider.normalizedValue);
    }
}

