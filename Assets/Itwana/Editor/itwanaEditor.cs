using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor( typeof(itwana))]
public class itwanaEditor: Editor{
    itwana _itwana;

    void OnEnable(){

        _itwana = (itwana)target;
    }

    public override void OnInspectorGUI(){

		
        EditorGUILayout.Space(10);
        EditorGUILayout.BeginVertical ("box");
        EditorStyles.label.fontStyle = FontStyle.Bold;

        EditorGUILayout.BeginHorizontal ("box");
        _itwana.type = (itwana.Type)EditorGUILayout.EnumPopup ("Type", _itwana.type);
        EditorGUILayout.EndVertical();
      

        if (_itwana.type == itwana.Type.Move ||_itwana.type == itwana.Type.Scale ||_itwana.type == itwana.Type.Rotate) {
			
            EditorGUILayout.BeginVertical ("box");
            _itwana.method = (itwana.Method)EditorGUILayout.EnumPopup ("Method", _itwana.method);
            EditorGUILayout.EndVertical();

            if (_itwana.method != itwana.Method.None)
            {
                EditorGUILayout.BeginVertical("box");
                _itwana.axis = (itwana.Axis) EditorGUILayout.EnumPopup("Axis", _itwana.axis);
                EditorGUILayout.EndVertical();

                XYZFields();
                TargetLocationField();
                
                if (_itwana.method != itwana.Method.Update)
                    EaseTypeLoopType();

                DelayTime();
                RepeatOnclickIgnoreTimeScale();
            }
        }
        else if (_itwana.type == itwana.Type.Stab) {
            EditorGUILayout.BeginVertical ("box");
            _itwana.clip =(AudioClip)EditorGUILayout.ObjectField ("Clip",_itwana.clip, typeof(AudioClip),true);
            EditorGUILayout.EndVertical();
            PitchVolume();
            RepeatOnclickIgnoreTimeScale();
        }
        else if (_itwana.type == itwana.Type.FollowPath) {
            EditorGUILayout.BeginVertical ("box");
            _itwana.pathName= EditorGUILayout.TextField("PathName",_itwana.pathName);
            EditorGUILayout.EndVertical();
            EaseTypeLoopType();
            DelayTime();
            RepeatOnclickIgnoreTimeScale();
        }

        else if (_itwana.type == itwana.Type.Audio) {
            EditorGUILayout.BeginVertical ("box");
            _itwana.method = (itwana.Method)EditorGUILayout.EnumPopup ("Method", _itwana.method);
            EditorGUILayout.EndVertical();
            if (_itwana.method == itwana.Method.To || _itwana.method == itwana.Method.From || _itwana.method == itwana.Method.Update)
            {
                PitchVolume();
                RepeatOnclickIgnoreTimeScale();
            }
            
        }
        EditorGUILayout.EndVertical ();
        
        Version();
        
    }

    private void TargetLocationField()
    {         
        
        if( _itwana.axis==itwana.Axis.None||_itwana.axis!=itwana.Axis.TargetLocation)
            return;
        
        EditorGUILayout.BeginVertical ("box");
        EditorGUILayout.BeginHorizontal();
      
        if (_itwana.axis == itwana.Axis.TargetLocation) {
            _itwana.targetGameObject=(GameObject)EditorGUILayout.ObjectField ("target",_itwana.targetGameObject,typeof(GameObject),true);
        }
        
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
        
    }


    private    void XYZFields()
    {
        
        if( _itwana.axis==itwana.Axis.None ||_itwana.axis==itwana.Axis.TargetLocation)
            return;
        
      
        EditorGUILayout.BeginVertical ("box");
        EditorGUILayout.BeginHorizontal();
        if (_itwana.axis == itwana.Axis.X)
            XValueField();

        if (_itwana.axis == itwana.Axis.Y)
            YValueField();

        if (_itwana.axis == itwana.Axis.Z)
            ZValueField();
				
        if (_itwana.axis == itwana.Axis.XYZ) {
            EditorGUIUtility.fieldWidth = 50;
            EditorGUIUtility.labelWidth = 20;
            XValueField();
            YValueField();
            ZValueField();
        }
        if (_itwana.axis == itwana.Axis.XY) {
            EditorGUIUtility.fieldWidth = 50;
            EditorGUIUtility.labelWidth = 20;
            XValueField();
            YValueField();
        }
        if (_itwana.axis == itwana.Axis.YZ) {
            EditorGUIUtility.fieldWidth = 50;
            EditorGUIUtility.labelWidth = 20;
            YValueField();
            ZValueField();
        }
        if (_itwana.axis == itwana.Axis.XZ) {
            EditorGUIUtility.fieldWidth = 50;
            EditorGUIUtility.labelWidth = 20;
            XValueField();
            ZValueField();
        }
        EditorGUILayout.BeginHorizontal ("box");
        EditorGUILayout.EndHorizontal();
		
		
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }

