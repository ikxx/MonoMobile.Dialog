﻿using System;
using Android.Content;
using Android.Widget;

namespace MonoMobile.Dialog
{
	public class DialogHelper
	{
		private Context context;
		private RootElement formLayer;

		//public event Action<Section, Element> ElementClick;
		//public event Action<Section, Element> ElementLongClick;

		public RootElement Root { get; set; }

		private DialogAdapter DialogAdapter { get; set; }

		public DialogHelper(Context context, ListView dialogView, RootElement root)
		{
			this.Root = root;
			this.Root.Context = context;

			dialogView.Adapter = this.DialogAdapter = new DialogAdapter(context, this.Root);
			dialogView.ItemClick += 
				// mc++				
				new EventHandler<AdapterView.ItemClickEventArgs>(dialogView_ItemClick)
				// new EventHandler<ItemEventArgs>(ListView_ItemClick) // obsolete
				;
			dialogView.ItemLongClick += ListView_ItemLongClick;;
			dialogView.Tag = root;
		}

		/// <summary>
		/// mc++
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void dialogView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			var elem = this.DialogAdapter.ElementAtIndex(e.Position);
			if (elem != null && elem.LongClick != null)
			{
				elem.LongClick();
			}
		}

		void ListView_ItemLongClick (object sender, AdapterView.ItemLongClickEventArgs e)
		{
			var elem = this.DialogAdapter.ElementAtIndex(e.Position);
			if (elem != null && elem.LongClick != null) {
				elem.LongClick();
			}
		}

		/// <summary>
		/// mc++ Obsolete!
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void ListView_ItemClick(object sender, ItemEventArgs e)
		{
			var elem = this.DialogAdapter.ElementAtIndex(e.Position);
			if(elem != null)
				elem.Selected();
		}
		
		public void ReloadData()
		{
			if(Root == null) {
				return;
			}
			
			this.DialogAdapter.ReloadData();
		}
		
	}
}