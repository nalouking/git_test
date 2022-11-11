using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

[ExecuteInEditMode]
public class Bloom : MonoBehaviour
{
    public Shader BloomShader;
    public Vector2 DownSample = new Vector2(1, 1);
    [Tooltip("Bloom大小")]
    public float BloomWidth = 0;
    [Tooltip("Bloom强度")]
    [Range(0, 2)]
    public float BloomStrength = 0;
    [Tooltip("执行迭代次数")]
    [Range(0, 3)]
    public int IterativeTimes = 1;
    public bool AverageBlur = false;

    public List<RenderTexture> AllTempTexture = new List<RenderTexture>();

    void OnRenderImage(RenderTexture src, RenderTexture des)
    {
        foreach(RenderTexture R in AllTempTexture)
        {
            RenderTexture.ReleaseTemporary(R);
        }
        AllTempTexture.Clear();
        Material M = new Material(BloomShader);
        M.hideFlags = HideFlags.DontSave;
        Vector2 TexSize = new Vector2(src.width * DownSample.x, src.height * DownSample.y);
        RenderTexture buffer0 = RenderTexture.GetTemporary((int)TexSize.x, (int)TexSize.y);
        RenderTexture buffer1;
        M.SetVector("_BloomWidth", new Vector4(BloomWidth * DownSample.x / src.width, BloomWidth * DownSample.x / src.height, 0, 0));
        M.SetFloat("_BloomStrength", BloomStrength);

        Graphics.Blit(src, buffer0, M, 0);

        for (int i = 0; i < IterativeTimes; i++)
        {
            buffer1 = RenderTexture.GetTemporary((int)TexSize.x, (int)TexSize.y);
            Graphics.Blit(buffer0, buffer1, M, 1);
            RenderTexture.ReleaseTemporary(buffer0);
            buffer0 = buffer1;
            AllTempTexture.Add(buffer1);
        }

        buffer1 = null;
        if (AverageBlur)
        {
            buffer1 = RenderTexture.GetTemporary((int)TexSize.x, (int)TexSize.y);
            M.SetVector("_BloomWidth", new Vector4(BloomWidth * DownSample.x / src.width, BloomWidth * DownSample.x / src.height, 0, 0));
            Graphics.Blit(buffer0, buffer1, M, 2);
            RenderTexture.ReleaseTemporary(buffer0);
            buffer0 = buffer1;
        }

        M.SetTexture("_BloomTex", buffer0);
        Graphics.Blit(src, des, M, 3);

        RenderTexture.ReleaseTemporary(buffer0);
        RenderTexture.ReleaseTemporary(buffer1);
        RenderTexture.ReleaseTemporary(src);
        RenderTexture.ReleaseTemporary(des);
        DestroyImmediate(M);
    }
}
