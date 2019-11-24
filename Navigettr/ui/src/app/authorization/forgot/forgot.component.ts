import { Component, OnInit } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";
import { FormGroup, Validators, FormBuilder } from "@angular/forms";
import {
  AlertService,
  AuthenticationService,
  UserService
} from "../../service/index";

@Component({ templateUrl: "forgot.component.html" })
export class ForgotComponent implements OnInit {
  forgot: FormGroup;
  OTP: FormGroup;
  reset: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;
  show: boolean = false;
  confirm: boolean = false;
  error = "";

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService,
    private alertService: AlertService,
    private userService: UserService
  ) {
    this.forgot = this.formBuilder.group({
      PhoneNumber: [
        "",
        Validators.compose([
          Validators.required,
          Validators.minLength(10),
          Validators.maxLength(10)
        ])
      ]
    });
    this.OTP = this.formBuilder.group({
      Otp: [
        "",
        Validators.compose([
          Validators.required,
          Validators.minLength(6),
          Validators.maxLength(6)
        ])
      ]
    });
    this.reset = this.formBuilder.group({
      Password: ["", Validators.required],
      confirmPassword: ["", Validators.required]
    });
  }

  ngOnInit() {
    if (localStorage.getItem("token")) {
      this.router.navigate(["/productlisting"]);
    }

    // reset login status
    // this.authenticationService.logout();

    // get return url from route parameters or default to '/'
    // this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  get f() {
    return this.forgot.controls.PhoneNumber;
  }
  get o() {
    return this.OTP.controls.Otp;
  }
  get r() {
    return this.reset.controls;
  }

  onSubmit() {
    this.loading = true;
    this.error = "";
    // this.userService.requestOTP(this.forgot.value)
    //   .toPromise()
    //   .then(val => {
    //     if (val['Data']['UserId']) {
    //       this.show = true;
    //     }
    //     else {
    //       this.alertService.error(val['Data']['ExMessage']);
    //       this.error = val['Data']['ExMessage']
    //     }
    //     this.loading = false;
    //   },
    //     error => {
    //       this.alertService.error(error);
    //       this.loading = false;
    //     })
  }

  onOtp() {
    this.loading = true;
    // this.userService.verifyCode(this.forgot.value, this.OTP.value)
    //   .subscribe(
    //     result => {
    //       if (result.Status) {
    //         localStorage.setItem('UserModel', JSON.stringify(result.Data));
    //       } else {
    //       }
    //     },
    //     error => {
    //       //Validation error
    //       if (error.status == 422) {
    //         // this._errorMessage = "There was an error on submission. Please check again.";
    //         // let errorFields = JSON.parse(error.Data);
    //         let errorFields = error.Data;
    //       } else {
    //         //this._errorMessage = error.Data[0];
    //       }
    //     }
    //   );
  }

  changeNumber() {
    this.confirm = false;
    this.show = false;
  }
}
