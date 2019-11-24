import { Component, OnInit, ViewChild } from "@angular/core";
import { FormGroup, FormControl, Validators, FormArray } from "@angular/forms";
import { ModalDirective } from "ngx-bootstrap";
import { UserService, AlertService } from "app/service";

@Component({
  selector: "app-bulk-import-location",
  templateUrl: "./bulk-import-location-pop-up.component.html",
  styleUrls: ["./bulk-import-location-pop-up.component.scss"]
})
export class BulkImportLocationPopUpComponent implements OnInit {
  @ViewChild("bulkUploadModal") bulkUploadModal: ModalDirective;
  @ViewChild("gmap") gmapElement: any;
  partnerId: any;
  loading: boolean = false;
  bulkLocationForm: FormGroup;
  constructor(
    private userService: UserService,
    private alertService: AlertService
  ) {
    this.userService.transmitdata.subscribe(data => {
      if (data != "closeModal") {
        this.partnerId = data;
      }
    });
     this.bulkLocationForm = new FormGroup({
      bulkLocationCSV: new FormControl("", Validators.required)
    });
  }
  get f() {
    return this.bulkLocationForm.controls;
  }
  ngOnInit() {
  }
  onAddSubmit() {
    this.loading = true;
    var val = this.bulkLocationForm.value;
    // this.userService
    //   .addPartnerLocation(
    //     this.partnerId,
    //     val.addressline1,
    //     val.addressline2,
    //     val.city,
    //     val.state,
    //     val.country,
    //     val.zip_code,
    //     val.latitude,
    //     val.longitude,
    //     val.mobile_number
    //   )
    //   .subscribe(
    //     data => {
    //       this.loading = false;
    //       this.alertService.success("Registration successful");
    //       this.hideModal();
    //     },
    //     error => {
    //       this.loading = false;
    //       this.alertService.error(error.data);
    //     }
    //   );
  }

  hideModal(): void {
    this.bulkUploadModal.hide();
    this.bulkLocationForm.reset();
  }

  showModal(): void {
    this.bulkUploadModal.show();
  }
}
