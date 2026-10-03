namespace SunamoMsSqlLegacy._sunamo;



internal class InstantSB
{
    private StringBuilder _sb = new StringBuilder();
    private string _tokensDelimiter;

    public InstantSB(string znak)
    {
        _tokensDelimiter = znak;
    }

    public override string ToString()
    {
        string vratit = _sb.ToString();
        return vratit;
    }

    public void AddItem(object var)
    {
        string s = var.ToString();
        if (s != _tokensDelimiter && s != "")
        {
            _sb.Append(s + _tokensDelimiter);
        }
    }

    public void AddRaw(object tab)
    {
        _sb.Append(tab.ToString());
    }

    public void AddItems(params object[] polozky)
    {
        foreach (object var in polozky)
        {
            AddItem(var);
        }
    }

    public void EndLine(object o)
    {
        string s = o.ToString();
        if (s != _tokensDelimiter && s != "")
        {
            _sb.Append(s);
        }
    }

    public void AppendLine(string p)
    {
        EndLine((p + Environment.NewLine));
    }

    public void AppendLine()
    {
        EndLine(Environment.NewLine);
    }

    public void RemoveEndDelimiter()
    {
        _sb.Remove(_sb.Length - _tokensDelimiter.Length, _tokensDelimiter.Length);
    }
}
