using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreGraphics;

namespace KSStapleMenu
{
	internal class ExpandIndicatorView : UIView
	{
		public ExpandIndicatorView(RectangleF rect) : base(rect)
		{}

		public override void Draw (RectangleF rect)
		{
			base.Draw (rect);
			float midY = this.Bounds.Height / 2f;
			float right = this.Bounds.Width;
			float bottom = this.Bounds.Height;

			var ctx = UIGraphics.GetCurrentContext();
			ctx.SaveState();
			CGPath path = new CGPath();
			path.MoveToPoint(new PointF(right, 0f));
			path.AddLineToPoint(2f, midY);
			path.AddLineToPoint(right, bottom);
			ctx.AddPath(path);
			ctx.SetStrokeColor(UIColor.Black.CGColor);
			ctx.SetLineWidth(2f);
			ctx.SetLineCap(CGLineCap.Round);
			ctx.StrokePath();
			ctx.RestoreState();
			ctx.Dispose();
			path.Dispose();
		}
	}

	internal class KSMenuItemHostView : UIView
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="KSStapleMenu.KSMenuItemHostView"/> class.
		/// </summary>
		/// <param name="menu">the menu that is using this view</param>
		/// <param name="menuItem">the item encapsulated by the host view</param>
		internal KSMenuItemHostView (KSStapleMenu menu, KSStapleMenuItem menuItem) : base()
		{
			this.menu = menu;
			this.ItemId = menuItem.Id;

//			this.BackgroundColor = UIColor.Blue;
//			this.Layer.BorderColor = UIColor.Yellow.CGColor;
//			this.Layer.BorderWidth = 2f;

			if(menuItem.NumberOfElements > 1)
			{
				this.expandIndicatorView = new ExpandIndicatorView(new RectangleF(0, 0, 8, 16))
				{
					BackgroundColor = UIColor.Clear,
				};

				this.AddSubview(this.expandIndicatorView);
			}
		}

		private ExpandIndicatorView expandIndicatorView;

		private KSStapleMenu menu;
		public string ItemId;

		/// <summary>
		/// Expands the sub elements.
		/// </summary>
		internal void ExpandSubElements ()
		{
			float x = 0;

			if(this.menu.Mode == KSStapleMenu.STAPLEMENU_MODE.Right)
			{
				x = this.Bounds.Width - 2 * this.menu.ItemSize.Width;
			}
			else
			{
				x = this.menu.ItemSize.Width;
			}

			int itemIndex = this.menu.GetSelectedItemIndex(this.ItemId);
			int currentIndex = 0;
			foreach(var subview in this.Subviews)
			{
				if(subview is ExpandIndicatorView)
				{
					continue;
				}

				if(currentIndex != itemIndex)
				{
					subview.Alpha = 1f;
					subview.Center = new PointF(x + subview.Bounds.Width / 2f, subview.Center.Y);
					if(this.menu.Mode == KSStapleMenu.STAPLEMENU_MODE.Right)
					{
						x -= this.menu.ItemSize.Width;
					}
					else
					{
						x += this.menu.ItemSize.Width;
					}
				}

				currentIndex++;
			}
		}

		/// <summary>
		/// Collapses the sub elements.
		/// </summary>
		internal void CollapseSubElements()
		{
			float x = 0;
			
			if(this.menu.Mode == KSStapleMenu.STAPLEMENU_MODE.Right)
			{
				x = this.Bounds.Width - this.menu.ItemSize.Width;
			}
			else
			{
				x = 0;
			}

			int itemIndex = this.menu.GetSelectedItemIndex(this.ItemId);
			int currentIndex = 0;
			foreach(var subview in this.Subviews)
			{
				if(subview is ExpandIndicatorView)
				{
					continue;
				}
				
				if(currentIndex != itemIndex)
				{
					subview.Alpha = 0f;
				}
				else
				{
					subview.Alpha = 1f;
				}

				if(this.menu.Mode == KSStapleMenu.STAPLEMENU_MODE.Right)
				{
					subview.Center = new PointF(x + subview.Bounds.Width / 2f, this.Bounds.Height / 2f);
				}
				else
				{
					subview.Center = new PointF(subview.Bounds.Width / 2f, this.Bounds.Height / 2f);
				}

				currentIndex++;
			}
		}

		public override void LayoutSubviews ()
		{
			int selectedElementIndex = this.menu.GetSelectedItemIndex (this.ItemId);

			int currentIndex = 0;
			foreach(var subview in this.Subviews)
			{
				if(subview is ExpandIndicatorView)
				{
					if(this.menu.Mode == KSStapleMenu.STAPLEMENU_MODE.Left)
					{
						this.expandIndicatorView.Center = new PointF(this.menu.ItemSize.Width - this.expandIndicatorView.Bounds.Width / 2f- 5f, this.menu.ItemSize.Height / 2f);
					}
					else
					{
						this.expandIndicatorView.Center = new PointF(this.Bounds.Width - this.menu.ItemSize.Width + this.expandIndicatorView.Bounds.Width / 2f + 5f, this.menu.ItemSize.Height / 2f);
					}
					continue;
				}

				subview.Alpha = currentIndex == selectedElementIndex ? 1f : 0f;
				currentIndex++;
				//subview.Center = new PointF( this.Bounds.Width / 2f, this.Bounds.Height / 2f);
			}
		}
	}
}

