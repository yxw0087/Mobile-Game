using System;
using System.Collections.Generic;
using CocosDenshion;
using CocosSharp;
using System.Linq;

using Box2D.Common;
using Box2D.Dynamics;
using Box2D.Collision.Shapes;

namespace Game
{
    public class GameLayer : CCLayerColor
    {
        const float bulletSpeed = 800f;
        const float GAME_DURATION = 60.0f;

        CCSprite MrBlue;

		List<CCSprite> redList;
		List<CCSprite> yellowList;
		List<CCSprite> defeated;
		CCSprite bullets;

        public GameLayer ()
        {
            var touchListener = new CCEventListenerTouchAllAtOnce ();
            touchListener.OnTouchesEnded = OnTouchesEnded;

            AddEventListener (touchListener, this);
			Color = new CCColor3B (CCColor4B.Blue);
            Opacity = 200;

			bullets = new CCSprite ("bullet");
           
			AddChild (bullets);
            AddMrBlue ();
			redList = new List<CCSprite>();
			yellowList = new List<CCSprite> ();
			defeated = new List<CCSprite> ();
            StartScheduling();

            CCSimpleAudioEngine.SharedEngine.PlayBackgroundMusic ("Sounds/backgroundMusic", true);
        }

        void StartScheduling()
        {
			Schedule (t => {
				redList.Add (RedEnemy ());
			}, 9.0f);

			Schedule (t => {
				yellowList.Add (YellowEnemy ());
			}, 1.0f);

			Schedule (t => {
				redList.Add (BottomRedEnemy ());
			}, 5.5f);

			Schedule (t => {
				redList.Add (BottomYellowEnemy ());
			}, 8.8f);

			Schedule (t => {
				yellowList.Add (TopYellowEnemy ());
			}, 4.0f);

			Schedule (t => {
				redList.Add (TopRedEnemy ());
			}, 5.0f);

			Schedule (t => {
				yellowList.Add (RightYellowEnemy ());
			}, 8.5f);

			Schedule (t => {
				redList.Add (RightRedEnemy ());
			}, 5.5f);

			Schedule (t => CheckCollision ());
        }
			

        void AddMrBlue ()
        {
			MrBlue = new CCSprite ("Blue");
			MrBlue.Scale = 0.35f;

            AddChild (MrBlue);
        }

        CCPoint GetRandomPosition (CCSize spriteSize)
        {
            double rnd = CCRandom.NextDouble ();
            double randomX = (rnd > 0) 
                ? rnd * VisibleBoundsWorldspace.Size.Width - spriteSize.Width / 2 
                : spriteSize.Width / 2;

            return new CCPoint ((float)randomX, VisibleBoundsWorldspace.Size.Height - spriteSize.Height / 2);
        }
			
		CCSprite YellowEnemy(){
			Random number = new Random ();
			int place = number.Next (1, 801);

			CCSprite yellow = new CCSprite ("Yellow");
			yellow.PositionX = -50;
			yellow.PositionY = (float)place;
			yellow.Scale = .2f;

			AddChild (yellow);

			float ds = CCPoint.Distance (yellow.Position, MrBlue.Position);
			var dt = ds / 100f;
			var moveYellow = new CCMoveTo (dt, MrBlue.Position);
			yellow.RunAction (moveYellow);

			return yellow;
		}

		CCSprite RightYellowEnemy(){

			Random number = new Random ();
			int place = number.Next (1, 801);

			CCSprite yellow = new CCSprite ("Yellow");
			yellow.PositionX = 1200;
			yellow.PositionY = (float)place;
			yellow.Scale = .2f;

			AddChild (yellow);

			float ds = CCPoint.Distance (yellow.Position, MrBlue.Position);
			var dt = ds / 250f;
			var moveYellow = new CCMoveTo (dt, MrBlue.Position);
			yellow.RunAction (moveYellow);

			return yellow;
		}

		CCSprite RightRedEnemy(){

			Random number = new Random ();
			int place = number.Next (1, 801);

			CCSprite red = new CCSprite ("Red");
			red.PositionX = 1200;
			red.PositionY = (float)place;
			red.Scale = .2f;

			AddChild (red);

			float ds = CCPoint.Distance (red.Position, MrBlue.Position);
			var dt = ds / 200f;
			var moveRed = new CCMoveTo (dt, MrBlue.Position);
			red.RunAction (moveRed);

			return red;
		}

		CCSprite TopYellowEnemy(){

			Random number = new Random ();
			int place = number.Next (1, 1001);

			CCSprite yellow = new CCSprite ("Yellow");
			yellow.PositionX = (float)place;
			yellow.PositionY = 1000;
			yellow.Scale = .2f;

			AddChild (yellow);

			float ds = CCPoint.Distance (yellow.Position, MrBlue.Position);
			var dt = ds / 150f;
			var moveYellow = new CCMoveTo (dt, MrBlue.Position);
			yellow.RunAction (moveYellow);

			return yellow;
		}

