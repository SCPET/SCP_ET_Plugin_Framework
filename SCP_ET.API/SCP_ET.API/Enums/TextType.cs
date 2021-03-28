namespace SCP_ET.API.Enums
{
    public enum TextType : byte
    {
        Common, // SCP Names and stuff used everywhere
        Menu, // Stuff used in menus
        Item, // items
        NPC, // npcs (not scp names, only unique npcs)
        Effects, // effects
        Commands, // admin/chat command responses
        Hints, // Loading Screen Hints
        Misc, // Doesn't fit the rest
        Mod, // For Mod Developers to put what they want
    }
}