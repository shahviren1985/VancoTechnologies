import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import {
  LocationStrategy,
  HashLocationStrategy,
  CommonModule
} from "@angular/common";

import { SelectDropDownModule } from 'ngx-select-dropdown';
import { AppComponent } from "./app.component";
import { BsDropdownModule } from "ngx-bootstrap/dropdown";
import { TabsModule } from "ngx-bootstrap/tabs";
import { NAV_DROPDOWN_DIRECTIVES } from "./shared/nav-dropdown.directive";

import { ChartsModule } from "ng2-charts/ng2-charts";
import { SIDEBAR_TOGGLE_DIRECTIVES } from "./shared/sidebar.directive";
import { AsideToggleDirective } from "./shared/aside.directive";
import { BreadcrumbsComponent } from "./shared/breadcrumb.component";
import { ReactiveFormsModule, FormsModule } from "@angular/forms";
import {
  AlertService,
  AuthenticationService,
  UserService,
  PagerService
} from "./service/index";
import { HttpClientModule } from "@angular/common/http";

// Routing Module
import { AppRoutingModule } from "./app.routing";

// Layouts
import { FullLayoutComponent } from "./layouts/full-layout.component";
import { SimpleLayoutComponent } from "./layouts/simple-layout.component";
import { OrdersComponent } from "./navigettr/orders/orders.component";
import { ModalModule } from "ngx-bootstrap";
import { LoginComponent } from "./authorization/login/login.component";
import { RegisterComponent } from "./authorization/register/register.component";
import { AuthGuard } from "./guards";
import { SearchPipe } from "./pipe/search.pipe";
import { ForgotComponent } from "./authorization/forgot/forgot.component";
import { HttpModule } from "@angular/http";
import { NgxLoadingModule } from "ngx-loading";
import { PartnerComponent } from "./navigettr/partner/partner.component";
import { AddPartnerPopUpComponent } from "./popup/add-partner-pop-up/add-partner-pop-up.component";
import { LocationComponent } from "./navigettr/locations/locations.component";
import { AddPartnerLocationPopUpComponent } from "./popup/add-partnerLocation-pop-up/add-partnerLocation-pop-up.component";
import { DetailLayoutComponent } from "./layouts/detail-layout.component";
import { OffersComponent } from "./navigettr/offers/offers.component";
import { AddPartnerOffersPopUpComponent } from "./popup/add-partner-offers-pop-up/add-partner-offers-pop-up.component";
import { RatesComponent } from "./navigettr/rates/rates.component";
import { AddPartnerRatesPopUpComponent } from "./popup/add-partner-rates-pop-up/add-partner-rates-pop-up.component";
import { SettingsComponent } from "./navigettr/settings/settings.component";
import { CustompartnerchargesComponent } from "./navigettr/custompartnercharges/custompartnercharges.component";
import { GlobalconfigurationsComponent } from "./navigettr/globalconfigurations/globalconfigurations.component";
import { AlertComponent } from "./directives";
import { BulkImportLocationPopUpComponent } from "./popup/bulk-import-location-pop-up/bulk-import-location-pop-up.component";
import { EventBus } from "./service/event-bus.service";
import { Ng4GeoautocompleteModule } from "ng4-geoautocomplete";
import { AuthGuardAdminService } from "./service/auth-gaurd-admin";
import { AuthGuardPartnerService } from "./service/auth-gaurd-partner";
import { DecimalValidatorDirective } from "./navigettr/rates/decimalvalidation.directive";
import { ImageCropperModule } from 'ngx-image-cropper';

@NgModule({
  imports: [
    ImageCropperModule,
    SelectDropDownModule,
    BrowserModule,
    AppRoutingModule,
    BsDropdownModule.forRoot(),
    TabsModule.forRoot(),
    ChartsModule,
    ReactiveFormsModule,
    ModalModule.forRoot(),
    HttpClientModule,
    FormsModule,
    CommonModule,
    HttpModule,
    NgxLoadingModule.forRoot({}),
    Ng4GeoautocompleteModule.forRoot()
  ],
  declarations: [
    AppComponent,
    AlertComponent,
    FullLayoutComponent,
    DetailLayoutComponent,
    SimpleLayoutComponent,
    NAV_DROPDOWN_DIRECTIVES,
    BreadcrumbsComponent,
    SIDEBAR_TOGGLE_DIRECTIVES,
    AsideToggleDirective,
    OrdersComponent,
    PartnerComponent,
    LoginComponent,
    RegisterComponent,
    SearchPipe,
    DecimalValidatorDirective,
    ForgotComponent,
    AddPartnerPopUpComponent,
    AddPartnerLocationPopUpComponent,
    LocationComponent,
    OffersComponent,
    AddPartnerOffersPopUpComponent,
    BulkImportLocationPopUpComponent,
    RatesComponent,
    AddPartnerRatesPopUpComponent,
    SettingsComponent,
    CustompartnerchargesComponent,
    GlobalconfigurationsComponent
  ],
  providers: [
    {
      provide: LocationStrategy,
      useClass: HashLocationStrategy
    },
    AlertService,
    AuthenticationService,
    UserService,
    AuthGuard,
    PagerService,
    EventBus,
    AuthGuardAdminService,
    AuthGuardPartnerService
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
// testing
