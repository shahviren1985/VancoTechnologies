package com.sringara.sringara;

import android.Manifest;
import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Matrix;
import android.os.Bundle;
import android.os.Handler;
import android.support.v4.app.ActivityCompat;
import android.support.v4.content.ContextCompat;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.util.DisplayMetrics;
import android.util.Log;
import android.util.TypedValue;
import android.view.View;
import android.view.WindowManager;
import android.widget.FrameLayout;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;

import com.sringara.sringara.adapter.ListAdapter;
import com.sringara.sringara.models.ProductList;

import org.opencv.android.BaseLoaderCallback;
import org.opencv.android.CameraBridgeViewBase;
import org.opencv.android.CameraBridgeViewBase.CvCameraViewFrame;
import org.opencv.android.CameraBridgeViewBase.CvCameraViewListener2;
import org.opencv.android.LoaderCallbackInterface;
import org.opencv.android.OpenCVLoader;
import org.opencv.core.Core;
import org.opencv.core.Mat;
import org.opencv.core.MatOfRect;
import org.opencv.core.Point;
import org.opencv.core.Rect;
import org.opencv.core.Size;
import org.opencv.objdetect.CascadeClassifier;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.util.ArrayList;
import java.util.Collections;

public class MainActivity extends Activity implements CvCameraViewListener2 {

    private static final String    TAG                 = "OCVSample::Activity";
    public static final int        JAVA_DETECTOR       = 0;
    public static final int        NATIVE_DETECTOR     = 1;

    private Mat mRgba;
    private Mat mGray;
    private File                   mCascadeFile;
    private CascadeClassifier mJavaDetector;
    private DetectionBasedTracker  mNativeDetector;

    private int                    mDetectorType       = NATIVE_DETECTOR;
    private String[]               mDetectorName;

    private float                  mRelativeFaceSize   = 0.2f;
    private int                    mAbsoluteFaceSize   = 0;

    private boolean isEaring = false;

    private CameraBridgeViewBase mOpenCvCameraView;

    private BaseLoaderCallback mLoaderCallback = new BaseLoaderCallback(this) {
        @Override
        public void onManagerConnected(int status) {
            switch (status) {
                case LoaderCallbackInterface.SUCCESS:
                {
                    Log.i(TAG, "OpenCV loaded successfully");

                    System.loadLibrary("detection_based_tracker");

                    try {
                        InputStream is = getResources().openRawResource(R.raw.haarcascade_frontalface_default);
                        File cascadeDir = getDir("cascade", Context.MODE_PRIVATE);
                        mCascadeFile = new File(cascadeDir, "haarcascade_frontalface_default.xml");
                        FileOutputStream os = new FileOutputStream(mCascadeFile);

                        byte[] buffer = new byte[4096];
                        int bytesRead;
                        while ((bytesRead = is.read(buffer)) != -1) {
                            os.write(buffer, 0, bytesRead);
                        }
                        is.close();
                        os.close();

                        mJavaDetector = new CascadeClassifier(mCascadeFile.getAbsolutePath());
                        if (mJavaDetector.empty()) {
                            Log.e(TAG, "Failed to load cascade classifier");
                            mJavaDetector = null;
                        } else
                            Log.i(TAG, "Loaded cascade classifier from " + mCascadeFile.getAbsolutePath());

                        mNativeDetector = new DetectionBasedTracker(mCascadeFile.getAbsolutePath(), 0);

                        cascadeDir.delete();

                    } catch (IOException e) {
                        e.printStackTrace();
                        Log.e(TAG, "Failed to load cascade. Exception thrown: " + e);
                    }

                    mOpenCvCameraView.enableView();
                } break;
                default:
                {
                    super.onManagerConnected(status);
                } break;
            }
        }
    };
    private Rect rect;
    private ImageView ivEaringLeftOverlay;
    private ImageView ivEaringRightOverlay;
    private RecyclerView rvList;
    private TextView mTvDistance;
    private String selectType;
    private ImageView mIvBack;
    private String id;
    private SqlliteDatabaseHelper databaseHelper;
    private ListAdapter adapter;
    private LinearLayout mLlPrgLoader;
    private ImageView ivNeckLaceOverlay;
    private boolean isOnlyEarringPoint = false;