    private   void XValueField()
    {
        EditorGUILayout.BeginHorizontal ("box");
        _itwana.x = EditorGUILayout.FloatField ("X", _itwana.x);
        EditorGUILayout.EndHorizontal();
    }
    private  void YValueField()
    {
        EditorGUILayout.BeginHorizontal ("box");
        _itwana.y = EditorGUILayout.FloatField ("Y", _itwana.y);
        EditorGUILayout.EndHorizontal();
    }
    private   void ZValueField()
    {
        EditorGUILayout.BeginHorizontal ("box");
        _itwana.z = EditorGUILayout.FloatField ("Z", _itwana.z);
        EditorGUILayout.EndHorizontal();
    }

    private    void EaseTypeLoopType()
    {
		
        EditorGUILayout.BeginVertical ("box");
        EditorGUIUtility.fieldWidth = 10;
        EditorGUIUtility.labelWidth = 60;
		
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginHorizontal ("box");
        _itwana.easeType=(iTween.EaseType)EditorGUILayout.EnumPopup ("EaseType", _itwana.easeType);
        EditorGUILayout.EndHorizontal();
		
        EditorGUILayout.BeginHorizontal ("box");
        _itwana.loopType=(iTween.LoopType)EditorGUILayout.EnumPopup ("LoopType", _itwana.loopType);
        EditorGUILayout.EndHorizontal();
        
        
		
		
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }
	
	
    private   void DelayTime()
    {
		
        EditorGUILayout.BeginVertical ("box");
        EditorGUIUtility.fieldWidth = 50;
        EditorGUIUtility.labelWidth = 50;
		
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginHorizontal ("box");
        _itwana.delay = EditorGUILayout.FloatField("Delay", _itwana.delay);
        EditorGUILayout.EndHorizontal();
		
        EditorGUILayout.BeginHorizontal ("box");
        _itwana.time = EditorGUILayout.FloatField("Time", _itwana.time);
        EditorGUILayout.EndHorizontal();
		
		
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }

    private   void RepeatOnclickIgnoreTimeScale()
    {
        EditorGUILayout.BeginVertical ("box");
        EditorGUIUtility.fieldWidth = 10;
        EditorGUIUtility.labelWidth = 80;

		
        EditorGUILayout.BeginHorizontal();
		
        EditorGUILayout.BeginHorizontal ("box");
        _itwana.repeat = EditorGUILayout.Toggle ("Repeat", _itwana.repeat);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal ("box");
        _itwana.onClick = EditorGUILayout.Toggle ("Onclick",_itwana.onClick);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndHorizontal();
        
        
        EditorGUILayout.BeginVertical ();
        EditorGUIUtility.labelWidth = 200;
        
        EditorGUILayout.BeginHorizontal ("box");
        _itwana.ignoreTimeScale=   EditorGUILayout.Toggle("IgnoreTimeScale",  _itwana.ignoreTimeScale);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndHorizontal();
        
        
        EditorGUILayout.EndVertical();
    }

    private  void PitchVolume()
    {
        EditorGUILayout.BeginVertical ("box");
        _itwana.pitch = EditorGUILayout.Slider ("Pitch", _itwana.pitch, -3, 3);
        _itwana.volume = EditorGUILayout.Slider ("Volume", _itwana.volume, 0, 1);
        EditorGUILayout.EndVertical();
    }

    private void Version()
    {
        EditorGUILayout.BeginVertical();
        EditorStyles.label.fontSize = 10;
        EditorGUILayout.LabelField("Verion 3.0");

        EditorGUILayout.EndVertical ();
    }

}