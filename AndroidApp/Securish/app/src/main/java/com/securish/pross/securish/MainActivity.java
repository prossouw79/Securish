package com.securish.pross.securish;

import android.content.ClipData;
import android.content.ClipboardManager;
import android.content.Intent;
import android.os.Bundle;
import android.support.design.widget.FloatingActionButton;
import android.support.design.widget.Snackbar;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.View;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.RadioButton;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

public class MainActivity extends AppCompatActivity {

    String algorithm = "VERNAM";
    String enc_cipherText = "";
    String dec_plainText = "";
    boolean encrypt = false;
    boolean modeChosen = false;
    ClipboardManager clipboard;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);
        clipboard = (ClipboardManager) getSystemService(CLIPBOARD_SERVICE);



        FloatingActionButton fab = (FloatingActionButton) findViewById(R.id.fab);
        fab.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if(enc_cipherText != "")
                {
                    Intent sharingIntent = new Intent(android.content.Intent.ACTION_SEND);
                    sharingIntent.setType("text/plain");
                    sharingIntent.putExtra(android.content.Intent.EXTRA_SUBJECT, "You received some encrypted text! Secured by Securish!");
                    sharingIntent.putExtra(android.content.Intent.EXTRA_TEXT, "Encrypted text:    " + enc_cipherText);
                    startActivity(Intent.createChooser(sharingIntent, "Share via:"));

                }
            }
        });

        Spinner spinner = (Spinner) findViewById(R.id.spinner);
        ArrayAdapter<CharSequence> adapter = ArrayAdapter.createFromResource(this,
                R.array.algorithms, android.R.layout.simple_spinner_item);
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        spinner.setAdapter(adapter);
        spinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {

                switch (position) {
                    case 0:
                        algorithm = "VIGENERE";
                        break;
                    case 1:
                        algorithm = "SUBSTITUTION";
                        break;
                    case 2:
                        algorithm = "TRANSPOSITION";
                        break;
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {

                // sometimes you need nothing here
            }
        });


        Button clickButton = (Button) findViewById(R.id.btn_go);
        clickButton.setOnClickListener( new View.OnClickListener() {

            @Override
            public void onClick(View v) {
                buttonClick();
            }
        });

    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu_main, menu);
        return true;
    }

    private void buttonClick() {
        makeSnackbar("Current algorithm is " + algorithm);
        TextView txtOut = (TextView) findViewById(R.id.txt_output);
        TextView txtIn = (TextView) findViewById(R.id.txt_plaintext);
        TextView txtKey = (TextView) findViewById(R.id.txt_key);
        Crypto crypt = new Crypto();


        String plaintext = txtIn.getText().toString();
        String keytext = txtKey.getText().toString();
        String result = "";

        char[] text = plaintext.toCharArray();
        char[] key = keytext.toCharArray();


        if(!modeChosen)
            makeSnackbar("Please select mode!");
        else {
                if (plaintext.length() == 0 || keytext.length() == 0) {
                    makeSnackbar("Please provide input!");
                } else {
                    try {
                    switch (algorithm) {
                        case "VIGENERE": {
                            if (encrypt) {
                                result = crypt.DoVigenere(plaintext, keytext, true);
                                makeSnackbar("RESULT:\t" + result);
                                txtOut.setText("ENCRYPTED:  " + result + "\n\n Use Envelope Button to share!");
                                enc_cipherText = result;
                            } else {
                                result = crypt.DoVigenere(plaintext, keytext, false);
                                makeSnackbar("RESULT:\t" + result);
                                txtOut.setText("DECRYPTED:  " + result + "\n\n Use Envelope Button to share!");
                                dec_plainText = result;
                            }
                            break;
                        }
                        case "SUBSTITUTION": {
                            if (encrypt) {
                                result = crypt.DoSubstitutionText(plaintext, 5, true);
                                makeSnackbar("RESULT:\t" + result);
                                txtOut.setText("Result is:  " + result + "\n\n Use Envelope Button to share!");
                                enc_cipherText = result;
                            } else {
                                result = crypt.DoSubstitutionText(plaintext, 5, false);
                                makeSnackbar("RESULT:\t" + result);
                                txtOut.setText("Result is:  " + result + "\n\n Use Envelope Button to share!");
                                dec_plainText = result;
                            }
                            break;
                        }
                        case "TRANSPOSITION": {
                            if (encrypt) {
                                result = crypt.doTransposition(text, true);
                                makeSnackbar("RESULT:\t" + result);
                                txtOut.setText("Result is:  " + result + "\n\n Use Envelope Button to share!");
                                enc_cipherText = result;
                            } else {
                                result = crypt.doTransposition(text, false);
                                makeSnackbar("RESULT:\t" + result);
                                txtOut.setText("Result is:  " + result + "\n\n Use Envelope Button to share!");
                                dec_plainText = result;
                            }
                            break;
                        }

                    }
                }
                    catch(Exception e){
                        txtOut.setText(e.getMessage());
                    }

                }
            if (encrypt)
                addToClipboard(enc_cipherText);
            else
                addToClipboard(dec_plainText);
            }
        }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();

        makeSnackbar("Settings");

        //noinspection SimplifiableIfStatement
        if (id == R.id.action_settings) {
            return true;
        }
        return super.onOptionsItemSelected(item);
    }


    private void makeSnackbar(String shortmsg)
    {

        Snackbar.make(findViewById(android.R.id.content), shortmsg, Snackbar.LENGTH_LONG)
                .show();

    }
    public void onRadioButtonClicked(View view) {
        // Is the button now checked?
        boolean checked = ((RadioButton) view).isChecked();

        // Check which radio button was clicked
        switch(view.getId()) {
            case R.id.radio_encrypt:
                if (checked)
                    encrypt = true;
                   // makeSnackbar("ENCRYPT");
                    break;
            case R.id.radio_decrypt:
                if (checked)
                    encrypt = false;
                   // makeSnackbar("DECRYPT");
                    break;
        }
        modeChosen = true;
    }

    private void addToClipboard(String txt)
    {
        ClipData clip = ClipData.newPlainText("Securish text",txt);
        clipboard.setPrimaryClip(clip);
    }


}
