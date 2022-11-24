using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XML_Printer.Entities;

namespace XML_Printer.Libs
{
    internal class TracksPrinter
    {
        public void PrintTracks(List<Track> tracks)
        {
            foreach (Track track in tracks)
            {
                Console.WriteLine($"Song: {track.Name} in {track.Path}");
            }

        }

        public void PrintTracks(Album album)
        {
           List<Track> tracks = album.Tracks;
            this.PrintTracks(tracks);
        }
    }
}
