using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class cameraScript : MonoBehaviour
{

    public Camera mainCamera;
    public RenderTexture cameraRT;
    public Texture2D depthTexture;

    // called before Start for all objects
    public void Awake()
    {
        
        mainCamera = GetComponent<Camera>();
        mainCamera.depthTextureMode = DepthTextureMode.Depth;
        Debug.Log(mainCamera);
        
        //GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;

        /*
        cameraRT = RenderTexture.GetTemporary(Screen.width, Screen.height, 24);

        Debug.Log(cameraRT.GetNativeDepthBufferPtr());
        depthTexture = Texture2D.CreateExternalTexture(Screen.width, Screen.height, TextureFormat.RGB24, false, false, cameraRT.GetNativeDepthBufferPtr());

        CommandBuffer cmdBuffer = new CommandBuffer();
        cmdBuffer.name = "cmdBuffer";

        cmdBuffer.SetRenderTarget(BuiltinRenderTextureType.CurrentActive);
        cmdBuffer.ClearRenderTarget(false, true, Color.blue);   // TODO: Slow? Neccessary?
        cmdBuffer.Blit(BuiltinRenderTextureType.CurrentActive, cameraRT);

        mainCamera.AddCommandBuffer(CameraEvent.AfterSkybox, cmdBuffer);
        */
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
