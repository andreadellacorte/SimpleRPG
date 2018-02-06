using Assets.Gamelogic.Core;
using Improbable;
using Improbable.Core;
using Improbable.Player;
using Improbable.Notes;
using Improbable.Projectiles;
using Improbable.Unity.Core.Acls;
using Improbable.Worker;
using Quaternion = UnityEngine.Quaternion;
using UnityEngine;
using Improbable.Unity.Entity;

namespace Assets.Gamelogic.EntityTemplates
{
    public class EntityTemplateFactory : MonoBehaviour
    {
        public static Entity CreatePlayerCreatorTemplate()
        {
            var playerCreatorEntityTemplate = EntityBuilder.Begin()
                .AddPositionComponent(Improbable.Coordinates.ZERO.ToUnityVector(), CommonRequirementSets.PhysicsOnly)
                .AddMetadataComponent(entityType: SimulationSettings.PlayerCreatorPrefabName)
                .SetPersistence(true)
                .SetReadAcl(CommonRequirementSets.PhysicsOrVisual)
                .AddComponent(new Rotation.Data(Quaternion.identity.ToNativeQuaternion()), CommonRequirementSets.PhysicsOnly)
                .AddComponent(new PlayerCreation.Data(), CommonRequirementSets.PhysicsOnly)
                .Build();

            return playerCreatorEntityTemplate;
        }

        public static Entity CreatePlayerTemplate(string clientId)
        {
            var playerTemplate = EntityBuilder.Begin()
                .AddPositionComponent(new Improbable.Coordinates(0, SimulationSettings.PlayerSpawnHeight, 0).ToUnityVector(), CommonRequirementSets.PhysicsOnly)
                .AddMetadataComponent(entityType: SimulationSettings.PlayerPrefabName)
                .SetPersistence(false)
                .SetReadAcl(CommonRequirementSets.PhysicsOrVisual)
                .AddComponent(new Rotation.Data(SimulationSettings.PlayerRotation.ToNativeQuaternion()), CommonRequirementSets.PhysicsOnly)
                .AddComponent(new ClientAuthorityCheck.Data(), CommonRequirementSets.SpecificClientOnly(clientId))
                .AddComponent(new ClientConnection.Data(SimulationSettings.TotalHeartbeatsBeforeTimeout), CommonRequirementSets.PhysicsOnly)
                .AddComponent(new PlayerInput.Data(new Joystick(xAxis: 0, yAxis: 0), false, false), CommonRequirementSets.SpecificClientOnly(clientId))
                .AddComponent(new Health.Data(SimulationSettings.PlayerSpawnHealth), CommonRequirementSets.PhysicsOnly)
                .AddComponent(new Score.Data(0), CommonRequirementSets.PhysicsOnly)
                .AddComponent(new Size.Data(1.0F), CommonRequirementSets.PhysicsOnly)
                .AddComponent(new NoticeCreator.Data(), CommonRequirementSets.SpecificClientOnly(clientId))
                .AddComponent(new ArrowCreator.Data(), CommonRequirementSets.SpecificClientOnly(clientId))
                .Build();

            return playerTemplate;
        }

        public static Entity CreateNoticeTemplate(string text, Coordinates coordinates)
        {
            var noticeTemplate = EntityBuilder.Begin()
                .AddPositionComponent(coordinates.ToUnityVector(), CommonRequirementSets.PhysicsOnly)
                .AddMetadataComponent(entityType: SimulationSettings.NoticePrefabName)
                .SetPersistence(true)
                .SetReadAcl(CommonRequirementSets.PhysicsOrVisual)
                .AddComponent(new Rotation.Data(Quaternion.identity.ToNativeQuaternion()), CommonRequirementSets.PhysicsOnly)
                .AddComponent(new Notice.Data(text), CommonRequirementSets.PhysicsOnly)
                .Build();

            return noticeTemplate;
        }

        public static Entity CreateArrowTemplate(EntityId owner, Coordinates coordinates, Coordinates angle)
        {
            var arrowTemplate = EntityBuilder.Begin()
                .AddPositionComponent(coordinates.ToUnityVector(), CommonRequirementSets.PhysicsOnly)
                .AddMetadataComponent(entityType: SimulationSettings.ArrowPrefabName)
                .SetPersistence(true)
                .SetReadAcl(CommonRequirementSets.PhysicsOrVisual)
                //.AddComponent(new Rotation.Data(SimulationSettings.PlayerRotation.ToNativeQuaternion()),
                //  CommonRequirementSets.PhysicsOnly)
                .AddComponent(new Rotation.Data((Quaternion.Euler(angle.ToUnityVector())).ToNativeQuaternion()),
                    CommonRequirementSets.PhysicsOnly)
                .AddComponent(new Owner.Data(owner), CommonRequirementSets.PhysicsOnly)
                .Build();

            return arrowTemplate;
        }
    }
}
