using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderTest : MonoBehaviour
{
    public ComputeShader computeShader;
    public RenderTexture renderTexture;

    void Start()
    {
        renderTexture = new RenderTexture(256, 256, 24)
        {
            enableRandomWrite = true
        };
        renderTexture.Create();
        computeShader.SetTexture(0, "Result", renderTexture);
        computeShader.Dispatch(0, renderTexture.width / 8, renderTexture.height / 8, 1);
    }

}
