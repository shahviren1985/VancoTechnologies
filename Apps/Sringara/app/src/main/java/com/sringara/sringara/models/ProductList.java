package com.sringara.sringara.models;

public class ProductList {

    String id;
    String name;
    String description;
    String designer;
    String provider;
    String stoneType;
    String colorFilter;
    String productFilter;
    String availableQuantity;
    String deliveryInDays;
    String courierCharges;
    String imagePath;
    String price;
    String db_id;
    String mainImage;
    int isFavourite = 0;
    String[] available_city;
    String delivery_in_days;


    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public String getDesigner() {
        return designer;
    }

    public void setDesigner(String designer) {
        this.designer = designer;
    }

    public String getProvider() {
        return provider;
    }

    public void setProvider(String provider) {
        this.provider = provider;
    }

    public String getStoneType() {
        return stoneType;
    }

    public void setStoneType(String stoneType) {
        this.stoneType = stoneType;
    }

    public String getColorFilter() {
        return colorFilter;
    }

    public void setColorFilter(String colorFilter) {
        this.colorFilter = colorFilter;
    }

    public String getProductFilter() {
        return productFilter;
    }

    public void setProductFilter(String productFilter) {
        this.productFilter = productFilter;
    }

    public String getAvailableQuantity() {
        return availableQuantity;
    }

    public void setAvailableQuantity(String availableQuantity) {
        this.availableQuantity = availableQuantity;
    }

    public String getPrice() {
        return price;
    }

    public void setPrice(String price) {
        this.price = price;
    }

    public String getDeliveryInDays() {
        return deliveryInDays;
    }

    public void setDeliveryInDays(String deliveryInDays) {
        this.deliveryInDays = deliveryInDays;
    }

    public String getCourierCharges() {
        return courierCharges;
    }

    public void setCourierCharges(String courierCharges) {
        this.courierCharges = courierCharges;
    }

    public String getMainImage() {
        return mainImage;
    }

    public void setMainImage(String mainImage) {
        this.mainImage = mainImage;
    }

    public String[] getAvailable_city() {
        return available_city;
    }

    public void setAvailable_city(String[] available_city) {
        this.available_city = available_city;
    }

    public String getImagePath() {
        return imagePath;
    }

    public void setImagePath(String imagePath) {
        this.imagePath = imagePath;
    }

    public String getDb_id() {
        return db_id;
    }

    public int getIsFavourite() {
        return isFavourite;
    }

    public void setIsFavourite(int isFavourite) {
        this.isFavourite = isFavourite;
    }

    public void setDb_id(String db_id) {
        this.db_id = db_id;
    }

    public String getDelivery_in_days() {
        return delivery_in_days;
    }

    public void setDelivery_in_days(String delivery_in_days) {
        this.delivery_in_days = delivery_in_days;
    }

}
