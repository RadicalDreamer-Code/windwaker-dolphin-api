using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Common {
    public class Data {
        public static ReadOnlyCollection<Offset> Offsets => offsets.AsReadOnly();
        private static List<Offset> offsets = new List<Offset>{
            { new Offset(){ id = OffsetId.INVENTORY, address = 0x803C4C44, length = 0x15 } },
            { new Offset(){ id = OffsetId.MELODIES, address = 0x803C4CC5, length = 0x01 } },
            { new Offset(){ id = OffsetId.TRIFORCE, address = 0x803C4CC6, length = 0x01 } },
            { new Offset(){ id = OffsetId.GEMS, address = 0x803C4CC7, length = 0x01 } },
            { new Offset(){ id = OffsetId.SWORDINHAND, address = 0x803C4C16, length = 0x01 } },
            { new Offset(){ id = OffsetId.SCHIELDINHAND, address = 0x803C4C17, length = 0x01 } },
            { new Offset(){ id = OffsetId.CURRENTHEALTH, address = 0x803C4C0B, length = 0x01 } },
            { new Offset(){ id = OffsetId.CURRENTWINDWAKERBEAT, address = 0x803A2CAE, length = 0x01 } },
            { new Offset(){ id = OffsetId.ACTIVEWINDWAKERNOTES, address = 0x803A2CAF, length = 0x01 } },
            { new Offset(){ id = OffsetId.PLAYERSTATUS, address = 0x803CA8D0, length = 0x04 } },
            { new Offset(){ id = OffsetId.PLAYERSTATUS2, address = 0x803CA8D4, length = 0x04 } },
            { new Offset(){ id = OffsetId.CAMERAEVENT, address = 0x803C9EA2, length = 0x01 } },
            { new Offset(){ id = OffsetId.EVENTCONTROL, address = 0x803C9DE1, length = 0x01 } },
        };

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