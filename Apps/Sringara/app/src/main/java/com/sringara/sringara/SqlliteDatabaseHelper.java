package com.sringara.sringara;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

import com.sringara.sringara.helper.Utils;
import com.sringara.sringara.models.ProductList;

import java.util.ArrayList;

public class SqlliteDatabaseHelper extends SQLiteOpenHelper {

    private static final String DATABASE_NAME = "DBSringara";

    private static final int DATABASE_VERSION = 1;

    private static final String TABLE_NAME_PRODUCTS = "TB_PRODUCTS";

    private static final String FIELD_NAME_ID = "id";
    private static final String FIELD_NAME_PRODUCT_ID = "prod_id";
    private static final String FIELD_NAME_NAME = "name";
    private static final String FIELD_NAME_DESCRITPION = "description";
    private static final String FIELD_NAME_DESIGNER = "designer";
    private static final String FIELD_NAME_PROVIDER = "provider";
    private static final String FIELD_NAME_STONE_TYPE = "stone_type";
    private static final String FIELD_NAME_COLOR_FILTER = "color_filter";
    private static final String FIELD_NAME_PRODUCT_FILTER = "product_filter";
    private static final String FIELD_NAME_MAIN_IMAGES = "main_images";
    private static final String FIELD_NAME_IMAGES = "images";
    private static final String FIELD_NAME_AVAILABLE_QUANTITY = "available_quantity";
    private static final String FIELD_NAME_PRICE = "price";
    private static final String FIELD_NAME_AVAILABLE_CITY = "available_city";
    private static final String FIELD_NAME_IMAGE_LOCAL_PATH= "image_path";
    private static final String FIELD_NAME_DELIVERY_IN_DAYS = "delivery_in_days";
    private static final String FIELD_NAME_COURIRER_CHARGES = "courirer_charges";
    private static final String FIELD_NAME_IS_FAVOURITE = "is_favourite";

    public SqlliteDatabaseHelper(Context context) {
        super(context, DATABASE_NAME, null, DATABASE_VERSION);
    }

    private static final String PRODUCTS_TABLE_CREATE_QUERY = "create table "+TABLE_NAME_PRODUCTS+" ( "+FIELD_NAME_ID+" integer primary key,"+FIELD_NAME_PRODUCT_ID+" text,"+FIELD_NAME_NAME+" text,"+FIELD_NAME_DESCRITPION+" text,"+FIELD_NAME_DESIGNER+" text,"+FIELD_NAME_IMAGE_LOCAL_PATH+" text,"+FIELD_NAME_PROVIDER+" text,"+FIELD_NAME_STONE_TYPE+" text,"+FIELD_NAME_COLOR_FILTER+" text,"+FIELD_NAME_PRODUCT_FILTER+" text,"+FIELD_NAME_MAIN_IMAGES+" text,"+FIELD_NAME_IMAGES+" text,"+FIELD_NAME_AVAILABLE_QUANTITY+" text,"+FIELD_NAME_PRICE+" text,"+FIELD_NAME_AVAILABLE_CITY+" text,"+FIELD_NAME_DELIVERY_IN_DAYS+" text,"+FIELD_NAME_IS_FAVOURITE +" INTEGER DEFAULT 0, "+FIELD_NAME_COURIRER_CHARGES+" text)";

    @Override
    public void onCreate(SQLiteDatabase database) {
        database.execSQL(PRODUCTS_TABLE_CREATE_QUERY);
    }

    @Override
    public void onUpgrade(SQLiteDatabase database,int oldVersion,int newVersion){
        database.execSQL("DROP TABLE IF EXISTS "+TABLE_NAME_PRODUCTS);
        onCreate(database);
    }

