import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { AuthenticationService, UserService } from "../service/index";

@Component({
  selector: "app-dashboard",
  templateUrl: "./detail-layout.component.html"
})
export class DetailLayoutComponent implements OnInit {
  currentYear: any;
  currentId: 0;
  token: any;
  isAdmin: boolean = false;
  userName: string = "";
  selectedLinktype: string = "";
  currentUrl: string = "";
  constructor(
    private userService: UserService,
    private authenticationService: AuthenticationService,
    private router: Router
  ) {
    this.currentUrl = this.router.url;
    this.userService.transmitdata.subscribe(data => {
      if (data != "closeModal") {
        this.currentId = data;
      }
    });
  }
  public disabled = false;
  public status: { isopen: boolean } = { isopen: false };
  public toggled(open: boolean): void {}

  public toggleDropdown($event: MouseEvent): void {
    $event.preventDefault();
    $event.stopPropagation();
    this.status.isopen = !this.status.isopen;
  }
  logout() {
    // remove user from local storage to log user out
    this.authenticationService.logout();
  }

  ngOnInit(): void {
    if (localStorage.getItem("token")) {
      this.token = JSON.parse(localStorage.getItem("token"));
      if (this.token && this.token.RoleName == "Admin") {
        this.isAdmin = true;
      }
      this.userName = this.token.name;
    }
    this.currentYear = new Date().getFullYear();
    if (this.currentUrl.match("location")) {
      this.selectedLinktype = "location";
    } else if (this.currentUrl.match("offer")) {
      this.selectedLinktype = "offers";
    } else if (this.currentUrl.match("rate")) {
      this.selectedLinktype = "rates";
    } else if (this.currentUrl.match("setting")) {
      this.selectedLinktype = "settings";
    }
  }

  goToLocations() {
    this.selectedLinktype = "location";
    this.router.navigateByUrl("partner/partner-locations/" + this.currentId);
  }
  goToOffers() {
    this.selectedLinktype = "offers";
    this.router.navigateByUrl("partner/partner-offers/" + this.currentId);
  }
  goToRates() {
    this.selectedLinktype = "rates";
    this.router.navigateByUrl("partner/partner-rates/" + this.currentId);
  }
  goToAccountHistory() {
    this.selectedLinktype = "rates";
    this.router.navigateByUrl("partner/partner-locations/" + this.currentId);
  }
  goToAnalytics() {
    this.selectedLinktype = "rates";
    this.router.navigateByUrl("partner/partner-locations/" + this.currentId);
  }
  goToSettings() {
    this.selectedLinktype = "settings";
    this.router.navigateByUrl("partner/partner-settings/" + this.currentId);
  }
}
