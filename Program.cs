using System;
using WebSocketSharp;
using Client.Dolphin;

namespace Client {
    class Program {
        static void Main(string[] args) {
            Library.Initialize();

            Library.OnWindwakerBeat += OnWindwakerBeat;
            Library.OnPlayingWindwakerChanged += OnPlayingWindwakerChanged;
            Library.OnTreasureChestOpen += OnTreasureChestOpen;

            while(true) {}
        }

        // example functions
        static void OnWindwakerBeat()
        {
            Console.WriteLine("Beat!");
        }

        static void OnPlayingWindwakerChanged(bool isPlaying)
        {
            string text = isPlaying ? "is played" : "isn't played";
            Console.WriteLine($"Windwaker {text}" );
        }

        static void OnTreasureChestOpen()
        {
            Console.WriteLine("Open Chest");
        }
    }
}
