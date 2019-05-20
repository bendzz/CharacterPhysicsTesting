using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class physChar : MonoBehaviour
{
    public bool useTargetRotations;

    public Transform testBone;
    public Transform testBone2;
    public Rigidbody testRigid;
    public Rigidbody testRigid2;
    //public CharacterJoint testJoint;
    //public CharacterJoint testJoint2;
    public ConfigurableJoint testJoint;
    public ConfigurableJoint testJoint2;

    //public Transform testRotation;
    public Vector3 testRot;
    public Quaternion testQuat;
    public Transform target;

    //public Animator refAnimator;
    public Transform targetBone;

    public HingeJoint hinge;
    public float strength;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(testBone.rotation);
        testRigid = testBone.GetComponentInChildren(typeof(Rigidbody)) as Rigidbody;
        testRigid2 = testBone2.GetComponentInChildren(typeof(Rigidbody)) as Rigidbody;
        //testJoint = testBone.GetComponentInChildren(typeof(CharacterJoint)) as CharacterJoint;
        //testJoint2 = testBone2.GetComponentInChildren(typeof(CharacterJoint)) as CharacterJoint;
        testJoint = testBone.GetComponentInChildren(typeof(ConfigurableJoint)) as ConfigurableJoint;

        //Debug.Log(testRigid.mass);
        //testRigid.AddTorque(transform.forward*1000, ForceMode.VelocityChange);
        //testRigid.AddRelativeTorque(transform.forward * 1000);
        //testRigid.MoveRotation(Quaternion.Euler(50, 0, 0));
        //testBone.Rotate()
        //testRigid.rotation = testRotation.rotation;//Quaternion.Euler(testRotation.rotation);
        //testRigid.rotation = Quaternion.Euler(testRot);
        //testRigid.rotation = testQuat;

        //Vector3 relativePos = target.position - transform.position;
        //testRigid.rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        //testBone.rotation = Quaternion.Euler(testRot);

        //refAnimator.SetTarget()
        //Debug.Log(refAnimator.targetRotation);

        //testBone.rotation = targetBone.rotation;
        //testRigid.freezeRotation = true;
        //testRigid.rotation = targetBone.rotation;
        //testRigid2.freezeRotation = true;
        //testRigid.MoveRotation(targetBone.rotation);
        //testRigid2.rotation = targetBone.rotation;

        //testRigid2.AddTorque(testRot, ForceMode.VelocityChange);
        //testJoint.targetAngularVelocity = testRot;

        //testJoint.targetRotation = testQuat;
        //testJoint.targetAngularVelocity = testRot;
        //testJoint.curr
        //testJoint.moto

        //testRigid.rotation = targetBone.rotation;

        //hinge.motor

        //SetJointStrength(testJoint, 100000000);

        //testJoint.targetRotation = targetBone.rotation;
        //testJoint.targetRotation = Quaternion.identity;
        //testJoint.targetRotation = Quaternion.Euler(testRot);

        //testJoint.targetRotation = targetBone.localRotation * Quaternion.Euler(testRot); ;
        //testJoint.targetRotation = Quaternion.Euler(targetBone.localEulerAngles + new Vector3(180, 180, 0));
        //testJoint.targetRotation = Quaternion.Euler(targetBone.localEulerAngles + testRot);

        /// Manually accelerating to match a position
        /// using Position Damper 3000, Maximum Force 3000
        /*
        //Quaternion targetRot = Quaternion.Euler(targetBone.localEulerAngles + new Vector3(180, 180, 0));
        Quaternion targetRot = Quaternion.Euler(targetBone.localEulerAngles);
        Vector3 TR = targetRot.eulerAngles;
        Vector3 CR = testBone.localEulerAngles; //testJoint.transform.rotation.eulerAngles;

        Vector3 DR = new Vector3(Mathf.DeltaAngle(CR.x, TR.x), Mathf.DeltaAngle(CR.y, TR.y), Mathf.DeltaAngle(CR.z, TR.z));
        DR = new Vector3(DR.x * testRot.x, DR.y * testRot.y, DR.z * testRot.z);

        testJoint.targetAngularVelocity = DR * strength;
        Debug.Log(TR + " " + CR + " " + DR + " " + testJoint.projectionAngle);
        */



        //Debug.Log(targetBone.rotation + " " + transform.forward);
        //Debug.Log(targetBone.localRotation + "  " + testJoint.targetRotation + " Jrot " + testJoint.transform.rotation + " Jrot euler " + testJoint.transform.rotation.eulerAngles +  " angVel " + targetAngularVelocity);


        Time.timeScale = 1f;

    }

    private void FixedUpdate()
    {
        testRigid = testBone.GetComponentInChildren(typeof(Rigidbody)) as Rigidbody;
        testRigid2 = testBone2.GetComponentInChildren(typeof(Rigidbody)) as Rigidbody;
        //testJoint = testBone.GetComponentInChildren(typeof(CharacterJoint)) as CharacterJoint;
        //testJoint2 = testBone2.GetComponentInChildren(typeof(CharacterJoint)) as CharacterJoint;
        testJoint = testBone.GetComponentInChildren(typeof(ConfigurableJoint)) as ConfigurableJoint;

        if (useTargetRotations)
        {
            Vector3 TB = targetBone.localEulerAngles;
            Vector3 TB2 = new Vector3(-TB.x, TB.y, TB.z);
            //testJoint.targetRotation = Quaternion.Euler(targetBone.localEulerAngles + new Vector3(140, 180, 0));
            //testJoint.targetRotation = Quaternion.Euler(targetBone.localEulerAngles + testRot);
            testJoint.targetRotation = Quaternion.Euler(TB2 + new Vector3(200, 180, 0));
        }
        else
        {
            /// just force the bone to rotate; won't push off against parent bones for force
            //Vector3 localRot = testBone.parent.transform.rotation.eulerAngles;
            //testBone.rotation = Quaternion.Euler(targetBone.localEulerAngles + testRot);
            testBone.localRotation = Quaternion.Euler(targetBone.localEulerAngles); //+ new Vector3(-20, 0, 0));
        }
    }

    public void SetJointStrength(ConfigurableJoint joint, float strength)
    {
        // Crawler ML values used: https://github.com/Unity-Technologies/ml-agents/blob/cb0bfa0382650dee2071eb415147d795721297b1/UnitySDK/Assets/ML-Agents/Examples/Crawler/Prefabs/Crawler.prefab
        
        //var rawVal = (strength + 1f) * 0.5f * thisJDController.maxJointForceLimit;
        var rawVal = (strength + 1f) * 0.5f * 10000;
        var jd = new JointDrive
        {
            //positionSpring = thisJDController.maxJointSpring,
            positionSpring = 40000,
            //positionDamper = thisJDController.jointDampen,
            positionDamper = 3000,
            maximumForce = rawVal
        };
        joint.slerpDrive = jd;
        //currentStrength = jd.maximumForce;
    }
}
