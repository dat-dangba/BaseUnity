using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUI : BaseMonoBehaviour
{
    [SerializeField]
    private bool destroyOnHide = false;

    /*
     * Setup trước khi show
     * */
    public virtual void SetUp()
    {

    }

    /*
     * Show UI
     */
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    /*
     * Hide UI sau thời gian delayTime mặc định là 0
     */
    public virtual void Hide(float delayTime = 0)
    {
        if (delayTime > 0)
        {
            Invoke(nameof(HideDirectly), delayTime);
        }
        else
        {
            HideDirectly();
        }
    }

    /*
     * Hide 
     */
    protected virtual void HideDirectly()
    {
        if (destroyOnHide)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
