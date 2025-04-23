using UnityEngine;

///<summary>Stores the data of a weapon</summary>
[CreateAssetMenu(menuName = "ScriptableObjects/Items/Weapon", fileName = "New weaponScrObj")]
public class WeaponScrObj : Item
{
    [SerializeField] private int _rateOfFire;
    [SerializeField] private float _effectiveRange;
    [SerializeField] private AudioClip _shotSound;
    [SerializeField] private AudioClip _shotEmptySound;
    [SerializeField] private AudioClip _reloadSound;

    private int bulletsInMagazine;

    private void OnEnable()
    {
        bulletsInMagazine = 0;
    }

    public int GetRateOfFire()
    {
        return _rateOfFire;
    }
    public float GetEffectiveRange()
    {
        return _effectiveRange;
    }
    public int GetBulletsInMagazine()
    {
        return bulletsInMagazine;
    }
    public void SetBulletsInMagazine(int num)
    {
        bulletsInMagazine = num;
    }
    
    public AudioClip GetShotSound()
    {
        return _shotSound;
    }

    public AudioClip GetShotEmptySound()
    {
        return _shotEmptySound;
    }

    public AudioClip GetReloadSound()
    {
        return _reloadSound;
    }

}
