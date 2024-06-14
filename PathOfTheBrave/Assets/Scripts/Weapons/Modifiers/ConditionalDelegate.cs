using Weapons.Components;
using UnityEngine;

namespace Weapons.Modifiers
{
    public delegate bool ConditionalDelegate(Transform source, out DirectionalInformation directionalInformation);
}