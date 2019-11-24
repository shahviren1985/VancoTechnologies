import { Injectable } from "@angular/core";
import { Router, CanActivate } from "@angular/router";
@Injectable()
export class AuthGuardPartnerService implements CanActivate {
  constructor(public router: Router) {}
  canActivate(): boolean {
    if (localStorage.getItem("token")) {
      const token = JSON.parse(localStorage.getItem("token"));
      if (token && token.RoleName == "Partner") {
        return true;
      } else {
        this.router.navigate(["login"]);
        return false;
      }
    }
    this.router.navigate(["login"]);
    return false;
  }
}
