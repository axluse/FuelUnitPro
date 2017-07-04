using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * AudioSourceの設定を自動的に設定 
 */
namespace FuelUnitPro.Common {
    public static class SoundMGR {

        #region propertys
        public static int masterBGMVolume { get; set; }
        public static int masterSEVolume { get; set; }
        public static int masterVoiceVolume { get; set; }
        public enum SoundType {
            BGM,
            SE,
            Voice
        }
        #endregion

        #region 自動ボリューム調節メソッド
        public static void autoVolume(AudioSource audioSource, SoundType soundType = SoundType.BGM) {
            switch (soundType) {
                // BGM
                case SoundType.BGM:
                    audioSource.volume = masterBGMVolume;
                    break;
                
                // SE
                case SoundType.SE:
                    audioSource.volume = masterSEVolume;
                    break;

                // Voice
                case SoundType.Voice:
                    audioSource.volume = masterVoiceVolume;
                    break;
            }
        }
        #endregion

    }
}