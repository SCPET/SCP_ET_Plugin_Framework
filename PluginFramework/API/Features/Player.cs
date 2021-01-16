using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using JetBrains.Annotations;
using PluginFramework.Classes;
using PluginFramework.Enums;
using UnityEngine;
using VirtualBrightPlayz.SCP_ET.Items.ItemSystem;
using VirtualBrightPlayz.SCP_ET.NPCs.Interfaces;
using VirtualBrightPlayz.SCP_ET.Player;
using VirtualBrightPlayz.SCP_ET.ServerGroups;
using Object = UnityEngine.Object;

namespace PluginFramework.API.Features
{
    public class Player : MonoBehaviour
    {
        private static List<Player> list = new List<Player>();
        
        private IPlayer player;

        public void Awake()
        {
            player = this.gameObject.GetComponent<IPlayer>();
        }

        public void OnEnable()
        {
            list = Object.FindObjectsOfType<Player>().ToList();
        }
        
        public void OnDisable()
        {
            list = Object.FindObjectsOfType<Player>().ToList();
        }
        
        public void OnDestroy()
        {
            list = Object.FindObjectsOfType<Player>().ToList();
        }

        /// <summary>
        /// Gets all of the players in the server.
        /// </summary>
        public static ReadOnlyCollection<Player> List
        {
            get => list.AsReadOnly();
        }

        /// <summary>
        /// Gets the player's gameobject.
        /// </summary>
        public GameObject GameObject
        {
            get => this.gameObject;
        }

        /// <summary>
        /// Gets or sets the player's dimension.
        /// </summary>
        public DimensionType CurrentDimension
        {
            get => this.player.CurrentDimension;

            set => this.player.CurrentDimension = value;
        }
        
        /// <summary>
        /// Sends a message to the player's chat window.
        /// </summary>
        public void SendPlayerChatMessage(string message)
        {
            this.player.SendPlayerChatMessage(message);
        }

        /// <summary>
        /// Kills the player.
        /// </summary>
        public void KillPlayer()
        {
            this.player.KillPlayer();
        }

        /// <summary>
        /// Kick a player from the server.
        /// </summary>
        /// <param name="message">The message to be sent to the player.</param>
        public void Kick(string message = "You have been kicked from the server!")
        {
            this.PlayerController.Disconnect(message);
        }

        /// <summary>
        /// Gets or sets the player's name.
        /// </summary>
        public string PlayerName
        {
            get => this.PlayerController.NetworkplayerName;
            set => this.PlayerController.NetworkplayerName = value;
        }

        /// <summary>
        /// Gets or sets the player's group.
        /// </summary>
        public string PlayerGroup
        {
            get => this.PlayerController.NetworkplayerGroup;
            set => this.PlayerController.NetworkplayerGroup = value;
        }

        /// <summary>
        /// Gets the player's controller.
        /// </summary>
        public PlayerController PlayerController => this.player.PlayerController;

        /// <summary>
        /// Gets or sets the player's health.
        /// </summary>
        public float Health
        {
            get => this.PlayerController.healthController.NetworkHealth;
            set => this.PlayerController.healthController.NetworkHealth = value;
        }

        /// <summary>
        /// Gets or sets the player's current role.
        /// </summary>
        public Role Role
        {
            get => (Role) this.PlayerController.stats.ClassId;
            set
            {
                if (value > Role.Guard || value < Role.Spectator) return;
                this.PlayerController.stats.ClassId = (int) value;
            }
        }

        /// <summary>
        /// Gets the player's steam id, or null if they don't have one.
        /// </summary>
        [CanBeNull]
        public string SteamID
        {
            get
            {
                var id = this.PlayerController.ConnectionToClient.AuthenticationData.ToString();
                return string.IsNullOrEmpty(id) ? null : id;
            }
        }

        /// <summary>
        /// Gets the player's IP address.
        /// </summary>
        public string IP
        {
            get => this.PlayerController.ConnectionToClient.Address.ToString().Replace("[::ffff:", string.Empty).Replace("]", string.Empty).Split(':')[0];
        }
        
        /// <summary>
        /// Gets the player's transform.
        /// </summary>
        public Transform Transform => this.GameObject.transform;

        /// <summary>
        /// Gets or sets the player's position.
        /// </summary>
        public Vector3 Position
        {
            get => this.Transform.position;
            set => this.Transform.position = value;
        }

        /// <summary>
        /// Gets or sets the player's rotation.
        /// </summary>
        public Quaternion Rotation
        {
            get => this.Transform.rotation;
            set => this.Transform.rotation = value;
        }

        /// <summary>
        /// Gets or sets the player's scale.
        /// </summary>
        public Vector3 Scale
        {
            get => this.Transform.localScale;
            set => this.Transform.localScale = value;
        }

        /// <summary>
        /// Gets or sets if the player is in god mode.
        /// </summary>
        public bool GodMode
        {
            get => this.PlayerController.Network_godMode;
            set => this.PlayerController.Network_godMode = value;
        }

        /// <summary>
        /// Gets or sets the player's walk speed.
        /// </summary>
        public float WalkSpeed
        {
            get => this.PlayerController.NetworkwalkSpeed;
            set => this.PlayerController.NetworkwalkSpeed = value;
        }

        /// <summary>
        /// Gets or sets the player's sprint speed.
        /// </summary>
        public float SprintSpeed
        {
            get => this.PlayerController.NetworksprintSpeed;
            set => this.PlayerController.NetworksprintSpeed = value;
        }

        /// <summary>
        /// Gets or sets the player's crouch speed.
        /// </summary>
        public float CrouchSpeed
        {
            get => this.PlayerController.NetworkcrouchSpeed;
            set => this.PlayerController.NetworkcrouchSpeed = value;
        }

        /// <summary>
        /// Gets or sets the player's jump height.
        /// </summary>
        public float JumpHeight
        {
            get => this.PlayerController.NetworkjumpHeight;
            set => this.PlayerController.NetworkjumpHeight = value;
        }

        /// <summary>
        /// Gets or sets whether or not the player is noclipping.
        /// </summary>
        public bool Noclip
        {
            get => this.PlayerController.NetworkisFly;
            set => this.PlayerController.NetworkisFly = value;
        }
        
        /// <summary>
        /// Gets or sets whether or not the player can be targeted.
        /// </summary>
        public bool NoTarget
        {
            get => this.PlayerController.NetworknoTarget;
            set => this.PlayerController.NetworknoTarget = value;
        }

        /// <summary>
        /// Checks if the player has a specific permission.
        /// </summary>
        /// <param name="node">The permission to check.</param>
        /// <returns>Whether or not the player has the permission.</returns>
        public bool CheckPermision(string node)
        {
            return ServerGroups.CheckPermission(this.PlayerController.ConnectionToClient, node);
        }
        
        public override bool Equals(object obj)
        {
            return obj != null && obj is Player p && p.GameObject.Equals(this.GameObject);
        }

        public static bool operator ==(Player obj1, object obj2)
        {
            return !(obj1 is null) && obj1.Equals(obj2);
        }

        public static bool operator !=(Player obj1, object obj2)
        {
            return !(obj1 == obj2);
        }
    }
}