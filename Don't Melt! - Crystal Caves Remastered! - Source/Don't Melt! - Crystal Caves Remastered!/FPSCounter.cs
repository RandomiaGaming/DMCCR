using EpsilonEngine;
using System.Reflection;
namespace DMCCR
{
    public sealed class FPSCounter : SceneManager
    {
        private Texture Font0;
        private Texture Font1;
        private Texture Font2;
        private Texture Font3;
        private Texture Font4;
        private Texture Font5;
        private Texture Font6;
        private Texture Font7;
        private Texture Font8;
        private Texture Font9;

        private Texture[] Font;

        System.Diagnostics.Stopwatch Stopwatch = new System.Diagnostics.Stopwatch();
        public FPSCounter(Scene scene) : base(scene)
        {
            Font0 = new Texture(Game, Assembly.GetExecutingAssembly().GetManifestResourceStream("DMCCR.Don_t_Melt____Crystal_Caves_Remastered_.Textures.0.png"));
            Font1 = new Texture(Game, Assembly.GetExecutingAssembly().GetManifestResourceStream("DMCCR.Don_t_Melt____Crystal_Caves_Remastered_.Textures.1.png"));
            Font2 = new Texture(Game, Assembly.GetExecutingAssembly().GetManifestResourceStream("DMCCR.Don_t_Melt____Crystal_Caves_Remastered_.Textures.2.png"));
            Font3 = new Texture(Game, Assembly.GetExecutingAssembly().GetManifestResourceStream("DMCCR.Don_t_Melt____Crystal_Caves_Remastered_.Textures.3.png"));
            Font4 = new Texture(Game, Assembly.GetExecutingAssembly().GetManifestResourceStream("DMCCR.Don_t_Melt____Crystal_Caves_Remastered_.Textures.4.png"));
            Font5 = new Texture(Game, Assembly.GetExecutingAssembly().GetManifestResourceStream("DMCCR.Don_t_Melt____Crystal_Caves_Remastered_.Textures.5.png"));
            Font6 = new Texture(Game, Assembly.GetExecutingAssembly().GetManifestResourceStream("DMCCR.Don_t_Melt____Crystal_Caves_Remastered_.Textures.6.png"));
            Font7 = new Texture(Game, Assembly.GetExecutingAssembly().GetManifestResourceStream("DMCCR.Don_t_Melt____Crystal_Caves_Remastered_.Textures.7.png"));
            Font8 = new Texture(Game, Assembly.GetExecutingAssembly().GetManifestResourceStream("DMCCR.Don_t_Melt____Crystal_Caves_Remastered_.Textures.8.png"));
            Font9 = new Texture(Game, Assembly.GetExecutingAssembly().GetManifestResourceStream("DMCCR.Don_t_Melt____Crystal_Caves_Remastered_.Textures.9.png"));

            Stopwatch.Start();

            Font = new Texture[]
            {
                Font0,
                Font1,
                Font2,
                Font3,
                Font4,
                Font5,
                Font6,
                Font7,
                Font8,
                Font9
            };
        }
        protected override void Render()
        {
            int currentFPSInt = (int)Game.CurrentFPS;

            string currentFPSString = currentFPSInt.ToString();

            char[] currentFPSChars = currentFPSString.ToCharArray();

            int currentFPSCharsLength = currentFPSChars.Length;

            int offset = 0;
            for (int i = 0; i < currentFPSCharsLength; i++)
            {
                char c = currentFPSChars[i];

                if (c == '0')
                {
                    Scene.DrawTextureScreenSpaceUnsafe(Font0._XNABase, new Microsoft.Xna.Framework.Vector2(offset, 0), Microsoft.Xna.Framework.Color.White);
                }
                else if (c == '1')
                {
                    Scene.DrawTextureScreenSpaceUnsafe(Font0._XNABase, new Microsoft.Xna.Framework.Vector2(offset, 0), Microsoft.Xna.Framework.Color.White);
                }
                if (c == '2')
                {
                    Scene.DrawTextureScreenSpaceUnsafe(Font2._XNABase, new Microsoft.Xna.Framework.Vector2(offset, 0), Microsoft.Xna.Framework.Color.White);
                }
                else if (c == '3')
                {
                    Scene.DrawTextureScreenSpaceUnsafe(Font3._XNABase, new Microsoft.Xna.Framework.Vector2(offset, 0), Microsoft.Xna.Framework.Color.White);
                }
                else if (c == '4')
                {
                    Scene.DrawTextureScreenSpaceUnsafe(Font4._XNABase, new Microsoft.Xna.Framework.Vector2(offset, 0), Microsoft.Xna.Framework.Color.White);
                }
                else if (c == '5')
                {
                    Scene.DrawTextureScreenSpaceUnsafe(Font5._XNABase, new Microsoft.Xna.Framework.Vector2(offset, 0), Microsoft.Xna.Framework.Color.White);
                }
                else if (c == '6')
                {
                    Scene.DrawTextureScreenSpaceUnsafe(Font6._XNABase, new Microsoft.Xna.Framework.Vector2(offset, 0), Microsoft.Xna.Framework.Color.White);
                }
                else if (c == '7')
                {
                    Scene.DrawTextureScreenSpaceUnsafe(Font7._XNABase, new Microsoft.Xna.Framework.Vector2(offset, 0), Microsoft.Xna.Framework.Color.White);
                }
                else if (c == '8')
                {
                    Scene.DrawTextureScreenSpaceUnsafe(Font8._XNABase, new Microsoft.Xna.Framework.Vector2(offset, 0), Microsoft.Xna.Framework.Color.White);
                }
                else
                {
                    Scene.DrawTextureScreenSpaceUnsafe(Font9._XNABase, new Microsoft.Xna.Framework.Vector2(offset, 0), Microsoft.Xna.Framework.Color.White);
                }

                offset += 11;
            }
        }
    }
}
