using System;
using System.Collections.Generic;
using ImGuiNET;
using LDGJ45.Core.GameSystems;
using LDGJ45.Core.World;
using Microsoft.Xna.Framework;
using Vector2 = System.Numerics.Vector2;

namespace LDGJ45.Editor.UI
{
    public sealed class EditorApp
    {
        private readonly ImGuiRenderer _imGuiRenderer;
        private readonly IReadOnlyList<IWindow> _windows;
        private readonly GameClock _gameClock;
        private readonly WorldSystem _worldSystem;

        public EditorApp(
            ImGuiRenderer imGuiRenderer,
            GameWindow gameWindow,
            IReadOnlyList<IWindow> windows,
            GameClock gameClock,
            WorldSystem worldSystem
        )
        {
            _imGuiRenderer = imGuiRenderer;
            _windows = windows;
            _gameClock = gameClock;
            _worldSystem = worldSystem;

            _imGuiRenderer.RebuildFontAtlas();
            var imGuiIo = ImGui.GetIO();
            imGuiIo.ConfigWindowsMoveFromTitleBarOnly = true;
            imGuiIo.ConfigFlags |= ImGuiConfigFlags.DockingEnable;

            gameWindow.AllowUserResizing = true;

            _worldSystem.CreateNewWorld();
        }

        public void Render()
        {
            _imGuiRenderer.BeforeLayout(_gameClock.GameTime);
            RenderDock();
            for (var i = 0; i < _windows.Count; i++)
                _windows[i].Render();

            _imGuiRenderer.AfterLayout();
        }

        private void RenderDock()
        {
            var mainViewport = ImGui.GetMainViewport();
            ImGui.SetNextWindowSize(mainViewport.Size);
            ImGui.SetNextWindowPos(mainViewport.Pos);
            ImGui.PushStyleVar(ImGuiStyleVar.WindowBorderSize, 0);
            ImGui.PushStyleVar(ImGuiStyleVar.WindowRounding, 0);
            ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, Vector2.Zero);
            ImGui.Begin(
                "MainWindow",
                ImGuiWindowFlags.NoDocking | ImGuiWindowFlags.NoMove | ImGuiWindowFlags.NoTitleBar |
                ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoBringToFrontOnFocus | ImGuiWindowFlags.NoNavFocus
                | ImGuiWindowFlags.NoBackground | ImGuiWindowFlags.MenuBar
            );
            ImGui.PopStyleVar(3);
            ImGui.DockSpace(ImGui.GetID("MainDockSpace"), Vector2.Zero, ImGuiDockNodeFlags.PassthruCentralNode);

            RenderMainMenuBar();

            ImGui.End();
        }

        private void RenderMainMenuBar()
        {
            if (ImGui.BeginMenuBar())
            {
                RenderGameObjectsMenu();
                ImGui.EndMenuBar();
            }
        }

        private void RenderGameObjectsMenu()
        {
            if (ImGui.BeginMenu("Game objects"))
            {
                foreach (var gameObjectType in Enum.GetValues(typeof(GameObjectType)))
                {
                    if (ImGui.Selectable(gameObjectType.ToString()))
                    {
                        _worldSystem.CreateGameObject((GameObjectType) gameObjectType);
                    }
                }

                ImGui.EndMenu();
            }
        }
    }
}