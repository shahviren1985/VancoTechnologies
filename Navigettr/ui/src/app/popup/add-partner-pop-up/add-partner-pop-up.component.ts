import { Component, OnInit, ViewChild, ViewContainerRef } from "@angular/core";
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { ModalDirective } from "ngx-bootstrap";
import { UserService, AlertService } from "app/service";
import { EventBus } from "app/service/event-bus.service";

@Component({
  selector: "app-add-partner-pop-up",
  templateUrl: "./add-partner-pop-up.component.html",
  styleUrls: ["./add-partner-pop-up.component.scss"]
})
export class AddPartnerPopUpComponent implements OnInit {
  @ViewChild("addItemModal") addItemModal: ModalDirective;
  partnerForm: FormGroup;
  loading: boolean = false;
  constructor(
    private userService: UserService,
    private alertService: AlertService,
    private _eventBus: EventBus
  ) {
    this.partnerForm = new FormGroup({
      name: new FormControl("", Validators.required),
      userName: new FormControl("", Validators.required),
      email: new FormControl(
        "",
        Validators.compose([Validators.required, Validators.email])
      )
    });
  }
  get f() {
    return this.partnerForm.controls;
  }
  ngOnInit() {}
  onAddSubmit() {
    this.loading = true;
    var val = this.partnerForm.value;
    this.userService.addPartner(val.name, val.email, val.userName).subscribe(
      data => {
        this.loading = false;
        this.partnerForm.reset();
        this._eventBus.emit({ type: "REFRESH_PARTNER_LIST" });
        this.hideModal();
      },
      error => {
        this.loading = false;
        this.alertService.error(error.data);
      }
    );
  }

  hideModal(): void {
    this.addItemModal.hide();
  }

  showModal(): void {
    this.addItemModal.show();
  }
}
