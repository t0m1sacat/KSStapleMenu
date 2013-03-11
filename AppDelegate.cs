using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;

namespace KSStapleMenu
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;
		KSStapleMenuViewController viewController;

		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
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

			var stapleMenu = new KSStapleMenu (KSStapleMenu.STAPLEMENU_MODE.Right, 50f, new SizeF(60, 60));

			var item1 = new KSStapleMenuItem ("INK", UIImage.FromBundle ("item1"), "Ink", 12f, UIColor.Red);
			item1.AddElement (UIImage.FromBundle ("item1_sub1"), "Ink Blue", 12f, UIColor.Red);
			item1.AddElement (UIImage.FromBundle ("item1_sub2"), "Ink Red", 12f, UIColor.Red);

			var item2 = new KSStapleMenuItem ("FREETEXT", UIImage.FromBundle ("item2"));
			item2.AddElement (UIImage.FromBundle ("item1_sub1"));
			item2.AddElement (UIImage.FromBundle ("item1_sub2"));

			var item3 = new KSStapleMenuItem ("NOTE", UIImage.FromBundle ("item3"));

			stapleMenu.AddItems (item1, item2, item3);

			stapleMenu.SelectItem ("INK", 1);

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

