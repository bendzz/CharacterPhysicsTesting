using UnityEngine;
using System.Collections;

public class ShaderTest : MonoBehaviour {
    private Material m_Material;
    public Material Material
    {
        get {
            if (!m_Material)
            {
                m_Material = new Material(Shader.Find("Custom/UAVRenderer"));
            }
            return m_Material;
        }
    }

    struct vin
    {
        public float x;
        public float y;
    }

    ComputeBuffer fieldbuf;
    ComputeBuffer vdatabuf;

    void OnEnable()
    {
        Setup();
    }

    void OnDisable()
    {
        if (fieldbuf != null)
        {
            fieldbuf.Dispose();
            vdatabuf.Dispose();
            fieldbuf = null;
            vdatabuf = null;
        }
    }

    vin[] vdata = new vin[1000];
    void Setup()
    {
        if (fieldbuf == null)
        {
            fieldbuf = new ComputeBuffer(1000, sizeof(float), ComputeBufferType.Default);
            vdatabuf = new ComputeBuffer(1000, sizeof(float) * 2, ComputeBufferType.Default);
        }

        vdata[0].x = -0.5f;
        vdata[0].y = -0.5f;

        vdata[1].x = 0.5f;
        vdata[1].y = 0.5f;

        vdata[2].x = 0.5f;
        vdata[2].y = -0.5f;

        vdatabuf.SetData(vdata);
    }

    float[] fdata = new float[1000];
	// Update is called once per frame
	void OnRenderObject() {
        Graphics.ClearRandomWriteTargets();
        Setup();
        Material.SetPass(0);
        m_Material.SetBuffer("vdata", vdatabuf);
        m_Material.SetBuffer("Field", fieldbuf);
        Graphics.SetRandomWriteTarget(1, fieldbuf);
        Graphics.DrawProcedural(MeshTopology.Triangles, 1000);

        fieldbuf.GetData(fdata);
        var printed = false;
        for (int i = 0; i < fdata.Length; i++)
        {
            if (fdata[i] > 0)
            {
                print(i + ":  " + fdata[i]);
                printed = true;
            }
        }
        if (!printed)
            print("Failed to set uav from pixel shader");
    }
}
