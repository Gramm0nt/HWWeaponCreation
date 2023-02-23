using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    private const string AttackTriggerName = "Attack";
    private const string WeaponIndexName = "WeaponIndex";

    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private int _money;
    [SerializeField] private Transform _attackPoint;

    private Weapon _currentWeapon;
    private int _currentWeaponNumber = 0;
    private Animator _animator;

    public int Money => _money;

    public event UnityAction<int> MoneyChanged;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        ChangeWeapon(_weapons[_currentWeaponNumber]);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _currentWeapon.Attack(_attackPoint);
            _animator.SetTrigger(AttackTriggerName);
        }
    }

    public void BuyWeapon(Weapon weapon)
    {
        _money -= weapon.Price;
        MoneyChanged?.Invoke(_money);
        _weapons.Add(weapon);
    }

    public void NextWeapon()
    {
        if (_currentWeaponNumber == _weapons.Count - 1)
            _currentWeaponNumber = 0;
        else
            _currentWeaponNumber++;

        ChangeWeapon(_weapons[_currentWeaponNumber]);
    }

    public void PreviousWeapon()
    {
        if (_currentWeaponNumber == 0 )
            _currentWeaponNumber = _weapons.Count - 1;
        else
            _currentWeaponNumber--;

        ChangeWeapon(_weapons[_currentWeaponNumber]);
    }

    private void ChangeWeapon(Weapon weapon)
    {
        _currentWeapon = weapon;
        _animator.SetInteger(WeaponIndexName, _currentWeaponNumber);
    }
}