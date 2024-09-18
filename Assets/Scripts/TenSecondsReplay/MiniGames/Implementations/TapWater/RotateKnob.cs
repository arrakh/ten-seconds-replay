using System;
using System.Collections;
using System.Collections.Generic;
using TenSecondsReplay.MiniGames.Implementations.Okapi;
using UnityEngine;

public class RotateKnob : MonoBehaviour
{

    
   [SerializeField] private OkapiMiniGame miniGame;

    private void OnEnable()
    {
        miniGame.onKeyInput += OnGameInput;
    }

    private void OnGameInput()
    {
        var angle = transform.eulerAngles;

        angle.z = angle.z - 15;

        transform.eulerAngles = angle;
    }

    private void OnDisable()
    {
        miniGame.onKeyInput -= OnGameInput;
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
