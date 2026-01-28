using System.IO;
using FreePlay.Util;
using Godot;

namespace FreePlay.Core;

public class FileManager
{
    static FileManager()
    {
        Directory.CreateDirectory(StringLib.DefaultRepo);
    }

    internal SongInfo DebugSongInfo()
    {
        return LoadSongInfo(StringLib.DebugSong, StringLib.DefaultAudioExtension);
    }

    public SongInfo LoadSongInfo(string file, string audioExtension)
    {
        string af = file + audioExtension;
        var song = GD.Load(af) as AudioStream;

        var meta = File.ReadAllLines(file + StringLib.MetaExtension);

        return new SongInfo { clip = song, name = meta[0], artist = meta[1], album = meta[2] };
    }
}