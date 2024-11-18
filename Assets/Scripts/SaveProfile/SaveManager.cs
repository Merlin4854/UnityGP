using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

namespace SaveLoad.Runtime
{
    public static class SaveManager
    {
        private static readonly string saveFolder = Application.persistentDataPath + "/SaveData";

        public static void Delete(string profileName)
        {
            if(!File.Exists(saveFolder + "/" + profileName))
            {
                Debug.LogError("Profile not found: " + profileName);
                return;
            }

            Debug.Log("Deleting profile: " + profileName);
            File.Delete(saveFolder + "/" + profileName);
        }

        public static SaveProfile<T> Load<T>(string profileName) where T : SaveProfileData
        {
            if(!File.Exists(saveFolder + "/" + profileName))
            {
                Debug.LogError("Profile not found: " + profileName);
                return null;
            }

            var fileContents = File.ReadAllText(saveFolder + "/" + profileName);
            Debug.Log(fileContents);
            return JsonConvert.DeserializeObject<SaveProfile<T>>(fileContents);
        }

        public static void Save<T>(SaveProfile<T> saveProfile) where T : SaveProfileData
        {
            if(File.Exists(saveFolder + "/" + saveProfile.name))
            {
                Debug.LogError("Profile alrady exist: " + saveProfile.name);
                return;
            }

            var serializedData = JsonConvert.SerializeObject(saveProfile, Formatting.Indented,
                new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
            //encrypt
            if(!Directory.Exists(saveFolder))
                Directory.CreateDirectory(saveFolder);

            File.WriteAllText(saveFolder + "/" + saveProfile.name, serializedData);
        }
    }
}