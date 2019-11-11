﻿using Buddy.Coroutines;
using ff14bot;
using ff14bot.AClasses;
using ff14bot.Behavior;
using ff14bot.Helpers;
using ff14bot.Managers;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media;
using TreeSharp;

namespace ManualPlugin
{
    public class Manual : BotPlugin
    {
        private Composite _coroutine;
        private ManualSettings _settingsForm;
        private static uint _buff = 48;

        public override string Author
        {
            get { return "fix by athlon"; }
        }

#if RB_CN
        public override string Name => "你今天吃指南没";
#else
        public override string Name => "Manual";
#endif

        public override Version Version
        {
            get { return new Version(1, 1, 0); }
        }

        private static async Task<bool> EatManual()
        {
            if (Settings.Instance.Id == 0 || !InventoryManager.FilledSlots.ContainsManualitem(Settings.Instance.Id))
            {
                Logging.Write(Colors.Aquamarine, "[Manual] No Manual selected, check your settings");
                return false;
            }

            if (GatheringManager.WindowOpen)
            {
                Logging.Write(Colors.Aquamarine, "[Manual] Waiting for gathering window to close");
                return false;
            }

            var item = InventoryManager.FilledSlots.GetManualItem(Settings.Instance.Id);

            if (item == null) return false;

            if (item.EnglishName.Contains("Engineering"))
            {
                _buff = 45;
            }
            else if (item.EnglishName.Contains("Survival"))
            {
                _buff = 46;
            }
            else
            {
                return false;
            }

            if(Core.Player.HasAura(_buff)) return false;

            if (FishingManager.State != FishingState.None)
            {
                Logging.Write(Colors.Aquamarine, "[Manual] Stop fishing");
                ActionManager.DoAction("Quit", Core.Me);
                await Coroutine.Wait(5000, () => FishingManager.State == FishingState.None);
            }

            if (Core.Me.IsMounted)
            {
                Logging.Write(Colors.Aquamarine, "[Manual] Dismounting to eat");
                await CommonTasks.StopAndDismount();
            }

            Logging.Write(Colors.Aquamarine, "[Manual] (" + _buff + ") Eating " + item.Name);
            item.UseItem();
            await Coroutine.Wait(5000, () => Core.Player.HasAura(_buff));

            return true;
        }

        public override void OnInitialize()
        {
            _coroutine = new Decorator(c => !Core.Player.HasAura(45) || !Core.Player.HasAura(46), new ActionRunCoroutine(r => EatManual()));
        }

        public override void OnEnabled()
        {
            TreeRoot.OnStart += OnBotStart;
            TreeRoot.OnStop += OnBotStop;
            TreeHooks.Instance.OnHooksCleared += OnHooksCleared;

            if (TreeRoot.IsRunning)
            {
                AddHooks();
            }
        }

        public override void OnDisabled()
        {
            TreeRoot.OnStart -= OnBotStart;
            TreeRoot.OnStop -= OnBotStop;
            RemoveHooks();

        }

        public override void OnShutdown()
        {
            TreeRoot.OnStart -= OnBotStart;
            TreeRoot.OnStop -= OnBotStop;
            RemoveHooks();
        }

        public override bool WantButton
        {
            get { return true; }
        }

        public override void OnButtonPress()
        {
            if (_settingsForm == null || _settingsForm.IsDisposed || _settingsForm.Disposing)
            {
                _settingsForm = new ManualSettings();
            }

            _settingsForm.ShowDialog();
        }

        private void AddHooks()
        {
            Logging.Write(Colors.Aquamarine, "Adding Manual Hook");
            TreeHooks.Instance.AddHook("TreeStart", _coroutine);
        }

        private void RemoveHooks()
        {
            Logging.Write(Colors.Aquamarine, "Removing Manual Hook");
            TreeHooks.Instance.RemoveHook("TreeStart", _coroutine);
        }

        private void OnBotStop(BotBase bot)
        {
            RemoveHooks();
        }

        private void OnBotStart(BotBase bot)
        {
            AddHooks();
        }

        private void OnHooksCleared(object sender, EventArgs e)
        {
            RemoveHooks();
        }
    }

    public class Settings : JsonSettings
    {
        private static Settings _instance;

        public static Settings Instance
        {
            get { return _instance ?? (_instance = new Settings()); ; }
        }

        public Settings()
            : base(Path.Combine(CharacterSettingsDirectory, "Manual.json"))
        {
        }

        [Setting]
        public uint Id { get; set; }
    }
}
