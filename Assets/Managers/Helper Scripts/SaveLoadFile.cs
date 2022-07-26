using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Managers.Helper_Scripts{
    public enum SaveSlots{
        One, Two, Three, Four, Five
    }
    
    public static class SaveLoadFile
    {
        private const string FILENAME_1 = "/SteamCloud_Ertugrul1.sav";
        private const string FILENAME_2 = "/SteamCloud_Ertugrul2.sav";
        private const string FILENAME_3 = "/SteamCloud_Ertugrul3.sav";
        private const string FILENAME_4 = "/SteamCloud_Ertugrul4.sav";
        private const string FILENAME_5 = "/SteamCloud_Ertugrul5.sav";

        public static void Save(SteamCloudPrefs steamCloudPrefs, SaveSlots SaveSlot)
        {
            var FILENAME = GetSlotPath(SaveSlot);
            var bf = new BinaryFormatter();
            var stream = new FileStream(Application.persistentDataPath + FILENAME, FileMode.Create);

            bf.Serialize(stream, steamCloudPrefs);
            stream.Close();
        }

        public static SteamCloudPrefs Load(SaveSlots SaveSlot){
            var FILENAME = GetSlotPath(SaveSlot);
            if(File.Exists(Application.persistentDataPath + FILENAME)){
                var bf = new BinaryFormatter();
                var stream = new FileStream(Application.persistentDataPath + FILENAME, FileMode.Open);

                var data = bf.Deserialize(stream) as SteamCloudPrefs;

                stream.Close();

                return data;
            }
            return null;
            
        }

        private static string GetSlotPath(SaveSlots SaveSlot){
            return SaveSlot switch{
                SaveSlots.One => FILENAME_1,
                SaveSlots.Two => FILENAME_2,
                SaveSlots.Three => FILENAME_3,
                SaveSlots.Four => FILENAME_4,
                SaveSlots.Five => FILENAME_5,
                _ => throw new ArgumentOutOfRangeException(nameof(SaveSlot), SaveSlot, null)
            };
        }
    }
}