using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Generated : MonoBehaviour
{
    [SerializeField] private InputField width;
    [SerializeField] private InputField height;
    [SerializeField] private GameObject prefab;
    [SerializeField] private string letters;
    [SerializeField] private Text info;
    [SerializeField] private GridLayoutGroup _gridLayoutGroup;
    int h , w = 0;
    void Start()
    {
        width.text = PlayerPrefs.GetString("width", "2");
        height.text = PlayerPrefs.GetString("height", "2");
        info.text = "";
        _gridLayoutGroup = GetComponent<GridLayoutGroup>();
    }
    /// <summary>
    /// Start new generate grid conten
    /// </summary>
    public void StartGenerate()
    {
        if (!int.TryParse(width.text, out w)) return;
        if (!int.TryParse(height.text, out h)) return;
        if (w < 0 || h < 0) return;
        // destroy old objects
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        _gridLayoutGroup.constraintCount = w;
        for (int i = 0; i < w*h; i++)
            instanceWorld();
        Resizing();
        RandomWorlds();
        info.text = "Сгенерировано " + (w * h) + " обьектов";
        PlayerPrefs.SetString("width", width.text);
        PlayerPrefs.SetString("height", height.text);
    }
    /// <summary>
    /// Randomize words and calculation font size
    /// </summary>
    public void RandomWorlds()
    {
        Text _text;
        float max_size = _gridLayoutGroup.cellSize.x > _gridLayoutGroup.cellSize.y
            ? _gridLayoutGroup.cellSize.y
            : _gridLayoutGroup.cellSize.x;
        foreach (Transform child in transform)
        {
            _text = child.GetComponent<Text>();
            if (_text != null)
            {
                _text.text = letters[Random.Range(0, letters.Length - 1)].ToString();
                _text.fontSize = (int) math.abs(max_size * 0.85f);
            }        
        }
    }
    /// <summary>
    /// Create and Instantiate word
    /// </summary>
    private void instanceWorld()
    {
        GameObject o = Instantiate(prefab, new Vector3(transform.position.x,transform.position.y, transform.position.z), Quaternion.identity);
        o.transform.SetParent(transform, false);
    }
    /// <summary>
    /// Resize cell size 
    /// </summary>
    private void Resizing()
    {
        _gridLayoutGroup.cellSize = new Vector2(
            math.abs(200 / (w > 1 ? w / 2 : w)),
            math.abs(200 / (h > 1 ? h / 2 : h))
            );
        _gridLayoutGroup.enabled = false;
        _gridLayoutGroup.enabled = true;
    }
}
