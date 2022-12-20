using System;

public class EditionProperties
{
    public EditionProperties(string titlename, DateTime pubdate)
    {
        TitleName = titlename;
        PubDate = pubdate;
    }
    public string TitleName { get; private set; }
    public DateTime PubDate { get; private set; }
    public bool IsExtra => TitleName.Contains("E");
    public bool IsSuplement => TitleName.Contains("DO1A") | TitleName.Contains("SUP");
}
