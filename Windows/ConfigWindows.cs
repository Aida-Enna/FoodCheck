using Dalamud.Bindings.ImGui;
using Dalamud.Interface.Windowing;
using Lumina.Excel.Sheets;
using System;
using System.Linq;
using Veda;

namespace FoodCheck.Windows
{
    public class ConfigWindow : Window, IDisposable
    {
        public ConfigWindow(Plugin plugin) : base("Food Check Config###FC_Config")
        {
            Flags = ImGuiWindowFlags.AlwaysAutoResize;

            SizeCondition = ImGuiCond.Always;
        }

        public void Dispose()
        { }

        private bool ShowSupport;

        public override void Draw()
        {
            if (!Plugin.PluginConfig.PostOnReadyCheck & !Plugin.PluginConfig.PostOnCountdown)
            {
                ImGui.TextColored(new System.Numerics.Vector4(255, 0, 0, 255), "Note: You have both checking methods disabled, the plugin will do nothing.");
                ImGui.Separator();
            }
            ImGui.Checkbox("Notify if someone has food with less than these minutes remaining: ", ref Plugin.PluginConfig.CheckForFoodUnderXMinutes);
            ImGui.SameLine();
            ImGui.SetNextItemWidth(50);
            ImGui.DragInt("###MinutesRemaining", ref Plugin.PluginConfig.MinutesToCheck, 1, 1, 61);
            ImGui.Text("This is the message that will be shown, you can modify it here:");
            ImGui.SetNextItemWidth(500);
            ImGui.InputText("", ref Plugin.PluginConfig.CustomizableMessage, 400);
            ImGui.Text("<names> will be replaced with the name(s) of the people who need to eat food.");
            ImGui.Checkbox("Only use first names         ", ref Plugin.PluginConfig.OnlyUseFirstNames);
            ImGui.SameLine();
            ImGui.Checkbox("Only check in high-end duties", ref Plugin.PluginConfig.OnlyDoHighEndDuties);
            ImGui.Spacing();
            ImGui.Checkbox("Check food (ready check)", ref Plugin.PluginConfig.PostOnReadyCheck);
            ImGui.SameLine();
            ImGui.Checkbox("Check food (countdown)", ref Plugin.PluginConfig.PostOnCountdown);

            if (ImGui.Button("Save and close"))
            {
                Plugin.PluginConfig.Save();
                Toggle();
            }
            ImGui.SameLine();
            ImGui.Indent(300);
            if (ImGui.Button("Want to help support my work?"))
            {
                ShowSupport = !ShowSupport;
            }
            if (ImGui.IsItemHovered()) { ImGui.SetTooltip("Click me!"); }
            if (ShowSupport)
            {
                ImGui.Indent(-300);
                ImGui.Text("Here are the current ways you can support the work I do.\nEvery bit helps, thank you! ♥ Have a great day!");
                ImGui.PushStyleColor(ImGuiCol.Button, new System.Numerics.Vector4(0.19f, 0.52f, 0.27f, 1));
                if (ImGui.Button("Donate via Paypal"))
                {
                    Functions.OpenWebsite("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=QXF8EL4737HWJ");
                }
                ImGui.PopStyleColor();
                ImGui.SameLine();
                ImGui.PushStyleColor(ImGuiCol.Button, new System.Numerics.Vector4(0.95f, 0.39f, 0.32f, 1));
                if (ImGui.Button("Become a Patron"))
                {
                    Functions.OpenWebsite("https://www.patreon.com/bePatron?u=5597973");
                }
                ImGui.PopStyleColor();
                ImGui.SameLine();
                ImGui.PushStyleColor(ImGuiCol.Button, new System.Numerics.Vector4(0.25f, 0.67f, 0.87f, 1));
                if (ImGui.Button("Support me on Ko-Fi"))
                {
                    Functions.OpenWebsite("https://ko-fi.com/Y8Y114PMT");
                }
                ImGui.PopStyleColor();
            }
        }
    }
}