    public MainActivity() {
        mDetectorName = new String[2];
        mDetectorName[JAVA_DETECTOR] = "Java";
        mDetectorName[NATIVE_DETECTOR] = "Native (tracking)";

        Log.i(TAG, "Instantiated new " + this.getClass());
    }



    private boolean checkPermission() {
        if (ContextCompat.checkSelfPermission(this, Manifest.permission.CAMERA)
                != PackageManager.PERMISSION_GRANTED) {
            return false;
        }
        return true;
    }

    private void requestPermission() {

        ActivityCompat.requestPermissions(this,
                new String[]{Manifest.permission.CAMERA},
                101);
    }

    ArrayList<ProductList> listsItem = new ArrayList<ProductList>();
    @Override
    public void onCreate(Bundle savedInstanceState) {
        Log.i(TAG, "called onCreate");
        super.onCreate(savedInstanceState);
        getWindow().addFlags(WindowManager.LayoutParams.FLAG_KEEP_SCREEN_ON);

        setContentView(R.layout.activity_main);

        rvList = (RecyclerView) findViewById(R.id.rvList);
        ivEaringLeftOverlay = (ImageView) findViewById(R.id.ivEaringLeftOverlay);
        ivEaringRightOverlay = (ImageView) findViewById(R.id.ivEaringRightOverlay);
        mLlPrgLoader = (LinearLayout) findViewById(R.id.llPrgLoader);
        mLlPrgLoader.setVisibility(View.VISIBLE);
        mIvBack = (ImageView) findViewById(R.id.ivBack);
        mTvDistance = (TextView) findViewById(R.id.tvDistance);
        ivNeckLaceOverlay = (ImageView) findViewById(R.id.ivNeckLaceOverlay);

        mOpenCvCameraView = (CameraBridgeViewBase) findViewById(R.id.fd_activity_surface_view);
        mOpenCvCameraView.setVisibility(CameraBridgeViewBase.VISIBLE);

        mIvBack.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                finish();
            }
        });

        databaseHelper = new SqlliteDatabaseHelper(MainActivity.this);

        mOpenCvCameraView.setCvCameraViewListener(this);

        rvList.setLayoutManager(new LinearLayoutManager(this));

        if (checkPermission()) {
        } else {
            requestPermission();
        }

        if(getIntent().getExtras() != null) {
            selectType = getIntent().getStringExtra("selectType");
            id = getIntent().getStringExtra("id");
            selectProductType(selectType);
        }
        Collections.reverse(listsItem);

    }

    private void selectItem(int position) {
        if(!com.sringara.sringara.helper.Utils.isStringNull(listsItem.get(position).getImagePath())) {
            if (listsItem.get(position).getProductFilter().equals("Earring")) {
                BitmapFactory.Options options = new BitmapFactory.Options();
                options.inPreferredConfig = Bitmap.Config.ARGB_8888;
                Bitmap image = BitmapFactory.decodeFile(getFilesDir() + "/" + listsItem.get(position).getImagePath(), options);
                Matrix matrix = new Matrix();

                matrix.postRotate(270);
                image = Bitmap.createBitmap(image, 0, 0, image.getWidth(), image.getHeight(), matrix, true);
                image.setHasAlpha(true);

                isEaring = true;
                ivEaringLeftOverlay.setImageBitmap(image);
                ivEaringRightOverlay.setImageBitmap(image);

                isDisplay = false;

                runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        lastResizeDistance = -1;
                        lastImageSize = 0;
                        countDisplayStart = 0;
                        ivNeckLaceOverlay.setVisibility(View.GONE);
                    }
                });
            } else {
                BitmapFactory.Options options = new BitmapFactory.Options();
                options.inPreferredConfig = Bitmap.Config.ARGB_8888;
                Bitmap image = BitmapFactory.decodeFile(getFilesDir() + "/" + listsItem.get(position).getImagePath(), options);
                Matrix matrix = new Matrix();

                matrix.postRotate(270);
                image = Bitmap.createBitmap(image, 0, 0, image.getWidth(), image.getHeight(), matrix, true);
                image.setHasAlpha(true);

                ivNeckLaceOverlay.setImageBitmap(image);
                isEaring = false;

                isDisplay = false;


                runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        lastResizeDistance = -1;
                        lastImageSize = 0;
                        countDisplayStart = 0;
                        ivEaringLeftOverlay.setVisibility(View.GONE);
                        ivEaringRightOverlay.setVisibility(View.GONE);

                    }
                });
            }
        }
    }

    private void selectProductType(String typeFilter) {
        selectType = typeFilter;
        int selectPosition = -1;

        if(typeFilter.equals("Earring")) {
            setEarrings();
        } else if(typeFilter.equals("Necklace")) {
            setNeckLace();
        } else {
            setPendant();
        }

        for(int i=0;i<listsItem.size();i++) {
            if(listsItem.get(i).getDb_id().equals(id)) {
                selectItem(i);
                selectPosition = i;
                break;
            }
        }

        if(selectPosition != -1) {
            selectPosition = listsItem.size() - (selectPosition + 1);
        }

        adapter = new ListAdapter(listsItem, this, new ListAdapter.OnItemClickListener() {
            @Override
            public void setOnItemClick(int position) {
                selectItem(position);
            }
        }, selectPosition);

        rvList.setAdapter(adapter);


        final int finalSelectPosition = selectPosition;
        new android.os.Handler().postDelayed(new Runnable() {
            @Override
            public void run() {

                if(finalSelectPosition != -1) {
                    rvList.scrollToPosition(finalSelectPosition);
                }
            }
        }, 500);
    }

    private void setEarrings() {
        listsItem.clear();
        selectType = "Earring";
        listsItem.addAll(databaseHelper.getList(selectType));
    }

    private void setNeckLace() {
        listsItem.clear();
        selectType = "Necklace";
        listsItem.addAll(databaseHelper.getList(selectType));
    }

    private void setPendant() {
        listsItem.clear();
        selectType = "Pendant";
        listsItem.addAll(databaseHelper.getList(selectType));
    }

    @Override
    public void onRequestPermissionsResult(int requestCode, String permissions[], int[] grantResults) {
        switch (requestCode) {
            case 101:
                if (grantResults.length > 0 && grantResults[0] == PackageManager.PERMISSION_GRANTED) {
                    Intent intent = new Intent(MainActivity.this, MainActivity.class);
                    intent.putExtra("id", id);
                    intent.putExtra("selectType", selectType);
                    startActivity(intent);
                    finish();
                } else {
                    Toast.makeText(getApplicationContext(), "Permission Denied", Toast.LENGTH_SHORT).show();

                }
                break;
        }
    }

    @Override
    public void onPause()
    {
        super.onPause();
        if (mOpenCvCameraView != null)
            mOpenCvCameraView.disableView();
    }

    @Override
    public void onResume()
    {
        super.onResume();
        new Handler().postDelayed(new Runnable() {
            @Override
            public void run() {
                if(MainActivity.this != null) {
                    if (!OpenCVLoader.initDebug()) {
                        Log.d(TAG, "Internal OpenCV library not found. Using OpenCV Manager for initialization");
                        OpenCVLoader.initAsync(OpenCVLoader.OPENCV_VERSION_3_0_0, MainActivity.this, mLoaderCallback);
                    } else {
                        Log.d(TAG, "OpenCV library found inside package. Using it!");
                        mLoaderCallback.onManagerConnected(LoaderCallbackInterface.SUCCESS);
                    }
                    if(mLlPrgLoader != null) {
                        mLlPrgLoader.setVisibility(View.GONE);
                    }
                }
            }
        }, 150);
    }

    public void onDestroy() {
        super.onDestroy();
        mOpenCvCameraView.disableView();
    }

    public void onCameraViewStarted(int width, int height) {
        mGray = new Mat();
        mRgba = new Mat();
    }

    public void onCameraViewStopped() {
        mGray.release();
        mRgba.release();
    }

    boolean isDisplay = false;
    double lastDistance = 0;
    int countDisplayStart = 0;
    int lastImageSize = 0;
    double lastResizeDistance = -1;

    public Mat onCameraFrame(CvCameraViewFrame inputFrame) {
        try {
            System.gc();
            mGray.release();
            mRgba.release();

            mRgba = inputFrame.rgba();
            mGray = inputFrame.gray();

            if (mAbsoluteFaceSize == 0) {
                int height = 250;//mGray.rows();
                if (Math.round(height * mRelativeFaceSize) > 0) {
                    mAbsoluteFaceSize = Math.round(height * mRelativeFaceSize);
                }
                mNativeDetector.setMinFaceSize(mAbsoluteFaceSize);
            }

            MatOfRect faces = new MatOfRect();
            Core.transpose(mGray, mGray);
            Core.flip(mGray, mGray, 0);
            Core.flip(mRgba, mRgba, 1);

            if (mDetectorType == NATIVE_DETECTOR) {
                if (mNativeDetector != null)
                    mNativeDetector.detect(mGray, faces);
            } else if (mDetectorType == JAVA_DETECTOR) {
                if (mJavaDetector != null)
                    mJavaDetector.detectMultiScale(mGray, faces, 1.5, 1, 1, // TODO: objdetect.CV_HAAR_SCALE_IMAGE
                            new Size(mAbsoluteFaceSize, mAbsoluteFaceSize), new Size());
            } else {
                Log.e(TAG, "Detection method is not selected!");
            }

            Rect[] facesArray = faces.toArray();
            final Point tl = new Point();
            final Point br = new Point();

            if(facesArray.length == 0) {
                runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        ivEaringLeftOverlay.setVisibility(View.GONE);
                        ivEaringRightOverlay.setVisibility(View.GONE);
                        ivNeckLaceOverlay.setVisibility(View.GONE);
                    }
                });
            }

            DisplayMetrics displayMetrics = new DisplayMetrics();
            getWindowManager().getDefaultDisplay().getMetrics(displayMetrics);

            int cols = mRgba.cols();
            int rows = mRgba.rows();

            final int xOffset = (mOpenCvCameraView.getWidth() - cols) / 2;
            final int yOffset = (mOpenCvCameraView.getHeight() - rows) / 2;

            for (int i = 0, f = facesArray.length; i < f; i++) {
                if (i <= 0) {
                    tl.x = facesArray[i].tl().y;
                    tl.y = facesArray[i].tl().x;
                    br.x = facesArray[i].br().y;
                    br.y = facesArray[i].br().x;

                    final double dista = faceDistance(facesArray[i].width, facesArray[i].tl(), facesArray[i].br(), mGray.height());
                    final int faceDis = (int)dista;

                    if(faceDis <=20 || faceDis>=55) {
                        runOnUiThread(new Runnable() {
                            @Override
                            public void run() {
                                ivEaringLeftOverlay.setVisibility(View.GONE);
                                ivEaringRightOverlay.setVisibility(View.GONE);
                                ivNeckLaceOverlay.setVisibility(View.GONE);
                            }
                        });
                        break;
                    }

                    if(isDisplay && isEaring && Math.abs(lastDistance - dista) <= 0.6) {

                    } else if(isDisplay && !isEaring && Math.abs(lastDistance - dista) <= 0.4){

                    } else {

                        if (!isEaring) {
                            runOnUiThread(new Runnable() {
                                @Override
                                public void run() {
                                    int margin = 0;
                                    if(lastResizeDistance == -1 || Math.abs(lastResizeDistance - dista) >= 3) {
                                        if (dista > 40) {
                                            if (xOffset > 50) {
                                                lastImageSize = 85;
                                            } else {
                                                lastImageSize = 85;
                                            }
                                        } else if (dista > 35) {
                                            if (xOffset > 50) {
                                                lastImageSize = 100;
                                            } else {
                                                lastImageSize = 100;
                                            }
                                        } else if (dista > 25) {
                                            if (xOffset > 50) {
                                                lastImageSize = 125;
                                            } else {
                                                lastImageSize = 125;
                                            }
                                        } else {
                                            if (xOffset > 50) {
                                                margin += 25;
                                            }
                                            lastImageSize = 145;
                                        }
                                        lastResizeDistance = dista;
                                    }
                                    mTvDistance.setText("" + faceDis +"\n\n NS "+lastImageSize);
                                    setNecklaceImage(lastImageSize);
                                    ivNeckLaceOverlay.setX((int) br.x + xOffset + margin);
                                    ivNeckLaceOverlay.setY((int) (((tl.y+br.y)/2) - ((int)(ivNeckLaceOverlay.getHeight() / 2.050)))  + yOffset);
                                    ivNeckLaceOverlay.setVisibility(View.VISIBLE);
                                    ivNeckLaceOverlay.bringToFront();
                                    isDisplay = true;

                                    lastDistance = dista;
                                    countDisplayStart++;
                                }
                            });
                        }
                        if (isEaring) {
                            runOnUiThread(new Runnable() {
                                @Override
                                public void run() {
                                    if(lastResizeDistance == -1 || Math.abs(lastResizeDistance - dista) >= 3) {
                                        if (dista > 40) {
                                            if (isOnlyEarringPoint) {
                                                lastImageSize = 8;
                                            } else {
                                                lastImageSize = 20;
                                            }
                                        } else if (dista > 35) {
                                            if (isOnlyEarringPoint) {
                                                lastImageSize = 16;
                                            } else {
                                                lastImageSize = 24;
                                            }
                                        } else if (dista > 25) {
                                            if (isOnlyEarringPoint) {
                                                lastImageSize = 16;
                                            } else {
                                                lastImageSize = 27;
                                            }
                                        } else {
                                            if (isOnlyEarringPoint) {
                                                lastImageSize = 22;
                                            } else {
                                                lastImageSize = 35;
                                            }
                                        }
                                        lastResizeDistance = dista;
                                    }
                                    mTvDistance.setText("" + faceDis +"\n\n ES "+lastImageSize);
                                    setLeftEaringImage(lastImageSize);
                                    setRightEaringImage(lastImageSize);
                                }
                            });

                            earOverlay(facesArray, i, br, tl, faceDis);
                            earRightOverlay(facesArray, i, br, tl, faceDis);
                            isDisplay = true;

                            lastDistance = dista;
                            countDisplayStart++;
                        }
                    }
                } else {
                    break;
                }
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
        return mRgba;
    }

    private void setNecklaceImage(int width) {
        FrameLayout.LayoutParams params = (FrameLayout.LayoutParams) ivNeckLaceOverlay.getLayoutParams();
        int height = (int) TypedValue.applyDimension(TypedValue.COMPLEX_UNIT_DIP, width, getResources().getDisplayMetrics());
        params.width = height;
        ivNeckLaceOverlay.setLayoutParams(params);
        ivNeckLaceOverlay.requestLayout();
    }



    private void setLeftEaringImage(int width) {
        FrameLayout.LayoutParams params = (FrameLayout.LayoutParams) ivEaringLeftOverlay.getLayoutParams();
        int height = (int) TypedValue.applyDimension(TypedValue.COMPLEX_UNIT_DIP, width, getResources().getDisplayMetrics());
        params.width = height;
        ivEaringRightOverlay.setLayoutParams(params);
    }


    private void setRightEaringImage(int width) {
        FrameLayout.LayoutParams params = (FrameLayout.LayoutParams) ivEaringRightOverlay.getLayoutParams();
        int height = (int) TypedValue.applyDimension(TypedValue.COMPLEX_UNIT_DIP, width, getResources().getDisplayMetrics());
        params.width = height;
        ivEaringRightOverlay.setLayoutParams(params);
    }

    public static final double FACE_WIDTH = 10;
    static Point p1 = new Point(10,10);
    static Point p2 = new Point(100,70);

    public double faceDistance(double facePixelWidth, Point p1, Point p2, double PIC_WIDTH)
    {
        int slope = (int) ((p2.y-p1.y)/(p2.x-p1.x));
        double propOfPic = PIC_WIDTH / facePixelWidth;
        double surfSize = propOfPic * FACE_WIDTH;
        double dist = surfSize / slope;
        return dist;
    }

    private void earOverlay(final Rect[] facesArray, final int i, final Point br, final Point tl, final int distance) {
        try {
            try {
                runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        DisplayMetrics displayMetrics = new DisplayMetrics();
                        getWindowManager().getDefaultDisplay().getMetrics(displayMetrics);
                        final int height = displayMetrics.heightPixels;
                        final int width = displayMetrics.widthPixels;
                        Point point = new Point();
                        point.x = (int) (int) (tl.x + facesArray[i].width / 2) + 50  + (Math.abs(mGray.height()-width)/2);
                        point.y = (int) (br.y - (facesArray[i].height * 0.13));



                        int cols = mRgba.cols();
                        int rows = mRgba.rows();

                        int xOffset = (mOpenCvCameraView.getWidth() - cols) / 2;
                        final int yOffset = (mOpenCvCameraView.getHeight() - rows) / 2;


                        int upSideCoordinate =  (distance <= 25) ? 15 :  40;

                        ivEaringLeftOverlay.setX((int) (int) (tl.x + facesArray[i].width / 2) + upSideCoordinate  + xOffset);
                        if(yOffset > 10) {
                            ivEaringLeftOverlay.setX((int) (int) (tl.x + facesArray[i].width / 2) + upSideCoordinate  + xOffset + 10);
                            ivEaringLeftOverlay.setY((int) ((br.y) + yOffset + 1));
                        } else {
                            double leftSideCoordinate =  (distance <= 25) ? (facesArray[i].height * 0.14) :  (facesArray[i].height * 0.16);
                            ivEaringLeftOverlay.setY((int) ((br.y) + yOffset - leftSideCoordinate));
                        }

                        ivEaringLeftOverlay.setVisibility(View.VISIBLE);
                        ivNeckLaceOverlay.bringToFront();

                    }
                });

            } catch (Exception e) {
                e.printStackTrace();
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    private void earRightOverlay(final Rect[] facesArray, final int i, Point br, final Point tl, final int distance) {
        try {
            try {
                runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        DisplayMetrics displayMetrics = new DisplayMetrics();
                        getWindowManager().getDefaultDisplay().getMetrics(displayMetrics);
                        Point point = new Point();
                        point.x = (int) (int) (tl.x + facesArray[i].width / 2) + 50;
                        point.y = (int) (tl.y) + ((facesArray[i].height * 0.10));


                        int cols = mRgba.cols();
                        int rows = mRgba.rows();

                        int xOffset = (mOpenCvCameraView.getWidth() - cols) / 2;
                        final int yOffset = (mOpenCvCameraView.getHeight() - rows) / 2;

                        int upSideCoordinate =  (distance <= 25) ? 15 :  40;

                        ivEaringRightOverlay.setX((int) (int) (tl.x + facesArray[i].width / 2) + upSideCoordinate  + xOffset);
                        if(yOffset > 10) {
                            ivEaringLeftOverlay.setX((int) (int) (tl.x + facesArray[i].width / 2) + upSideCoordinate  + xOffset + 10);
                            ivEaringRightOverlay.setY((int) ((tl.y) + yOffset - 20));
                        } else {
                            double rightSideCoordinate =  (distance < 35) ? (distance <= 25) ? (tl.y * 0.36) :  (tl.y * 0.14) : (tl.y * 0.03);
                            ivEaringRightOverlay.setY((int) ((tl.y) + yOffset + rightSideCoordinate));// - 10
                        }

                        ivEaringRightOverlay.setVisibility(View.VISIBLE);
                        ivNeckLaceOverlay.bringToFront();
                    }
                });

            } catch (Exception e) {
                e.printStackTrace();
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
    }


    private void setMinFaceSize(float faceSize) {
        mRelativeFaceSize = faceSize;
        mAbsoluteFaceSize = 0;
    }

    private void setDetectorType(int type) {
        if (mDetectorType != type) {
            mDetectorType = type;

            if (type == NATIVE_DETECTOR) {
                Log.i(TAG, "Detection Based Tracker enabled");
                mNativeDetector.start();
            } else {
                Log.i(TAG, "Cascade detector enabled");
                mNativeDetector.stop();
            }
        }
    }
}
