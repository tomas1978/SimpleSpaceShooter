using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace SimpleSpaceShooter
{
    class Sprite
    {
        Texture2D texture;
        Rectangle rect;

        public Sprite(int xpos, int ypos, Texture2D newTexture)
        {
            texture = newTexture;
            rect = new Rectangle(xpos, ypos, texture.Width, texture.Height);
        }

        public void Move(int x, int y)
        {
            rect.X += x;
            rect.Y += y;
        }

        public Texture2D spriteTexture
        {
            get { return texture; }
        }

        public Rectangle spriteRect
        {
            get { return rect; }
        }
    }
}
