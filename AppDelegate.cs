using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;

namespace KSStapleMenu
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		UIWindow window;
		KSStapleMenuViewController viewController;


		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			window = new UIWindow (UIScreen.MainScreen.Bounds);
			
			viewController = new KSStapleMenuViewController ();
			window.RootViewController = viewController;
			window.MakeKeyAndVisible ();

			var txt = new UITextField(new RectangleF(0, 0, 400, 30));
			txt.TextAlignment = UITextAlignment.Center;
			viewController.View.AddSubview(txt);
			txt.Center = new PointF (viewController.View.Center.X, viewController.View.Center.Y + 60);


			// Create a new annotation/sync/review selection toolbar.
			var stapleMenu = new KSStapleMenu ( KSStapleMenu.STAPLEMENU_MODE.Right, 80f, new SizeF ( 80f, 80f ) );
			KSStapleMenuItem inkItem = null;
			KSStapleMenuItem noteItem = null;
			KSStapleMenuItem freetextItem = null;
			KSStapleMenuItem hilightItem = null;
				
			inkItem = new KSStapleMenuItem ( "INK", UIImage.FromBundle ( "Assets/Images/PDF/annot-ink-0" ), "Ink 0", 12f, UIColor.Black );
			for ( int i = 1; i <= 3; i++ )
			{
				inkItem.AddElement ( UIImage.FromBundle ( "Assets/Images/PDF/annot-ink-" + i ), "Ink " + i, 12f, UIColor.Black );
			}
			noteItem = new KSStapleMenuItem ( "NOTE", UIImage.FromBundle ( "Assets/Images/PDF/annot-note" ), "Note", 12f, UIColor.Black );
			freetextItem = new KSStapleMenuItem ( "FREETEXT", UIImage.FromBundle ( "Assets/Images/PDF/annot-freetext" ), "Freetext", 12f, UIColor.Black );
			hilightItem = new KSStapleMenuItem ( "HIGHLIGHT", UIImage.FromBundle ( "Assets/Images/PDF/annot-highlight-0" ), "Highlight 0", 12f, UIColor.Black );
			for ( int i = 1; i <= 3; i++ )
			{
				hilightItem.AddElement ( UIImage.FromBundle ( "Assets/Images/PDF/annot-highlight-" + i ), "Highlight " + i, 12f, UIColor.Black );
			}
			KSStapleMenuItem syncItem = null;
			KSStapleMenuItem switchReviewsItem = null;
			// Only show sync and review selection button if server side reviews are enabled and the user has permissions to annotate.
			syncItem = new KSStapleMenuItem ( "SYNC", UIImage.FromBundle ( "Assets/Images/PDF/annot-sync" ), "Sync", 12f, UIColor.Black );
			switchReviewsItem = new KSStapleMenuItem ( "REVIEWS", UIImage.FromBundle ( "Assets/Images/PDF/reviewselect" ), "Switch", 12f, UIColor.Black );
			
			stapleMenu.AddItems ( inkItem, noteItem, freetextItem, hilightItem, syncItem, switchReviewsItem );

			// Add a callback to get informed if an element was tapped.
			stapleMenu.ItemSelected += (string id, int index) =>
			{
				Console.WriteLine ("Selected item {0} with index {1}", id, index);
				txt.Text = string.Format ("Selected item {0} with index {1}", id, index);
			};

			viewController.View.AddSubview (stapleMenu);



			return true;
		}
	}
}

