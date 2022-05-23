using UnityEngine;

public static class TransitionManager
{
    public static bool isTransit;
    public  static GameObject[] gameObjects;

    //Player(and his components)
    public static int playerAttack;
    public static int playerHealth;
    public static float jumpForce;
    public static float maxSpeed;
    public static float dashSpeed;

    //situative scr
    public static bool SceneSwitch;

    //Bag
    public static bool[] _isBagFull;
    public static RectTransform[] _bagSlots;

    //Inventory
    public static bool[] _isFull;
    public static RectTransform[] _slots;

    //Coins
    public static int _coinsCount;
}
