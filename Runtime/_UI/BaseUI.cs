using System.Collections;
using System.Collections.Generic;
using Teo.AutoReference;
using UnityEngine;

public class BaseUI : BaseMonoBehaviour
{
    [SerializeField] protected bool destroyOnHide = false;

    [SerializeField, GetInChildren, Name("Container")]
    protected Transform container;

    protected float durationAnim = 0.3f;

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
        if (!IsUseAnim()) return;
        StartCoroutine(AnimShow());
    }

    protected virtual IEnumerator AnimShow()
    {
        if (!container) yield break;

        container.localScale = Vector3.zero;
        float time = 0;
        while (time < durationAnim)
        {
            float t = time / durationAnim;
            t = EaseOutBack(t);
            container.localScale = new Vector3(t, t, t);
            time += Time.deltaTime;
            yield return null;
        }

        container.localScale = Vector3.one;
    }

    private float EaseOutBack(float t, float s = 1.70158f)
    {
        return 1 + (t - 1) * (t - 1) * ((s + 1) * (t - 1) + s);
    }

    /*
     * Hide UI sau thời gian delayTime mặc định là 0
     */
    public virtual void Hide(float delayTime = 0)
    {
        if (!IsUseAnim())
        {
            Invisible();
            return;
        }

        StartCoroutine(AnimHide(delayTime));
    }

    /*
     * Hide
     */

    protected virtual void Invisible()
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

    protected virtual IEnumerator AnimHide(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (container)
        {
            float time = durationAnim;
            while (time > 0)
            {
                float t = time / durationAnim;
                t = EaseInBack(t);
                container.localScale = new Vector3(t, t, t);
                time -= Time.deltaTime;
                yield return null;
            }

            container.localScale = Vector3.zero;
        }

        Invisible();
    }

    private float EaseInBack(float t, float s = 1.70158f)
    {
        return 1 - ((1 - t) * (1 - t) * ((s + 1) * (1 - t) - s));
    }

    protected virtual bool IsUseAnim()
    {
        return true;
    }
}