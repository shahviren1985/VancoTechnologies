import { Component, OnInit } from "@angular/core";
import {
  FormGroup,
  FormControl,
  Validators,
  FormBuilder,
  FormArray
} from "@angular/forms";
import { UserService, AlertService } from "app/service";

@Component({
  selector: "app-custompartnercharges",
  templateUrl: "./custompartnercharges.component.html",
  styleUrls: ["./custompartnercharges.component.scss"]
})
export class CustompartnerchargesComponent implements OnInit {
  // partnerChargesForm: FormGroup;
  loading: boolean = false;
  partners = [];
  currentpartnerCharge = [];
  partnerChargeUI = [];
  selectedPartnerId = 0;
  formInvalid = false;
  chargeType = "";
  config = {
    displayKey: "partnerName", //if objects array passed which key to be displayed defaults to description
    search: true, //true/false for the search functionlity defaults to false,
    height: "auto", //height of the list so that if there are more no of items it can show a scroll defaults to auto. With auto height scroll will never appear
    placeholder: "Select Partner", // text to be displayed when no item is selected defaults to Select,
    customComparator: () => {}, // a custom function using which user wants to sort the items. default is undefined and Array.sort() will be used in that case,
    limitTo: this.partners.length, // a number thats limits the no of options displayed in the UI similar to angular's limitTo pipe
    moreText: "more", // text to be displayed whenmore than one items are selected like Option 1 + 5 more
    noResultsFound: "No partners found!", // text to be displayed when no items are found while searching
    searchPlaceholder: "Search Partner", // label thats displayed in search input,
    searchOnKey: "partnerName" // key on which search should be performed this will be selective search. if undefined this will be extensive search on all keys
  };
  constructor(
    private userService: UserService,
    private formBuilder: FormBuilder,
    private alertService: AlertService
  ) {}

  ngOnInit() {
    this.loading = true;
    let self = this;
    this.userService.getPartners(1, "", "1000").subscribe(
      data => {
        this.loading = false;
        this.partners = data;
      },
      error => {
        this.loading = false;
        if (error.status == 404) {
        }
      }
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

  partnerSelectionChanged(selectedValue) {
    if (selectedValue.value) {
      this.selectedPartnerId = selectedValue.value.id;
      this.loading = true;
      this.userService.getPartnerCharges(selectedValue["value"].id).subscribe(
        data => {
          this.arrangeNewData(data);
        },
        error => {
          this.loading = false;
          if (error.status == 404) {
          }
        }
      );
    }
  }
  private arrangeNewData(data: any) {
    this.loading = false;
    this.currentpartnerCharge = data;
    const customCharge = data.filter(charge => charge.isDefault == false);
    if (customCharge.length > 0) {
      this.chargeType = "custom_charges";
    } else {
      this.chargeType = "default_charges";
    }
    this.partnerChargeUI = this.checkDuplicateInObject(
      "category",
      this.currentpartnerCharge
    );
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
          PartnerID: this.selectedPartnerId,
          category: element.category,
          dateActivation: element.monthlyChargeActDate,
          dateExpiry: element.monthlyChargeExpDate,
          charges: element.monthlyCharges,
          isDefault: element.monthlyIsDefault
        };
        let quaterly = {
          id: element.quaterlyChargeId,
          frequency: "Quaterly",
          PartnerID: this.selectedPartnerId,
          category: element.category,
          dateActivation: element.quaterlyChargeActDate,
          dateExpiry: element.quaterlyChargeExpDate,
          charges: element.quaterlyCharges,
          isDefault: element.quaterlyIsDefault
        };
        let annually = {
          id: element.annuallyChargeId,
          frequency: "Annually",
          PartnerID: this.selectedPartnerId,
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
          this.alertService.success("Partner charges updated Successfully!!");
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