		CCSprite TopRedEnemy(){

			Random number = new Random ();
			int place = number.Next (1, 1001);

			CCSprite red = new CCSprite ("Red");
			red.PositionX = (float)place;
			red.PositionY = 700;
			red.Scale = .2f;

			AddChild (red);

			float ds = CCPoint.Distance (red.Position, MrBlue.Position);
			var dt = ds / 50f;
			var moveRed = new CCMoveTo (dt, MrBlue.Position);
			red.RunAction (moveRed);

			return red;
		}

		CCSprite BottomYellowEnemy(){

			Random number = new Random ();
			int place = number.Next (1, 1001);

			CCSprite yellow = new CCSprite ("Yellow");
			yellow.PositionX = (float)place;
			yellow.PositionY = 0;
			yellow.Scale = .2f;

			AddChild (yellow);

			float ds = CCPoint.Distance (yellow.Position, MrBlue.Position);
			var dt = ds / 50f;
			var moveRed = new CCMoveTo (dt, MrBlue.Position);
			yellow.RunAction (moveRed);

			return yellow;
		}

		CCSprite BottomRedEnemy(){

			Random number = new Random ();
			int place = number.Next (1, 1001);

			CCSprite red = new CCSprite ("Red");
			red.PositionX = (float)place;
			red.PositionY = 0;
			red.Scale = .2f;

			AddChild (red);

			float ds = CCPoint.Distance (red.Position, MrBlue.Position);
			var dt = ds / 50f;
			var moveRed = new CCMoveTo (dt, MrBlue.Position);
			red.RunAction (moveRed);

			return red;
		}

		CCSprite RedEnemy(){

			Random number = new Random ();
			int place = number.Next (1, 701);

			CCSprite red = new CCSprite ("Red");
			red.PositionX = -50;
			red.PositionY = (float)place;
			red.Scale = .2f;

			AddChild (red);

			float ds = CCPoint.Distance (red.Position, MrBlue.Position);
			var dt = ds / 100f;
			var moveRed = new CCMoveTo (dt, MrBlue.Position);
			red.RunAction (moveRed);

			return red;
		}

        void CheckCollision ()
        {
			redList.ForEach (red => {
				bool defend = red.BoundingBoxTransformedToParent.IntersectsRect (bullets.BoundingBoxTransformedToParent);
				if (defend) {
					defeated.Add(red);
					red.RemoveFromParent ();
				}
			});

			defeated.ForEach (red => redList.Remove (red));

			yellowList.ForEach (yellow => {
				bool defend = yellow.BoundingBoxTransformedToParent.IntersectsRect (bullets.BoundingBoxTransformedToParent);
				if (defend) {
					defeated.Add(yellow);
					yellow.RemoveFromParent ();
				}
			});

			defeated.ForEach ( yellow => yellowList.Remove (yellow));

			yellowList.ForEach (yellow => {
				bool hurt = yellow.BoundingBoxTransformedToParent.IntersectsRect (MrBlue.BoundingBoxTransformedToParent);
				if (hurt) {
					EndGame ();
				}
			});

			redList.ForEach (red => {
				bool hurt = red.BoundingBoxTransformedToParent.IntersectsRect (MrBlue.BoundingBoxTransformedToParent);
				if (hurt) {
					EndGame ();
				}
			});
		}

        void EndGame ()
        {
            UnscheduleAll();

			var gameOverScene = GameOverLayer.SceneWithScore (Window, defeated.Count);
            var transitionToGameOver = new CCTransitionMoveInR (0.3f, gameOverScene);

            Director.ReplaceScene (transitionToGameOver);
        }

        void OnTouchesEnded (List<CCTouch> touches, CCEvent touchEvent)
        {
            var location = touches [0].LocationOnScreen;
            location = WorldToScreenspace (location); 
			float distance = CCPoint.Distance (bullets.Position, location);

            var distanceTime = distance / bulletSpeed;

            var moveMrBlue = new CCMoveTo (distanceTime, location);
			bullets.RunAction (moveMrBlue);

        }
			
        protected override void AddedToScene ()
        {
            base.AddedToScene ();

            Scene.SceneResolutionPolicy = CCSceneResolutionPolicy.NoBorder;

			MrBlue.Position = VisibleBoundsWorldspace.Center;
			bullets.Position = VisibleBoundsWorldspace.Center;

            var b = VisibleBoundsWorldspace;

        }

        public static CCScene GameScene (CCWindow mainWindow)
        {
            var scene = new CCScene (mainWindow);
            var layer = new GameLayer ();
			
            scene.AddChild (layer);

            return scene;
        }
    }
}