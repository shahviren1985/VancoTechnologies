import {
  FormGroup,
  FormControl,
  Validators,
  FormArray,
  FormBuilder
} from "@angular/forms";
import { Component, OnInit, ViewChild } from "@angular/core";
import { ModalDirective } from "ngx-bootstrap";
import { UserService, AlertService } from "app/service";
import { EventBus } from "app/service/event-bus.service";
import { Observable } from "rxjs";
import { Http } from "@angular/http";

@Component({
  selector: "app-add-partnerLocation-pop-up",
  templateUrl: "./add-partnerLocation-pop-up.component.html",
  styleUrls: ["./add-partnerLocation-pop-up.component.scss"]
})
export class AddPartnerLocationPopUpComponent implements OnInit {
  @ViewChild("addItemModal") addItemModal: ModalDirective;
  @ViewChild("gmap") gmapElement: any;
  partnerId: any;
  markers: any[] = [];
  modelTitle: string;
  showMap: boolean = false;
  loading: boolean = false;
  fullAddress: string = "";
  showLat: boolean = true;
  showLng: boolean = true;
  workingTime: any[] = [];
  oldWorkTime = [];
  daysInWeek = [
    { id: 1, name: "Sunday" },
    { id: 2, name: "Monday" },
    { id: 3, name: "Tuesday" },
    { id: 4, name: "Wednesday" },
    { id: 5, name: "Thursday" },
    { id: 6, name: "Friday" },
    { id: 7, name: "Saturday" }
  ];
  hours = [
    { selected: "true", id: 1, name: "1 am" },
    { selected: "false", id: 2, name: "2 am" },
    { selected: "false", id: 3, name: "3 am" },
    { selected: "false", id: 4, name: "4 am" },
    { selected: "false", id: 5, name: "5 am" },
    { selected: "false", id: 6, name: "6 am" },
    { selected: "false", id: 7, name: "7 am" },
    { selected: "false", id: 8, name: "8 am" },
    { selected: "false", id: 9, name: "9 am" },
    { selected: "false", id: 10, name: "10 am" },
    { selected: "false", id: 11, name: "11 am" },
    { selected: "false", id: 12, name: "12 pm" },
    { selected: "false", id: 13, name: "1  pm" },
    { selected: "false", id: 14, name: "2  pm" },
    { selected: "false", id: 15, name: "3  pm" },
    { selected: "false", id: 16, name: "4  pm" },
    { selected: "false", id: 17, name: "5  pm" },
    { selected: "false", id: 18, name: "6  pm" },
    { selected: "false", id: 19, name: "7  pm" },
    { selected: "false", id: 20, name: "8  pm" },
    { selected: "false", id: 21, name: "9  pm" },
    { selected: "false", id: 22, name: "10 pm" },
    { selected: "false", id: 23, name: "11 pm" },
    { selected: "false", id: 24, name: "12 am" }
  ];
  // map: google.maps.Map;
  lat: any;
  lng: any;

  isModelInEditMode: boolean;
  partnerLocationForm: FormGroup;
  constructor(
    private userService: UserService,
    private alertService: AlertService,
    private _eventBus: EventBus,
    private fb: FormBuilder,
    private http: Http
  ) {
    this.modelTitle = "Add Location";
    this.userService.transmitdata.subscribe(data => {
      if (data != "closeModal") {
        this.partnerId = data;
      }
    });
    // const days = this.daysInWeek.map(c => new FormControl(false));
    // const hours = this.hours.map(c => new FormControl(false));

    this.partnerLocationForm = new FormGroup({
      ID: new FormControl(""),
      addressline1: new FormControl("", Validators.required),
      addressline2: new FormControl(""),
      city: new FormControl("", Validators.required),
      state: new FormControl("", Validators.required),
      country: new FormControl("", Validators.required),
      zip_code: new FormControl("", Validators.required),
      latitude: new FormControl("", Validators.required),
      longitude: new FormControl("", Validators.required),
      mobile_number: new FormControl("", Validators.required)
    });
  }

