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
}