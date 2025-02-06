using UnityEngine;

public class SaveManager : BaseSaveManager<DataSave>
{
    #region Singleton
    private static SaveManager instance;
    public static SaveManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SaveManager>();
                if (instance == null)
                {
                    GameObject singleton = new(typeof(SaveManager).Name);
                    instance = singleton.AddComponent<SaveManager>();
                    DontDestroyOnLoad(singleton);
                }
            }
            return instance;
        }
    }

    protected override void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Transform root = transform.root;
            if (root != transform)
            {
                DontDestroyOnLoad(root);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
            Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    protected override DataSave GetDefaultData()
    {
        return new DataSave();
    }
}
