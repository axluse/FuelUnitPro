using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/**
 * FuelUnitPro
 * ウィンドウユニット：トーク用とアラート用の２種類呼び出せます。
 * ☆簡単にウィンドウを呼び出して、ゲームに組み込めます。使い方は↓
 */
namespace FuelUnitPro.Common {
    public static class Window {

        #region 各変数制御
        // フラグメント管理
        public static bool isActiveable { get; set; }        // windowが使用可能か？
        public static bool isWindow { get; set; }           // windowが表示されているか？
        public static bool isNext { get; set; }                // windowを閉じるか

        // 表示するオブジェクトについて
        public static string scene { get; set; }
        public static string message { get; set; }
        public static Type type { get; set; }

        // Enum
        public enum Type {
            Alert,
            Talk
        }
        #endregion

        /// <summary>
        /// ■Windowはこのひとつのコマンドで設置できる
        /// 
        /// <使い方>
        /// Window.Start("表示するテキスト", Type.Talk);
        /// Window.Start("表示するテキスト", Type.Alert);
        /// 
        /// ※talk window表示の場合は引数を省略できる)))))))
        /// Window.Start("表示するてきすと");
        /// 
        /// </summary>
        /// <returns>true=初期化正常終了 false=初期化失敗</returns>
        public static bool Start(string text,Type windowType = Type.Talk) {
            // メッセージを格納
            message = text;
            // ウィンドウタイプに合わせて表示するシーンを決定
            switch (windowType) {
                // アラートウィンドウ
                case Type.Alert:
                    break;

                // トークウィンドウ
                case Type.Talk:
                    break;
            }

            Window.Open();
        }

        #region Window基本ロジック
        /**
         * Windowを表示
         */
        public static bool Open() {
            isSeted();
            if (isActiveable) {
                SceneManager.LoadScene(scene, LoadSceneMode.Additive);
                isWindow = true;
                return true;
            } else {
                return false;
            }
        }

        /**
         * 表示テキストを取得し配置
         */
         public static void StringSet(Text messageText) {
            messageText.text = message;
        }

        /**
         * Windowを閉じる
         */
        public static bool Close(GameObject window) {
            isSeted();
            if(isWindow && isActiveable) {
                SceneManager.UnloadScene(scene);
                return true;
            } else {
                return false;
            }
        }

        /**
         * セッティングが終了しているか
         */
        public static void isSeted() {
            #region シーン名/表示メッセージが格納済み
            if (scene != "") {
                if (message != "") {
                    isActiveable = true;
                    #endregion

            #region それ以外
                } else {
                    isActiveable = false;
                }
            } else {
                isActiveable = false;
            }
            #endregion
        }

        #endregion

    }
}