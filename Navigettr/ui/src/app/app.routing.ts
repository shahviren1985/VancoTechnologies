import { NgModule, Component } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

// Layouts
import { FullLayoutComponent } from "./layouts/full-layout.component";
import { SimpleLayoutComponent } from "./layouts/simple-layout.component";
import { LoginComponent } from "./authorization/login/login.component";
import { RegisterComponent } from "./authorization/register/register.component";
import { AuthGuard } from "./guards/auth.guard";
import { ForgotComponent } from "./authorization/forgot/forgot.component";
import { PartnerComponent } from "./navigettr/partner/partner.component";
import { LocationComponent } from "./navigettr/locations/locations.component";
import { DetailLayoutComponent } from "./layouts/detail-layout.component";
import { OffersComponent } from "./navigettr/offers/offers.component";
import { RatesComponent } from "./navigettr/rates/rates.component";
import { SettingsComponent } from "./navigettr/settings/settings.component";
import { CustompartnerchargesComponent } from "./navigettr/custompartnercharges/custompartnercharges.component";
import { GlobalconfigurationsComponent } from "./navigettr/globalconfigurations/globalconfigurations.component";
import { AuthGuardAdminService as AdminAuthGuard } from "./service/auth-gaurd-admin";
import { AuthGuardPartnerService as PartnerAuthGuard } from "./service/auth-gaurd-partner";

export const routes: Routes = [
  {
    path: "",
    redirectTo: "login",
    pathMatch: "full"
  },
  {
    path: "",
    component: FullLayoutComponent,
    canActivate: [AuthGuard],
    data: {
      title: "Home"
    },
    children: [
      {
        path: "partner",
        component: PartnerComponent,
        canActivate: [AdminAuthGuard],
        data: {
          title: "Partner"
        }
      },
      {
        path: "charges",
        component: CustompartnerchargesComponent,
        canActivate: [AdminAuthGuard],
        data: {
          title: "Charges"
        }
      },
      {
        path: "configurations",
        component: GlobalconfigurationsComponent,
        canActivate: [AdminAuthGuard],
        data: {
          title: "Configurations"
        }
      }
    ]
  },
  {
    path: "partner",
    component: DetailLayoutComponent,
    canActivate: [AuthGuard],
    data: {
      title: "Partner"
    },
    children: [
      {
        path: "partner-locations/:id",
        component: LocationComponent,
        canActivate: [AuthGuard],
        data: {
          title: "Partners Locations"
        }
      },
      {
        path: "partner-offers/:id",
        component: OffersComponent,
        canActivate: [AuthGuard],
        data: {
          title: "Partners Offers"
        }
      },
      {
        path: "partner-rates/:id",
        component: RatesComponent,
        canActivate: [PartnerAuthGuard],
        data: {
          title: "Partners Rates"
        }
      },
      {
        path: "partner-settings/:id",
        component: SettingsComponent,
        canActivate: [PartnerAuthGuard],
        data: {
          title: "Partners Settings"
        }
      }
    ]
  },
  {
    path: "login",
    component: LoginComponent
  },
  {
    path: "register",
    component: RegisterComponent
  },
  {
    path: "forgotPassword",
    component: ForgotComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
