// See https://aka.ms/new-console-template for more information
namespace BSSFR
{
    class SongInfo
    {
        public string? Author { get; set; }
        public string? SongName { get; set; }
        public string? Mapper { get; set; }

    }
    class Renamer
    {

        static void Main(string[] args)
        {
            string currentDir = Directory.GetCurrentDirectory();


                var songDirs = Directory.EnumerateDirectories(currentDir);
                
                foreach (string songDir in songDirs)
                {
                    //Console.WriteLine(songDir);
                    SongInfo songInfo = Renamer.DatToSongInfo(songDir + "/info.dat");
                    
                string newDir = currentDir + @"\" + @songInfo.Author + " - " + @songInfo.SongName + " - " + @songInfo.Mapper + " Mapper";
                    //Console.WriteLine(songDir);
                    Console.WriteLine(newDir);
                if (songDir != newDir) { Directory.Move(songDir, newDir); }
                
            }
            Console.WriteLine("press a key to quit.....");
            Console.ReadKey();
        }

        static string  RemoveInvalidChars (string stringtoformat )
        {
            char[] forbiddenChars = { '\\', '/', ':', '*', '?', '"', '<', '>', '|' };
            foreach (char c in forbiddenChars)
            {
                stringtoformat = stringtoformat.Replace(c.ToString(), "");
            }
            return stringtoformat;
        }
        internal static SongInfo DatToSongInfo(string path)
        {
            char[] charsToTrim = { '{', '}'};
            string text = System.IO.File.ReadAllText(path).Trim(charsToTrim);
            string splitter = "\",";
            string[] lines = text.Split(splitter);

            SongInfo songInfo = new SongInfo();
            string author = lines[4].Split(':')[1].Trim().Remove(0, 1).Trim();
            if (author.Length == 0)  { author = "Unknown Author"; } 
            songInfo.Author = RemoveInvalidChars(author);
            //Console.WriteLine(author);

            
            string songName = lines[1].Split(':')[1].Trim().Remove(0, 1).Trim();
            //Console.WriteLine(lines[3]);
            songInfo.SongName = RemoveInvalidChars(songName);
           //Console.WriteLine(songName);   

           string mapper = lines[3].Split(':')[1].Trim().Remove(0, 1).Trim();
            if (mapper.Length == 0) { mapper = "Unknown Mapper"; }
            songInfo.Mapper = RemoveInvalidChars(mapper);
           //Console.WriteLine(mapper);

            return songInfo;
        }
    }
    

}
