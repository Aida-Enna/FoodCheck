using Dalamud.Configuration;
using Dalamud.Game.Text;
using Dalamud.Plugin;
using Newtonsoft.Json;
using System;

namespace FoodCheck
{
    public class Configuration : IPluginConfiguration
    {
        public int Version { get; set; }
        public bool PostOnReadyCheck = true;
        public bool PostOnCountdown = true;
        public bool OnlyDoHighEndDuties = true;
        public bool OnlyUseFirstNames = true;
        public string CustomizableMessage = "<names> need to EAT FOOD! <se.7>";
        public bool CheckForFoodUnderXMinutes = false;
        public int MinutesToCheck = 10;

        private IDalamudPluginInterface pluginInterface;

        public void Initialize(IDalamudPluginInterface pluginInterface)
        {
            this.pluginInterface = pluginInterface;
        }

        public void Save()
        {
            this.pluginInterface.SavePluginConfig(this);
        }
    }
}
