using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

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

			var stapleMenu = new KSStapleMenu (KSStapleMenu.STAPLEMENU_MODE.Right, 50f);

			var item1 = new KSStapleMenuItem ("INK", UIImage.FromBundle ("item1"), "Ink");
			item1.AddElement (UIImage.FromBundle ("item1_sub1"), "Ink Blue");
			item1.AddElement (UIImage.FromBundle ("item1_sub2"), "Ink Red");
			item1.SelectedIndex = 1;

			var item2 = new KSStapleMenuItem ("FREETEXT", UIImage.FromBundle ("item2"), "Freetext");
			var item3 = new KSStapleMenuItem ("NOTE", UIImage.FromBundle ("item3"), "Note");
			stapleMenu.AddItems (item1, item2, item3);
			viewController.View.AddSubview (stapleMenu);

			return true;
		}
	}
}

