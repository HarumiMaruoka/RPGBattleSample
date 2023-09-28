// 日本語対応
using Glib.Talk;
using UnityEngine;
using UnityEngine.UI;

public class TalkSelectable : MonoBehaviour
{
    [SerializeField]
    private Text _text;
    [SerializeField]
    private Image _image;
    [SerializeField]
    private Color _nomalColor = Color.white;
    [SerializeField]
    private Color _hoveredColor = Color.blue;

    private Glib.Talk.Node _node;

    public void Initialize(Node node)
    {
        _node = node;
        _text.text = _node.Text;

        node.OnHovered += Hovered;
        node.OnUnhovered += Unhovered;

        if (node.IsHovered) Hovered();
        else Unhovered();
    }
    public void Dispose()
    {
        _node.OnHovered -= Hovered;
        _node.OnUnhovered -= Unhovered;
    }
    public void Hovered()
    {
        _image.color = _hoveredColor;
    }
    public void Unhovered()
    {
        _image.color = _nomalColor;
    }
}