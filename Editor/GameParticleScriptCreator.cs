using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UnityExt
{
    public static class GameParticleScriptCreator
    {
        [MenuItem(itemName: "Assets/Create/Kaiyum/UnityExt/Create New Game Particle Play Script", isValidateFunction: false, priority: 51)]
        static void CreateScriptFromTemplate()
        {
            KEditorUtil.ProjectResourceUtil.CreateScriptFromTemplate("NewParticlePlayScript", "UnityExt", "Editor", "tmpl", "game_particle_play_scr");
        }
    }
}