import { Component, OnInit } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";
import { FormGroup, Validators, FormBuilder } from "@angular/forms";
import {
  AlertService,
  AuthenticationService,
  UserService
} from "../../service/index";

@Component({ templateUrl: "login.component.html" })
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  returnUrl: string;
  error = "";
  submitted: boolean = false;
  loading: boolean = false;
  token: any;
  isAdmin: boolean = false;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService,
    private userService: UserService,
    private alertService: AlertService
  ) {
    this.loginForm = this.formBuilder.group({
      userName: [
        "",
        Validators.compose([Validators.required, Validators.maxLength(50)])
      ],
      password: [
        "",
        Validators.compose([Validators.required, Validators.maxLength(50)])
      ]
    });
  }

  ngOnInit() {
    if (localStorage.getItem("token")) {
      this.token = JSON.parse(localStorage.getItem("token"));
      if (this.token && this.token.RoleName == "Admin") {
        this.router.navigate(["/partner"]);
      } else if (this.token && this.token.RoleName == "Partner") {
        this.router.navigate(["partner/partner-locations/" + this.token.UserId]);
      }
    }
    this.route.queryParams.subscribe(params => {
      this.returnUrl = params["returnUrl"];
      // alert(this.returnUrl);
    });
    // get return url from route parameters or default to '/'
    this.returnUrl = this.route.snapshot.queryParams["returnUrl"] || "/";
  }

  // convenience getter for easy access to form fields
  get f() {
    return this.loginForm.controls;
  }

  onSubmit() {
    this.submitted = true;
    this.error = "";

    // stop here if form is invalid
    if (this.loginForm.invalid) {
      return;
    }
    this.loading = true;
    this.userService
      .login(this.f.userName.value, this.f.password.value)
      .subscribe(
        result => {
          this.loading = false;
          localStorage.setItem("token", JSON.stringify(result[0]));
          if (result[0].RoleName == "Partner") {
            this.router.navigate([
              "partner/partner-locations/" + result[0].UserId
            ]);
          } else {
            this.router.navigate(["/partner"]);
          }
          this.submitted = true;
        },
        error => {
          this.alertService.error(error.data);
          this.loading = false;
        }
      );
  }
}
