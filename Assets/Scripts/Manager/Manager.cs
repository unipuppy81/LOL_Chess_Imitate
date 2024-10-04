using UnityEngine;

public class Manager : MonoBehaviour
{
    #region Singleton

    private static Manager instance;
    private static bool initialized;
    public static Manager Instance
    {
        get
        {
            if (!initialized)
            {
                initialized = true;

                GameObject obj = GameObject.Find("@Manager");
                if (obj == null)
                {
                    obj = new() { name = "@Manager" };
                    obj.AddComponent<Manager>();
                    DontDestroyOnLoad(obj);
                    instance = obj.GetComponent<Manager>();
                }
            }
            return instance;
        }
    }

    #endregion

    #region Manager

    private readonly GameManager game = new();
    private readonly ItemManager item = new();
    private readonly ChampionManager champion = new();
    private readonly SkillManager skill = new();

    public static GameManager Game => Instance != null ? Instance.game : null;

    public static ItemManager Item => Instance != null ? Instance.item : null;

    public static ChampionManager Champion => Instance != null ? Instance.champion : null;

    public static SkillManager Skill => Instance != null ? Instance.skill : null;
    #endregion
}