  private getWorkingTime() {
    this.workingTime = [];
    this.daysInWeek.forEach(element => {
      let obj = {};
      obj["id"] = 0;
      obj["day"] = element.name;
      obj["isSelected"] = false;
      obj["startTime"] = this.hours;
      obj["selectedStartTime"] = this.hours[0].name;
      obj["EndTime"] = this.hours;
      obj["selectedEndTime"] = this.hours[0].name;
      this.workingTime.push(obj);
    });
  }

  createItem(rate): FormGroup {
    return this.fb.group({
      id: new FormControl(rate.Id, Validators.required),
      from: new FormControl(rate.from, Validators.required),
      to: new FormControl(rate.to, Validators.required),
      indicative: new FormControl(rate.indicative, Validators.required),
      guarantee: new FormControl(rate.guaranteed, Validators.required),
      status: new FormControl(rate.Status == "Active" ? true : false)
    });
  }

  onStartChange(value, index) {
    this.workingTime[index].selectedStartTime = value;
  }

  onEndChange(value, index) {
    this.workingTime[index].selectedEndTime = value;
  }

  // get locationWorkList() {
  //   // return this.partnerLocationForm.get("PartnerlocationWorkList") as FormArray;
  // }

  get f() {
    return this.partnerLocationForm.controls;
  }
  ngOnInit() {
    this.getWorkingTime();
  }

  // private toggleMap() {
  //   this.showMap = !this.showMap;
  //   const self = this;
  //   if (this.showMap) {
  //     this.initMap(self);
  //   }
  // }

  private getLatLong() {
    this.loading = true;
    this.fullAddress =
      this.partnerLocationForm.controls["addressline1"].value +
      "+" +
      this.partnerLocationForm.controls["addressline2"].value +
      "+" +
      this.partnerLocationForm.controls["city"].value +
      "+" +
      this.partnerLocationForm.controls["state"].value +
      "+" +
      this.partnerLocationForm.controls["zip_code"].value;
    // 1600+Amphitheatre+Parkway,+Mountain+View,+CA
    let url =
      "https://maps.googleapis.com/maps/api/geocode/json?address=" +
      this.fullAddress +
      "&key=AIzaSyC7Bgi2lHuE5WaMAsVeTZznuLC0ocAAC04";
    this.http
      .get(url)
      .map(data => data.json())
      .subscribe(data => {
        this.loading = false;
        if (data.status == "OK") {
          this.showLat = true;
          this.showLng = true;
          this.partnerLocationForm.patchValue({
            latitude: data.results[0].geometry.location.lat,
            longitude: data.results[0].geometry.location.lng
          });
        } else {
          this.partnerLocationForm.patchValue({
            latitude: "",
            longitude: ""
          });
          this.showLat = false;
          this.showLng = false;
        }
      });
    error => {
      this.loading = false;
      this.partnerLocationForm.patchValue({
        latitude: "",
        longitude: ""
      });
      this.showLat = false;
      this.showLng = false;
    };
  }

  // private initMap(self: this) {
  //   navigator.geolocation.getCurrentPosition(successCallback, errorCallback);
  //   function successCallback(position) {
  //     const geolocate = new google.maps.LatLng(
  //       position.coords.latitude,
  //       position.coords.longitude
  //     );
  //     if (self.lat && self.lng) {
  //       self.getMap(self.lat, self.lng);
  //     } else {
  //       self.getMap(geolocate.lat(), geolocate.lng());
  //     }
  //     self.lat = geolocate.lat();
  //     self.lng = geolocate.lng();

  //     self.geocodePosition(self.lat, self.lng);
  //   }
  //   function errorCallback(position) {
  //     self.getMap(72.8776559, 19.0759837);
  //   }
  // }

