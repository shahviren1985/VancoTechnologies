import { Injectable } from "@angular/core";
import { Http, Headers, Response } from "@angular/http";

import { environment } from "../../environments/environment";
import { User } from "../models/user";
import { Observable, Subject } from "rxjs/Rx";


@Injectable()
export class UserService {
  transmitdata: Observable<any>;
  private dataSubject = new Subject<any>();
  constructor(private http: Http) {
    this.transmitdata = this.dataSubject.asObservable();
  }
  transmitdataMethod(data) {
    this.dataSubject.next(data);
  }
  login(username: String, password: String) {
    return this.http
      .post(`${environment.apiUrl}login`, {
        username: username,
        Password: password
      })
      .map(response => response.json())
      .map(response => {
        return response;
      })
      .catch(this.handleError);
  }
  getPartnerOffers(partnerId, page = 1, name = "", pageData = "10") {
    return this.http
      .post(`${environment.apiUrl}getPartnerOffers?PartnerId=${partnerId}`, {
        Status: "",
        OfferName: name,
        Page: page,
        PageData: pageData
      })
      .map(response => response.json())
      .map(response => {
        return response;
      })
      .catch(this.handleError);
  }
  getPartnerLocations(partnerId, page) {
    return this.http
      .post(`${environment.apiUrl}getPartnerLocations?PartnerId=${partnerId}`, {
        status: "",
        zipCode: "",
        city: "",
        Page: page,
        PageData: "10"
      })
      .map(response => response.json())
      .map(response => {
        return response;
      })
      .catch(this.handleError);
  }
  getPartnerRates(partnerId, page) {
    return this.http
      .post(`${environment.apiUrl}getPartnerRates?PartnerId=${partnerId}`, {
        // status: "",
        // zipCode: "",
        // city: "",
        // Page: page,
        // PageData: "10"
      })
      .map(response => response.json())
      .map(response => {
        return response;
      })
      .catch(this.handleError);
  }
  getPartners(page: number, name: string, pageData = "10") {
    return this.http
      .post(`${environment.apiUrl}getPartners`, {
        Status: "",
        name: name,
        Page: page,
        PageData: pageData
      })
      .map(response => response.json())
      .map(response => {
        return response;
      })
      .catch(this.handleError);
  }
  getGlobalConfig() {
    return this.http
      .post(`${environment.apiUrl}getGlobalConfig`, {})
      .map(response => response.json())
      .map(response => {
        return response;
      })
      .catch(this.handleError);
  }
  saveGlobalConfig(payload) {
    return this.http
      .post(`${environment.apiUrl}saveGlobalConfig`, payload)
      .map(response => response.json())
      .map(response => {
        return response;
      })
      .catch(this.handleError);
  }
  getPartnerCharges(partnerId: number) {
    return this.http
      .post(`${environment.apiUrl}getPartnerCharges?PartnerID=${partnerId}`, {})
      .map(response => response.json())
      .map(response => {
        return response;
      })
      .catch(this.handleError);
  }
  addPartner(PartnerName, EmailAddress, UserName) {
    return this.http
      .post(`${environment.apiUrl}addPartner`, {
        UserID: "0",
        LoginType: "Partner",
        UserName: UserName,
        Password: "123456",
        RoleId: "3",
        UStatus: "Active",
        EmailAddress: EmailAddress,
        MobileNo: "9920748255",
        PartnerID: "0",
        name: PartnerName,
        PartnerLogoPath: "images/log.png",
        ExpiryDate: "2019-07-07 16:16:33.537",
        PStatus: "Active"
      })
      .map(response => response.json())
      .map(response => {
        if (response.IsSuccessStatusCode) {
        } else {
        }
        return response;
      })
      .catch(this.handleError);
  }

  uploadBrandLogo(formdata, partnerId) {
    return this.http
      .post(
        `${environment.apiUrl}uploadBrandLogo?partnerId=${partnerId}`,
        formdata
      )
      .map(response => response.json())
      .map(response => {
        if (response.IsSuccessStatusCode) {
        } else {
        }
        return response;
      })
      .catch(this.handleError);
  }

