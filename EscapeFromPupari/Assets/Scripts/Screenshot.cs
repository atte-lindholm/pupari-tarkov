using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;

public class Screenshot : MonoBehaviour
{
    public Camera camera;
    public string fullPath;
    public bool ScreenShot = false;

#if (UNITY_EDITOR)
    [ContextMenu("Screenshot")]
    void TakeScreenshot(string fullPath)
    {
        if (camera == null)
        {
            camera = GetComponent<Camera>();
        }
        RenderTexture rt = new RenderTexture(256, 256, 24);
        camera.targetTexture = rt;
        Texture2D screeShot = new Texture2D(256, 256, TextureFormat.RGBA32, false);
        camera.Render();
        RenderTexture.active = rt;
        screeShot.ReadPixels(new Rect(0, 0, 256, 256), 0, 0);
        camera.targetTexture = null;
        RenderTexture.active = null;

        if (Application.isEditor)
        {
            DestroyImmediate(rt);
        }
        else
        {
            Destroy(rt);
        }

        byte[] bytes = screeShot.EncodeToPNG();
        System.IO.File.WriteAllBytes(fullPath, bytes);

        TakeScreenshot(fullPath);
        AssetDatabase.Refresh();


    }
#endif
}
