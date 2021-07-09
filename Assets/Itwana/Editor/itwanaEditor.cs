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
		
        EditorGUILayout.BeginHorizontal ("box");
        _itwana.type = (itwana.Type)EditorGUILayout.EnumPopup ("type", _itwana.type);
        EditorGUILayout.EndVertical();

        if (_itwana.type == itwana.Type.Move ||_itwana.type == itwana.Type.Scale ||_itwana.type == itwana.Type.Rotate) {
			
            EditorGUILayout.BeginVertical ("box");
            _itwana.method = (itwana.Method)EditorGUILayout.EnumPopup ("method", _itwana.method);
            EditorGUILayout.EndVertical();
			
         
            EditorGUILayout.BeginVertical ("box");
            _itwana.axis=(itwana.Axis)EditorGUILayout.EnumPopup ("axis", _itwana.axis);
            EditorGUILayout.EndVertical();
				
				
            XYZFields();
            TargetLocationField();

            if (_itwana.method != itwana.Method.Update)
                EaseTypeLoopType();

            DelayTime();
				
            RepeatOnclickIgnoreTimeScale();

        }

        if (_itwana.type == itwana.Type.Stab) {
            EditorGUILayout.BeginVertical ("box");
            _itwana.clip =(AudioClip)EditorGUILayout.ObjectField ("Clip",_itwana.clip, typeof(AudioClip),true);
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginVertical ("box");
            _itwana.pitch = EditorGUILayout.Slider ("pitch",_itwana.pitch, -3, 3);
            _itwana.volume = EditorGUILayout.Slider ("volume", _itwana.volume, 0, 1);
            EditorGUILayout.EndVertical();
            RepeatOnclickIgnoreTimeScale();
        }
        if (_itwana.type == itwana.Type.FollowPath) {
            EditorGUILayout.BeginVertical ("box");
            _itwana.pathName= EditorGUILayout.TextField("pathName",_itwana.pathName);
            EditorGUILayout.EndVertical();
            EaseTypeLoopType();
            DelayTime();
            RepeatOnclickIgnoreTimeScale();
        }

        if (_itwana.type == itwana.Type.Audio) {
            EditorGUILayout.BeginVertical ("box");
            _itwana.method = (itwana.Method)EditorGUILayout.EnumPopup ("method", _itwana.method);
            EditorGUILayout.EndVertical();
            if (_itwana.method == itwana.Method.To || _itwana.method == itwana.Method.From || _itwana.method == itwana.Method.Update) {
                EditorGUILayout.BeginVertical ("box");
                _itwana.pitch = EditorGUILayout.Slider ("pitch", _itwana.pitch, -3, 3);
                _itwana.volume = EditorGUILayout.Slider ("volume", _itwana.volume, 0, 1);
                EditorGUILayout.EndVertical();
                RepeatOnclickIgnoreTimeScale();
            }
        }

        EditorGUILayout.EndVertical ();




    }

    void TargetLocationField()
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


    void XYZFields()
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

    void XValueField()
    {
        EditorGUILayout.BeginHorizontal ("box");
        _itwana.x = EditorGUILayout.FloatField ("x", _itwana.x);
        EditorGUILayout.EndHorizontal();
    }
    void YValueField()
    {
        EditorGUILayout.BeginHorizontal ("box");
        _itwana.y = EditorGUILayout.FloatField ("y", _itwana.y);
        EditorGUILayout.EndHorizontal();
    }
    void ZValueField()
    {
        EditorGUILayout.BeginHorizontal ("box");
        _itwana.z = EditorGUILayout.FloatField ("z", _itwana.z);
        EditorGUILayout.EndHorizontal();
    }

    void EaseTypeLoopType()
    {
		
        EditorGUILayout.BeginVertical ("box");
        EditorGUIUtility.fieldWidth = 20;
        EditorGUIUtility.labelWidth = 100;
		
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginHorizontal ("box");
        _itwana.easeType=(iTween.EaseType)EditorGUILayout.EnumPopup ("easeType", _itwana.easeType);
        EditorGUILayout.EndHorizontal();
		
        EditorGUILayout.BeginHorizontal ("box");
        _itwana.loopType=(iTween.LoopType)EditorGUILayout.EnumPopup ("loopType", _itwana.loopType);
        EditorGUILayout.EndHorizontal();
		
		
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }
	
	
    void DelayTime()
    {
		
        EditorGUILayout.BeginVertical ("box");
        EditorGUIUtility.fieldWidth = 50;
        EditorGUIUtility.labelWidth = 50;
		
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginHorizontal ("box");
        _itwana.delay = EditorGUILayout.FloatField("delay", _itwana.delay);
        EditorGUILayout.EndHorizontal();
		
        EditorGUILayout.BeginHorizontal ("box");
        _itwana.time = EditorGUILayout.FloatField("time", _itwana.time);
        EditorGUILayout.EndHorizontal();
		
		
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }

    void RepeatOnclickIgnoreTimeScale()
    {
        EditorGUILayout.BeginVertical ("box");
        EditorGUIUtility.fieldWidth = 10;
        EditorGUIUtility.labelWidth = 80;

		
        EditorGUILayout.BeginHorizontal();
		
        EditorGUILayout.BeginHorizontal ("box");
        _itwana.repeat = EditorGUILayout.Toggle ("repeat", _itwana.repeat);
        EditorGUILayout.EndHorizontal();

				
        EditorGUILayout.BeginHorizontal ("box");
        _itwana.onClick = EditorGUILayout.Toggle ("onclick",_itwana.onClick);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndHorizontal();
        
        
        EditorGUILayout.BeginVertical ();
        EditorGUIUtility.fieldWidth = 100;
        EditorGUIUtility.labelWidth = 150;
        
        EditorGUILayout.BeginHorizontal ("box");
		
        _itwana.ignoreTimeScale = EditorGUILayout.Toggle("ignoreTimeScale", _itwana.ignoreTimeScale);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndHorizontal();
        
        
        EditorGUILayout.EndVertical();
    }



}