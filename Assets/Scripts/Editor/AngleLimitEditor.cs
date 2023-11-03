using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[CustomEditor(typeof(RotateObject)), CanEditMultipleObjects]
public class AngleLimitEditor : Editor
{
    JointAngularLimitHandle m_Handle = new JointAngularLimitHandle();

    // the OnSceneGUI callback uses the Scene view camera for drawing handles by default
    protected virtual void OnSceneGUI()
    {
        RotateObject jointExample = (RotateObject)target;

        if(!jointExample.ContraintX)
        {
            // copy the target object's data to the handle
            m_Handle.xMin = jointExample.xMin;
            m_Handle.xMax = jointExample.xMax;
        }
        else
        {
            m_Handle.xMin = 0;
            m_Handle.xMax = 0;
        }

        if (!jointExample.ContraintY)
        {
            // CharacterJoint and ConfigurableJoint implement y- and z-axes symmetrically
            m_Handle.yMin = jointExample.yMin;
            m_Handle.yMax = jointExample.yMax;
        }
        else
        {
            m_Handle.yMin = 0;
            m_Handle.yMax = 0;
        }

        if (!jointExample.ContraintZ)
        {
            m_Handle.zMin = jointExample.zMin;
            m_Handle.zMax = jointExample.zMax;
        }
        else
        {
            m_Handle.zMin = 0;
            m_Handle.zMax = 0;
        }
        // set the handle matrix to match the object's position/rotation with a uniform scale
        Matrix4x4 handleMatrix = Matrix4x4.TRS(
            jointExample.transform.position,
            jointExample.transform.rotation,
            Vector3.one
        );

        EditorGUI.BeginChangeCheck();

        using (new Handles.DrawingScope(handleMatrix))
        {
            // maintain a constant screen-space size for the handle's radius based on the origin of the handle matrix
            m_Handle.radius = HandleUtility.GetHandleSize(Vector3.zero);

            // draw the handle
            EditorGUI.BeginChangeCheck();
            m_Handle.DrawHandle();
            if (EditorGUI.EndChangeCheck())
            {
                // record the target object before setting new values so changes can be undone/redone
                Undo.RecordObject(jointExample, "Change Joint Example Properties");

                if (!jointExample.ContraintX)
                {
                    // copy the handle's updated data back to the target object
                    jointExample.xMin = m_Handle.xMin;
                    jointExample.xMax = m_Handle.xMax;
                }

                if (!jointExample.ContraintY)
                {
                    jointExample.yMax = m_Handle.yMax;
                    jointExample.yMin = m_Handle.yMin;
                }

                if (!jointExample.ContraintZ)
                {
                    jointExample.zMax = m_Handle.zMax;
                    jointExample.zMin = m_Handle.zMin;
                }
            }
        }
    }
}
