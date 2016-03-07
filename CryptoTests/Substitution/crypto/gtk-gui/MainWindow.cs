
// This file has been generated by the GUI designer. Do not modify.
public partial class MainWindow
{
	private global::Gtk.VBox vbox10;
	private global::Gtk.VBox vbox11;
	private global::Gtk.Label lbl_Text;
	private global::Gtk.Entry entry3;
	private global::Gtk.HBox hbox9;
	private global::Gtk.Button btn_text_encrypt;
	private global::Gtk.Button btn_text_decrypt;
	private global::Gtk.Label label6;
	private global::Gtk.Entry entry5;
	private global::Gtk.VBox vbox12;
	private global::Gtk.ScrolledWindow GtkScrolledWindow;
	private global::Gtk.TextView textview3;
	private global::Gtk.Label lbl_Files;
	private global::Gtk.Button btn_OpenFile;
	private global::Gtk.VBox vbox13;
	private global::Gtk.HBox hbox10;
	private global::Gtk.Button btn_file_encrypt;
	private global::Gtk.Button btn_decrypt_encrypt;
	private global::Gtk.Entry entry6;
	private global::Gtk.Label label7;
	private global::Gtk.ScrolledWindow GtkScrolledWindow1;
	private global::Gtk.TextView textview4;

	protected virtual void Build ()
	{
		global::Stetic.Gui.Initialize (this);
		// Widget MainWindow
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString ("MainWindow");
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.vbox10 = new global::Gtk.VBox ();
		this.vbox10.Name = "vbox10";
		this.vbox10.Spacing = 6;
		// Container child vbox10.Gtk.Box+BoxChild
		this.vbox11 = new global::Gtk.VBox ();
		this.vbox11.Name = "vbox11";
		this.vbox11.Spacing = 6;
		// Container child vbox11.Gtk.Box+BoxChild
		this.lbl_Text = new global::Gtk.Label ();
		this.lbl_Text.Name = "lbl_Text";
		this.lbl_Text.LabelProp = global::Mono.Unix.Catalog.GetString ("Text Encryption");
		this.vbox11.Add (this.lbl_Text);
		global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.vbox11 [this.lbl_Text]));
		w1.Position = 0;
		w1.Expand = false;
		w1.Fill = false;
		// Container child vbox11.Gtk.Box+BoxChild
		this.entry3 = new global::Gtk.Entry ();
		this.entry3.CanFocus = true;
		this.entry3.Name = "entry3";
		this.entry3.IsEditable = true;
		this.vbox11.Add (this.entry3);
		global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox11 [this.entry3]));
		w2.Position = 1;
		w2.Expand = false;
		w2.Fill = false;
		// Container child vbox11.Gtk.Box+BoxChild
		this.hbox9 = new global::Gtk.HBox ();
		this.hbox9.Name = "hbox9";
		this.hbox9.Spacing = 6;
		// Container child hbox9.Gtk.Box+BoxChild
		this.btn_text_encrypt = new global::Gtk.Button ();
		this.btn_text_encrypt.CanFocus = true;
		this.btn_text_encrypt.Name = "btn_text_encrypt";
		this.btn_text_encrypt.UseUnderline = true;
		this.btn_text_encrypt.Label = global::Mono.Unix.Catalog.GetString ("Encrypt");
		this.hbox9.Add (this.btn_text_encrypt);
		global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox9 [this.btn_text_encrypt]));
		w3.Position = 0;
		w3.Expand = false;
		w3.Fill = false;
		// Container child hbox9.Gtk.Box+BoxChild
		this.btn_text_decrypt = new global::Gtk.Button ();
		this.btn_text_decrypt.CanFocus = true;
		this.btn_text_decrypt.Name = "btn_text_decrypt";
		this.btn_text_decrypt.UseUnderline = true;
		this.btn_text_decrypt.Label = global::Mono.Unix.Catalog.GetString ("Decrypt");
		this.hbox9.Add (this.btn_text_decrypt);
		global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox9 [this.btn_text_decrypt]));
		w4.Position = 1;
		w4.Expand = false;
		w4.Fill = false;
		// Container child hbox9.Gtk.Box+BoxChild
		this.label6 = new global::Gtk.Label ();
		this.label6.Name = "label6";
		this.label6.LabelProp = global::Mono.Unix.Catalog.GetString ("Using Key:");
		this.hbox9.Add (this.label6);
		global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox9 [this.label6]));
		w5.Position = 2;
		w5.Expand = false;
		w5.Fill = false;
		// Container child hbox9.Gtk.Box+BoxChild
		this.entry5 = new global::Gtk.Entry ();
		this.entry5.CanFocus = true;
		this.entry5.Name = "entry5";
		this.entry5.IsEditable = true;
		this.hbox9.Add (this.entry5);
		global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.hbox9 [this.entry5]));
		w6.PackType = ((global::Gtk.PackType)(1));
		w6.Position = 3;
		this.vbox11.Add (this.hbox9);
		global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.vbox11 [this.hbox9]));
		w7.Position = 2;
		w7.Expand = false;
		w7.Fill = false;
		this.vbox10.Add (this.vbox11);
		global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.vbox10 [this.vbox11]));
		w8.Position = 0;
		w8.Expand = false;
		w8.Fill = false;
		// Container child vbox10.Gtk.Box+BoxChild
		this.vbox12 = new global::Gtk.VBox ();
		this.vbox12.Name = "vbox12";
		this.vbox12.Spacing = 6;
		// Container child vbox12.Gtk.Box+BoxChild
		this.GtkScrolledWindow = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow.Name = "GtkScrolledWindow";
		this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
		this.textview3 = new global::Gtk.TextView ();
		this.textview3.CanFocus = true;
		this.textview3.Name = "textview3";
		this.GtkScrolledWindow.Add (this.textview3);
		this.vbox12.Add (this.GtkScrolledWindow);
		global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.vbox12 [this.GtkScrolledWindow]));
		w10.Position = 0;
		// Container child vbox12.Gtk.Box+BoxChild
		this.lbl_Files = new global::Gtk.Label ();
		this.lbl_Files.Name = "lbl_Files";
		this.lbl_Files.LabelProp = global::Mono.Unix.Catalog.GetString ("File Encryption");
		this.vbox12.Add (this.lbl_Files);
		global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.vbox12 [this.lbl_Files]));
		w11.Position = 1;
		w11.Expand = false;
		w11.Fill = false;
		// Container child vbox12.Gtk.Box+BoxChild
		this.btn_OpenFile = new global::Gtk.Button ();
		this.btn_OpenFile.CanFocus = true;
		this.btn_OpenFile.Name = "btn_OpenFile";
		this.btn_OpenFile.UseUnderline = true;
		this.btn_OpenFile.Label = global::Mono.Unix.Catalog.GetString ("Open File");
		this.vbox12.Add (this.btn_OpenFile);
		global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.vbox12 [this.btn_OpenFile]));
		w12.Position = 2;
		w12.Expand = false;
		w12.Fill = false;
		this.vbox10.Add (this.vbox12);
		global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.vbox10 [this.vbox12]));
		w13.Position = 1;
		// Container child vbox10.Gtk.Box+BoxChild
		this.vbox13 = new global::Gtk.VBox ();
		this.vbox13.Name = "vbox13";
		this.vbox13.Spacing = 6;
		// Container child vbox13.Gtk.Box+BoxChild
		this.hbox10 = new global::Gtk.HBox ();
		this.hbox10.Name = "hbox10";
		this.hbox10.Spacing = 6;
		// Container child hbox10.Gtk.Box+BoxChild
		this.btn_file_encrypt = new global::Gtk.Button ();
		this.btn_file_encrypt.CanFocus = true;
		this.btn_file_encrypt.Name = "btn_file_encrypt";
		this.btn_file_encrypt.UseUnderline = true;
		this.btn_file_encrypt.Label = global::Mono.Unix.Catalog.GetString ("Encrypt");
		this.hbox10.Add (this.btn_file_encrypt);
		global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.hbox10 [this.btn_file_encrypt]));
		w14.Position = 0;
		w14.Expand = false;
		w14.Fill = false;
		// Container child hbox10.Gtk.Box+BoxChild
		this.btn_decrypt_encrypt = new global::Gtk.Button ();
		this.btn_decrypt_encrypt.CanFocus = true;
		this.btn_decrypt_encrypt.Name = "btn_decrypt_encrypt";
		this.btn_decrypt_encrypt.UseUnderline = true;
		this.btn_decrypt_encrypt.Label = global::Mono.Unix.Catalog.GetString ("Decrypt");
		this.hbox10.Add (this.btn_decrypt_encrypt);
		global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.hbox10 [this.btn_decrypt_encrypt]));
		w15.Position = 1;
		w15.Expand = false;
		w15.Fill = false;
		// Container child hbox10.Gtk.Box+BoxChild
		this.entry6 = new global::Gtk.Entry ();
		this.entry6.CanFocus = true;
		this.entry6.Name = "entry6";
		this.entry6.IsEditable = true;
		this.hbox10.Add (this.entry6);
		global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(this.hbox10 [this.entry6]));
		w16.PackType = ((global::Gtk.PackType)(1));
		w16.Position = 2;
		// Container child hbox10.Gtk.Box+BoxChild
		this.label7 = new global::Gtk.Label ();
		this.label7.Name = "label7";
		this.label7.LabelProp = global::Mono.Unix.Catalog.GetString ("Using Key:");
		this.hbox10.Add (this.label7);
		global::Gtk.Box.BoxChild w17 = ((global::Gtk.Box.BoxChild)(this.hbox10 [this.label7]));
		w17.PackType = ((global::Gtk.PackType)(1));
		w17.Position = 3;
		w17.Expand = false;
		w17.Fill = false;
		this.vbox13.Add (this.hbox10);
		global::Gtk.Box.BoxChild w18 = ((global::Gtk.Box.BoxChild)(this.vbox13 [this.hbox10]));
		w18.Position = 0;
		w18.Expand = false;
		w18.Fill = false;
		// Container child vbox13.Gtk.Box+BoxChild
		this.GtkScrolledWindow1 = new global::Gtk.ScrolledWindow ();
		this.GtkScrolledWindow1.Name = "GtkScrolledWindow1";
		this.GtkScrolledWindow1.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow1.Gtk.Container+ContainerChild
		this.textview4 = new global::Gtk.TextView ();
		this.textview4.CanFocus = true;
		this.textview4.Name = "textview4";
		this.GtkScrolledWindow1.Add (this.textview4);
		this.vbox13.Add (this.GtkScrolledWindow1);
		global::Gtk.Box.BoxChild w20 = ((global::Gtk.Box.BoxChild)(this.vbox13 [this.GtkScrolledWindow1]));
		w20.Position = 1;
		this.vbox10.Add (this.vbox13);
		global::Gtk.Box.BoxChild w21 = ((global::Gtk.Box.BoxChild)(this.vbox10 [this.vbox13]));
		w21.Position = 2;
		this.Add (this.vbox10);
		if ((this.Child != null)) {
			this.Child.ShowAll ();
		}
		this.DefaultWidth = 667;
		this.DefaultHeight = 411;
		this.Show ();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
		this.btn_text_encrypt.Clicked += new global::System.EventHandler (this.OnBtnTextEncryptClicked);
		this.btn_text_decrypt.Clicked += new global::System.EventHandler (this.OnBtnTextDecryptClicked);
		this.btn_OpenFile.Clicked += new global::System.EventHandler (this.OnBtnOpenFileClicked);
		this.btn_file_encrypt.Clicked += new global::System.EventHandler (this.OnBtnFileEncryptClicked);
		this.btn_decrypt_encrypt.Clicked += new global::System.EventHandler (this.OnBtnDecryptEncryptClicked);
	}
}
