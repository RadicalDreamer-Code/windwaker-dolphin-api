using Client.Dolphin;
using Common;
using System;
using System.Collections.Generic;

namespace Client
{
    class Library {
        private static MemManager memManager;
        public static bool IsInitialized => memManager != null;

        private static byte currentEvent = 0x0;

        public static Action<byte[]> OnInventoryChanged = delegate { };
        public static Action<byte> OnMelodyChanged = delegate { };
        public static Action<byte> OnTriforceChanged = delegate { };
        public static Action<byte> OnGemsChanged = delegate { };
        public static Action<byte> OnHealthChanged = delegate { };
        public static Action<byte> OnPlayerStatusChanged = delegate { };
        public static Action<bool> OnPlayingWindwakerChanged = delegate { };
        public static Action OnWindwakerBeat = delegate { };
        public static Action<bool> OnCameraLockedChanged = delegate { };
        public static Action OnDoorOpen = delegate { };
        public static Action OnTreasureChestOpen = delegate { };

        public static void Initialize()
        {
            DolphinAccessor dolphinAccessor = new DolphinAccessor();
            memManager = new MemManager(dolphinAccessor);
            memManager.Initialize();

            memManager.RamChanged += OnRamChanged;
        }

        private static void OnRamChanged(object source, RAMDataEventArgs e)
        {
            Offset offset = e.offset;
            byte[] buffer = e.buffer;

            switch (e.offset.id)
            {
                case OffsetId.INVENTORY:
                    OnInventoryChanged(buffer);
                    break;

                case OffsetId.MELODIES:
                    OnMelodyChanged(buffer[0]);
                    break;

                case OffsetId.TRIFORCE:
                    OnTriforceChanged(buffer[0]);
                    break;

                case OffsetId.GEMS:
                    OnGemsChanged(buffer[0]);
                    break;

                case OffsetId.SWORDINHAND:
                    break;

                case OffsetId.SCHIELDINHAND:
                    break;

                case OffsetId.CURRENTHEALTH:
                    OnHealthChanged(buffer[0]);
                    break;

                case OffsetId.CURRENTWINDWAKERBEAT:
                    OnWindwakerBeat();
                    break;

                case OffsetId.ACTIVEWINDWAKERNOTES:
                    break;

                case OffsetId.PLAYERSTATUS:
                    OnPlayerStatusChanged(buffer[buffer.Length - 1]);
                    break;

                case OffsetId.PLAYERSTATUS2:
                    OnPlayingWindwakerChanged(buffer[buffer.Length - 1] == 0x01);
                    break;

                case OffsetId.CAMERAEVENT:
                    byte value = buffer[offset.length - 1];

                    if (value == 0x00)
                    {
                        OnCameraLockedChanged(false);
                    }
                    else
                    {
                        OnCameraLockedChanged(true);
                        if (currentEvent == 0x01)
                        {
                            OnDoorOpen();
                            break;
                        }

                        if (currentEvent == 0x0A)
                        {
                            OnTreasureChestOpen();
                            break;
                        }

                    }
                    break;

                case OffsetId.EVENTCONTROL:
                    currentEvent = buffer[offset.length - 1];
                    break;

                default:
                    break;
            }
        }
    }
}
