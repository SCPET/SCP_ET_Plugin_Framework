using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using PluginFramework.Classes;
using PluginFramework.Enums;
using UnityEngine;
using VirtualBrightPlayz.SCP_ET.NPCs;
using VirtualBrightPlayz.SCP_ET.NPCs.Interfaces;
using VirtualBrightPlayz.SCP_ET.NPCs.SCP;
using VirtualBrightPlayz.SCP_ET.Player.Classes;
using VirtualBrightPlayz.SCP_ET.World.SCP_106;

namespace PluginFramework.API.Features
{
    public class Entity : MonoBehaviour
    {
        private static Dictionary<Type, Role> roles = new Dictionary<Type, Role>()
        {
            {typeof(SCP008), Role.Scp008},
            {typeof(SCP049), Role.Scp049},
            {typeof(SCP096), Role.Scp096},
            {typeof(SCP106), Role.Scp106},
            {typeof(SCP1499), Role.Scp1499},
            {typeof(SCP173), Role.Scp173},
            {typeof(SCP939), Role.Scp939},
            {typeof(SCP966), Role.Scp966},
            {typeof(NPCClassD), Role.ClassD},
            {typeof(ClassD), Role.ClassD},
            {typeof(Spectator), Role.Spectator},
            {typeof(Guard), Role.Guard}
        };
        
        private IEntity entity;
        private Player player;

        public void Awake()
        {
            entity = this.gameObject.GetComponent<IEntity>();
            gameObject.TryGetComponent<Player>(out var player);
        }

        /// <summary>
        /// Get the role of the entity.
        /// </summary>
        public Role Role
        {
            get
            {
                if (entity.gameObject.TryGetComponent<PocketDim>(out var _))
                {
                    return Role.Scp106;
                }
                
                foreach (var component in entity.gameObject.GetComponents<IEntity>())
                {
                    if (component is IPlayer p)
                    {
                        return (Role) p.PlayerController.stats.ClassId;
                    }

                    if (roles.TryGetValue(component.GetType(), out var role))
                    {
                        return role;
                    }
                }
                
                return Role.None;
            }
        }

        /// <summary>
        /// Gets the entity's gameobject.
        /// </summary>
        public GameObject GameObject => this.entity.gameObject;

        /// <summary>
        /// Create a player from the entity. If the entity is not a player, this will return null.
        /// </summary>
        [CanBeNull]
        public Player Player => player;
        
        /// <summary>
        /// Gets or sets the entity's dimension.
        /// </summary>
        public DimensionType CurrentDimension
        {
            get => this.entity.CurrentDimension;

            set => this.entity.CurrentDimension = value;
        }
        
        /// <summary>
        /// Gets the entity's transform.
        /// </summary>
        public Transform Transform => this.GameObject.transform;

        /// <summary>
        /// Gets or sets the entity's position.
        /// </summary>
        public Vector3 Position
        {
            get => this.Transform.position;
            set => this.Transform.position = value;
        }

        /// <summary>
        /// Gets or sets the entity's rotation.
        /// </summary>
        public Quaternion Rotation
        {
            get => this.Transform.rotation;
            set => this.Transform.rotation = value;
        }

        /// <summary>
        /// Gets or sets the entity's scale.
        /// </summary>
        public Vector3 Scale
        {
            get => this.Transform.localScale;
            set => this.Transform.localScale = value;
        }
    }
}