    public long addProduct(ProductList productList) {

        SQLiteDatabase database = this.getWritableDatabase();
        ContentValues values = new ContentValues();
        values.put(FIELD_NAME_PRODUCT_ID, productList.getId());
        values.put(FIELD_NAME_NAME, productList.getName());
        values.put(FIELD_NAME_DESCRITPION, productList.getDescription());
        values.put(FIELD_NAME_DESIGNER, productList.getDesigner());
        values.put(FIELD_NAME_PROVIDER, productList.getProvider());
        values.put(FIELD_NAME_STONE_TYPE, productList.getStoneType());
        values.put(FIELD_NAME_COLOR_FILTER, productList.getColorFilter());
        values.put(FIELD_NAME_PRODUCT_FILTER, productList.getProductFilter());
        values.put(FIELD_NAME_MAIN_IMAGES, productList.getMainImage());
        values.put(FIELD_NAME_IMAGES, "");
        values.put(FIELD_NAME_AVAILABLE_QUANTITY, productList.getAvailableQuantity());
        values.put(FIELD_NAME_IS_FAVOURITE, productList.getIsFavourite());
        values.put(FIELD_NAME_PRICE, productList.getPrice());
        values.put(FIELD_NAME_AVAILABLE_CITY, "");
        values.put(FIELD_NAME_IMAGE_LOCAL_PATH, "");
        values.put(FIELD_NAME_DELIVERY_IN_DAYS, productList.getDeliveryInDays());
        values.put(FIELD_NAME_COURIRER_CHARGES, productList.getCourierCharges());
        return database.insert(TABLE_NAME_PRODUCTS, null, values);
    }

    public boolean rowIdExists(String id) {
        SQLiteDatabase db = this.getWritableDatabase();
        Cursor cursor = db.rawQuery("select * from " + TABLE_NAME_PRODUCTS
                + " where "+FIELD_NAME_PRODUCT_ID+"=?", new String[] { "" + id });
        boolean exists = (cursor.getCount() > 0);
        cursor.close();
        db.close();
        return exists;
    }

    public void updateProduct(String id, ProductList productList) {
        SQLiteDatabase database = this.getWritableDatabase();
        try {
            ContentValues values = new ContentValues();
            values.put(FIELD_NAME_PRODUCT_ID, productList.getId());
            values.put(FIELD_NAME_NAME, productList.getName());
            values.put(FIELD_NAME_DESCRITPION, productList.getDescription());
            values.put(FIELD_NAME_DESIGNER, productList.getDesigner());
            values.put(FIELD_NAME_PROVIDER, productList.getProvider());
            values.put(FIELD_NAME_STONE_TYPE, productList.getStoneType());
            values.put(FIELD_NAME_COLOR_FILTER, productList.getColorFilter());
            values.put(FIELD_NAME_IS_FAVOURITE, productList.getIsFavourite());
            values.put(FIELD_NAME_PRODUCT_FILTER, productList.getProductFilter());
            String image = getProductImageById(id);
            if (!Utils.isStringNull(image) && !productList.getMainImage().equals(image)) {
                values.put(FIELD_NAME_IMAGE_LOCAL_PATH, "");
            }
            values.put(FIELD_NAME_MAIN_IMAGES, productList.getMainImage());
            values.put(FIELD_NAME_IMAGES, "");
            values.put(FIELD_NAME_AVAILABLE_QUANTITY, productList.getAvailableQuantity());
            values.put(FIELD_NAME_PRICE, productList.getPrice());
            values.put(FIELD_NAME_AVAILABLE_CITY, "");
            values.put(FIELD_NAME_DELIVERY_IN_DAYS, productList.getDeliveryInDays());
            values.put(FIELD_NAME_COURIRER_CHARGES, productList.getCourierCharges());
            database.update(TABLE_NAME_PRODUCTS, values, FIELD_NAME_PRODUCT_ID + "='" + id+"'", null);
        } catch (Exception e) {
            e.printStackTrace();
        }
        database.close();
    }

    public void updateImagePath(int id, String path) {

        SQLiteDatabase database = this.getWritableDatabase();
        ContentValues values = new ContentValues();
        values.put(FIELD_NAME_IMAGE_LOCAL_PATH, path);
        database.update(TABLE_NAME_PRODUCTS,values, FIELD_NAME_ID+"="+id, null);
        database.close();
    }

    public void addToFavourite(int id) {

        SQLiteDatabase database = this.getWritableDatabase();
        ContentValues values = new ContentValues();
        values.put(FIELD_NAME_IS_FAVOURITE, 1);
        database.update(TABLE_NAME_PRODUCTS,values, FIELD_NAME_ID+"="+id, null);
        database.close();
    }


