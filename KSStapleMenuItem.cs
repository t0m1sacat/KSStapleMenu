using System;
using System.Linq;
using System.Collections.Generic;
using MonoTouch.UIKit;
using System.Drawing;

namespace KSStapleMenu
{
	public class KSStapleMenuItem
	{
		public KSStapleMenuItem (string id, UIImage image, string title = null)
		{
			this.Id = id;
			this.elements = new List<UIButton> ();
			this.AddElement (image, title);
		}

		public string Id
		{
			get;
			private set;
		}

		public UIView View
		{
			get
			{
				if(this.elements.Count <= 0)
				{
					return null;
				}
				return this.elements[this.SelectedIndex];
			}
		}

		private List<UIButton> elements;

		public int SelectedIndex
		{
			get
			{
				return this.selectedIndex;
			}
			set
			{
				this.selectedIndex = value;
				this.View.SetNeedsLayout();
			}
		}
		private int selectedIndex;

		public int AddElement(UIImage image, string title = null)
		{
			var button = UIButton.FromType (UIButtonType.Custom);
			button.SetImage (image, UIControlState.Normal);
			button.Frame = new RectangleF (new PointF (0, 0), image.Size);
			this.elements.Add (button);

			// Return index of the element.
			var index = this.elements.Count - 1;

			return index;
		}

		public float MaxWidth
		{
			get
			{
				if(this.elements == null)
				{
					return 0f;
				}
				return this.elements.Max(elem => elem.Bounds.Width);
			}
		}

		public float MaxHeight
		{
			get
			{
				if(this.elements == null)
				{
					return 0f;
				}
				return this.elements.Max(elem => elem.Bounds.Height);
			}
		}
	}
}

