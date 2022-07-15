using Client.Dolphin;
using Common;
using System;
using System.Collections.Generic;

namespace Client
{
    public class Library {
        private static MemManager memManager;
        private static DolphinAccessor dolphinAccessor;
        public static bool IsInitialized => memManager != null;
        public static Game game = new Game() { id = GameId.NOT_FOUND };

        private static byte currentEvent = 0x0;

        public static Action<byte[]> OnInventoryChanged = delegate { };
        public static Action<byte> OnMelodyChanged = delegate { };
        public static Action<byte> OnTriforceChanged = delegate { };
        public static Action<byte> OnGemsChanged = delegate { };
        public static Action<byte> OnHealthChanged = delegate { };
        public static Action<byte> OnPlayerStatusChanged = delegate { };
        public static Action<bool> OnPlayingWindwakerChanged = delegate { };
        public static Action<bool> OnCameraLockedChanged = delegate { };
        public static Action OnWindwakerBeat = delegate { };
        public static Action OnDoorOpen = delegate { };
        public static Action OnTreasureChestOpen = delegate { };

        public static bool Initialize()
        {
            dolphinAccessor = new DolphinAccessor();
            memManager = new MemManager(dolphinAccessor);

            game = memManager.Initialize();
            if (game.id == GameId.NOT_FOUND)
                return false;

            memManager.RamChanged += OnRamChanged;
            return true;
        }

        public static void Stop()
        {
            dolphinAccessor.unhook();
            memManager.RamChanged -= OnRamChanged;
            memManager.Stop();
        }

        private static void OnRamChanged(object source, RAMDataEventArgs e)
        {
            Offset offset = e.offset;
            byte[] buffer = e.buffer;

            switch (e.offset.id)
            {
                case OffsetId.WW_INVENTORY:
                    OnInventoryChanged(buffer);
                    break;

                case OffsetId.WW_MELODIES:
                    OnMelodyChanged(buffer[0]);
                    break;

                case OffsetId.WW_TRIFORCE:
                    OnTriforceChanged(buffer[0]);
                    break;

                case OffsetId.WW_GEMS:
                    OnGemsChanged(buffer[0]);
                    break;

                case OffsetId.WW_SWORDINHAND:
                    break;

                case OffsetId.WW_SCHIELDINHAND:
                    break;

                case OffsetId.WW_CURRENTHEALTH:
                    OnHealthChanged(buffer[0]);
                    break;

                case OffsetId.WW_CURRENTWINDWAKERBEAT:
                    OnWindwakerBeat();
                    break;

                case OffsetId.WW_ACTIVEWINDWAKERNOTES:
                    break;

                case OffsetId.WW_PLAYERSTATUS:
                    OnPlayerStatusChanged(buffer[buffer.Length - 1]);
                    break;

                case OffsetId.WW_PLAYERSTATUS2:
                    OnPlayingWindwakerChanged(buffer[buffer.Length - 1] == 0x01);
                    break;

                case OffsetId.WW_CAMERAEVENT:
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

                case OffsetId.WW_EVENTCONTROL:
                    currentEvent = buffer[offset.length - 1];
                    break;

                default:
                    break;
            }
        }
    }
}
