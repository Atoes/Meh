using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mario
{
    class weapon
    {
        const int MAX_DISTANCE = 500;
        public bool Visible;
        bool f;
        Texture2D fireball;
        Vector2 mPosition;
        Vector2 mSpeed;
        Vector2 mDirection = new Vector2(1, 0);
        Vector2 mEndPosition;
        public Rectangle size;

        public weapon(Vector2 point, Vector2 current, Texture2D text, bool fl)
        {
            mEndPosition = point;
            mPosition = current;
            fireball = text;
            Visible = true;
            mSpeed = new Vector2(500, 0);
            f = fl;
            size = new Rectangle((int)mPosition.X, (int)mPosition.Y, fireball.Width, fireball.Height);
        }


        public void Update(GameTime gametime)
        {
            float elapsedTime = (float)gametime.ElapsedGameTime.TotalSeconds;
            if ((mPosition.X < 0) || (mPosition.X > 1000))
                Visible = false;
            if (f == false)
                    mPosition += (mDirection * mSpeed * elapsedTime);
            else if (f == true)
            {
                mDirection = new Vector2(-1, 0);
                mPosition += (mDirection * mSpeed * elapsedTime);
            }
            size.X = (int)mPosition.X;
            size.Y = (int)mPosition.Y;
            if (Vector2.Distance(mPosition, mEndPosition) == 0)
            {
                Visible = false;
            }
        }

        public bool visibility(weapon w)
        {
            if (w.Visible == false)
                return false;
            else
                return true;

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (Visible == true)
            {
                if (f == false)
                    spriteBatch.Draw(fireball, mPosition, null, Color.White, 0f, new Vector2(fireball.Width / 2, fireball.Height / 2), 1.0f, SpriteEffects.None, 0f);
                else if (f == true)
                    spriteBatch.Draw(fireball, mPosition, null, Color.White, 0f, new Vector2(fireball.Width / 2, fireball.Height / 2), 1.0f, SpriteEffects.FlipHorizontally, 0f);
            }
        }

        public void Kill()
        {
            Visible = false;
        }
    }
}