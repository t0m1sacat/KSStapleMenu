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


			// Create a new menu that will attach the right side of the parent view.
			var stapleMenu = new KSStapleMenu (KSStapleMenu.STAPLEMENU_MODE.Right, 50f, new SizeF(60, 60));

			// First item consists of three elements.
			var item1 = new KSStapleMenuItem ("INK", UIImage.FromBundle ("item1"), "Ink", 12f, UIColor.Red);
			item1.AddElement (UIImage.FromBundle ("item1_sub1"), "Ink Blue", 12f, UIColor.Red);
			item1.AddElement (UIImage.FromBundle ("item1_sub2"), "Ink Red", 12f, UIColor.Red);

			// Second item consists of three elements.
			var item2 = new KSStapleMenuItem ("FREETEXT", UIImage.FromBundle ("item2"));
			item2.AddElement (UIImage.FromBundle ("item1_sub1"));
			item2.AddElement (UIImage.FromBundle ("item1_sub2"));

			// Third item has only one element.
			var item3 = new KSStapleMenuItem ("NOTE", UIImage.FromBundle ("item3"));

			// Add the items to the menu.
			stapleMenu.AddItems (item1, item2, item3);

			// Preselect the 2nd subelement of the "INK" item.
			stapleMenu.SelectItem ("INK", 1);

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

