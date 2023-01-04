using UnityEngine;

public class KeyboardBirdInput : BirdInput
{
    [SerializeField]    
    KeyCode _flap = KeyCode.Space;

    [SerializeField]
    KeyCode _swipeUp = KeyCode.UpArrow;

    [SerializeField]
    KeyCode _swipeDown = KeyCode.DownArrow;

    void Update()
    {
        TriggerOnKeyDown(_flap, this.OnTap);
        TriggerOnKeyDown(_swipeUp, this.OnSwipeUp);
        TriggerOnKeyDown(_swipeDown, this.OnSwipeDown);

        this.isHeld = Input.GetKey(_flap);
    }

    void TriggerOnKeyDown(KeyCode key, System.Action OnDown)
    {
        if (Input.GetKeyDown(key))
            OnDown?.Invoke();
    }
}