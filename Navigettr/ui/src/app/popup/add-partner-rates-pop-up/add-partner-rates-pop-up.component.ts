import { Component, OnInit, ViewChild } from "@angular/core";
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { ModalDirective } from "ngx-bootstrap";
import { Http } from "@angular/http";
import { UserService, AlertService } from "app/service";
import { EventBus } from "app/service/event-bus.service";
export type Currency = {
  symbol: string;
  name: string;
  symbol_native: string;
  decimal_digits: number;
  rounding: number;
  code: string;
  name_plural: string;
  disabled: boolean;
};

@Component({
  selector: "app-add-partner-rates-pop-up",
  templateUrl: "./add-partner-rates-pop-up.component.html",
  styleUrls: ["./add-partner-rates-pop-up.component.scss"]
})
export class AddPartnerRatesPopUpComponent implements OnInit {
  @ViewChild("addItemModal") addItemModal: ModalDirective;
  currencies: Array<Currency>;
  partnerRatesPopUpForm: FormGroup;
  loading: boolean = false;
  partnerId: number = 0;
  modelTitle: string;
  isModelInEditMode: boolean;

  constructor(
    private _eventBus: EventBus,
    private http: Http,
    private userService: UserService,
    private alertService: AlertService
  ) {
    this.modelTitle = "Add Rate";
    this.userService.transmitdata.subscribe(data => {
      if (data != "closeModal") {
        this.partnerId = data;
      }
    });
    this.partnerRatesPopUpForm = new FormGroup({
      id: new FormControl(""),
      from: new FormControl("", Validators.required),
      to: new FormControl("", Validators.required),
      guaranteed: new FormControl("", Validators.required),
      indicative: new FormControl("", Validators.required)
    });
  }
  get f() {
    return this.partnerRatesPopUpForm.controls;
  }

  setDisabled(event) {
    let index: number = event.target["selectedIndex"];
    this.currencies.forEach(element => {
      element.disabled = false;
    });
    this.currencies[index].disabled = true;
  }
  ngOnInit() {
    this.http
      .get("/navigettr/assets/common_currency.json")
      .map(data => data.json() as Array<Currency>)
      .subscribe(data => {
        this.currencies = data;
      });
  }
  hideModal(): void {
    this.addItemModal.hide();
  }

  showModal(): void {
    this.addItemModal.show();
  }
  onAddSubmit() {
    if (this.partnerRatesPopUpForm.valid) {
      this.loading = true;
      var val = this.partnerRatesPopUpForm.value;
      let payload = [];
      let data = {
        Id: 0,
        PartnerId: this.partnerId,
        FromRate: val.from,
        ToRate: val.to,
        Guaranteed: val.guaranteed,
        Indicative: val.indicative,
        Status: "Active"
      };
      payload.push(data);
      this.userService.addPartnerRates(payload).subscribe(
        data => {
          this.loading = false;
          this._eventBus.emit({
            type: "REFRESH_PARTNER_RATE_LIST",
            isadd: true
          });
          this.partnerRatesPopUpForm.reset();
          this.hideModal();
        },
        error => {
          this.loading = false;
          this.alertService.error(error.data);
        }
      );
    }
  }
}
