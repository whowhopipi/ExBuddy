﻿namespace ExBuddy.OrderBotTags.Behaviors
{
    using Buddy.Coroutines;
    using Clio.XmlEngine;
	using ExBuddy.Attributes;
	using ExBuddy.Helpers;
	using ExBuddy.Interfaces;
	using ExBuddy.Logging;
	using ff14bot.Managers;
	using ff14bot.NeoProfiles;
	using ff14bot.Objects;
	using System.Threading.Tasks;
	using System.Windows.Media;
	using TreeSharp;

	public abstract class ExProfileBehavior : ProfileBehavior, ILogColors
	{
		protected internal readonly Logger Logger;

		// ReSharper disable once InconsistentNaming
		protected bool isDone;

		private string statusText;

		static ExProfileBehavior()
		{
			ReflectionHelper.CustomAttributes<LoggerNameAttribute>.RegisterByAssembly();

			// Until we find a better way to do it.
			Condition.AddNamespacesToScriptManager("ExBuddy", "ExBuddy.Helpers");
		}

		protected ExProfileBehavior()
		{
			Logger = new Logger(this, includeVersion: true);
		}

		public override sealed bool IsDone
		{
			get { return isDone; }
		}

		[XmlAttribute("Name")]
		public string Name { get; set; }

        [XmlAttribute("SpellDelay")]
        public int SpellDelay { get; set; }

        public override sealed string StatusText
		{
			get { return string.Concat(GetType().Name, ": ", statusText); }

			set { statusText = value; }
		}

		protected internal static LocalPlayer Me
		{
			get { return GameObjectManager.LocalPlayer; }
		}

		protected virtual Color Error
		{
			get { return Logger.Colors.Error; }
		}

		protected virtual Color Info
		{
			get { return Logger.Colors.Info; }
	    }

	    protected virtual Color Warn
	    {
	        get { return Logger.Colors.Warn; }
	    }

	    protected virtual Color Mew
	    {
	        get { return Logger.Colors.Mew; }
	    }

        public override string ToString()
		{
			return this.DynamicToString("StatusText", "Behavior");
		}

        #region Main logic
        protected override Composite CreateBehavior()
        {
            return new ExCoroutineAction(ctx => TheMain(), this);
        }

        protected abstract Task<bool> Main();

        protected async Task<bool> TheMain()
        {
            bool flag = await Main();

            if (flag)
            {
                await DoMainSuccess();
            }
            else
            {
                await DoMainFailed();
            }
            return flag;
        }

        protected virtual async Task<bool> DoMainSuccess()
        {
            await Coroutine.Sleep(200);
            return true;
        }

        protected virtual async Task<bool> DoMainFailed()
        {
            await Coroutine.Sleep(200);
            return true;
        }
        #endregion

        protected virtual void DoReset()
		{
		}
        
		protected override sealed void OnResetCachedDone()
		{
			DoReset();
			isDone = false;
		}

		#region ILogColors Members

		Color ILogColors.Error
		{
			get { return Error; }
		}

		Color ILogColors.Info
		{
			get { return Info; }
	    }

	    Color ILogColors.Warn
	    {
	        get { return Warn; }
	    }

	    Color ILogColors.Mew
	    {
	        get { return Mew; }
	    }

        #endregion ILogColors Members
    }
}