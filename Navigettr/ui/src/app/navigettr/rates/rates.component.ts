import { Component, OnInit, ViewChild } from "@angular/core";
import { PagerService, UserService, AlertService } from "app/service";
import { Http } from "@angular/http";
import {
  FormGroup,
  FormControl,
  Validators,
  FormBuilder,
  FormArray
} from "@angular/forms";
import { AlertComponent } from "ngx-bootstrap";
import { ActivatedRoute } from "@angular/router";
import { EventBus } from "app/service/event-bus.service";

@Component({
  selector: "app-rates",
  templateUrl: "./rates.component.html",
  styleUrls: ["./rates.component.scss"]
})
export class RatesComponent implements OnInit {
  @ViewChild("addPartnerRatesModal") addPartnerRatesModal;
  pagesIndex: Array<number>;
  pageStart: number = 1;
  pages: number = 4;
  pager: any = {};
  pagedItems: any[];
  token: any;
  isAdmin: boolean = false;
  loading: boolean = false;
  partnerId = 0;
  showNoRecord: boolean = false;
  notFoundMessage: String = "";
  rates: FormArray;
  previousValues: any[];
  partnerName: string = "";
  Element: any = [
    {
      id: 1,
      from: "AED",
      to: "INR",
      status: "true"
    },
    {
      id: 2,
      from: "USD",
      to: "INR",
      status: "false"
    }
  ];

  partnerRatesForm: FormGroup;

  constructor(
    private alertService: AlertService,
    private userService: UserService,
    private http: Http,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private pagerService: PagerService,
    private _eventBus: EventBus
  ) {
    this.partnerRatesForm = this.formBuilder.group({
      rates: this.formBuilder.array([])
    });
    this._eventBus.on().subscribe((eventBus: any) => {
      if (eventBus.filterBy.type === "REFRESH_PARTNER_RATE_LIST") {
        this.alertService.success("Partner rate added Successfully!!");
        this.getPartnerRates(1);
      }
    });
  }
  get f() {
    return this.partnerRatesForm.controls;
  }
  createItem(rate): FormGroup {
    return this.formBuilder.group({
      id: new FormControl(rate.Id, Validators.required),
      from: new FormControl(rate.from, Validators.required),
      to: new FormControl(rate.to, Validators.required),
      indicative: new FormControl(rate.indicative, Validators.required),
      guarantee: new FormControl(rate.guaranteed, Validators.required),
      status: new FormControl(rate.Status == "Active" ? true : false)
    });
  }

  removeRate(index) {
    let check = this.partnerRatesForm.get("rates") as FormArray;
    check.removeAt(index);
  }

  ngOnInit() {
    if (localStorage.getItem("token")) {
      this.token = JSON.parse(localStorage.getItem("token"));
      if (this.token && this.token.RoleName == "Admin") {
        this.isAdmin = true;
      }
      this.partnerName = this.token.name;
    }
    this.loading = true;
    this.route.params.subscribe(params => {
      this.partnerId = params["id"];
      this.userService.transmitdataMethod(this.partnerId);
      this.getPartnerRates(1);
    });
  }

  private onbulkUpdateRates() {
    this.loading = true;
    let form_values = this.partnerRatesForm.controls.rates.value;
    let payload = [];
    form_values.forEach(element => {
      let data = {
        Id: element.id,
        PartnerId: this.partnerId,
        FromRate: element.from,
        ToRate: element.to,
        Guaranteed: element.guarantee,
        Indicative: element.indicative,
        Status: element.status == true ? "Active" : "Inactive"
      };
      payload.push(data);
    });
    this.userService.addPartnerRates(payload).subscribe(
      data => {
        this.loading = false;
        this.Element = data;
        if (payload.length > 1) {
          this.alertService.success("Partner rates updated Successfully!!");
        } else {
          this.alertService.success("Partner rate updated Successfully!!");
        }
        this.getPartnerRates(1);
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

  private getPartnerRates(page) {
    let that = this;
    this.userService.getPartnerRates(this.partnerId, page).subscribe(
      data => {
        that.previousValues = data;
        let control = <FormArray>this.partnerRatesForm.controls.rates;
        control.controls = [];
        data.forEach(element => {
          control.push(this.createItem(element));
        });
        this.loading = false;
        this.Element = data;
        this.setPage(1);
        that.partnerRatesForm.controls["rates"]["controls"].forEach(element => {
          element["controls"]["guarantee"].valueChanges.subscribe(value => {});
          element["controls"]["indicative"].valueChanges.subscribe(value => {});
        });
      },
      error => {
        this.loading = false;
        if (error.status == 404) {
          let control = <FormArray>this.partnerRatesForm.controls.rates;
          control.controls = [];
          this.showNoRecord = true;
          this.notFoundMessage = error.data;
        }
      }
    );
  }

  addRates(): void {
    this.addPartnerRatesModal.showModal();
  }
  onBlurGuarantee(index, newValue): void {
    newValue = parseFloat(newValue);
    const prevVal = parseFloat(this.previousValues[index].guaranteed);
    if (prevVal < newValue) {
      let val = (newValue + prevVal) / 2;
      let diffPer = ((newValue - prevVal) / val) * 100;
      if (diffPer >= 5) {
        alert(
          "The difference between new and previous value is more than 5%. Are you sure you want to continue?"
        );
      }
    }
  }
  onBlurIndicative(index, newValue): void {
    newValue = parseFloat(newValue);
    const prevVal = parseFloat(this.previousValues[index].indicative);
    if (prevVal < newValue) {
      let val = (newValue + prevVal) / 2;
      let diffPer = ((newValue - prevVal) / val) * 100;
      if (diffPer >= 5) {
        alert(
          "The difference between new and previous value is more than 5%. Are you sure you want to continue?"
        );
      }
    }
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
  onRateChange(searchValue: string) {
    console.log(searchValue);
  }
}
