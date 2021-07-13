using System.Collections;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class itwana : MonoBehaviour {

    #region  Enums

    public enum Axis{
        None,X,Y,Z,XY,XZ,YZ,XYZ,TargetLocation
    }
    public enum Type{
        None,Scale,Move,Rotate,Stab,FollowPath,Audio
    }
    public enum Method{
        None,To,From,Add,Update,By,Punch,Shake
    }

    #endregion    
   
    
    
    #region Variables
	
    public Type type;
    public Method  method;
    public Axis axis;
    public iTween.EaseType easeType=iTween.EaseType.linear;
    public iTween.LoopType loopType;

    public GameObject targetGameObject;

    public AudioClip clip;
    public float x, y, z;
    public float time=1, delay;
	
    public float pitch=1;
    public float volume=1;
	
    
    public bool repeat=false;
    public bool ignoreTimeScale=false;
    public bool onClick = false;
    public string pathName="Path0";


    public GameObject otherObject;
    
	
    #endregion
    private   Hashtable hash=new Hashtable();

    private Vector3 pos,rot,scal;

    private GameObject objectForTween;

    public bool isUpdate = false;

    private void Awake()
    {
        SetObjectForTween();
    }

    void SetObjectForTween()
    {
        if (otherObject)
        {
            objectForTween = otherObject;
        }
        else
        {
            objectForTween = this.gameObject;
        }
    }


    void OnEnable()
    {
        SetObjectForTween();
        pos = objectForTween.transform.localPosition;
        scal = objectForTween.transform.localScale;
        rot = objectForTween.transform.localEulerAngles;
        
        if(repeat)
            if(!onClick)
                StartTweens();
    }
    void Start(){
        if (!repeat)
            if(!onClick)
                StartTweens();
    }
    
    private void OnDisable()
    {
        if (repeat) {
            objectForTween. transform.localPosition = pos;
            objectForTween.     transform.localScale = scal;
            objectForTween.  transform.localEulerAngles = rot;
            isUpdate = false;

        }
    }


    void StartTweens(){
        
        hash=new Hashtable();
        
        if (method != Method.Update)
        {
            hash.Add("EaseType", "" + easeType);
            hash.Add("LoopType", "" + loopType);
        }

        hash.Add("time", time);
        hash.Add("delay", delay);

        if (ignoreTimeScale)
        {
            hash.Add("ignoretimescale", ignoreTimeScale);
        }
        
        switch(type)
        {
            case Type.Move:
            {
                MoveMethods ();
                break;
            }
            case Type.Rotate:
            {
                RotateMethods();
                break;
            }
            case Type.Scale:
            {
                ScaleMethods ();
                break;
            }
            case Type.Stab:
            {
                StabMethod ();
                break;
            }
            case Type.FollowPath:
            {
                MovePathMethod ();
                break;
            }
            case Type.Audio:
            {
                AudioMethods ();
                break;
            }
            default: break;
                Debug.LogWarning ("Select Correct type of itween");
        } 
        

    }
    void Update()
    {
        if (isUpdate)
            UpdateTween();
    }

    private void UpdateTween()
    {
        switch(type)
        {
            case Type.Move:
            {
                iTween.MoveUpdate (objectForTween, hash);
                break;
            }
            case Type.Rotate:
            {
                iTween.RotateUpdate (objectForTween, hash);
                break;
            }
            case Type.Scale:
            {
                iTween.ScaleUpdate (objectForTween, hash);
                break;
            }
            case Type.Stab:
            {
                break;
            }
            case Type.FollowPath:
            {
                break;
            }
            case Type.Audio:
            {
                iTween.AudioUpdate (objectForTween, hash);
                break;
            }
        }
    }
    public void OnClickTwana()
    {
        StartTweens();
    }

    void MovePathMethod (){
        hash.Add ("path", iTweenPath.GetPath (pathName));
        iTween.MoveTo (objectForTween,hash);
    }

    void AudioMethods(){
        if (Method.To == method) {
      
            iTween.AudioTo (objectForTween,hash);
        }
        else if (Method.From== method) {

          
            iTween.AudioFrom (objectForTween,hash);
        }
        else if (Method.Update== method) {

            iTween.AudioFrom (objectForTween,hash);
        }
        else
        {
            Debug.LogWarning("there is no method in Type " + type);
        }
    }
    
    private void StabMethod (){
        hash.Add ("Audioclip", clip);
        hash.Add ("pitch",pitch);
        hash.Add ("volume", volume);
        iTween.Stab (objectForTween, hash );
    }

    private void SetAxisValues(ref Vector3 temp)
    {
        switch (axis)
        {
            case Axis.X:
            {
                temp.x = x;
                if (method == Method.Punch || method == Method.Shake)
                {
                    temp.y = 0;
                    temp.z = 0;
                }
                break;
            }
            case Axis.Y:
            {
                temp.y = y;
                if (method == Method.Punch || method == Method.Shake)
                {
                    temp.x = 0;
                    temp.z = 0;
                }
                break;
            }
            case Axis.Z:
            {
                temp.z = z;
                if (method == Method.Punch || method == Method.Shake)
                {
                    temp.x = 0;
                    temp.y = 0;
                }
                break;
            }
            case Axis.XY:
            {
                temp.x = x;
                temp.y = y;
                
                if (method == Method.Punch || method == Method.Shake)
                {
                    temp.z = 0;
                }
                break;
            }
            case Axis.YZ:
            {
                temp.y = y;
                temp.z = z;
                
                if (method == Method.Punch || method == Method.Shake)
                {
                    temp.x = 0;
                }
                break;
            }
            case Axis.XZ:
            {
                temp.x = x;
                temp.z = z;

                if (method == Method.Punch || method == Method.Shake)
                {
                    temp.y = 0;
                }
                break;
            }
            case Axis.XYZ:
            {
                temp.x = x;
                temp.y = y;
                temp.z = z;
                break;
            }
            case Axis.TargetLocation:
            {
                if (type == Type.Move)
                {
                    temp = targetGameObject.transform.position;
                }
                else if (type == Type.Rotate)
                {
                    temp = targetGameObject.transform.localEulerAngles;
                }
                else if (type == Type.Scale)
                {
                    temp = targetGameObject.transform.localScale;
                }

                break;
            }
        }

    
    }
    
    private  void ScaleMethods(){

        
        Vector3 tempScale = objectForTween.transform.localScale;
        SetAxisValues(ref tempScale);
        
        if(method!=Method.Punch || method!=Method.Shake)
            hash.Add ("scale", tempScale);
        
        switch (method)
        {
            case Method.To:
            {
                iTween.ScaleTo (objectForTween,hash);
                break;
            }
            case Method.From:
            {
                iTween.ScaleFrom (objectForTween,hash);
                break;
            }
            case Method.Add:
            {
                iTween.ScaleAdd(objectForTween,hash);
                break;
            }
            case Method.Update:
            {
                isUpdate = true;
                break;
            }
            case Method.By:
            {
                iTween.ScaleBy(objectForTween,hash);
                break;
            }
            case Method.Punch:
            {
                hash.Add ("x", tempScale.x);
                hash.Add ("y", tempScale.y);
                hash.Add ("z", tempScale.z);
                iTween.PunchScale (objectForTween,hash);
                break;
            }
            case Method.Shake:
            {
                hash.Add ("x", tempScale.x);
                hash.Add ("y", tempScale.y);
                hash.Add ("z", tempScale.z);
                iTween.ShakeScale (objectForTween,hash);
                break;
            }
        }


    }

    private void MoveMethods(){
        Vector3 tempPos = objectForTween.transform.localPosition;
        SetAxisValues(ref tempPos);
        if(method!=Method.Punch || method!=Method.Shake)
            hash.Add ("position", tempPos);
        
        switch (method)
        {
            case Method.To:
            {
                iTween.MoveTo (objectForTween,hash);
                break;
            }
            case Method.From:
            {
                iTween.MoveFrom (objectForTween,hash);
                break;
            }
            case Method.Add:
            {
                iTween.MoveAdd(objectForTween,hash);
                break;
            }
            case Method.Update:
            {
                isUpdate = true;
                break;
            }
            case Method.By:
            {
                iTween.MoveBy(objectForTween,hash);
                break;
            }
            case Method.Punch:
            {
                hash.Add ("x", tempPos.x);
                hash.Add ("y", tempPos.y);
                hash.Add ("z", tempPos.z);
                iTween.PunchPosition (objectForTween,hash);
                break;
            }
            case Method.Shake:
            {
                hash.Add ("x", tempPos.x);
                hash.Add ("y", tempPos.y);
                hash.Add ("z", tempPos.z);
                iTween.ShakePosition (objectForTween,hash);
                break;
            }
        }
    }

    private void RotateMethods()
    {
        Vector3 tempRot = objectForTween.transform.localEulerAngles;
        SetAxisValues(ref tempRot);
        
        if(method!=Method.Punch || method!=Method.Shake)
            hash.Add ("rotation", tempRot);
        
        switch (method)
        {
            case Method.To:
            {
                iTween.RotateTo(objectForTween,hash);
                break;
            }
            case Method.From:
            {
                iTween.RotateFrom(objectForTween,hash);
                break;
            }
            case Method.Add:
            {
                iTween.RotateAdd(objectForTween,hash);
                break;
            }
            case Method.Update:
            {
                isUpdate = true;
                break;
            }
            case Method.By:
            {
                iTween.RotateBy(objectForTween,hash);
                break;
            }
            case Method.Punch:
            {
                hash.Add ("x", tempRot.x);
                hash.Add ("y", tempRot.y);
                hash.Add ("z", tempRot.z);
                iTween.PunchRotation (objectForTween,hash);
                break;
            }
            case Method.Shake:
            {
                hash.Add ("x", tempRot.x);
                hash.Add ("y", tempRot.y);
                hash.Add ("z", tempRot.z);
                iTween.ShakeRotation (objectForTween,hash);
                break;
            }
        }
    }
}