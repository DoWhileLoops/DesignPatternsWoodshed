using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternsWoodshed
{
    //Band
    /// Band
    ///     Albums
    ///     Members
    /// Album
    /// Musician
    /// BandBuilder
    ///     : BandMemberBuilder
    ///     : BandAlbumBuilder
    /// 

    public enum Genre { Rock, Blues, Jazz, Funk, Country, Classical };
    public enum Instrument { Guitar, Bass, Keyboards, Drums, Vocals };

    public class Band
    {
        public List<Musician> musicians = new List<Musician>();
        public List<Album> albums = new List<Album>();
        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            string indent = "  ";

            sb.AppendLine("Musicians:" + indent);
            foreach(Musician m in musicians)
            {
                sb.AppendLine(m.ToString());
            }
            sb.AppendLine();
            sb.AppendLine("Albums:" + indent);
            foreach (Album a in albums)
            {
                sb.AppendLine(a.ToString());
            }

            return sb.ToString();
        }
    }

    public class Album
    {
        public string name { get; set; }
        public Genre genre { get; set; }

        public Album() { }

        public Album(string name, Genre genre)
        {
            this.name = name;
            this.genre = genre;
        }

        public override string ToString()
        {
            return $"{Helpers.Capitalize(nameof(name))}: {name}, {nameof(Genre)}: {genre}";
        }
    }

    public class Musician
    {
        public string name { get; set; }
        public Instrument instrument { get; set; }

        public Musician() { }

        public Musician(string name, Instrument instrument)
        {
            this.name = name;
            this.instrument = instrument;
        }

        public override string ToString()
        {
            return $"{Helpers.Capitalize(nameof(name))}: {name}, {Helpers.Capitalize(nameof(instrument))}: {instrument}";
        }
    }

    public class BandBuilder
    {
        protected Band band = new Band();

        public BandMemberBuilder Members => new BandMemberBuilder(band);
        public BandAlbumBuilder Albums => new BandAlbumBuilder(band);
        
        public static implicit operator Band(BandBuilder bb)
        {
            return bb.band;
        }
    }

    public class BandMemberBuilder : BandBuilder
    {
        Musician musician = new Musician();

        public BandMemberBuilder(Band band)
        {
            this.band = band;
        }

        public BandMemberBuilder Name(string name)
        {
            this.musician.name = name;
            return this;
        }

        public BandMemberBuilder Plays(Instrument instrument)
        {
            this.musician.instrument = instrument;
            return this;
        }

        public BandMemberBuilder Add()
        {
            this.band.musicians.Add(this.musician);
            return this;
        }

        public BandMemberBuilder Clear()
        {
            this.musician = new Musician();
            return this;
        }

        public BandMemberBuilder AddAndClear()
        {
            this.band.musicians.Add(this.musician);
            this.musician = new Musician();
            return this;
        }
    }

    public class BandAlbumBuilder : BandBuilder
    {
        public Album album = new Album();

        public BandAlbumBuilder(Band band)
        {
            this.band = band;
        }

        public BandAlbumBuilder Name(string name)
        {
            this.album.name = name;
            return this;
        }

        public BandAlbumBuilder Genre(Genre genre)
        {
            this.album.genre = genre;
            return this;
        }

        public BandAlbumBuilder Add()
        {
            this.band.albums.Add(this.album);
            return this;
        }

        public BandAlbumBuilder Clear()
        {
            this.album = new Album();
            return this;
        }

        public BandAlbumBuilder AddAndClear()
        {
            this.band.albums.Add(this.album);
            this.album = new Album();
            return this;
        }
    }

    
    public class Helpers
    {
        public static string Capitalize(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            char[] chars = s.ToCharArray();

            string holder = chars[0].ToString();
            chars[0] = holder.ToUpper().ToCharArray()[0];
            return new string(chars);
        }
    }

    class Program
    {
        
        static void Main(string[] args)
        {

            var bb = new BandBuilder();

            Band band = bb.Members.Name("Roger Waters").Plays(Instrument.Bass).AddAndClear()
                                  .Name("David Gilmour").Plays(Instrument.Guitar).AddAndClear()
                                  .Albums.Name("Dark Side Of The Moon").Genre(Genre.Rock).AddAndClear()
                                  .Albums.Name("Devil Went Down To Georgia").Genre(Genre.Classical).AddAndClear();

            Console.WriteLine(band);

            Console.ReadKey();
        }
    }

    
}
