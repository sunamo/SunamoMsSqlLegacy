namespace SunamoMsSqlLegacy._sunamo;

internal class InstantSB
{
    private StringBuilder _sb = new StringBuilder();
    private string _tokensDelimiter;

    internal InstantSB(string znak)
    {
        _tokensDelimiter = znak;
    }

    public override string ToString()
    {
        string vratit = _sb.ToString();
        return vratit;
    }

    internal void AddItem(object var)
    {
        string s = var.ToString();
        if (s != _tokensDelimiter && s != "")
        {
            _sb.Append(s + _tokensDelimiter);
        }
    }

    internal void AddRaw(object tab)
    {
        _sb.Append(tab.ToString());
    }

    internal void AddItems(params object[] polozky)
    {
        foreach (object var in polozky)
        {
            AddItem(var);
        }
    }

    internal void EndLine(object o)
    {
        string s = o.ToString();
        if (s != _tokensDelimiter && s != "")
        {
            _sb.Append(s);
        }
    }

    internal void AppendLine(string p)
    {
        EndLine((p + Environment.NewLine));
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