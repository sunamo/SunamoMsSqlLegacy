namespace SunamoMsSqlLegacy._sunamo;

internal class InstantSB
{
    private StringBuilder _sb = new StringBuilder();
    private string _tokensDelimiter;

    internal InstantSB(string character)
    {
        _tokensDelimiter = character;
    }

    public override string ToString()
    {
        string result = _sb.ToString();
        return result;
    }

    internal void AddItem(object var)
    {
        string text = var.ToString();
        if (text != _tokensDelimiter && text != "")
        {
            _sb.Append(text + _tokensDelimiter);
        }
    }

    internal void AddRaw(object tab)
    {
        _sb.Append(tab.ToString());
    }

    internal void AddItems(params object[] items)
    {
        foreach (object var in items)
        {
            AddItem(var);
        }
    }

    internal void EndLine(object value)
    {
        string text = value.ToString();
        if (text != _tokensDelimiter && text != "")
        {
            _sb.Append(text);
        }
    }

    internal void AppendLine(string text)
    {
        EndLine((text + Environment.NewLine));
    }

    internal void AppendLine()
    {
        EndLine(Environment.NewLine);
    }

    internal void RemoveEndDelimiter()
    {
        _sb.Remove(_sb.Length - _tokensDelimiter.Length, _tokensDelimiter.Length);
    }
}