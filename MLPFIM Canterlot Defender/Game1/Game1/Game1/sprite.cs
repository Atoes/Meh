using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Mario
{
    class sprite
    {
        private Texture2D image;
        public Vector2 pos = new Vector2(0, 0);
        public Rectangle col;
        Random r = new Random();
        public bool Visibility;
        public bool gotThrough;

        public sprite(Texture2D text)
        {
            image = text;
            Visibility = true;
            pos.X = 911;
            pos.Y = r.Next(0, 600);
            col = new Rectangle((int)pos.X, (int)pos.Y, image.Width, image.Height);
        }

        public Vector2 returnPosition()
        {
            return pos;
        }

        public void Update(GameTime time, Vector2 direction, Vector2 speed)
        {
            if (pos.X < 0)
            {
                gotThrough = true;
            }
            pos -= direction * speed * (float)time.ElapsedGameTime.TotalSeconds;
            col.X = (int)pos.X;
            col.Y = (int)pos.Y;
        }

        public void Draw(SpriteBatch SpriteBatch)
        {
            Rectangle source = new Rectangle(0, 0, image.Width, image.Height);
            if (Visibility == true)
                SpriteBatch.Draw(image, pos, Color.White);
        }

    }
}
