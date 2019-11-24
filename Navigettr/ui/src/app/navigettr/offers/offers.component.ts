import { Component, OnInit, ViewChild } from "@angular/core";
import { PagerService, UserService, AlertService } from "app/service";
import { Http } from "@angular/http";
import { ActivatedRoute } from "@angular/router";
import { EventBus } from "app/service/event-bus.service";

@Component({
  selector: "app-offers",
  templateUrl: "./offers.component.html",
  styleUrls: ["./offers.component.scss"]
})
export class OffersComponent implements OnInit {
  @ViewChild("addPartnerOffersModal") addPartnerOffersModal;
  pagesIndex: Array<number>;
  pageStart: number = 1;
  pages: number = 4;
  pager: any = {};
  pagedItems: any[];
  Element: any = [];
  partnerOffers: any = [];
  loading: boolean = false;
  partnerId = 0;
  showNoRecord: boolean = false;
  notFoundMessage: String = "";
  token: any;
  isAdmin: boolean = false;
  partnerName: string = "";
  constructor(
    private route: ActivatedRoute,
    private userService: UserService,
    private alertService: AlertService,
    private http: Http,
    private pagerService: PagerService,
    private _eventBus: EventBus
  ) {
    this._eventBus.on().subscribe((eventBus: any) => {
      if (eventBus.filterBy.type === "REFRESH_PARTNER_OFFER_LIST") {
        this.alertService.success("New offer added successfully!!");
        this.getPartnerOffers();
      }
    });
  }

  updateOfferItem(locationItem): void {
    this.addPartnerOffersModal.openUpdateOfferModel(locationItem);
  }

  ngOnInit() {
    if (localStorage.getItem("token")) {
      this.token = JSON.parse(localStorage.getItem("token"));
      if (this.token && this.token.RoleName == "Admin") {
        this.isAdmin = true;
      }
      if (this.isAdmin) {
        this.partnerName = localStorage.getItem("partnerName");
      } else {
        this.partnerName = this.token.name;
      }
    }
    this.loading = true;
    this.route.params.subscribe(params => {
      this.partnerId = params["id"];
      this.userService.transmitdataMethod(this.partnerId);
    });
    this.getPartnerOffers();
  }

  private getPartnerOffers(page: number = 1) {
    this.userService.getPartnerOffers(this.partnerId).subscribe(
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
          this.notFoundMessage = error.data;
        }
      }
    );
  }

  addOffer(): void {
    this.addPartnerOffersModal.showModal();
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
}
