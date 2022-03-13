﻿using EpsilonEngine;
using System.Reflection;
namespace DMCCR
{
    public sealed class GoalGate : PhysicsObject
    {
        private Stage _stagePlayer;

        public GoalGate(Stage stagePlayer, PhysicsLayer physicsLayer) : base(stagePlayer, false)
        {
            _stagePlayer = stagePlayer;

            Texture goalGateTexture = new Texture(Game, Assembly.GetExecutingAssembly().GetManifestResourceStream("DMCCR.Don_t_Melt____Crystal_Caves_Remastered_.Textures.GoalGate.png"));

            TextureRenderer textureRenderer = new TextureRenderer(this, 1);

            textureRenderer.Texture = goalGateTexture;

            SetColliderShape(new Rectangle[1] { new Rectangle(0, 0, 15, 31) });

            CollisionPhysicsLayers = physicsLayer;

            LogOverlaps = true;
        }
        public override string ToString()
        {
            return $"DMCCR.GoalGate()";
        }
    }
}