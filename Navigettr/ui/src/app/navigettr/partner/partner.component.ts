import { Component, OnInit, ViewChild, ViewContainerRef } from "@angular/core";
import { ModalDirective } from "ngx-bootstrap/modal";
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { elementDef } from "@angular/core/src/view";
import { PagerService } from "../../service/pager.service";
import { Http, Headers, RequestOptions, Response } from "@angular/http";
import { Router } from "@angular/router";
import { UserService, AlertService, AuthenticationService } from "app/service";
import { EventBus } from "app/service/event-bus.service";
export type Currency = {
  symbol: string;
  name: string;
  symbol_native: string;
  decimal_digits: number;
  rounding: number;
  code: string;
  name_plural: string;
};

@Component({
  selector: "app-partner",
  templateUrl: "./partner.component.html",
  styleUrls: ["./partner.component.scss"]
})
export class PartnerComponent implements OnInit {
  @ViewChild("addPartnerModal") addPartnerModal;
  currentItem: any;
  imageChangedEvent: any = "";
  croppedImage: any = "";
  pagesIndex: Array<number>;
  pageStart: number = 1;
  pages: number = 4;
  pager: any = {};
  pagedItems: any[];
  currencies: Array<Currency>;
  loading: boolean = false;
  showNoRecord: boolean = false;
  notFoundMessage: String = "";
  searchval: string = "";
  token: any;
  isAdmin: boolean = false;

  Element: any = [];

  constructor(
    private alertService: AlertService,
    private userService: UserService,
    private authService: AuthenticationService,
    private http: Http,
    private pagerService: PagerService,
    private router: Router,
    vcr: ViewContainerRef,
    private _eventBus: EventBus
  ) {
    this._eventBus.on().subscribe((eventBus: any) => {
      if (eventBus.filterBy.type === "REFRESH_PARTNER_LIST") {
        this.alertService.success("New partner added successfully!!");
        this.GetPartners();
      }
    });
  }

  ngOnInit() {
    if (localStorage.getItem("token")) {
      this.token = JSON.parse(localStorage.getItem("token"));
      if (this.token && this.token.RoleName == "Admin") {
        this.isAdmin = true;
      } else {
        this.authService.logout();
      }
    }
    this.GetPartners();
    this.http
      // .get("/assets/common_currency.json")
      .get("/assets/common_currency.json")
      .map(data => data.json() as Array<Currency>)
      .subscribe(data => {
        this.currencies = data;
      });
  }

  GetPartners(page: number = 1) {
    this.loading = true;
    let self = this;
    this.userService.getPartners(page, this.searchval).subscribe(
      data => {
        this.showNoRecord = false;
        this.loading = false;
        this.Element = data;
        this.setPage(page, data[0].totalCount);
      },
      error => {
        this.loading = false;
        if (error.status == 404) {
          this.showNoRecord = true;
          this.pagedItems = [];
          this.notFoundMessage = error.data;
        }

        // this.alertService.error(error.data);
      }
    );
  }

  GotoLocations(partnerId, partnerName) {
    localStorage.setItem("partnerName", partnerName);
    this.router.navigateByUrl("partner/partner-locations/" + partnerId);
  }

  searchPartner() {
    this.GetPartners();
  }

  hideModal(): void {
    this.addPartnerModal.hideModal();
  }
  addItem(): void {
    this.addPartnerModal.showModal();
  }
  setPage(page: number, totalCount: number) {
    // get pager object from service
    // this.pager = this.pagerService.getPager(this.Element.length, page);
    this.pager = this.pagerService.getPager(totalCount, page);

    // get current page of items
    // this.pagedItems = this.Element.slice(
    //   this.pager.startIndex,
    //   this.pager.endIndex + 1
    // );
    this.pagedItems = this.Element;
  }
  success(message: string) {
    this.alertService.warn(message);
  }
}