  savePartnerRates() {
    return this.http
      .post(environment.apiUrl + "savePartnerRates", {
        Id: 1,
        PartnerId: 2,
        FromRate: "sample string 3",
        ToRate: "sample string 4",
        Guaranteed: 5.1,
        Indicative: 6.1,
        Status: "sample string 7"
      })
      .map(response => response.json())
      .map(response => {
        if (response.IsSuccessStatusCode) {
        } else {
        }
        return response;
      })
      .catch(this.handleError);
  }
  savePartnerCharges(payload) {
    return this.http
      .post(environment.apiUrl + "savePartnerCharges", payload)
      .map(response => response.json())
      .map(response => {
        return response;
      })
      .catch(this.handleError);
  }
  getRoles() {
    return this.http
      .post(environment.apiUrl + "GetRoles", {
        RoleId: 1,
        RoleName: "sample string 2"
      })
      .map(response => response.json())
      .map(response => {
        if (response.IsSuccessStatusCode) {
          localStorage.setItem("frontend-token", response.Data.token);
        } else {
          localStorage.removeItem("frontend-token");
        }
        return response;
      })
      .catch(this.handleError);
  }
  addPartnerOffer(
    id,
    partnerId,
    offerName,
    offerText,
    OfferStartDate,
    OfferEndDate,
    dateActivated,
    dateExpired
  ) {
    return this.http
      .post(environment.apiUrl + "addPartnerOffer", {
        Id: id,
        PartnerId: partnerId,
        OfferName: offerName,
        OfferType: "Of7",
        OfferText: offerText,
        OfferStartDate: OfferStartDate,
        OfferEndDate: OfferEndDate,
        Status: "ACTIVE",
        DateActivation: dateActivated,
        DateExpiry: dateExpired
      })
      .map(response => response.json())
      .map(response => {
        return response;
      })
      .catch(this.handleError);
  }
  getPartnersCount() {
    return this.http
      .post(environment.apiUrl + "getPartnersCount", {
        UserID: 1,
        LoginType: "sample string 2",
        UserName: "sample string 3",
        Password: "sample string 4",
        RoleId: 5,
        UStatus: "sample string 6",
        EmailAddress: "sample string 7",
        MobileNo: "sample string 8",
        PartnerName: "sample string 9",
        PartnerLogoPath: "sample string 10",
        ExpiryDate: "2019-02-05T01:51:15.2763761-07:00",
        PartnerID: 12,
        Page: 13,
        PageData: 14,
        PStatus: "sample string 15"
      })
      .map(response => response.json())
      .map(response => {
        if (response.IsSuccessStatusCode) {
          localStorage.setItem("frontend-token", response.Data.token);
        } else {
          localStorage.removeItem("frontend-token");
        }
        return response;
      })
      .catch(this.handleError);
  }
  addPartnerRates(payload) {
    return this.http
      .post(environment.apiUrl + "savePartnerRates", payload)
      .map(response => response.json())
      .map(response => {
        return response;
      })
      .catch(this.handleError);
  }
  addPartnerLocation(
    locationId,
    partnerId,
    addressLine1,
    addressLine2,
    city,
    state,
    country,
    zipCode,
    lat,
    long,
    mobileNumber,
    locationWorkList
  ) {
    if (!locationId) {
      locationId = 0;
    }
    let dt = new Date();
    let dateActivated = new Date();
    let dateExpired = new Date(dt.setFullYear(dt.getFullYear() + 1));
    return this.http
      .post(environment.apiUrl + "addPartnerLocation", {
        PartnerLocationID: locationId,
        PartnerID: partnerId,
        AddressLine1: addressLine1,
        AddressLine2: addressLine2,
        City: city,
        State: state,
        Country: country,
        ZipCode: zipCode,
        Latitude: lat,
        Longitude: long,
        MobileNumber: mobileNumber,
        PartnerlocationWorkList: locationWorkList,

        DateActivated: dateActivated,
        DateExpiry: dateExpired,
        Status: "Active"
      })
      .map(response => response.json())
      .map(response => {
        if (response.IsSuccessStatusCode) {
          localStorage.setItem("frontend-token", response.Data.token);
        } else {
          localStorage.removeItem("frontend-token");
        }
        return response;
      })
      .catch(this.handleError);
  }

  updatePartnerLocation(
    PartnerLocationID,
    partnerId,
    addressLine1,
    addressLine2,
    city,
    state,
    country,
    zipCode,
    lat,
    long,
    mobileNumber,
    locationWorkList
  ) {
    return this.http
      .post(environment.apiUrl + "addPartnerLocation", {
        PartnerLocationID: PartnerLocationID,
        PartnerID: partnerId,
        AddressLine1: addressLine1,
        AddressLine2: addressLine2,
        City: city,
        State: state,
        Country: country,
        ZipCode: zipCode,
        Latitude: lat,
        Longitude: long,
        MobileNumber: mobileNumber,
        PartnerlocationWorkList: locationWorkList,
        DateActivated: "2019-02-05T03:44:00.1738632-07:00",
        DateExpiry: "2019-02-05T03:44:00.1738632-07:00",
        Status: ""
      })
      .map(response => response.json())
      .map(response => {
        if (response.IsSuccessStatusCode) {
          localStorage.setItem("frontend-token", response.Data.token);
        } else {
          localStorage.removeItem("frontend-token");
        }
        return response;
      })
      .catch(this.handleError);
  }

  getPartnersLocationCount() {
    return this.http
      .post(environment.apiUrl + "getPartnersLocationCount", {
        PartnerLocationID: 1,
        PartnerID: 2,
        AddressLine1: "sample string 3",
        AddressLine2: "sample string 4",
        City: "sample string 5",
        State: "sample string 6",
        Country: "sample string 7",
        ZipCode: "sample string 8",
        Latitude: 9.1,
        Longitude: 10.1,
        MobileNumber: "sample string 11",
        DateActivated: "2019-02-05T03:46:53.972724-07:00",
        DateExpiry: "2019-02-05T03:46:53.972724-07:00",
        Page: 14,
        PageData: 15,
        Status: "sample string 16"
      })
      .map(response => response.json())
      .map(response => {
        if (response.IsSuccessStatusCode) {
          localStorage.setItem("frontend-token", response.Data.token);
        } else {
          localStorage.removeItem("frontend-token");
        }
        return response;
      })
      .catch(this.handleError);
  }
  private handleError(error: Response | any) {
    let errorMessage: any = {};
    // Connection error
    if (error.status == 0) {
      errorMessage = {
        success: false,
        status: 0,
        data: "Sorry, there was a connection error occurred. Please try again."
      };
    } else {
      let data = error.json();
      errorMessage.status = error.status;
      errorMessage.data = data.Message;
    }
    return Observable.throw(errorMessage);
  }

  public GetAddressByLatLong(lat, long) {
    return this.http
      .get(
        `https://maps.googleapis.com/maps/api/geocode/json?latlng=${lat},${long}&key=${
          environment.google_map_key
        }`
      )
      .map(response => response.json())
      .catch(this.handleError);
  }
}
