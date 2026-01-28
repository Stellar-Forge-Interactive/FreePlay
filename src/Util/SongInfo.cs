using Godot;

namespace FreePlay.Util;

public ref struct SongInfo
{
    public AudioStream clip;
    public string name;
    public string artist;
    public string album;
}