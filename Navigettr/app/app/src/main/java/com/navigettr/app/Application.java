package com.navigettr.app;

import org.acra.ACRA;
import org.acra.ReportingInteractionMode;
import org.acra.annotation.ReportsCrashes;

@ReportsCrashes(applicationLogFile = "", // will not be used
        //mailTo = "Creativeideaspune@gmail.com",
        mailTo = "info@navigettr.com",
        mode = ReportingInteractionMode.TOAST, resToastText = R.string.crashed)

public class Application extends android.app.Application {

    @Override
    public void onCreate() {
        super.onCreate();
        try {
            ACRA.init(this);
        } catch (RuntimeException e) {
        }
    }
}
