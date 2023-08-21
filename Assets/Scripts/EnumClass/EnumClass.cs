using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumClass
{
    public const int DEFAULT_UPGRADE_VALUE = 0;

    public const string GAME_PATH = "GameData";
    public const string LOCAL_FILE = "SaveData";
    public const string LOCAL_FILE_EXTENSION = ".json";
    public const string WAVE_DATA_FILE = "waveInfo";
    public const string UPGRADE_DATA_FILE = "upgradeStatInfo";
    public const string SHOP_DATA_FILE = "shopInfo";
    public const string PLAYER_BASE_DATA_FILE = "playerBaseInfo";

    public const int LEVEL_NUM_LENGHT = 2;
    public const int WAVE_NUM_LENGTH = 3;
    public const int DREGS_NUM_LENGTH = 4;

    public enum SceneNumber : int
    {
        Loading,
        Main,
    }

    public enum JoyStickHand : int
    {
        Right,
        Left,
    }

    public enum Upgrade : int
    {
        Power,
        Critical_Laser_Chance,
        Unbeatable_Chance,
        Move_Speed,
        Circle_Diffuse_Time_Decrease,
        Magnet_Range,
        End,
    }

    public enum MeteorSize : int
    {
        Small,
        Middle,
        Big,
        End,
    }

    public enum ItemType : int
    {
        Dregs,
        PerfectDregs,
        Heart,
    }

    public enum SOUND_BGM : int
    {
        LOBBY,
        INGAME,
    }

    public enum SOUND_EFFECT : int
    {
        BUTTON,
        PLAYER_ATTACK,
        PLAYER_DOUBLE_SHOT,
        PLAYER_CIRCLE_DIFFUSE,
        PLAYER_DAMAGED,
        PLAYER_AVOIDANCE,
        PLAYER_LASER,
        PLAYER_GET_ITEM,
        METEOR_DAMAGED,
        METEOR_DESTROYED,
        METEOR_MOVE,
    }

    public enum SCROLL_DIR : int
    {
        Horizontal,
        Vertical,
    }
}