    public ArrayList<ProductList> getListLastImageUpdate(String productType) {
        ArrayList<ProductList> listItems = new ArrayList<ProductList>();

        try {
            String selectQuery = "SELECT * FROM " + TABLE_NAME_PRODUCTS + " where "+FIELD_NAME_IMAGE_LOCAL_PATH +"=='' ORDER BY "+FIELD_NAME_IMAGE_LOCAL_PATH+" ASC";

            SQLiteDatabase database = this.getWritableDatabase();
            Cursor c = database.rawQuery(selectQuery, new String[]{});
            while (c.moveToNext()) {
                ProductList list = new ProductList();
                list.setDescription(c.getString(c.getColumnIndex(FIELD_NAME_DESCRITPION)));
                list.setAvailableQuantity(c.getString(c.getColumnIndex(FIELD_NAME_AVAILABLE_QUANTITY)));
                list.setName(c.getString(c.getColumnIndex(FIELD_NAME_NAME)));
                list.setColorFilter(c.getString(c.getColumnIndex(FIELD_NAME_COLOR_FILTER)));
                list.setCourierCharges(c.getString(c.getColumnIndex(FIELD_NAME_COURIRER_CHARGES)));
                list.setDeliveryInDays(c.getString(c.getColumnIndex(FIELD_NAME_DELIVERY_IN_DAYS)));
                list.setDesigner(c.getString(c.getColumnIndex(FIELD_NAME_DESIGNER)));
                list.setDb_id(c.getString(c.getColumnIndex(FIELD_NAME_ID)));
                list.setId(c.getString(c.getColumnIndex(FIELD_NAME_PRODUCT_ID)));
                list.setPrice(c.getString(c.getColumnIndex(FIELD_NAME_PRICE)));
                list.setDesigner(c.getString(c.getColumnIndex(FIELD_NAME_DESIGNER)));
                list.setMainImage(c.getString(c.getColumnIndex(FIELD_NAME_MAIN_IMAGES)));
                list.setProductFilter(c.getString(c.getColumnIndex(FIELD_NAME_PRODUCT_FILTER)));
                list.setProvider(c.getString(c.getColumnIndex(FIELD_NAME_PROVIDER)));
                list.setImagePath(c.getString(c.getColumnIndex(FIELD_NAME_IMAGE_LOCAL_PATH)));
                list.setStoneType(c.getString(c.getColumnIndex(FIELD_NAME_STONE_TYPE)));
                list.setIsFavourite(c.getInt(c.getColumnIndex(FIELD_NAME_IS_FAVOURITE)));
                list.setCourierCharges(c.getString(c.getColumnIndex(FIELD_NAME_COURIRER_CHARGES)));
                listItems.add(list);
            }
            c.close();
        } catch (Exception e) {
            e.printStackTrace();
        }

        return listItems;
    }

    public ArrayList<ProductList> getList(String productType) {
        ArrayList<ProductList> listItems = new ArrayList<ProductList>();

        try {
            String selectQuery = "SELECT * FROM " + TABLE_NAME_PRODUCTS + " where "+FIELD_NAME_PRODUCT_FILTER +"=='"+productType+"'";

            SQLiteDatabase database = this.getWritableDatabase();
            Cursor c = database.rawQuery(selectQuery, new String[]{});
            while (c.moveToNext()) {
                ProductList list = new ProductList();
                list.setDescription(c.getString(c.getColumnIndex(FIELD_NAME_DESCRITPION)));
                list.setAvailableQuantity(c.getString(c.getColumnIndex(FIELD_NAME_AVAILABLE_QUANTITY)));
                list.setName(c.getString(c.getColumnIndex(FIELD_NAME_NAME)));
                list.setColorFilter(c.getString(c.getColumnIndex(FIELD_NAME_COLOR_FILTER)));
                list.setCourierCharges(c.getString(c.getColumnIndex(FIELD_NAME_COURIRER_CHARGES)));
                list.setDeliveryInDays(c.getString(c.getColumnIndex(FIELD_NAME_DELIVERY_IN_DAYS)));
                list.setDesigner(c.getString(c.getColumnIndex(FIELD_NAME_DESIGNER)));
                list.setDb_id(c.getString(c.getColumnIndex(FIELD_NAME_ID)));
                list.setId(c.getString(c.getColumnIndex(FIELD_NAME_PRODUCT_ID)));
                list.setPrice(c.getString(c.getColumnIndex(FIELD_NAME_PRICE)));
                list.setDesigner(c.getString(c.getColumnIndex(FIELD_NAME_DESIGNER)));
                list.setMainImage(c.getString(c.getColumnIndex(FIELD_NAME_MAIN_IMAGES)));
                list.setProductFilter(c.getString(c.getColumnIndex(FIELD_NAME_PRODUCT_FILTER)));
                list.setProvider(c.getString(c.getColumnIndex(FIELD_NAME_PROVIDER)));
                list.setImagePath(c.getString(c.getColumnIndex(FIELD_NAME_IMAGE_LOCAL_PATH)));
                list.setStoneType(c.getString(c.getColumnIndex(FIELD_NAME_STONE_TYPE)));
                list.setIsFavourite(c.getInt(c.getColumnIndex(FIELD_NAME_IS_FAVOURITE)));
                list.setCourierCharges(c.getString(c.getColumnIndex(FIELD_NAME_COURIRER_CHARGES)));
                listItems.add(list);
            }
            c.close();
        } catch (Exception e) {
            e.printStackTrace();
        }

        return listItems;
    }

