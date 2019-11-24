import { Component, OnInit, ViewChild, ElementRef } from "@angular/core";
import {
  FormGroup,
  FormControl,
  Validators,
  FormBuilder,
  AbstractControl
} from "@angular/forms";
import { UserService, AlertService } from "app/service";
import { EventBus } from "app/service/event-bus.service";
import { ImageCroppedEvent } from "ngx-image-cropper";
import { ModalDirective } from "ngx-bootstrap";
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: "app-settings",
  templateUrl: "./settings.component.html",
  styleUrls: ["./settings.component.scss"]
})
export class SettingsComponent implements OnInit {
  partnerbandNameForm: FormGroup;
  partnerLinkForm: FormGroup;
  partnerPasswordForm: FormGroup;
  @ViewChild("cropLogoModal") cropLogoModal: ModalDirective;
  @ViewChild("fileInput") fileInput: ElementRef;
  imageChangedEvent: any = "";
  croppedImage: any = "";
  showFinalLogo: boolean = false;
  partnerId: number = 0;
  loading: boolean = false;

  constructor(
    private formBuilder: FormBuilder,
    private userService: UserService,
    private alertService: AlertService,
    private _eventBus: EventBus,
    private route: ActivatedRoute
  ) {
    this.route.params.subscribe(params => {
      this.partnerId = params["id"];
    });
    this.partnerbandNameForm = new FormGroup({
      brand_name: new FormControl("", Validators.required),
      brand_logo: new FormControl("", Validators.required)
    });
    this.partnerLinkForm = new FormGroup({
      redirect_link: new FormControl("", Validators.required),
      email_address: new FormControl("", [
        Validators.required,
        Validators.email
      ])
    });

    this.partnerPasswordForm = this.formBuilder.group(
      {
        old_password: ["", Validators.required],
        new_password: ["", [Validators.required]],
        reenter_new_password: [""]
      },
      { validator: this.checkPasswords }
    );
  }
  get f() {
    return this.partnerbandNameForm.controls;
  }
  get g() {
    return this.partnerLinkForm.controls;
  }
  get h() {
    return this.partnerPasswordForm.controls;
  }
  ngOnInit() {}
  fileChangeEvent(event: any): void {
    if (event.target.files && event.target.files[0]) {
      this.cropLogoModal.show();
    }
    this.showFinalLogo = false;
    this.imageChangedEvent = event;
  }
  imageCropped(event: ImageCroppedEvent) {
    this.croppedImage = event.base64;
  }
  imageLoaded() {
    // show cropper
  }
  loadImageFailed() {
    // show message
  }
  hideModal(): void {
    this.resetLogo();
    this.cropLogoModal.hide();
  }
  cropLogo() {
    this.showFinalLogo = true;
    this.hideModal();
  }
  resetLogo() {
    // this.fileInput.nativeElement.value = "";
  }

  uploadBrandLogo() {
    debugger;
    let inputEl: HTMLInputElement = this.fileInput.nativeElement;
    let fileCount: number = inputEl.files.length;
    let file: File = inputEl.files[0];
    let formData: FormData = new FormData();
    formData.append("", file, file.name);
    this.userService.uploadBrandLogo(formData, this.partnerId).subscribe(
      data => {
        this.loading = false;
      },
      error => {
        this.loading = false;
        this.alertService.error(error.data);
      }
    );
  }
  checkPasswords(group: FormGroup) {
    // here we have the 'passwords' group
    let pass = group.controls.new_password.value;
    let confirmPass = group.controls.reenter_new_password.value;

    return pass === confirmPass ? null : { notSame: true };
  }

  onAddBrand() {}

  onAddLink() {}

  onChangePassword() {}
}
