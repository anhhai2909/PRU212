using Interaction;
using Interaction.Interactables;
using System;
using System.Reflection;
using UnityEngine;
using Weapons;

namespace CoreSystem
{
    public class WeaponSwap : CoreComponent
    {
        public event Action<WeaponSwapChoiceRequest> OnChoiceRequested;
        public event Action<WeaponDataSO> OnWeaponDiscarded;

        private InteractableDetector interactableDetector;
        private WeaponInventory weaponInventory;
        public WeaponUI imageWeapon;

        private WeaponDataSO newWeaponData;

        private WeaponPickup weaponPickup;

        private int currentIndex = 0;

        private void HandleTryInteract(IInteractable interactable)
        {
            if (interactable is not WeaponPickup pickup)
                return;

            weaponPickup = pickup;

            newWeaponData = weaponPickup.GetContext();

            if (weaponInventory.TryGetEmptyIndex(out var index))
            {
                weaponInventory.TrySetWeapon(newWeaponData, index, out _);
                interactable.Interact();
                newWeaponData = null;
                return;
            }
            else
            {
                weaponInventory.AddEmptyPosition();
                weaponInventory.TrySetWeapon(newWeaponData, weaponInventory.weaponData.Length-1, out _);
                interactable.Interact();
                newWeaponData = null;
                return;
            }

            //OnChoiceRequested?.Invoke(new WeaponSwapChoiceRequest(
            //    HandleWeaponSwapChoice,
            //    weaponInventory.GetWeaponSwapChoices(),
            //    newWeaponData
            //));
        }

        private void HandleSwapToWeapon(bool next)
        {
            if (weaponInventory.weaponData == null || weaponInventory.weaponData.Length == 0)
            {
                return;
            }
            weaponInventory.TryChangeIndexWeapon(next);
            if(weaponInventory.TryGetWeapon(weaponInventory.currentIndex, out var data))
            {
                weaponInventory.TryChangeWeapon(data);
                imageWeapon.SetImage(data.Icon);
                imageWeapon.gameObject.GetComponent<CooldownController>().setCooldown(data.AttackCooldown);
            }

            //if (next)
            //{
            //    WeaponDataSO lastItem = weaponInventory.weaponData[weaponInventory.weaponData.Length - 1];
            //    for (int i = weaponInventory.weaponData.Length - 1; i > 0; i--)
            //    {
            //        weaponInventory.weaponData[i] = weaponInventory.weaponData[i - 1];
            //    }
            //    weaponInventory.weaponData[0] = lastItem;
            //}
            //else
            //{
            //    WeaponDataSO firstItem = weaponInventory.weaponData[0];
            //    for (int i = 0; i < weaponInventory.weaponData.Length - 1; i++)
            //    {
            //        weaponInventory.weaponData[i] = weaponInventory.weaponData[i + 1];
            //    }
            //    weaponInventory.weaponData[weaponInventory.weaponData.Length - 1] = firstItem;
            //}
        }

        private void HandleWeaponSwapChoice(WeaponSwapChoice choice)
        {
            if (!weaponInventory.TrySetWeapon(newWeaponData, choice.Index, out var oldData))
                return;

            newWeaponData = null;

            OnWeaponDiscarded?.Invoke(oldData);

            if (weaponPickup is null)
                return;

            weaponPickup.Interact();

        }

        protected override void Awake()
        {
            base.Awake();

            interactableDetector = core.GetCoreComponent<InteractableDetector>();
            weaponInventory = core.GetCoreComponent<WeaponInventory>();
            imageWeapon = GameObject.Find("ImageWeapon").GetComponent<WeaponUI>();
        }

        private void OnEnable()
        {
            interactableDetector.OnTryInteract += HandleTryInteract;
            weaponInventory.OnWeaponChanged += HandleSwapToWeapon;
        }


        private void OnDisable()
        {
            interactableDetector.OnTryInteract -= HandleTryInteract;
            weaponInventory.OnWeaponChanged -= HandleSwapToWeapon;
        }
    }
}