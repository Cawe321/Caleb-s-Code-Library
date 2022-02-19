/*
 * IMPORTANT!
 * Singleton
 * DontDestroyOnLoad  
*/
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// SingletonObject that persists throughout all scenes. 
/// 
/// *PURPOSE OF SCRIPT*
/// Used to replace calling <see cref="SceneManager.LoadScene(string)"/> with <see cref="SceneTransitionManager.SwitchScene(string, SceneTransitionManager.ENTRANCE_TYPE, SceneTransitionManager.EXIT_TYPE)"/>
/// The method mentioned above uses <see cref="SceneManager.LoadSceneAsync(string)"/> and provides support for a loading canvas that optionally includes a loading bar/percentage.
/// 
/// *ABOUT TRANSITION ANIMATIONS*
/// You may add your own <see cref="ENTRANCE_TYPE"/> and <see cref="EXIT_TYPE"/> enums when adding more transition animations.
/// You may refer to <see cref="SceneTransitionManager.StartFadeIn"/> and <see cref="SceneTransitionManager.StartFadeOut"/> for the existing animations of <see cref="ENTRANCE_TYPE.FADE_IN"/> and <see cref="EXIT_TYPE.FADE_OUT"/> respectively.
/// Remember to apply/call your new Transition functions in <see cref="SceneTransitionManager.StartSwitchingScenes(string, SceneTransitionManager.ENTRANCE_TYPE, SceneTransitionManager.EXIT_TYPE)"/>
/// 
/// </summary>
public class SceneTransitionManager : CalebCodeLibrary.SingletonObject.SingletonObject<SceneTransitionManager>
{
    [Header("References")]
    [Tooltip("{REQUIRED] The menu that will appear when transitioning.")]
    [SerializeField] Canvas splashScreen;
    [Tooltip("[OPTIONAL] The text that will show the percentage of loading progress.")]
    [SerializeField] TextMeshProUGUI textMesh;
    [Tooltip("{OPTIONAL] The progress bar that will show the percentage of loading progress. Will modify X scale on transform.")]
    [SerializeField] Transform progressBar;


    [Space(0.5f)]
    [Header("Fade Anim Settings")]
    [Tooltip("Time to fade in (seconds).")]
    public float fadeInDuration = 1f;
    [Tooltip("Time to fade out (seconds).")]
    public float fadeOutDuration = 1f;
    
    /// <summary>
    /// The type of splash screen entrance.
    /// </summary>
    public enum ENTRANCE_TYPE
    {
        FADE_IN,
    }

    /// <summary>
    /// The type of splash screen exit.
    /// </summary>
    public enum EXIT_TYPE
    {
        FADE_OUT,
    }

    /// <summary>
    /// Value to check from other scripts whether the splashScreen is active. 
    /// </summary>
    [HideInInspector]
    public bool SplashActive {  get 
                                { 
                                    return _splashActive; 
                                } 
                                private set 
                                { 
                                    _splashActive = value;  
                                    splashScreen.gameObject.SetActive(value);
                                }  
                             }
    private bool _splashActive;

    // Internal variables
    Coroutine currCoroutine;
    CanvasGroup canvasGroup;

    #region INITIALIZATION
    // On Awake()
    public override void Awake()
    {
        #region SINGLETON_HANDLING
        SingletonAwake();  // using SingletonObject<T>'s Awake()
        #endregion

        SplashActive = false;
    }

    // Used to check for neccessary components and add if neccessary
    private void Start()
    {
        if (splashScreen != null)
        {
            // Check for CanvasGroup component. If not found, automatically add one
            canvasGroup = splashScreen.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = splashScreen.gameObject.AddComponent<CanvasGroup>();
            }

            // Hardcode settings
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }
        else
            Debug.LogError("SceneTransitionManager: No splash screen selected.");
    }
    #endregion

    #region PUBLIC_FUNCTIONS
    public void SwitchScene(string sceneName, ENTRANCE_TYPE entranceType, EXIT_TYPE exitType)
    {
        // if not active, means it's ok to switch scenes
        if (currCoroutine == null)
        {
            currCoroutine = StartCoroutine(StartSwitchingScenes(sceneName, entranceType, exitType));
        }
    }
    #endregion

    #region PRIVATE_HELPER_FUNCTIONS
    IEnumerator StartSwitchingScenes(string sceneName, ENTRANCE_TYPE entranceType, EXIT_TYPE exitType)
    {
        // Handle entrance of loading screen
        switch (entranceType)
        {
            case ENTRANCE_TYPE.FADE_IN:
                {
                    yield return StartCoroutine(StartFadeIn());
                    break;
                }
        }
        // Load scene async
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            if (textMesh)
                textMesh.text = $"{(int)(asyncLoad.progress * 100f)}%";
            yield return new WaitForFixedUpdate();
        }

        if (textMesh)
            textMesh.text = "100%";

        // Handle exit of loading screen
        switch (exitType)
        {
            case EXIT_TYPE.FADE_OUT:
                {
                    yield return StartCoroutine(StartFadeOut());
                    break;
                }
        }

        // Switch scenes complete!
        currCoroutine = null;
    }
    #endregion

    #region TRANSITION_FUNCTIONS
    IEnumerator StartFadeIn()
    {
        SplashActive = true;
        // Ensure that canvas is invisible first
        canvasGroup.alpha = 0f;

        float speed = 1f / fadeInDuration;
        // Keep reducing alpha every frame until alpha is 0
        while (canvasGroup.alpha < 1f)
        {
            yield return new WaitForFixedUpdate();
            canvasGroup.alpha += speed * Time.fixedDeltaTime;
        }
        canvasGroup.alpha = 1f;
        
    }

    IEnumerator StartFadeOut()
    {
        // Ensure that canvas is visible first
        canvasGroup.alpha = 1f;

        float speed = 1f / fadeOutDuration;
        while (canvasGroup.alpha >= 0f)
        {
            yield return new WaitForFixedUpdate();
            canvasGroup.alpha -= speed * Time.fixedDeltaTime;
        }
        canvasGroup.alpha = 0f;
        SplashActive = false;
    }
    #endregion
}
