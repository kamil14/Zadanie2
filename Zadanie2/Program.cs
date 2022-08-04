using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Zadanie2
{
    class Program
    {
        static void Main(string[] args)
        {
            var onlyCutItems = new List<string>();
            var items = new List<string>();
            var numeratedItemsToSave = new List<string>();
            var numeratedCutItemsToSave = new List<string>();
            using (StreamReader sr = new StreamReader(@"C:\Users\kamil\source\repos\Zadanie2\Zadanie2\NapisyDoFilmu\NapisyDoFilmu.srt", false))
            {
                var timeToAdd = new TimeSpan(0, 0, 0, 5, 880);
                var movieSubtitles = sr.ReadToEnd();
                var movieSubtitlesSplited = movieSubtitles.Split("\n\n"); // splited by new line
                for (int i = 0; i < movieSubtitlesSplited.Length - 1; i++)
                {
                    var current = movieSubtitlesSplited[i].Split("\n");
                    var times = current[1].Split(" ");
                    var firstTime = TimeSpan.Parse(times[0]);
                    var firstTimeAfterAddTime = firstTime.Add(timeToAdd);
                    var secondTime = TimeSpan.Parse(times[2]);
                    var secondTimeAfterAddTime = secondTime.Add(timeToAdd);

                    if (firstTimeAfterAddTime.Milliseconds == 0)
                    {
                        onlyCutItems.Add(movieSubtitlesSplited[i]);
                    }
                    else
                    {
                        items.Add(movieSubtitlesSplited[i]);
                    }
                }
                var counterCutItems = 1;
                foreach (var item in onlyCutItems)
                {
                    var builder = new StringBuilder();
                    var splited = item.Split("\n");
                    splited[0] = counterCutItems.ToString();
                    for (int i = 0; i < splited.Length - 1; i++)
                    {
                        builder.Append(splited[i]);
                        builder.Append("\n");
                    }
                    counterCutItems++;
                    numeratedCutItemsToSave.Add(builder.ToString());
                }

                var counterItems = 1;
                foreach (var item in items)
                {
                    var builder = new StringBuilder();
                    var splited = item.Split("\n");
                    splited[0] = counterItems.ToString();
                    for (int i = 0; i < splited.Length - 1; i++)
                    {
                        builder.Append(splited[i]);
                        builder.Append("\n");
                    }
                    counterItems++;
                    numeratedItemsToSave.Add(builder.ToString());
                }
            }
            using (StreamWriter sv = new StreamWriter(@"C:\Users\kamil\source\repos\Zadanie2\Zadanie2\NapisyDoFilmu\NapisyDoFilmu.srt"))
            {
                try
                {
                    foreach (String s in numeratedItemsToSave)
                        sv.WriteLine(s);
                }
                catch (Exception)
                {

                    throw;
                }

            }
            using (StreamWriter sv = new StreamWriter(@"C:\Users\kamil\source\repos\Zadanie2\Zadanie2\NapisyDoFilmu\NapisyDoFilmuZWycietymiElementami.srt"))
            {
                try
                {
                    foreach (String s in numeratedCutItemsToSave)
                        sv.WriteLine(s);
                }
                catch (Exception)
                {

                    throw;
                }

            }

        }
    }
}
