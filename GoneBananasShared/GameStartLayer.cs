using System;
using System.Collections.Generic;
using CocosSharp;
using CocosDenshion;

namespace Game
{
    public class GameStartLayer : CCLayerColor
    {
        public GameStartLayer () : base ()
        {
            var touchListener = new CCEventListenerTouchAllAtOnce ();
            touchListener.OnTouchesEnded = (touches, ccevent) => Window.DefaultDirector.ReplaceScene (GameLayer.GameScene (Window));

            AddEventListener (touchListener, this);

			Color = CCColor3B.Blue;
            Opacity = 255;
        }

        protected override void AddedToScene ()
        {
            base.AddedToScene ();

            var firstline = new CCLabelTtf("READY TO SAVE MR. BLUE", "arial", 22) {
				Position = VisibleBoundsWorldspace.Center,
				Color = CCColor3B.Black,
                HorizontalAlignment = CCTextAlignment.Center,
                VerticalAlignment = CCVerticalTextAlignment.Center,
                AnchorPoint = CCPoint.AnchorMiddle
            };

			var secondline = new CCLabelTtf("TAP THE SCREEN THEN", "arial", 22) {
				Color = CCColor3B.Black,
				Position = VisibleBoundsWorldspace.Center,
				HorizontalAlignment = CCTextAlignment.Center,
				VerticalAlignment = CCVerticalTextAlignment.Center,
				AnchorPoint = CCPoint.AnchorMiddle,
				PositionY = 280

			};

            AddChild (firstline);
			AddChild (secondline);
			AddEnemy ();
        }

		public void AddEnemy()
		{
			CCSprite enemyRed = new CCSprite ("Red");
			enemyRed.Position = VisibleBoundsWorldspace.Center;
			enemyRed.PositionX = 940;
			enemyRed.PositionY = 150;

			CCSprite enemyYellow = new CCSprite ("Yellow");
			enemyYellow.Position = VisibleBoundsWorldspace.Center;
			enemyYellow.PositionX = 120;
			enemyYellow.PositionY = 470;

			AddChild (enemyYellow);
			AddChild (enemyRed);
		}

        public static CCScene GameStartLayerScene (CCWindow mainWindow)
        {
            var scene = new CCScene (mainWindow);
            var layer = new GameStartLayer ();

            scene.AddChild (layer);

            return scene;
        }
    }
}