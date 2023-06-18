using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicLayerTesting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GraphicPanel panel = GraphicPanelManager.instance.GetPanel("background");
        GraphicLayer layer = panel.GetLayer(0, true);

        layer.SetTexture("Graphics/Backgrounds/hallway 1");
    }
}
