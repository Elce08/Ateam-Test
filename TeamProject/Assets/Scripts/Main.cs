using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    PlayerInputAction choiceCharater;
    int choiceClass = 0;

    private void Start()
    {
        
    }

    private void Awake()
    {
        choiceCharater = new PlayerInputAction();
    }

    private void OnEnable()
    {
        choiceCharater.Player.Enable();
        choiceCharater.Player.Warrior.performed += OnWarrior;
        choiceCharater.Player.Archer.performed += OnArcher;
        choiceCharater.Player.Mage.performed += OnMage;
    }
 

    private void OnDisable()
    {
        choiceCharater.Player.Mage.performed -= OnMage;
        choiceCharater.Player.Archer.performed -= OnArcher;
        choiceCharater.Player.Warrior.performed -= OnWarrior;
        choiceCharater.Player.Disable();
    }

    private void OnWarrior(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        choiceClass = 1;
    }

    private void OnArcher(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        choiceClass = 2;
    }
    private void OnMage(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        choiceClass = 3;
    }

    private void ChoiceClass()
    {
        if(choiceClass == 1 )
        {
            Debug.Log($"Warrior{choiceClass}");
        }
        else if(choiceClass == 2 )
        {
            Debug.Log($"Archer{choiceClass}");
        }
        else if(choiceClass == 3 )
        {
            Debug.Log($"Mage{choiceClass}");
        }
        else
        {
            Debug.Log("캐릭터 재선택");
        }

    }
}
