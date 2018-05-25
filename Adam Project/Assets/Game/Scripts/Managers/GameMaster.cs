using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

    // Singleton Attributes
    private static GameMaster _instance;
    public static GameMaster Instance {
        get {
            return _instance;
        }
    }

    // Damage Reduction Attributes
    [Header ("Damage Mitigation")]
    [Tooltip ("AC Value considered Minimun Damage Reduction")]
    public int MinArmorClass = 8;
    [Tooltip ("AC Value considered Maximun Damage Reduction")]
    public int MaxArmorClass = 30;
    [Tooltip ("AC Bonus for attacks protected with the shield")]
    public int ShieldBonus = 5;
    [Tooltip ("Constant to Calculate the damage reduction made by the AC. Increase for more reduction by AC.")]
    public float DamageReductionConstant = 2;

    // Managers Required to Function
    [Header ("Controllers")]
    [SerializeField]
    private GameObject Player;

    private MovementInput _PlayerMovement;

    // Editor Functions
    [ExecuteInEditMode]
    void OnValidate() {
        MinArmorClass = MaxArmorClass < MinArmorClass ? MaxArmorClass : MinArmorClass;
    }

    // Functions
    void Awake () {
        if (_instance == null) {
            _instance = this;
        } else if (_instance != this)
            Destroy (this.gameObject);

        if(Player != null) {

        }
    }

    // Damage Reduction 
    public int GetDmgReduced (int damage, int currentAC, bool isShieldBlocking) {
        print (damage);
        var result = 0;

        if (!isShieldBlocking) {
            result = (int) Math.Floor (damage - (currentAC * DamageReductionConstant));
            result = result <= 0 ? 1 : result;
        } else
            Player.GetComponent<CreatureManager> ().AttackBlocked ();

        return result;
    }

    public void CallUIAttackBlocked() {
        print ("Blocked!");
    }
    // Inventory 
	/*
	public void SendItemToPlayer (Item item, InteractionEvent iEvent) {
        if (_PlayerInventory != null) {
            _PlayerInventory.AddItemToInventory (item);
            Destroy(GameObject.Find(item.Name));
            iEvent.End (EventResponse.Null);
        }
    }
    
    public void ToggleInventory() {
        _UIManager.ToggleInventory (_PlayerInventory.GetFullInventory());
    }

    public void RemoveItem (Item item) {
        _PlayerInventory.RemoveItemFromInventory (item);
    }

    public void RemoveItems (List<Item> items) {
        _PlayerInventory.RemoveItemsFromInventory (items);
    }

    public void SelectItemFromInventory(TurnInItem iEvent) {
        _UIManager.SelectItemToUse (_PlayerInventory.GetFullInventory(), iEvent);
    }

    public void SelectMultipleItemsFromInventory (TurnInMultipleItems iEvent) {
        _UIManager.SelectMultipleItemsToUse (_PlayerInventory.GetFullInventory (), iEvent);
    }

    // UI Management
    
    public void ShowSimpleQuestion(string question, string answer_A, string answer_B, InteractionEvent iEvent) 
    {
        _UIManager.ShowSimpleQuestion (question, answer_A, answer_B, iEvent);
    }

    public void ShowMessage(string message, InteractionEvent iEvent) {
        _UIManager.ShowMessage (message, iEvent);
    }
    
    //Time Management
    public IEnumerator StartEvent (Action RunMethod, float seconds) {
        yield return new WaitForSeconds (seconds);
        RunMethod ();
    }

    //Reactors

    public Reactable FindReactor (string name) {
        Reactable reactor = GameObject.Find (name).GetComponent<Reactable>();
        return reactor;
    }*/

    // General Getters
    public GameObject GetPlayer() {
        return Player;
    }
}
