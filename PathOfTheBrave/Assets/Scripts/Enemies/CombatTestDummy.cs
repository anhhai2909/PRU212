using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class CombatTestDummy : MonoBehaviour
{
    [SerializeField] private GameObject hitParticles;
    public Core Core { get; private set; }

    private void Awake()
    {
        Core = GetComponentInChildren<Core>();
    }

    private void Update()
    {
        Core.LogicUpdate();
    }
}
