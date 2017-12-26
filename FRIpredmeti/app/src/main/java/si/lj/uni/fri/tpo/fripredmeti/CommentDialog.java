package si.lj.uni.fri.tpo.fripredmeti;

/**
 * Created by timko on 18. 11. 2017.
 */

import android.app.Activity;
import android.app.Dialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.res.ColorStateList;
import android.graphics.Color;
import android.media.Image;
import android.net.Uri;
import android.util.Log;
import android.view.View;
import android.view.Window;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.RelativeLayout;
import android.widget.SeekBar;
import android.widget.TextView;

import java.util.ArrayList;
import java.util.HashMap;


import android.app.Activity;
import android.app.Dialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.res.ColorStateList;
import android.graphics.Color;
import android.net.Uri;
import android.support.v7.widget.CardView;
import android.util.Log;
import android.view.View;
import android.view.Window;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.SeekBar;
import android.widget.TextView;


import java.util.ArrayList;
import java.util.HashMap;

/**
 * Created by timko on 6. 09. 2017.
 */

public class CommentDialog  implements DialogInterface.OnDismissListener{
    private Activity mActivity;

    public CommentDialog(Activity a)
    {
        mActivity = a;
    }

    public Dialog showDialog(final Activity activity){
        final Dialog dialog = new Dialog(activity);
        mActivity = activity;
        dialog.requestWindowFeature(Window.FEATURE_NO_TITLE);
        dialog.setCanceledOnTouchOutside(true);
        dialog.setCancelable(true);
        dialog.setContentView(R.layout.comment_dialog);

        CardView cv = (CardView) dialog.findViewById(R.id.card_more);
        cv.setOnClickListener(new View.OnClickListener()
        {
            @Override
            public void onClick(View v)
            {
                ImageView tv = (ImageView) v.findViewById(R.id.view_more);
                LinearLayout ll = (LinearLayout) v.getParent();
                RelativeLayout rl = (RelativeLayout) ll.getChildAt(0);
                if(rl.getVisibility() == View.GONE)
                {
                    rl.setVisibility(View.VISIBLE);
                    tv.setBackgroundResource(R.drawable.ic_expand_less_black_24dp);
                }
                else
                {
                    rl.setVisibility(View.GONE);
                    tv.setBackgroundResource(R.drawable.ic_keyboard_arrow_down_black_24dp);
                }
            }
        });


        Button add = (Button) dialog.findViewById(R.id.button2);
        add.setOnClickListener(new Button.OnClickListener() {
            @Override
            public void onClick(View v) {
                //TODO: save to DB
                dialog.dismiss();
            }
        });

        Button cancel = (Button) dialog.findViewById(R.id.button3);
        cancel.setOnClickListener(new Button.OnClickListener() {
            @Override
            public void onClick(View v) {
                dialog.dismiss();
            }
        });

        dialog.show();
        return dialog;
    }

    @Override
    public void onDismiss(DialogInterface dialog) {}
}