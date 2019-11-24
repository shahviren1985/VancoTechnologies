package com.vanco.merlinapp.notification;

import android.util.Log;

import com.google.firebase.messaging.FirebaseMessagingService;
import com.google.firebase.messaging.RemoteMessage;

import org.json.JSONObject;


public class MyFirebaseMessagingService extends FirebaseMessagingService {

    private static final String TAG = MyFirebaseMessagingService.class.getSimpleName();


    @Override
    public void onMessageReceived(RemoteMessage remoteMessage) {
        Log.e(TAG, "From: " + remoteMessage.getFrom());

        if (remoteMessage == null)
            return;

       /* // Check if message contains a notification payload.
        if (remoteMessage.getNotification() != null) {
            Log.e(TAG, "Notification Body: " + remoteMessage.getNotification().getBody());
            handleNotification(remoteMessage.getNotification().getBody());
        }
*/
        // Check if message contains a data payload.

        if (remoteMessage.getData().size() > 0) {
            Log.e(TAG, "Data Payload: " + remoteMessage.getData().toString());
            try {

                JSONObject json = new JSONObject(remoteMessage.getData());
                handleDataMessage(json);
            } catch (Exception e) {
                e.printStackTrace();
                Log.e(TAG, "Exception: " + e.getMessage());
            }
        }
    }

    private void handleNotification(String message) {

    }

    private void handleDataMessage(JSONObject data) {

        /*WinningNotification winningNotification = new WinningNotification();
        winningNotification.setCouponId(Integer.parseInt(data.optString("coupon_id")));
        winningNotification.setCouponTitle(data.optString("coupon_title"));
        winningNotification.setDrawDate(data.optString("draw_date"));
        winningNotification.setCouponPrice(Double.parseDouble(data.optString("coupon_price")));
        winningNotification.setWinningPrice(Double.parseDouble(data.optString("winning_price")));
        winningNotification.setCouponDescription(data.optString("coupon_description"));
        winningNotification.setImage(data.optString("image"));

        if (!NotificationUtils.isAppIsInBackground(getApplicationContext())) {
            Intent resultIntent = new Intent(getApplicationContext(), FragmentContainerActivity.class);
            resultIntent.putExtra("param", WinDetailFragment.class.getSimpleName());
            resultIntent.putExtra("message", winningNotification);

            String winMessage = "Woooh!\nYou have win Rs." + winningNotification.getWinningPrice() + " by your GetLottery app \'" + winningNotification.getCouponTitle() + "\'  lottery ticket.";
            showNotificationMessage(getApplicationContext(), getString(R.string.app_name) + "- " + winningNotification.getCouponTitle(), winMessage, System.currentTimeMillis(), resultIntent,winningNotification);
        } else {
            // app is in background, show the notification in notification tray
            Intent resultIntent = new Intent(getApplicationContext(), FragmentContainerActivity.class);
            resultIntent.putExtra("param", WinDetailFragment.class.getSimpleName());
            resultIntent.putExtra("message", winningNotification);

            String winMessage = "Woooh!\nYou have win Rs." + winningNotification.getWinningPrice() + " by your GetLottery app " + winningNotification.getCouponTitle() + " lottery ticket.";
            showNotificationMessage(getApplicationContext(), getString(R.string.app_name) + " " + winningNotification.getCouponTitle(), winMessage, System.currentTimeMillis(), resultIntent,winningNotification);
            // check for image attachment
            //   if (TextUtils.isEmpty(imageUrl)) {


            // } else {
            // image is present, show notification with image
            //    showNotificationMessageWithBigImage(getApplicationContext(), title, message, timestamp, resultIntent, imageUrl);
            //}
        }*/

    }



}