using SCP_ET.API.Enums;
using SCP_ET.API.Interfaces;
using System;
using System.Collections.Generic;

namespace SCP_ET.API.Classes
{
    public abstract class Player : IEntity
    {
        public static List<Player> Players { get; set; } = new List<Player>();
        public abstract Vector Position { get; set; }
        public abstract object GameObject { get; }
        public abstract int ClassID { get; set; }
        public abstract object PlayerController { get; }
        public abstract object Effects { get; }
        public abstract object Connection { get; }
        public abstract object Inventory { get; }
        public abstract object PlayerMain { get; }
        public abstract object NetworkIdentity { get; }
        public abstract string PlayerName { get; set; }
        public abstract bool IsSpawned { get; set; }
        public abstract ulong SteamID { get; }
        public abstract string ModelTexture { get; set; }
        public abstract bool GodMode { get; set; }
        public abstract bool NoTarget { get; set; }
        public abstract bool NoClip { get; set; }
        public abstract bool IsLocalPlayer { get;}
        public abstract bool HasAuthority { get; }
        public abstract bool IsServer { get; }
        public abstract bool IsServerMuted { get; set; }
        public abstract bool InfinityRun { get; set; }
        public abstract bool InfinityAmmo { get; set; }
        public abstract bool NoBlink { get; set; }
        public abstract bool IsBlinking { get; set; }
        public abstract bool IsBlinkingDisabled { get; set; }
        public abstract bool ManualBlinking { get; set; }
        public abstract bool HasRing { get; set; }
        public abstract bool InputAllowed { get; set; }
        public abstract bool ScrambleSentMessages { get; set; }
        public abstract int ReserveAmmo { get; set; }
        public abstract int CurrentItemAmmo { get; set; }
        public abstract float IsMakingSound { get; }
        public abstract int CurrentItem { get; set; }
        public abstract string CurrentItemID { get; }
        public abstract object CurrentItemIcon { get; }
        public abstract int CurrentWornItem { get; set; }
        public abstract string CurrentWornItemID { get; }
        public abstract object CurrentWornIcon { get; }
        public abstract object CurrentWorn { get; }
        public abstract float Blink { get; set; }
        public abstract float MaxBlink { get; set; }
        public abstract float BlinkDuration { get; set; }
        public abstract float Health { get; set; }
        public abstract float MaxHealth { get; set; }
        public abstract object Weapon { get; }
        public abstract string CurrentZone { get; set; }
        public abstract object ClassMeshPrefab { get; set; }
        public abstract DimensionType CurrentDimension { get; set; }
        public abstract void TakeDamage(float dmgAmount, IEntity damager, DeathTypes deathType);
        public abstract void ClearDropInventory();
        public abstract void ClearItems();
        public abstract void ClientChangeClass(int oldClass, int newClass);
        public abstract bool CheckPermission(string permissionName);
        public abstract void SendTextChatMessage(string message, string color);
        public abstract void SendTranslatedTextChatMessage(string message, string color, int textType);
        public abstract void SendTranslatedTextChatMessageArgs(string message, string color, int textType, string argString);
        public abstract void KillPlayer();
        public abstract void KillPlayer(DeathTypes deathType, IEntity killer);
        public abstract void ShowStatus(string statusName);
        public abstract void DenyAccessSound(int id);
        public abstract void NewMission(string text);
        public abstract void HealPlayer(float healAmount, float healMax);
        public abstract void ClearEffects();
        public abstract void RemoveEffect(string effect);
        public abstract void AwardAchievement(string achievement);
        public abstract void Broadcast(string message, float duration = 5f);
        public abstract void PlaceDecal(DecalType decal, float size, bool sound = true);
        public abstract void BanPlayer(string reason, TimeSpan duration);
        public abstract void Disconnect(string reason, bool isTranslated = false, TextType translationType = TextType.Common);
    }
}
