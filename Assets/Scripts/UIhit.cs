using UnityEngine;
using UnityEngine.UI;

public class UIhit : MonoBehaviour
{
    Image img;

    private void Start()
    {
        img = GetComponent<Image>();
    }

    private void Update()
    {
        if (!img.enabled)
            return;

        Invoke("HitDissapear", 0.1f);
    }

    /// <summary>
    /// Исчезновение маркера попадания
    /// </summary>
    void HitDissapear() { img.enabled = false; }
}
