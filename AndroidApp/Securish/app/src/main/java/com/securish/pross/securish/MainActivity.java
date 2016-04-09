package com.securish.pross.securish;

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
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

public class MainActivity extends AppCompatActivity {

    String algorithm = "VERNAM";
    String cipherText = "";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);

        FloatingActionButton fab = (FloatingActionButton) findViewById(R.id.fab);
        fab.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if(cipherText != "")
                {
                    Intent sharingIntent = new Intent(android.content.Intent.ACTION_SEND);
                    sharingIntent.setType("text/plain");
                    sharingIntent.putExtra(android.content.Intent.EXTRA_SUBJECT, "You received some encrypted text! Secured by Securish!");
                    sharingIntent.putExtra(android.content.Intent.EXTRA_TEXT, "Encrypted text:    " + cipherText);
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
        try {
            switch (algorithm) {
                case "VIGENERE": {
                    result = crypt.DoVigenere(plaintext, keytext, true);
                    makeSnackbar("RESULT:\t" + result);
                    txtOut.setText("Result is:  " + result + "\n\n Use Envelope Button to share this ciphertext!");
                    cipherText = result;
                    break;
                }
                case "SUBSTITUTION": {
                    result = crypt.DoSubstitutionText(plaintext, 5, true);
                    makeSnackbar("RESULT:\t" + result);
                    txtOut.setText("Result is:  " + result + "\n\n Use Envelope Button to share this ciphertext!");
                    cipherText = result;
                    break;
                }
                case "TRANSPOSITION": {
                    result = crypt.doTransposition(text, true);
                    makeSnackbar("RESULT:\t" + result);
                    txtOut.setText("Result is:  " + result + "\n\n Use Envelope Button to share this ciphertext!");
                    cipherText = result;
                    break;
                }
            }
        }catch (Exception e) {
            txtOut.setText(e.getMessage());
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


}
