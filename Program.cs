﻿using System;
using WebSocketSharp;
using Client.Dolphin;

namespace Client {
    class Program {

        static public DateTime thisDate = DateTime.Now;

        static protected SerialManager serialManager = new SerialManager();
        static Player player = new Player(255, "Radical", serialManager);
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
