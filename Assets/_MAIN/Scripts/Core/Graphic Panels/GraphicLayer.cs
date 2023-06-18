using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicLayer
{
    public const string LAYER_OBJECT_NAME_FORMAT = "Layer: {0}";
    public int layerDepth = 0;
    public Transform panel;

    public void SetTexture(string filePath, float transitionSpeed = 1f, Texture blendingTexture = null)
    {
        Texture tex = Resources.Load<Texture>(filePath);

        if(tex == null)
        {
            Debug.LogError($"Could not load graphic texture from path '{filePath}'");
            return;
        }

        SetTexture(tex, transitionSpeed, blendingTexture);
    }

    public void SetTexture(Texture tex, float transitionSpeed = 1f, Texture blendingTexture = null, string filePath = "")
    {
        CreateGraphic(tex, transitionSpeed, filePath, blendingTexture);
    }

    private void CreateGraphic<T>(T graphicData, float transitionSpeed, string filePath, bool useAudioForVideo = true, Texture blendingTexture = null)
    {
        GraphicObject newGraphic;

        if (graphicData is Texture)
            newGraphic = new GraphicObject(this, filePath, graphicData as Texture);
    }
}