  geocodePosition(lat, long) {
    let city = "",
      state = "",
      zipCode = "",
      country = "",
      formatedAddress = "";

    this.userService.GetAddressByLatLong(lat, long).subscribe(
      (googleaAddressRes: any) => {
        if (
          googleaAddressRes &&
          googleaAddressRes.results &&
          googleaAddressRes.results[0]
        ) {
          const add1 = googleaAddressRes.results[0];
          formatedAddress = add1.formatted_address;
          add1.address_components.forEach(element => {
            if (element.types.findIndex(x => x === "locality") !== -1) {
              city = element.long_name;
            } else if (
              element.types.findIndex(
                x => x === "administrative_area_level_1"
              ) !== -1
            ) {
              state = element.long_name;
            } else if (element.types.findIndex(x => x === "country") !== -1) {
              country = element.long_name;
            } else if (
              element.types.findIndex(x => x === "postal_code") !== -1
            ) {
              zipCode = element.long_name;
            }
          });
        }
        this.partnerLocationForm.controls["addressline1"].setValue(
          formatedAddress
        );
        this.partnerLocationForm.controls["city"].setValue(city);
        this.partnerLocationForm.controls["state"].setValue(state);
        this.partnerLocationForm.controls["country"].setValue(country);
        this.partnerLocationForm.controls["zip_code"].setValue(zipCode);
        this.partnerLocationForm.controls["latitude"].setValue(lat);
        this.partnerLocationForm.controls["longitude"].setValue(long);
      },
      err => {
        console.log(err);
      }
    );
  }

  // private getMap(lat, lng) {
  //   let self = this;
  //   var mapProp = this.getMapProp(lat, lng);
  //   this.map = new google.maps.Map(this.gmapElement.nativeElement, mapProp);
  //   google.maps.event.addListener(this.map, "click", function(event) {
  //     var myLatLng = event.latLng;
  //     self.lat = myLatLng.lat();
  //     self.lng = myLatLng.lng();
  //     self.partnerLocationForm.patchValue({
  //       latitude: self.lat,
  //       longitude: self.lng
  //     });
  //     var mapProp = self.getMapProp(self.lat, self.lng);
  //     self.deleteMarkers();
  //     var marker = new google.maps.Marker({ position: mapProp.center });
  //     self.markers.push(marker);
  //     marker.setMap(self.map);
  //     self.geocodePosition(self.lat, self.lng);
  //   });

  //   var marker = new google.maps.Marker({ position: mapProp.center });
  //   self.markers.push(marker);
  //   marker.setMap(this.map);

  //   // var infowindow = new google.maps.InfoWindow({
  //   //   content: "Hey, We are here"
  //   // });
  //   // infowindow.open(this.map, marker);
  // }

  // private deleteMarkers() {
  //   this.clearMarkers();
  //   this.markers = [];
  // }
  // private clearMarkers() {
  //   this.setMapOnAll(null);
  // }

  // private setMapOnAll(map) {
  //   for (var i = 0; i < this.markers.length; i++) {
  //     this.markers[i].setMap(map);
  //   }
  // }

  // private getMapProp(lat: any, lng: any) {
  //   return {
  //     center: new google.maps.LatLng(lat, lng),
  //     zoom: 15,
  //     mapTypeId: google.maps.MapTypeId.ROADMAP
  //   };
  // }

  // autoCompleteCallback1(add1: any) {
  //   if (add1.response) {
  //     let city = "",
  //       state = "",
  //       zipCode = "",
  //       country = "",
  //       formatedAddress = "";
  //     formatedAddress = add1.formatted_address;
  //     add1.address_components.forEach(element => {
  //       if (element.types.findIndex(x => x === "locality") !== -1) {
  //         city = element.long_name;
  //       } else if (
  //         element.types.findIndex(x => x === "administrative_area_level_1") !==
  //         -1
  //       ) {
  //         state = element.long_name;
  //       } else if (element.types.findIndex(x => x === "country") !== -1) {
  //         country = element.long_name;
  //       } else if (element.types.findIndex(x => x === "postal_code") !== -1) {
  //         zipCode = element.long_name;
  //       }
  //     });

  //     this.partnerLocationForm.controls["addressline1"].setValue(
  //       formatedAddress
  //     );
  //     this.partnerLocationForm.controls["city"].setValue(city);
  //     this.partnerLocationForm.controls["state"].setValue(state);
  //     this.partnerLocationForm.controls["country"].setValue(country);
  //     this.partnerLocationForm.controls["zip_code"].setValue(zipCode);
  //   }
  // }
  onFormSubmit() {
    if (!this.isModelInEditMode) {
      this.onAddSubmit();
    } else if (this.isModelInEditMode) {
      this.updateLocation();
    }
  }

