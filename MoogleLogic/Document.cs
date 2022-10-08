namespace MoogleLogic;


public class Document
{
    public string Path{ get; }
    public string[] Text{ get; }
    public TextInfo Info{ get; }

    public Document(string[] text, string path, TextInfo info)
    {
        this.Text = text;
        this.Path = path;
        this.Info = info;
    }

}