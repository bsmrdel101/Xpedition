using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System;

namespace BEAN
{
    [CreateAssetMenu(fileName = "Inventory", menuName = "Inventory System/Inventory")]
    public class InventoryObject : ScriptableObject, ISerializationCallbackReceiver
    {
        [Header("Database")]
        public string savePath;
        private ItemDatabaseObject database;
        public List<InventorySlot> Container = new List<InventorySlot>();
        public List<InventorySlot> Hotbar = new List<InventorySlot>();

        [Header("Actions")]
        public static Action<ItemObject, int> AddItemAction;


        private void OnEnable()
        {
            AddItemAction += AddItem;

            #if UNITY_EDITOR
                database = (ItemDatabaseObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/Database.asset", typeof(ItemDatabaseObject));
            #else
                database = Resources.Load<ItemDatabaseObject>("Database");
            #endif
        }

        private void OnDisable()
        {
            AddItemAction -= AddItem;
        }

        public void AddItem(ItemObject _item, int _amount)
        {
            foreach (InventorySlot slot in Container)
            {
                if (slot.item == _item)
                {
                    slot.AddAmount(_amount);
                    DisplayInventory.UpdateInventoryAction();
                    return;
                }
            }
            Container.Add(new InventorySlot(database.GetId[_item], _item, _amount));
            DisplayInventory.UpdateInventoryAction();
        }

        public void Save()
        {
            string saveData = JsonUtility.ToJson(this, true);
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
            bf.Serialize(file, saveData);
            file.Close();
        }

        public void Load()
        {
            if (File.Exists(string.Concat(Application.persistentDataPath, savePath)))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
                JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
                file.Close();
            }
        }

        public void OnAfterDeserialize()
        {
            for (int i = 0; i < Container.Count; i++)
                Container[i].item = database.GetItem[Container[i].ID];
        }

        public void OnBeforeSerialize()
        {
        }
    }

    [System.Serializable]
    public class InventorySlot
    {
        public int ID;
        public ItemObject item;
        public int amount;

        public InventorySlot(int _id, ItemObject _item, int _amount)
        {
            ID =_id;
            item = _item;
            amount = _amount;
        }
        
        public void AddAmount(int value)
        {
            amount += value;
        }
    }
}