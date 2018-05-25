using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CreatureManager : MonoBehaviour {

    [Header ("Status")]
    public int Level = 1;
    public int MaxHitPoints = 0;
    public int CurrentHitPoints = 0;
    public int ArmorClass = 1;
    [Tooltip("Value interpreted as %")]
    public int CritChance = 5;
    public int WeaponBonus = 0;

    [Header ("Statistics")]
    public DamageType MainAttackType;
    public int Strength = 1;
    public int Intelligence = 1;
    public int Agility = 1;

    public bool isShieldBlocking = false;

    [ExecuteInEditMode]
    void OnValidate() {
        Level = Mathf.Clamp (Level, 1, 20);

        CurrentHitPoints = Mathf.Clamp (CurrentHitPoints, 0, MaxHitPoints);
        ArmorClass = Mathf.Clamp (ArmorClass, 0, 30);
        CritChance = CritChance < 0 ? 0 : CritChance;

        Strength = Mathf.Clamp (Strength, 0, 30);
        Intelligence = Mathf.Clamp (Intelligence, 0, 30);
        Agility = Mathf.Clamp (Agility, 0, 30);    
    }

    public int GetDamageDealt(DamageType type) {
        float mainStat = 0;

        switch (type) {
            case DamageType.Mele:
                mainStat = Strength;
                break;
        }

        return (int)((mainStat * Level) + WeaponBonus);
    }

    public void RecieveDamage(int damage) {
        ReduceHP (GameMaster.Instance.GetDmgReduced (damage, ArmorClass, isShieldBlocking));
    }

    public void ReduceHP (int damage) {
        print ("Recieved " + damage + " damage");

        if (CurrentHitPoints - damage <= 0) {
            CurrentHitPoints = 0;
            //TODO Call to death
        } else
            CurrentHitPoints -= damage;
    }

    public int getProficiencyBonus() {
        if (Level <= 4)
            return 2;
        else if (Level <= 8)
            return 3;
        else if (Level <= 12)
            return 4;
        else if (Level <= 16)
            return 5;
        else
            return 6;
    }

    public int GetModifier(float MainStat) {
        return (int) Mathf.Floor ((MainStat - 10) / 2);
    }

    public void SetShieldBlocking(bool isBlocking) {
        isShieldBlocking = isBlocking;
    }

    public abstract void AttackStart(Vector3 Direction);

    public abstract void AttackEnd();

    public abstract void BlockStart(Vector3 Direction);

    public abstract void BlockEnd();

    public abstract void AttackBlocked();
}
