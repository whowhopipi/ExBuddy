﻿namespace ExBuddy.OrderBotTags.Behaviors
{
    using ff14bot.Managers;
    using ff14bot.NeoProfiles;
    using ff14bot.Objects;

    public abstract class ExProfileBehavior : ProfileBehavior
    {
        protected internal static LocalPlayer Me
        {
            get
            {
                return GameObjectManager.LocalPlayer;
            }
        }
    }
}