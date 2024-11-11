using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveLoad.Runtime
{
    //sealed = not override or eredity from others
    [SerializeField]
    public sealed class SaveProfile<T> where T : SaveProfileData
    {
        public string name;
        public T saveData;

        private SaveProfile() { }

        public SaveProfile(string name, T saveData)
        {
            this.name = name;
            this.saveData = saveData;
        }
    }

    //inmutable
    public abstract record SaveProfileData { }

    public record PlayerSaveData : SaveProfileData
    {
        public Vector3 position;
        public int health;
        public int damage;
    }
}
