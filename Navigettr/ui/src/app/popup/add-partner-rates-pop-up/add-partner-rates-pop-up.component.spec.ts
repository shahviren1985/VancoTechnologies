import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddPartnerRatesPopUpComponent } from './add-partner-rates-pop-up.component';

describe('AddPartnerRatesPopUpComponent', () => {
  let component: AddPartnerRatesPopUpComponent;
  let fixture: ComponentFixture<AddPartnerRatesPopUpComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddPartnerRatesPopUpComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddPartnerRatesPopUpComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
