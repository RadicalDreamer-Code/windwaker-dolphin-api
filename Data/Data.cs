using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Common {
    public class Data {
        public static ReadOnlyCollection<Game> Games => games.AsReadOnly();
        private static List<Game> games = new List<Game>()
        {
            { new Game() { id = GameId.ZELDA_WINDWAKER_NTSC, name = "The Legend of Zelda: The Wind Waker (NTSC)", identifier = "GZLE01" } },
            { new Game() { id = GameId.RESIDENT_EVIL_4_PAL, name = "Resident Evil 4 (PAL)", identifier = "" } },
        };


        public static readonly Offset GameIdOffset = new Offset() { id = 0, address = 0x80000000, length = 0x06 };

        public static ReadOnlyCollection<Offset> WW_Offsets => offsets.AsReadOnly();
        private static List<Offset> offsets = new List<Offset>{
            { new Offset(){ id = OffsetId.WW_INVENTORY, description = "Inventory", address = 0x803C4C44, length = 0x15 } },
            { new Offset(){ id = OffsetId.WW_MELODIES, description = "Melodies", address = 0x803C4CC5, length = 0x01 } },
            { new Offset(){ id = OffsetId.WW_TRIFORCE, description = "Triforce", address = 0x803C4CC6, length = 0x01 } },
            { new Offset(){ id = OffsetId.WW_GEMS, description = "Gems", address = 0x803C4CC7, length = 0x01 } },
            { new Offset(){ id = OffsetId.WW_SWORDINHAND, description = "Sword", address = 0x803C4C16, length = 0x01 } },
            { new Offset(){ id = OffsetId.WW_SCHIELDINHAND, description = "Shield", address = 0x803C4C17, length = 0x01 } },
            { new Offset(){ id = OffsetId.WW_CURRENTHEALTH, description = "Health", address = 0x803C4C0B, length = 0x01 } },
            { new Offset(){ id = OffsetId.WW_CURRENTWINDWAKERBEAT, description = "WindWaker Beat", address = 0x803A2CAE, length = 0x01 } },
            { new Offset(){ id = OffsetId.WW_ACTIVEWINDWAKERNOTES, description = "WindWaker Notes", address = 0x803A2CAF, length = 0x01 } },
            { new Offset(){ id = OffsetId.WW_PLAYERSTATUS, description = "Player Status 1", address = 0x803CA8D0, length = 0x04 } },
            { new Offset(){ id = OffsetId.WW_PLAYERSTATUS2, description = "Player Status 2", address = 0x803CA8D4, length = 0x04 } },
            { new Offset(){ id = OffsetId.WW_CAMERAEVENT, description = "Camera Event", address = 0x803C9EA2, length = 0x01 } },
            { new Offset(){ id = OffsetId.WW_EVENTCONTROL, description = "Event?", address = 0x803C9DE1, length = 0x01 } },
        };

        public static ReadOnlyCollection<Offset> RE4_Offsets => RE4_offsets.AsReadOnly();
        private static List<Offset> RE4_offsets = new List<Offset>
        { };

        public static ReadOnlyCollection<Value> Values => values.AsReadOnly();
        private static List<Value> values = new List<Value> {
            { new Value(){ name = "Telescope", id = 0x20 } },
            { new Value(){ name = "Boat's Sail", id = 0x78 } },
            { new Value(){ name = "Wind Waker", id = 0x22} },
            { new Value(){ name = "Grappling Hook", id = 0x25 } },
            { new Value(){ name = "Spoil's Bag", id = 0x24 } },
            { new Value(){ name = "Boomerang", id = 0x2D } },
            { new Value(){ name = "Deku Leaf", id = 0x34 } },
            { new Value(){ name = "Tingle Tuner", id = 0x21 } },
            { new Value(){ name = "Picto-Box", id = 0x23 } },
            { new Value(){ name = "Iron Boots", id = 0x29 } },
            { new Value(){ name = "Magic Armor", id = 0x2A } },
            { new Value(){ name = "Bait Bag", id = 0x2C } },
            { new Value(){ name = "Heroes Bow", id = 0x27 } },
            { new Value(){ name = "Bomb Bag", id = 0x31 } },
            { new Value(){ name = "Empty Bottle", id = 0x50 } },
            { new Value(){ name = "Delivery Bag", id = 0x30 } },
            { new Value(){ name = "Hookshot", id = 0x2F } },
            { new Value(){ name = "Skullhammer", id = 0x33 } },
        };

        public static ReadOnlyCollection<Value> PlayerStatusValues => playerStatusValues.AsReadOnly();
        private static List<Value> playerStatusValues = new List<Value>
        {
            { new Value(){ name = "OpenDoor", id = 0x02 } },
        };

        public static ReadOnlyCollection<Value> CameraValues => cameraValues.AsReadOnly();
        private static List<Value> cameraValues = new List<Value>
        {
            { new Value(){ name = "OpenDoor", id = 0x02 } },
            { new Value(){ name = "StartWindWaker", id = 0x03 } },
        };
    }
}