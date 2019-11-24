import { Component } from "@angular/core";
import { FormGroup, Validators, FormBuilder } from "@angular/forms";
import { CustomValidators } from "../../helpers/custom-validators";
import { AlertService, UserService } from "app/service";
import { Router } from "@angular/router";
import { first } from "rxjs/operators";

@Component({
  templateUrl: "register.component.html"
})
export class RegisterComponent {
  regdForm: FormGroup;
  // Element: any = []
  submitted: boolean = false;
  loading: boolean = false;
  error = "";

  userType: any[] = [{ name: "User", id: "2" }, { name: "Milker", id: "1" }];

  constructor(
    private formBuilder: FormBuilder,
    private alertService: AlertService,
    private userService: UserService,
    private router: Router
  ) {
    this.regdForm = this.formBuilder.group({
      FirstName: ["", Validators.required],
      LastName: ["", Validators.required],
      PhoneNumber: [
        "",
        Validators.compose([
          Validators.required,
          Validators.minLength(10),
          Validators.maxLength(10)
        ])
      ],
      EmailId: [
        "",
        Validators.compose([Validators.required, CustomValidators.email])
      ],
      Password: [
        "",
        Validators.compose([Validators.required, Validators.minLength(6)])
      ],
      // ConfirmPassword: ['', Validators.required],
      UserTypeID: ["", Validators.required]
    });
  }
  ngOnInit() {}
  get f() {
    return this.regdForm.controls;
  }

  onSubmit() {
    this.submitted = true;
    this.error = "";

    // stop here if form is invalid
    if (this.regdForm.invalid) {
      return;
    }

    this.loading = true;
    // this.userService.register(this.regdForm.value)
    //   .pipe(first())
    //   .subscribe(
    //     data => {
    //       if (data['Status']) {
    //         this.alertService.success('Registration successful');
    //         this.router.navigate(['/login']);
    //       }
    //       else {
    //         this.alertService.error(data['Message']);
    //         this.error = data['Message'];
    //       }
    //       this.loading = false;
    //     },
    //     error => {
    //       this.alertService.error(error);
    //       this.loading = false;
    //     });
  }
}
