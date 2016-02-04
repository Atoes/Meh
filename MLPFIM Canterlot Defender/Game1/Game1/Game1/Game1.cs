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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D twily, hornPower, fireBall, changelings, canterlot, end;
        int x, y, z, limit, no, f, score;
        KeyboardState current;
        List<sprite> sprites;
        List<weapon> weap;
        bool attack, flip;
        float startY;
        Vector2 pos, shootPos, firePos, firePos2;
        Random r;
        SpriteFont font;
        SoundEffect boing;
        SoundEffect fire;
        Song song;
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            x = 300;
            y = 460;
            graphics.PreferredBackBufferHeight = 700;
            graphics.PreferredBackBufferWidth = 1000;
            graphics.ApplyChanges();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            r = new Random();
            attack = false;
            pos = new Vector2(x, y);
            shootPos = new Vector2(x, y + 10);
            firePos = new Vector2(shootPos.X + 190, shootPos.Y + 35);
            firePos2 = new Vector2(shootPos.X - 10, shootPos.Y + 35);
            startY = pos.Y;
            flip = false;
            sprites = new List<sprite>();
            weap = new List<weapon>();
            z = 0;
            limit = 0;
            score = 0;
            no = 0;
            f = 0;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            changelings = Content.Load<Texture2D>("changeling");
            canterlot = Content.Load<Texture2D>("canterlot");
            twily = Content.Load<Texture2D>("twilicorn");
            hornPower = Content.Load<Texture2D>("Twilight Attack");
            fireBall = Content.Load<Texture2D>("fire ball");
            end = Content.Load<Texture2D>("GameOver");
            font = Content.Load<SpriteFont>("Score");
            song = Content.Load<Song>("music");
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = true;
            boing = Content.Load<SoundEffect>("Boing");
            fire = Content.Load<SoundEffect>("magic_spell_cast_1");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        private void HandleCollision()
        {
            for (int i = 0; i < weap.Count; i++)
            {
                for (int x = 0; x < sprites.Count; x++)
                {
                    if (weap[i].size.Intersects(sprites[x].col))
                    {
                        weap[i].Visible = false;
                        sprites[x].Visibility = false;
                        boing.Play();
                        break;
                    }
                }
            }
        }

        public void Quit()
        {
            this.Exit();
        }

        //Updates the game
        protected override void Update(GameTime gameTime)
        {
            f = (int)gameTime.ElapsedGameTime.TotalSeconds;

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            current = Keyboard.GetState();
            Vector2 aDirection = new Vector2(1, 0);
            Vector2 aSpeed = new Vector2(300,0);
            Vector2 aSpeed2 = new Vector2(350, 0);
            Vector2 aSpeed3 = new Vector2(400, 0);

            if (z == 30)
            {
                sprites.Add(new sprite(changelings));
                z = 0;
            }

            z++;
            

            if (current.IsKeyDown(Keys.Down))
            {
                pos.Y += 9;
                shootPos.Y += 9;
                firePos.Y += 9;
                firePos2.Y += 9;
            }

            if (current.IsKeyDown(Keys.Up))
            {
                pos.Y -= 9;
                shootPos.Y -= 9;
                firePos.Y -= 9;
                firePos2.Y -= 9;
            }

            if (current.IsKeyDown(Keys.Left))
            {
                pos.X -= 7;
                shootPos.X -= 7;
                firePos.X -= 7;
                firePos2.X -= 7;
                flip = true;
            }
            if (current.IsKeyDown(Keys.Right))
            {
                pos.X += 7;
                shootPos.X += 7;
                firePos.X += 7;
                firePos2.X += 7;
                flip = false;
            }
            attack = false;
            if (current.IsKeyDown(Keys.Space))
            {
                attack = true;
                if ((limit >= 0) && (limit <= 7))
                {
                    if (flip == false)
                    {
                        weap.Add(new weapon(new Vector2(firePos.X + 500, firePos.Y), firePos, fireBall, flip));
                        limit++;
                    }
                    else if (flip == true)
                    {
                        weap.Add(new weapon(new Vector2(firePos2.X - 500, firePos2.Y), firePos2, fireBall, flip));
                        limit++;
                    }
                    fire.Play();
                }

                if (limit > 7)
                {
                    limit++;
                    if (limit == 17)
                        limit = 0;
                }
            }
            for (int x = 0; x < sprites.Count; x++)
            {
                if (sprites[x].gotThrough == true)
                {
                    sprites.Remove(sprites[x]);
                    x--;
                    no++;
                }
            }

            HandleCollision();

            for (int i = 0; i < weap.Count; i++)
            {
                bool b = weap[i].visibility(weap[i]);
                if (b == false)
                {
                    weap.Remove(weap[i]);
                    i--;
                }
            }

            for (int i = 0; i < sprites.Count; i++)
            {
                bool b = sprites[i].Visibility;
                if (b == false)
                {
                    sprites.Remove(sprites[i]);
                    i--;
                    score++;    
                }
            }

            for (int i = 0; i < weap.Count; i++)
            {
                weap[i].Update(gameTime);
            }


            for (int i = 0; i < sprites.Count; i++)
            {
                if(score<25)
                    sprites[i].Update(gameTime, aDirection, aSpeed);
                else if((score>25)&&(score<50))
                    sprites[i].Update(gameTime, aDirection, aSpeed2);
                else if (score > 50)
                    sprites[i].Update(gameTime, aDirection, aSpeed3);
            }

            base.Update(gameTime);
        }

        private void DrawText()
        {
            spriteBatch.DrawString(font, "Score: " + score, new Vector2(20, 45), Color.White);
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            Rectangle source = new Rectangle(0, 0, hornPower.Width, hornPower.Height);
            Rectangle source2 = new Rectangle(0, 0, twily.Width, twily.Height);



            // TODO: Add your drawing code here

            spriteBatch.Draw(canterlot, new Rectangle(0, 0, canterlot.Width, canterlot.Height), Color.White);

            if (attack == true)
            {
                if (flip == false)
                {
                    spriteBatch.Draw(hornPower, shootPos, source, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0f);

                }
                else
                {
                    spriteBatch.Draw(hornPower, shootPos, source, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.FlipHorizontally, 0f);

                }
            }
            else
            {
                if (flip == false)
                    spriteBatch.Draw(twily, pos, source2, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.None, 0f);
                else
                    spriteBatch.Draw(twily, pos, source2, Color.White, 0f, new Vector2(0, 0), 1f, SpriteEffects.FlipHorizontally, 0f);
            }

            for (int i = 0; i < sprites.Count; i++)
            {
                sprites[i].Draw(spriteBatch);
            }

            for (int i = 0; i < weap.Count; i++)
            {
                weap[i].Draw(gameTime, spriteBatch);
            }

            for (int i = 0; i < sprites.Count; i++)
            {
                if (sprites[i].gotThrough == true)
                    this.Exit();
            }

            DrawText();
            //End
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
