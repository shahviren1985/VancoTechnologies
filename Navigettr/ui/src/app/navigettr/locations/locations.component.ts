import { Component, OnInit, ViewChild } from "@angular/core";
import { ModalDirective } from "ngx-bootstrap/modal";
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { elementDef } from "@angular/core/src/view";
import { PagerService } from "../../service/pager.service";
import { Http, Headers, RequestOptions, Response } from "@angular/http";
import { ActivatedRoute } from "@angular/router";
import { UserService, AlertService } from "app/service";
import { EventBus } from "app/service/event-bus.service";
import { AddPartnerLocationPopUpComponent } from "app/popup/add-partnerLocation-pop-up/add-partnerLocation-pop-up.component";

@Component({
  selector: "app-partner-locations",
  templateUrl: "./locations.component.html",
  styleUrls: ["./locations.component.scss"]
})
export class LocationComponent implements OnInit {
  @ViewChild("addPartnerLocationModal")
  addPartnerLocationModal: AddPartnerLocationPopUpComponent;
  @ViewChild("bulkImportLocation") bulkImportLocation;
  pagesIndex: Array<number>;
  pageStart: number = 1;
  pages: number = 4;
  pager: any = {};
  pagedItems: any[];
  partnerId = 0;
  loading: boolean = false;
  showNoRecord: boolean = false;
  notFoundMessage: String = "";
  token: any;
  isAdmin: boolean = false;
  partnerName: string = "";

  Element: any = [];

  constructor(
    private alertService: AlertService,
    private userService: UserService,
    private route: ActivatedRoute,
    private http: Http,
    private pagerService: PagerService,
    private _eventBus: EventBus
  ) {
    this._eventBus.on().subscribe((eventBus: any) => {
      if (eventBus.filterBy.type === "REFRESH_PARTNER_LOCATION_LIST") {
        if (eventBus.filterBy.isadd) {
          this.alertService.success("Partner Location added Successfully!!");
        } else {
          this.alertService.success("Partner Location updated Successfully!!");
        }
        this.getPartnerLocation(1);
      }
    });
  }

  ngOnInit() {
    if (localStorage.getItem("token")) {
      this.token = JSON.parse(localStorage.getItem("token"));
      if (this.token && this.token.RoleName == "Admin") {
        this.isAdmin = true;
      }
    }

    if (this.isAdmin){
      this.partnerName = localStorage.getItem("partnerName");
    }else{
      this.partnerName = this.token.name;
    }
    this.loading = true;
    this.route.params.subscribe(params => {
      this.partnerId = params["id"];
      this.userService.transmitdataMethod(this.partnerId);
      this.getPartnerLocation(1);
    });
  }

  private getPartnerLocation(page) {
    this.userService.getPartnerLocations(this.partnerId, page).subscribe(
      data => {
        this.loading = false;
        this.Element = data;
        this.setPage(1);
      },
      error => {
        this.loading = false;
        if (error.status == 404) {
          this.showNoRecord = true;
          this.notFoundMessage = error.data;
        }
      }
    );
  }

  hideModal(): void {
    this.addPartnerLocationModal.hideModal();
  }
  addItem(): void {
    this.addPartnerLocationModal.showModal();
    this.userService.transmitdataMethod("closeModal");
  }

  updateLocationItem(locationItem): void {
    this.addPartnerLocationModal.openUpdateLocationModel(locationItem);
  }

  closeUpdateLocationModel(): void {
    this.addPartnerLocationModal.closeEditLocationModel();
  }

  openBulkImport(): void {
    this.bulkImportLocation.showModal();
  }
  hideBulkImport(): void {
    this.bulkImportLocation.hideModal();
  }
  setPage(page: number) {
    // get pager object from service
    this.pager = this.pagerService.getPager(this.Element.length, page);

    // get current page of items
    this.pagedItems = this.Element.slice(
      this.pager.startIndex,
      this.pager.endIndex + 1
    );
  }
}
