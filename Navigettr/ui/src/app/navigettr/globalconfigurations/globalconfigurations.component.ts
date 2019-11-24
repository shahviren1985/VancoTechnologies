import { Component, OnInit } from "@angular/core";
import {
  FormControl,
  FormGroup,
  Validators,
  FormArray,
  FormBuilder
} from "@angular/forms";
import { UserService, AlertService } from "app/service";

@Component({
  selector: "app-globalconfigurations",
  templateUrl: "./globalconfigurations.component.html",
  styleUrls: ["./globalconfigurations.component.scss"]
})
export class GlobalconfigurationsComponent implements OnInit {
  globalConfigurationForm: FormGroup;
  loading = false;
  globalConfig = [];
  globalCharge = [];
  offerValue = 0;
  selectedOffer = {};
  redirect = {};
  formInvalid = false;
  redirectValue = false;
  partnerChargeUI = [];
  offerCounters: any;

  constructor(
    private userService: UserService,
    private formBuilder: FormBuilder,
    private alertService: AlertService
  ) {
    this.offerCounters = [
      { id: 1, name: 1 },
      { id: 2, name: 2 },
      { id: 3, name: 3 },
      { id: 4, name: 4 },
      { id: 5, name: 5 },
      { id: 6, name: 6 },
      { id: 7, name: 7 },
      { id: 8, name: 8 },
      { id: 9, name: 9 },
      { id: 10, name: 10 }
    ];
  }

  ngOnInit() {
    this.loading = true;
    this.userService.getGlobalConfig().subscribe(
      data => {
        this.globalConfig = data;
        this.selectedOffer = data.filter(charge => charge.Key == "offers");
        this.selectedOffer = this.selectedOffer[0];
        this.offerValue = parseInt(this.selectedOffer["value"]);

        this.redirect = data.filter(charge => charge.Key == "redirect");
        this.redirect = this.redirect[0];
        this.redirectValue = this.redirect["value"];
      },
      error => {
        if (error.status == 404) {
        }
      }
    );

    this.userService.getPartnerCharges(1).subscribe(
      data => {
        this.loading = false;
        this.arrangeNewData(data);
      },
      error => {
        this.loading = false;
        if (error.status == 404) {
        }
      }
    );
  }

  offerSelectionChanged(selectedValue) {
    if (selectedValue.target.value) {
      this.selectedOffer["value"] = selectedValue.target.value;
    }
  }

  redirectChanged(value) {
    this.redirect["value"] = value;
  }

  private arrangeNewData(data: any) {
    this.loading = false;
    this.globalCharge = data;
    this.partnerChargeUI = this.checkDuplicateInObject(
      "category",
      this.globalCharge
    );
  }

  findWithAttr(array, attr, value) {
    for (var i = 0; i < array.length; i += 1) {
      if (array[i][attr] === value) {
        return i;
      }
    }
    return -1;
  }

