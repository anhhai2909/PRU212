using System;
using ObjectPoolSystem;
using UnityEngine;

namespace Interfaces
{
    public interface IObjectPoolItem
    {
        void SetObjectPool<T>(ObjectPool pool, T comp) where T : Component;

        void Release();
    }
}