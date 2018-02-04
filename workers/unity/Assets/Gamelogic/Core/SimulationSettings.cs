using UnityEngine;

namespace Assets.Gamelogic.Core
{
    public static class SimulationSettings
    {
        public static readonly string PlayerPrefabName = "Player";
        public static readonly string PlayerCreatorPrefabName = "PlayerCreator";
        public static readonly string CubePrefabName = "Cube";

        public static readonly float HeartbeatCheckIntervalSecs = 3;
        public static readonly uint TotalHeartbeatsBeforeTimeout = 3;
        public static readonly float HeartbeatSendingIntervalSecs = 3;

        public static readonly int TargetClientFramerate = 60;
        public static readonly int TargetServerFramerate = 60;
        public static readonly int FixedFramerate = 20;

        public static readonly float PlayerCreatorQueryRetrySecs = 4;
        public static readonly float PlayerEntityCreationRetrySecs = 4;

        public static readonly int PlayerSpawnHealth = 100;
        public static readonly float PlayerSpawnHeight = 200;
        public static readonly float PlayerAcceleration = 10;
        public static readonly float PlayerJumpPower = 5;
        public static readonly float PlayerSwordPower = 0.7F;
        public static readonly int PlayerSwordDamage = 25;

        public static readonly float PlayerKillSizeAward = 1.2F;

        public static readonly int PlayerHitPointAward = 25;
        public static readonly int PlayerKillPointAward = 100;
        public static readonly int PlayerKillHealthAward = 100;

        public static readonly string DefaultSnapshotPath = Application.dataPath + "/../../../snapshots/default.snapshot";
    }
}
