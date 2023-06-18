using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicLayerTesting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Running());
    }

    IEnumerator Running()
    {
        GraphicPanel panel = GraphicPanelManager.instance.GetPanel("background");
        GraphicLayer layer = panel.GetLayer(0, true);

        yield return new WaitForSeconds(1);

        Texture blendTex = Resources.Load<Texture>("Graphics/Transition Effects/hurricane");
        layer.SetTexture("Graphics/Backgrounds/hallway 1", blendingTexture: blendTex);

        layer.SetVideo("Graphics/Videos/Fantasy Landscape", blendingTexture:blendTex);

        yield return new WaitForSeconds(3);

        panel.Clear();

        yield return new WaitForSeconds(2);

        Debug.Log(layer.currentGraphic);
    }
}
