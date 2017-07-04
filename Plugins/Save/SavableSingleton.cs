using UnityEngine;
using System.IO;
using FuelUnitPro.Seculity;
using FuelUnitPro.Json;

/**
 * FuelUnitPro / Common / セーブデータ管理
 * 使用方法：クラス名.Save()
 */
namespace FuelUnitPro.Common {
    abstract public class SavableSingleton<T> where T : SavableSingleton<T>, new() {
        // クラスのインスタンス
        private static T instance;

        // 保存先フォルダ名
        public static string saveFolderPath = Application.persistentDataPath + "/Database/";
        public static string filePath = saveFolderPath + typeof(T).FullName + ".json";
        public static T Instance {
            get {
                if (null == instance) {
                    Load();
                }
                return instance;
            }
        }

        // セーブコンストラクタ
        public static void Save() {
            Instance._Save();
        }

        // セーブ実行内容
        public void _Save() {
            string json = JsonMapper.ToJson(Instance);
            json += "[END]";

            string crypted = Crypt.Encrypt(json);

            if (!Directory.Exists(saveFolderPath)) {
                Directory.CreateDirectory(saveFolderPath);
            }
            FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            BinaryWriter writer = new BinaryWriter(fileStream);
            writer.Write(crypted);
            writer.Close();
        }

        // ロード
        public static void Load() {
            T ret = null;
            string json = "";
            if (File.Exists(filePath)) {
                FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                BinaryReader reader = new BinaryReader(fileStream);
                if (reader != null) {
                    string str = reader.ReadString();
                    string decrypted = Crypt.Decrypt(str);

                    decrypted = System.Text.RegularExpressions.Regex.Replace(decrypted, @"¥[END¥].*$", "");

                    json = decrypted;
                    instance = JsonMapper.ToObject<T>(json);
                    reader.Close();

                }

            } else {
                instance = new T();
            }
        }

        // ファイル記載
        private void writeFile(string filePath) {
            using (FileStream fs = new FileStream(filePath, FileMode.Create)) {
                using (StreamWriter sw = new StreamWriter(fs)) {
                    sw.Write(JsonMapper.ToJson(Instance));
                }
            }
        }

        // セーブデータ削除
        public static void Reset() {
            if (File.Exists(filePath)) {
                File.Delete(filePath);
                if (!File.Exists(filePath)) {
                }
            }
        }
    }
}