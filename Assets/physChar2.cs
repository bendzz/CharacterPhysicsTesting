using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class physChar2 : MonoBehaviour
{
    public GameObject animChar;
    public GameObject physChar;

    public Vector3 startingSpeedBoost;
    public float timeScale;

    private List<physBone> physBones;

    // Start is called before the first frame update
    void Start()
    {
        setupDrivenBones();


        foreach (physBone bone in physBones)
        {
            Debug.Log(bone.phys + " " + bone.anim);
        }

    }

    private void setupDrivenBones()
    {
        /// connect all bones with a ConfigurableJoint to their corresponding bone in the animChar

        Transform[] animBones = animChar.GetComponentsInChildren<Transform>();
        physBones = new List<physBone>();

        foreach (Transform child in physChar.GetComponentsInChildren<Transform>())
        {
            //Debug.Log(child + " " + child.transform.localRotation);
            if (child.GetComponent<ConfigurableJoint>())
            {
                //Debug.Log("configurableJoint " + child.GetComponent<ConfigurableJoint>().transform.localRotation);

                foreach (Transform animBone in animBones)
                {
                    if (animBone.name == child.name)
                    {
                        //Debug.Log("animbone " + animBone);
                        physBones.Add(new physBone(child, animBone));
                    }
                }
            }

            // apply speed boost at start, just because
            if (child.GetComponent<Rigidbody>())
            {
                child.GetComponent<Rigidbody>().velocity = startingSpeedBoost;
            }
        }
    }

    public void FixedUpdate()
    {
        foreach (physBone bone in physBones)
        {
            if (bone.phys && bone.joint && bone.anim)
            {
                /// keep bones awake
                bone.phys.GetComponent<Rigidbody>().WakeUp();

                bone.joint.SetTargetRotationLocal(bone.anim.localRotation, bone.startRotation);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = timeScale;
    }
}

public struct physBone
{
    //https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/using-structs
    public Transform phys;
    public Transform anim;
    public ConfigurableJoint joint;
    public Quaternion startRotation;

    public physBone(Transform phys1, Transform anim1)
    {
        phys = phys1;
        anim = anim1;
        startRotation = phys.transform.localRotation;
        joint = phys.GetComponent<ConfigurableJoint>();
    }
}
