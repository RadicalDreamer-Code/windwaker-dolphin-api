using System;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Common;
using Client.Dolphin;
using System.Collections;

namespace Client {
    public class RAMDataEventArgs : EventArgs
    {
        public Offset offset { get; set; }
        public Byte[] buffer { get; set; }
    }

    class MemManager {
        static public DateTime thisDate = DateTime.Now;

        DolphinAccessor dolphinAccessor;
        public event EventHandler<RAMDataEventArgs> RamChanged;
        public List<Offset> FilteredOffsets = new List<Offset>();
        public List<OffsetId> SelectedOffsets = new List<OffsetId>();

        Offset offset;
        private List<byte[]> memValueList;
        private Timer timer;
        private TimeSpan checkTime = TimeSpan.FromMilliseconds(100);
        private byte[] buffer;
        public MemManager(DolphinAccessor dolphinAccessor) {
            this.dolphinAccessor = dolphinAccessor;
        }

        public Game Initialize() {
            Game noGame = new Game { id = GameId.NOT_FOUND };

            if (!dolphinAccessor.hook()) {
                System.Diagnostics.Debug.WriteLine("Couldn't hook to Dolphin");
                return noGame;
            }

            FilteredOffsets = new List<Offset>();

            Game game = SelectGame();

            if (game.id == GameId.NOT_FOUND)
            {
                dolphinAccessor.unhook();
                return noGame;
            }

            System.Diagnostics.Debug.WriteLine($"Hooked with {game.name}");

            if (game.id == GameId.ZELDA_WINDWAKER_NTSC)
                FilteredOffsets = Data.WW_Offsets.ToList<Offset>();
            else if (game.id == GameId.RESIDENT_EVIL_4_PAL)
                FilteredOffsets = Data.RE4_Offsets.ToList<Offset>();

            StartMemWatch();
            return game;
        }

        public void Stop()
        {
            // TODO: proper error handling
            dolphinAccessor.unhook();
            timer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        private void FilterOffsets(ReadOnlyCollection<Offset> Offsets) {
            foreach (Offset offset in Offsets) {
                if (SelectedOffsets.Any(SelectedOffset => SelectedOffset == offset.id)) {
                    this.FilteredOffsets.Add(offset);
                }
            }
        }

        private Game SelectGame()
        {
            Game noGame = new Game { id = GameId.NOT_FOUND };

            if (dolphinAccessor.Status != DolphinStatus.Hooked)
                return noGame;

            offset = Data.GameIdOffset;
            buffer = new byte[offset.length];

            if (!dolphinAccessor.read(Dolphin.Common.dolphinAddrToOffset(offset.address),
                MemoryType.ByteArray,
                out buffer,
                false,
                offset.length
                ))
            {

                Console.WriteLine("[{0:T}] " + "Failed to read RAM on " + offset.address, thisDate);
                Stop();
                return noGame;
            }

            string gameIdentifier = Encoding.UTF8.GetString(buffer);
            return Data.Games.Where(x => x.identifier == gameIdentifier).FirstOrDefault(noGame);
        }

        private void StartMemWatch() {
            System.Diagnostics.Debug.WriteLine("Start the Mem-Watch");
            //Mem-Cache
            memValueList = new List<byte[]>();
            
            foreach (Offset offset in FilteredOffsets) {
                memValueList.Add(new byte[0]);
            }

            timer = new Timer(obj => {
                for (int i = 0; i < this.FilteredOffsets.Count; i++) {
                
                    offset = this.FilteredOffsets[i];
                    buffer = new byte[offset.length];
                    //MemoryType memType = (size > 1) ? MemoryType.ByteArray : MemoryType.Byte; //TODO get right mem-type

                    if (!dolphinAccessor.read(Client.Dolphin.Common.dolphinAddrToOffset(offset.address),
                        MemoryType.ByteArray,
                        out buffer,
                        false,
                        offset.length
                        )) {

                        System.Diagnostics.Debug.WriteLine("[{0:T}] " + "Failed to read RAM on " + offset.address, thisDate);
                        //Stop();
                        continue;
                    }

                    if (memValueList[i].Length >= 1 && CompareArrays(buffer, memValueList[i])) {
                        //System.Diagnostics.Debug.WriteLine("Nothing changed for " + offset.address);
                        continue;
                    }

                    this.memValueList[i] = buffer;
                    OnRamChanged(offset, buffer);
                }
            }, null, checkTime, checkTime);
        }

        

        protected virtual void OnRamChanged(Offset offset, Byte[] buffer) {
            if (RamChanged != null) // if there are any subscribers
                RamChanged(this, new RAMDataEventArgs() {
                    offset = offset,
                    buffer = buffer
                });
        }   

        bool CompareArrays(byte[] arr1, byte[] arr2) {
            for(int i = 0; i < arr2.Length; i++) {
                if (arr2[i] != arr1[i])
                    return false;
            }
            return true;
        }
    }
}
