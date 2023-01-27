using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum Scene
    {
        Login,
        Lobby,
        Game,
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }
    public enum UIEvent
    {
        Click,
        Drag,
    }
    public enum MouseEvent
    {
        Press,
        PointDown,
        PointUp,
        Click,
    }
    public enum CamerMode
    {
        QuaterView,
    }

    public enum CharacterState
    {
        Idle,
        Move,
        Attack,
        Hit,
        Dead
    }

    public const float xSpawnLimit = 20;
    public const float ySpawnLimit = 12;
    public const float randRange = 5f;
    
    public const int Mouse = 0;
    public const int Snake = 1;
    public const int Sheep = 2;
    public const int Boar = 3;
    public const int BossSheep = 4;


}