    public ProductList getProduct(String id) {
        ProductList list = null;
        try {
            String selectQuery = "SELECT * FROM " + TABLE_NAME_PRODUCTS + " where "+FIELD_NAME_ID +"="+id+"";
            SQLiteDatabase database = this.getWritableDatabase();
            Cursor c = database.rawQuery(selectQuery, new String[]{});
            while (c.moveToNext()) {
                list = new ProductList();
                list.setDescription(c.getString(c.getColumnIndex(FIELD_NAME_DESCRITPION)));
                list.setAvailableQuantity(c.getString(c.getColumnIndex(FIELD_NAME_AVAILABLE_QUANTITY)));
                list.setName(c.getString(c.getColumnIndex(FIELD_NAME_NAME)));
                list.setColorFilter(c.getString(c.getColumnIndex(FIELD_NAME_COLOR_FILTER)));
                list.setCourierCharges(c.getString(c.getColumnIndex(FIELD_NAME_COURIRER_CHARGES)));
                list.setDeliveryInDays(c.getString(c.getColumnIndex(FIELD_NAME_DELIVERY_IN_DAYS)));
                list.setDesigner(c.getString(c.getColumnIndex(FIELD_NAME_DESIGNER)));
                list.setDb_id(c.getString(c.getColumnIndex(FIELD_NAME_ID)));
                list.setId(c.getString(c.getColumnIndex(FIELD_NAME_PRODUCT_ID)));
                list.setPrice(c.getString(c.getColumnIndex(FIELD_NAME_PRICE)));
                list.setDesigner(c.getString(c.getColumnIndex(FIELD_NAME_DESIGNER)));
                list.setMainImage(c.getString(c.getColumnIndex(FIELD_NAME_MAIN_IMAGES)));
                list.setProductFilter(c.getString(c.getColumnIndex(FIELD_NAME_PRODUCT_FILTER)));
                list.setProvider(c.getString(c.getColumnIndex(FIELD_NAME_PROVIDER)));
                list.setImagePath(c.getString(c.getColumnIndex(FIELD_NAME_IMAGE_LOCAL_PATH)));
                list.setStoneType(c.getString(c.getColumnIndex(FIELD_NAME_STONE_TYPE)));
                list.setIsFavourite(c.getInt(c.getColumnIndex(FIELD_NAME_IS_FAVOURITE)));
                list.setCourierCharges(c.getString(c.getColumnIndex(FIELD_NAME_COURIRER_CHARGES)));
            }
            c.close();
        } catch (Exception e) {
            e.printStackTrace();
        }

        return list;
    }

    public String getProductImageById(String id) {
        String imagePath = "";
        try {
            SQLiteDatabase database = this.getWritableDatabase();
            Cursor c = database.rawQuery("select * from " + TABLE_NAME_PRODUCTS
                    + " where "+FIELD_NAME_PRODUCT_ID+"=?", new String[] { "" + id });
            while (c.moveToNext()) {
                imagePath = c.getString(c.getColumnIndex(FIELD_NAME_MAIN_IMAGES));

            }
            c.close();
        } catch (Exception e) {
            e.printStackTrace();
        }

        return imagePath;
    }


}
