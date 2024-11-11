using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


namespace SaveLoad.Runtime
{
    public class PlayerProfile : MonoBehaviour
    {
        const string PLAYER_PROFILE_NAME = "Player1";
        PlayerHealth playerHealth;
        DamagePlayerOnContact attackPlayerOnContact;

        void Start()
        {
            playerHealth = GetComponent<PlayerHealth>();
            attackPlayerOnContact = GetComponent<DamagePlayerOnContact>();

            Debug.Log(Application.persistentDataPath);

            SaveManager.Delete(PLAYER_PROFILE_NAME);

            var playerSave = new PlayerSaveData {position = transform.position, health = playerHealth.currentHealth, damage = attackPlayerOnContact.damageAmount};

            var saveProfile = new SaveProfile<PlayerSaveData>(PLAYER_PROFILE_NAME, playerSave);

            SaveManager.Save(saveProfile);
        }

        private void Update ()
        {
            //load save profile
            if (Input.GetKeyDown(KeyCode.E)) 
            {
                var playerLoadedData = SaveManager.Load<PlayerSaveData>(PLAYER_PROFILE_NAME).saveData;
                var pos = playerLoadedData.position;
                transform.position = pos;
                var hp = playerLoadedData.health;
                attackPlayerOnContact.damageAmount = hp;
                var dam = playerLoadedData.damage;
                playerHealth.currentHealth = dam;
            }

            //Overwrithe save
            if (Input.GetKeyDown(KeyCode.S))
            {
                SaveManager.Delete(PLAYER_PROFILE_NAME);

                var playerSave = new PlayerSaveData { position = transform.position, health = currentHealth, damage = damageAmount };

                var saveProfile = new SaveProfile<PlayerSaveData>(PLAYER_PROFILE_NAME, playerSave);

                SaveManager.Save(saveProfile);
            }
        }
    }
}
