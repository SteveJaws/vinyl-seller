using UnityEngine;

// Zorgt dat de camera altijd een vaste beeldverhouding (standaard 16:9) toont.
// Past het scherm daar niet precies bij, dan komen er zwarte balken (letterbox/pillarbox).
[RequireComponent(typeof(Camera))]
[ExecuteAlways] // werkt ook in de editor, zodat je het direct in de Game view ziet
public class AspectRatioEnforcer : MonoBehaviour
{
    [Tooltip("Gewenste beeldverhouding (breedte / hoogte). 16/9 = 1.7778")]
    public float targetAspect = 16f / 9f;

    Camera cam;
    int lastWidth, lastHeight;

    void OnEnable()
    {
        cam = GetComponent<Camera>();
        Apply();
    }

    void Update()
    {
        // Alleen herberekenen als de schermgrootte veranderd is
        if (Screen.width != lastWidth || Screen.height != lastHeight)
            Apply();
    }

    void Apply()
    {
        lastWidth = Screen.width;
        lastHeight = Screen.height;

        float windowAspect = (float)Screen.width / Screen.height;
        float scale = windowAspect / targetAspect;

        if (scale < 1f)
        {
            // Scherm is "hoger" dan 16:9 (bijv. 4:3) → balken boven en onder
            cam.rect = new Rect(0f, (1f - scale) / 2f, 1f, scale);
        }
        else
        {
            // Scherm is breder dan 16:9 (bijv. 21:9) → balken links en rechts
            float width = 1f / scale;
            cam.rect = new Rect((1f - width) / 2f, 0f, width, 1f);
        }
    }

    void OnDisable()
    {
        // Script uitgezet? Dan camera weer normaal over het hele scherm
        if (cam != null)
            cam.rect = new Rect(0f, 0f, 1f, 1f);
    }
}
