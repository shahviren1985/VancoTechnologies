import { Component, OnInit, ViewChild } from "@angular/core";
import { FormGroup, Validators, FormControl } from "@angular/forms";
import { ModalDirective } from "ngx-bootstrap";
import { UserService, AlertService } from "app/service";
import { EventBus } from "app/service/event-bus.service";

@Component({
  selector: "app-add-partner-offers-pop-up",
  templateUrl: "./add-partner-offers-pop-up.component.html",
  styleUrls: ["./add-partner-offers-pop-up.component.scss"]
})
export class AddPartnerOffersPopUpComponent implements OnInit {
  @ViewChild("addItemModal") addItemModal: ModalDirective;
  partnerOffersForm: FormGroup;
  loading: boolean = false;
  partnerId: any;
  modelTitle: string;
  isModelInEditMode: boolean;
  constructor(
    private userService: UserService,
    private alertService: AlertService,
    private _eventBus: EventBus
  ) {
    this.userService.transmitdata.subscribe(data => {
      if (data != "closeModal") {
        this.partnerId = data;
      }
    });
    this.partnerOffersForm = new FormGroup({
      id: new FormControl("0"),
      offer_name: new FormControl("", Validators.required),
      offer: new FormControl("", Validators.required),
      offer_text: new FormControl("", Validators.required),
      offer_start_date: new FormControl("", Validators.required),
      offer_end_date: new FormControl("", Validators.required),
      offer_activation_date: new FormControl("", Validators.required),
      offer_expiry_date: new FormControl("", Validators.required)
    });
  }
  get f() {
    return this.partnerOffersForm.controls;
  }
  ngOnInit() {}
  hideModal(): void {
    this.addItemModal.hide();
  }

  showModal(): void {
    this.addItemModal.show();
  }
  openUpdateOfferModel(updateOfferItem): void {
    this.modelTitle = "Update location";
    this.isModelInEditMode = true;
    this.partnerOffersForm.patchValue({
      id: updateOfferItem.id,
      offer_name: updateOfferItem.offerName,
      offer_text: updateOfferItem.offerText,
      offer_start_date: updateOfferItem.offerStartDate,
      offer_end_date: updateOfferItem.offerEndDate,
      offer_activation_date: updateOfferItem.dateActivation,
      offer_expiry_date: updateOfferItem.dateExpiry
    });

    this.addItemModal.show();
  }
  onAddSubmit() {
    this.loading = true;
    var val = this.partnerOffersForm.value;
    this.userService
      .addPartnerOffer(
        val.id,
        this.partnerId,
        val.offer_name,
        val.offer_text,
        val.offer_start_date,
        val.offer_end_date,
        val.offer_activation_date,
        val.offer_expiry_date
      )
      .subscribe(
        data => {
          this.loading = false;
          this.partnerOffersForm.reset();
          this._eventBus.emit({ type: "REFRESH_PARTNER_OFFER_LIST" });
          this.hideModal();
        },
        error => {
          this.loading = false;
          this.alertService.error(error.data);
        }
      );
  }
}
