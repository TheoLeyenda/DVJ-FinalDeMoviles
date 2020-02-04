using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy
{
    // Start is called before the first frame update
    public Material trasparentMaterial;
    public Material normalMaterial;
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public BoxCollider boxCollider;
    public float DelayEnable;
    public float auxDelayEnable;
    public float DelayDisable;
    public float auxDelayDisable;
    public enum StateGhost
    {
        None,
        InEnable,
        InDisable,
    }
    private StateGhost stateGhost;
    protected override void Start()
    {
        stateGhost = StateGhost.InEnable;
        base.Start();
    }
    private void OnDisable()
    {
        stateGhost = StateGhost.InEnable;
    }
    // Update is called once per frame
    protected override void Update()
    {
        if (stateEnemy != StateEnemy.stune)
        {
            if (life > 0)
            {
                CheckStateGhost();
            }
            else
            {
                skinnedMeshRenderer.material = normalMaterial;
                boxCollider.enabled = true;
            }
        }
        base.Update();
        CheckState();
    }
    public void CheckStateGhost()
    {
        if (stateGhost == StateGhost.InEnable)
        {
            skinnedMeshRenderer.material = normalMaterial;
            boxCollider.enabled = true;
            CheckEnableGhost();
        }
        else if (stateGhost == StateGhost.InDisable)
        {
            skinnedMeshRenderer.material = trasparentMaterial;
            boxCollider.enabled = false;
            CheckDisableGhost();
        }
    }
    public void CheckEnableGhost()
    {
        if (DelayEnable > 0 && stateGhost == StateGhost.InEnable)
        {
            DelayEnable = DelayEnable - Time.deltaTime;
        }
        else if(DelayEnable <= 0)
        {
            DelayEnable = auxDelayEnable;
            stateGhost = StateGhost.InDisable;
        }
    }
    public void CheckDisableGhost()
    {
        if (DelayDisable > 0 && stateGhost == StateGhost.InDisable)
        {
            DelayDisable = DelayDisable - Time.deltaTime;
        }
        else if (DelayDisable <= 0)
        {
            DelayDisable = auxDelayDisable;
            stateGhost = StateGhost.InEnable;
        }
    }
}
