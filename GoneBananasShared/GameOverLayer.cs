using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using CocosSharp;
using SQLite;

namespace Game
{

	[Table("Items")]
	public class Stock {
		[PrimaryKey, AutoIncrement, Column("_id")]
		public int Id { get; set; }
		[MaxLength(8)]
		public int Score { get; set; }
	}

    public class GameOverLayer : CCLayerColor
    {
        string scoreMessage = string.Empty;
		int top;
		string scorelist = "Your top 5 scores:\n";
		public GameOverLayer (int score)
        {
			string dbPath = Path.Combine (
				Environment.GetFolderPath (Environment.SpecialFolder.Personal),
				"myscores.db3");
			var db = new SQLiteConnection (dbPath);
			db.CreateTable<Stock> ();
			var newStock = new Stock ();
			newStock.Score = score;
			db.Insert (newStock); 

            var touchListener = new CCEventListenerTouchAllAtOnce ();
            touchListener.OnTouchesEnded = (touches, ccevent) => Window.DefaultDirector.ReplaceScene (GameLayer.GameScene (Window));

            AddEventListener (touchListener, this);

			var sorted = db.Query<Stock>("SELECT DISTINCT Score FROM Items ORDER BY Score DESC LIMIT 5");
			foreach (var s in sorted) {
				scorelist += s.Score + "\n";
			}

			var topscore = db.Query<Stock>("SELECT * FROM Items ORDER BY Score DESC LIMIT 1");
			foreach (var s in topscore) {
				top = s.Score;
			}

			scoreMessage = String.Format ("GAME OVER. YOU KILLED {0} ENEMIES!\n\n{1}\nHIGH SCORE: {2}\n\n", score, scorelist, top);

            Color = new CCColor3B (CCColor4B.Blue);

            Opacity = 255;
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

        protected override void AddedToScene ()
        {
            base.AddedToScene ();

            Scene.SceneResolutionPolicy = CCSceneResolutionPolicy.ShowAll;

            var scoreLabel = new CCLabelTtf (scoreMessage, "arial", 22) {
                Position = new CCPoint (VisibleBoundsWorldspace.Size.Center.X, VisibleBoundsWorldspace.Size.Center.Y + 50),
                Color = new CCColor3B (CCColor4B.Yellow),
                HorizontalAlignment = CCTextAlignment.Center,
                VerticalAlignment = CCVerticalTextAlignment.Center,
                AnchorPoint = CCPoint.AnchorMiddle
            };

            AddChild (scoreLabel);

            var playAgainLabel = new CCLabelTtf ("TAP TO PLAY AGAIN", "arial", 22) {
				Position = new CCPoint (VisibleBoundsWorldspace.LowerLeft.X + 200, VisibleBoundsWorldspace.LowerLeft.Y + 100),
                Color = new CCColor3B (CCColor4B.Green),
                HorizontalAlignment = CCTextAlignment.Center,
                VerticalAlignment = CCVerticalTextAlignment.Center,
                AnchorPoint = CCPoint.AnchorMiddle,
            };

            AddChild (playAgainLabel);

            AddEnemy ();
        }

		public static CCScene SceneWithScore (CCWindow mainWindow, int score)
        {
            var scene = new CCScene (mainWindow);
            var layer = new GameOverLayer (score);

            scene.AddChild (layer);

            return scene;
        }
    }
}