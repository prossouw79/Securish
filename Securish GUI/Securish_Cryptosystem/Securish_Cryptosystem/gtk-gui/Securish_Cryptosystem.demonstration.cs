
// This file has been generated by the GUI designer. Do not modify.
namespace Securish_Cryptosystem
{
	public partial class demonstration
	{
		private global::Gtk.VBox vbox1;
		
		private global::Gtk.Label label1;
		
		private global::Gtk.Frame frame2;
		
		private global::Gtk.Alignment GtkAlignment1;
		
		private global::Gtk.ScrolledWindow GtkScrolledWindow;
		
		private global::Gtk.TextView txt_output;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget Securish_Cryptosystem.demonstration
			this.Name = "Securish_Cryptosystem.demonstration";
			this.Title = global::Mono.Unix.Catalog.GetString ("Demonstration");
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			this.AllowShrink = true;
			this.DefaultWidth = 500;
			this.DefaultHeight = 600;
			// Container child Securish_Cryptosystem.demonstration.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox ();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.label1 = new global::Gtk.Label ();
			this.label1.Name = "label1";
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString ("<b>Demonstration of system:</b>");
			this.label1.UseMarkup = true;
			this.label1.UseUnderline = true;
			this.vbox1.Add (this.label1);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.label1]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.frame2 = new global::Gtk.Frame ();
			this.frame2.Name = "frame2";
			this.frame2.ShadowType = ((global::Gtk.ShadowType)(0));
			// Container child frame2.Gtk.Container+ContainerChild
			this.GtkAlignment1 = new global::Gtk.Alignment (0F, 0F, 1F, 1F);
			this.GtkAlignment1.Name = "GtkAlignment1";
			this.GtkAlignment1.LeftPadding = ((uint)(12));
			// Container child GtkAlignment1.Gtk.Container+ContainerChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.txt_output = new global::Gtk.TextView ();
			this.txt_output.CanFocus = true;
			this.txt_output.Name = "txt_output";
			this.txt_output.Justification = ((global::Gtk.Justification)(3));
			this.txt_output.WrapMode = ((global::Gtk.WrapMode)(2));
			this.GtkScrolledWindow.Add (this.txt_output);
			this.GtkAlignment1.Add (this.GtkScrolledWindow);
			this.frame2.Add (this.GtkAlignment1);
			this.vbox1.Add (this.frame2);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.frame2]));
			w5.Position = 1;
			this.Add (this.vbox1);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Show ();
		}
	}
}