  checkDuplicateInObject(propertyName, inputArray) {
    var flags = [],
      output = [],
      l = inputArray.length,
      i;
    for (i = 0; i < inputArray.length; i++) {
      if (flags[inputArray[i].category]) {
        var indexItem = this.findWithAttr(
          output,
          "category",
          inputArray[i].category
        );
        if (inputArray[i].frequency == "Monthly") {
          output[indexItem]["monthlyCharges"] = inputArray[i].charges;
          output[indexItem]["monthlyChargeId"] = inputArray[i].id;
          output[indexItem]["monthlyIsDefault"] = inputArray[i].isDefault;
          output[indexItem]["monthlyChargeActDate"] =
            inputArray[i].dateActivation;
          output[indexItem]["monthlyChargeExpDate"] = inputArray[i].dateExpiry;
        } else if (inputArray[i].frequency == "Quaterly") {
          output[indexItem]["quaterlyCharges"] = inputArray[i].charges;
          output[indexItem]["quaterlyChargeId"] = inputArray[i].id;
          output[indexItem]["quaterlyIsDefault"] = inputArray[i].isDefault;
          output[indexItem]["quaterlyChargeActDate"] =
            inputArray[i].dateActivation;
          output[indexItem]["quaterlyChargeExpDate"] = inputArray[i].dateExpiry;
        } else if (inputArray[i].frequency == "Annually") {
          output[indexItem]["annuallyCharges"] = inputArray[i].charges;
          output[indexItem]["annuallyChargeId"] = inputArray[i].id;
          output[indexItem]["annuallyIsDefault"] = inputArray[i].isDefault;
          output[indexItem]["annuallyChargeActDate"] =
            inputArray[i].dateActivation;
          output[indexItem]["annuallyChargeExpDate"] = inputArray[i].dateExpiry;
        }
      } else {
        flags[inputArray[i].category] = true;
        var obj = {};
        obj["category"] = inputArray[i].category;
        if (inputArray[i].frequency == "Monthly") {
          obj["monthlyCharges"] = inputArray[i].charges;
          obj["monthlyChargeId"] = inputArray[i].id;
          obj["monthlyChargeActDate"] = inputArray[i].dateActivation;
          obj["monthlyChargeExpDate"] = inputArray[i].dateExpiry;
          obj["monthlyIsDefault"] = inputArray[i].isDefault;
        } else if (inputArray[i].frequency == "Quaterly") {
          obj["quaterlyCharges"] = inputArray[i].charges;
          obj["quaterlyChargeId"] = inputArray[i].id;
          obj["quaterlyChargeActDate"] = inputArray[i].dateActivation;
          obj["quaterlyChargeExpDate"] = inputArray[i].dateExpiry;
          obj["quaterlyIsDefault"] = inputArray[i].isDefault;
        } else if (inputArray[i].frequency == "Annually") {
          obj["annuallyCharges"] = inputArray[i].charges;
          obj["annuallyChargeId"] = inputArray[i].id;
          obj["annuallyChargeActDate"] = inputArray[i].dateActivation;
          obj["annuallyChargeExpDate"] = inputArray[i].dateExpiry;
          obj["annuallyIsDefault"] = inputArray[i].isDefault;
        }
        output.push(obj);
      }
    }
    return output;
  }
  onMonthlyChargeChange(category, value) {
    this.partnerChargeUI.forEach(element => {
      if (element.category == category) {
        element.monthlyIsDefault = false;
        if (value == "") {
          element.IsValid = false;
          this.formInvalid = true;
        } else {
          element.IsValid = true;
          this.formInvalid = false;
        }
      }
    });
  }
  onQuaterlyChargeChange(category, value) {
    this.partnerChargeUI.forEach(element => {
      if (element.category == category) {
        element.quaterlyIsDefault = false;
        if (value == "") {
          element.IsValid = false;
          this.formInvalid = true;
        } else {
          element.IsValid = true;
          this.formInvalid = false;
        }
      }
    });
  }
  onAnnuallyChargeChange(category, value) {
    this.partnerChargeUI.forEach(element => {
      if (element.category == category) {
        element.annuallyIsDefault = false;
        if (value == "") {
          element.IsValid = false;
          this.formInvalid = true;
        } else {
          element.IsValid = true;
          this.formInvalid = false;
        }
      }
    });
  }
  onSubmit() {
    const formInvalid = this.partnerChargeUI.filter(
      element => element.isValid == false
    );
    if (formInvalid.length < 1) {
      var payload = [];
      this.partnerChargeUI.forEach(element => {
        let monthly = {
          id: element.monthlyChargeId,
          frequency: "Monthly",
          PartnerID: 1,
          category: element.category,
          dateActivation: element.monthlyChargeActDate,
          dateExpiry: element.monthlyChargeExpDate,
          charges: element.monthlyCharges,
          isDefault: element.monthlyIsDefault
        };
        let quaterly = {
          id: element.quaterlyChargeId,
          frequency: "Quaterly",
          PartnerID: 1,
          category: element.category,
          dateActivation: element.quaterlyChargeActDate,
          dateExpiry: element.quaterlyChargeExpDate,
          charges: element.quaterlyCharges,
          isDefault: element.quaterlyIsDefault
        };
        let annually = {
          id: element.annuallyChargeId,
          frequency: "Annually",
          PartnerID: 1,
          category: element.category,
          dateActivation: element.annuallyChargeActDate,
          dateExpiry: element.annuallyChargeExpDate,
          charges: element.annuallyCharges,
          isDefault: element.annuallyIsDefault
        };
        payload.push(monthly);
        payload.push(quaterly);
        payload.push(annually);
      });
      this.loading = true;
      this.userService.savePartnerCharges(payload).subscribe(
        data => {
          this.arrangeNewData(data);
          payload = [];
          payload.push(this.selectedOffer);
          payload.push(this.redirect);
          this.userService.saveGlobalConfig(payload).subscribe(
            data => {
              this.alertService.success(
                "Global configuration updated Successfully!!"
              );
            },
            error => {
              if (error.status == 404) {
              }
            }
          );

          this.loading = false;
        },
        error => {
          this.alertService.error("Something wenr wrong!!");
          this.loading = false;
          if (error.status == 404) {
          }
        }
      );
    } else {
      this.alertService.error("Please correct your form!!");
      this.formInvalid = true;
    }
  }
}
