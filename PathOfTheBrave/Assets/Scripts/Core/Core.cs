using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Core : MonoBehaviour
{
    //public Movement Movement
    //{
    //    get => GenericNotImplementedError<Movement>.TryGet(movement, transform.parent.name);
    //    private set => movement = value;
    //}
    //public CollisionSenses CollisionSenses
    //{
    //    get => GenericNotImplementedError<CollisionSenses>.TryGet(collisionSenses, transform.parent.name);
    //    private set => collisionSenses = value;
    //}
    ////public Combat Combat
    ////{
    ////    get => GenericNotImplementedError<Combat>.TryGet(combat, transform.parent.name);
    ////    private set => combat = value;
    ////}

    //private Movement movement;
    //private CollisionSenses collisionSenses;
    ////private Combat combat;

    private readonly List<CoreComponent> CoreComponents = new List<CoreComponent>();

    private void Awake()
    {
        //Movement = GetComponentInChildren<Movement>();
        //CollisionSenses = GetComponentInChildren<CollisionSenses>();
        ////Combat = GetComponentInChildren<Combat>();
    }

    public void LogicUpdate()
    {
        //Movement.LogicUpdate();
        ////Combat.LogicUpdate();
        foreach (CoreComponent component in CoreComponents)
        {
            component.LogicUpdate();
        }
    }

    public void AddComponent(CoreComponent component)
    {
        if (!CoreComponents.Contains(component))
        {
            CoreComponents.Add(component);
        }
    }

    public T GetCoreComponent<T>() where T : CoreComponent
    {
        var comp = CoreComponents.OfType<T>().FirstOrDefault();

        if (comp == null)
        {
            Debug.LogWarning($"{typeof(T)} not found on {transform.parent.name}");
        }

        return comp;
    }

    public T GetCoreComponent<T>(ref T value) where T : CoreComponent
    {
        value = GetCoreComponent<T>();
        return value;
    }

}
