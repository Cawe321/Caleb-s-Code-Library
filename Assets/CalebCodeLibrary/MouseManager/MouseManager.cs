using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A singleton object that helps to control the mouse in game.
/// 
/// Still a WIP!
/// 
/// *PURPOSE OF SCRIPT*
/// Helps to collate all functions of Unity that is related to the mouse in one script(this). 
/// For example, perhaps you require a few lines of code to run everytime <see cref="Cursor.lockState"/> is changed. You may add those lines of code in <see cref="mouseLockState"/> set function and always set <see cref="Cursor.lockState"/> by using this function from now on.
/// Also Adds additional features/functions that are related to the mouse.
/// </summary>
public class MouseManager : CalebCodeLibrary.SingletonObject.SingletonObject<MouseManager>
{
    #region CursorLockState
    /// <summary>
    /// Gets/Sets the lockstate of the cursor.
    /// </summary>
    public CursorLockMode MouseLockState
    {
        get
        {
            return Cursor.lockState;
        }
        set
        {
            Cursor.lockState = value;
            
            OnCursorLockStateChange(); // An example for the summary provided for MouseManager. This function will be called whenever MouseLockState is changed via MouseManager and MouseManager ONLY.
        }
    }

    void OnCursorLockStateChange()
    {
        // You may add some code here that is required to run everytime the "Cursor.lockState" is changed
        // Take note that to ensure this function is always called whenever "Cursor.lockState" value is changed, you need to set that value ONLY THROUGH "MouseManager.MouseLockState" from now onwards.
        // ...
        Debug.Log("Cursor Lock State has changed!");
    }
    #endregion

    /// <summary>
    /// Get the position of the mouse away from center.
    /// </summary>
    public Vector2 MousePosAwayFromCenter
    {
        get
        {
            return new Vector2(Input.mousePosition.x - (Screen.width * 0.5f), Input.mousePosition.y - (Screen.height * 0.5f));
        }
    }


    public override void Awake()
    {
        // Singleton awake
        SingletonAwake(); // base.Awake() works as well since this class directly inherits from SingletonObject<T>
        
    }

}
