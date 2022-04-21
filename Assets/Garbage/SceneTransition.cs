using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent (typeof(Animator))]
public class SceneTransition : MonoBehaviour
{
    private static SceneTransition instance;
    private static bool shouldPlayOpeningAnimation = false;

    [SerializeField] private Text loadingPercentage;
    [SerializeField] private Image progressBar;

    private Animator animator;
    private AsyncOperation loadingSceneOperation;

    public static void SwitchToScene (int sceneBuildId)
    {
        instance.animator.SetTrigger("StartLoadSceneTrigger");

        instance.loadingSceneOperation = SceneManager.LoadSceneAsync(sceneBuildId);
        instance.loadingSceneOperation.allowSceneActivation = false;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }

        Destroy(this.gameObject);
    }

    private void Start()
    {
        if (shouldPlayOpeningAnimation)
            animator.SetTrigger("EndLoadSceneTrigger");
    }

    private void Update()
    {
        if (loadingSceneOperation != null)
        {
            loadingPercentage.text = Mathf.RoundToInt(loadingSceneOperation.progress * 100) + "%";
            progressBar.fillAmount = loadingSceneOperation.progress;
        }
    }

    public void OnAnimationOver ()
    {
        shouldPlayOpeningAnimation = true;
        loadingSceneOperation.allowSceneActivation = true;
    }
}