  onAddSubmit() {
    if (this.partnerLocationForm.valid) {
      this.loading = true;
      var val = this.partnerLocationForm.value;
      var locationWorkList = [];
      this.workingTime.forEach(element => {
        if (element.isSelected) {
          var obj = {
            id: 0,
            day: element.day,
            startTime: element.selectedStartTime,
            closeTime: element.selectedEndTime,
            status: ""
          };
          locationWorkList.push(obj);
        }
      });

      this.userService
        .addPartnerLocation(
          val.ID,
          this.partnerId,
          val.addressline1,
          val.addressline2,
          val.city,
          val.state,
          val.country,
          val.zip_code,
          val.latitude,
          val.longitude,
          val.mobile_number,
          locationWorkList
        )
        .subscribe(
          data => {
            this.loading = false;
            if (this.isModelInEditMode) {
              this._eventBus.emit({
                type: "REFRESH_PARTNER_LOCATION_LIST",
                isadd: false
              });
            } else {
              this._eventBus.emit({
                type: "REFRESH_PARTNER_LOCATION_LIST",
                isadd: true
              });
            }

            this.partnerLocationForm.reset();
            this.hideModal();
          },
          error => {
            this.loading = false;
            this.alertService.error(error.data);
          }
        );
    }
  }

  updateLocation() {
    this.loading = true;
    var val = this.partnerLocationForm.value;
    var locationWorkList = [];
    this.workingTime.forEach(element => {
      if (element.isSelected) {
        var obj = {
          id: element.id,
          day: element.day,
          startTime: element.selectedStartTime,
          closeTime: element.selectedEndTime,
          status: ""
        };
        locationWorkList.push(obj);
      } else {
        this.oldWorkTime.forEach(oldTime => {
          if (oldTime.WorkDay == element.day) {
            oldTime.Status = "INACTIVE";
            locationWorkList.push(oldTime);
          }
        });
      }
    });
    this.userService
      .updatePartnerLocation(
        val.PartnerLocationID,
        this.partnerId,
        val.addressline1,
        val.addressline2,
        val.city,
        val.state,
        val.country,
        val.zip_code,
        val.latitude,
        val.longitude,
        val.mobile_number,
        locationWorkList
      )
      .subscribe(
        data => {
          this.loading = false;
          this._eventBus.emit({ type: "REFRESH_PARTNER_LOCATION_LIST" });
          this.partnerLocationForm.reset();
          this.closeEditLocationModel();
        },
        error => {
          this.loading = false;
          this.alertService.error(error.data);
          this.closeEditLocationModel();
        }
      );
  }

  onLatChange(lat) {
    // this.lat = lat;
    // this.getMap(this.lat, this.lng);
  }

  onLongChange(lng) {
    // this.lng = lng;
    // this.getMap(this.lat, this.lng);
  }

  hideModal(): void {
    this.getWorkingTime();
    this.addItemModal.hide();
    this.modelTitle = "Add location";
    this.isModelInEditMode = false;
  }

  showModal(): void {
    this.partnerLocationForm.reset();
    this.addItemModal.show();
  }

  openUpdateLocationModel(updateLocationItem) {
    this.oldWorkTime = updateLocationItem.PartnerLocationWorkTimes;
    this.workingTime.forEach(wt => {
      this.oldWorkTime.forEach(owt => {
        if (owt.WorkDay == wt.day) {
          wt.isSelected = true;
          wt.selectedStartTime = owt.WorkStartTime;
          wt.selectedEndTime = owt.WorkEndTime;
        }
      });
    });
    this.modelTitle = "Update location";
    this.isModelInEditMode = true;
    this.partnerLocationForm.patchValue({
      ID: updateLocationItem.id,
      addressline1: updateLocationItem.addressLine1,
      addressline2: updateLocationItem.addressLine2,
      city: updateLocationItem.city,
      state: updateLocationItem.state,
      country: updateLocationItem.Country,
      zip_code: updateLocationItem.zipCode,
      latitude: updateLocationItem.latitude,
      longitude: updateLocationItem.longitude,
      mobile_number: updateLocationItem.mobileNumber
    });

    this.addItemModal.show();
  }

  closeEditLocationModel() {
    this.addItemModal.hide();
    this.modelTitle = "Add location";
    this.isModelInEditMode = false;
  }
}
