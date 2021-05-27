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
        int energy;
        int direction;
        int lastShotTime;
        int fireRate;

        public Sprite(int newXpos, int newYpos, int newEnergy, int newDirection, Texture2D newTexture)
        {
            texture = newTexture;
            rect = new Rectangle(newXpos, newYpos, texture.Width, texture.Height);
            energy = newEnergy;
            direction = newDirection;
        }

        public void Move(int x, int y)
        {
            rect.X += x;
            rect.Y += y;
        }

        public bool ReadyToFire()
        {
            return false;
        }

        public void Move2(Vector2 mVector)
        {
            rect.Location = new Point((int)mVector.X, (int)mVector.Y);
        }

        public Texture2D spriteTexture
        {
            get { return texture; }
        }

        public Rectangle spriteRect
        {
            get { return rect; }
        }

        public int Energy {
            set{ energy = value; }
            get { return energy; }
         }

        public int Direction
        {
            set { direction = value; }
            get { return direction; }
        }
        
        public int FireRate
        {
            set { fireRate = value; }
            get { return fireRate; }
        }

        public int LastShotTime
        {
            set { lastShotTime = value; }
            get { return lastShotTime; }
        }
    }
}
