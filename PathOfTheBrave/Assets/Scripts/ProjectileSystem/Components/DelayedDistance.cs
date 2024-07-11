using ProjectileSystem.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;
using Utilities;
using UnityEngine.Events;

public class DelayedDistance : ProjectileComponent
{
    [SerializeField] public UnityEvent whenGetDistance;
    [field: SerializeField] public float Distance { get; private set; } = 10f;

    private DistanceNotifier distanceNotifier = new DistanceNotifier();

    // Used so other projectile components, such as DrawModifyDelayedGravity, can modify how far the projectile travels before being affected by gravity
    [HideInInspector]
    public float distanceMultiplier = 1;

    // Once projectile has travelled Distance, set gravity to Gravity value
    private void HandleNotify()
    {
        rb.gameObject.SetActive(false);
        whenGetDistance.Invoke();
    }

    // On Init, enable the distance notifier to trigger once distance has been travelled.
    protected override void Init()
    {
        base.Init();

        rb.gravityScale = 0f;
        distanceNotifier.Init(transform.position, Distance * distanceMultiplier);
        distanceMultiplier = 1;
    }

    #region Plumbing

    protected override void Awake()
    {
        base.Awake();
        distanceNotifier.OnNotify += HandleNotify;
    }

    protected override void Update()
    {
        base.Update();

        distanceNotifier?.Tick(transform.position);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        distanceNotifier.OnNotify -= HandleNotify;
    }

    #endregion
}